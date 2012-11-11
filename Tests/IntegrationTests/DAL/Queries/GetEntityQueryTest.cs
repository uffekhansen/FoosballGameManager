using System;
using DAL.Commands;
using DAL.Queries;
using Domain.Entities;
using FluentAssertions;
using Tests.Infrastructure.TestBases;
using Xunit;

namespace Tests.IntegrationTests.DAL.Queries
{
	public abstract class GetEntityByIdQueryTest<T> : InDatabaseTest where T : AssignedIdEntity
	{
		protected readonly GetEntityByIdQuery<T> _getEntityByIdQuery;

		protected GetEntityByIdQueryTest()
		{
			_getEntityByIdQuery = new GetEntityByIdQuery<T>(_session);
		}

		protected abstract T CreateEntity();

		[Fact]
		public void Given_Entity_When_Executing_With_Entity_Id_Then_Entity_Is_Returned()
		{
			var arrangedEntity = CreateEntity();
			new AddCommand<T>(_session).Execute(arrangedEntity);

			var entity = _getEntityByIdQuery.Execute(arrangedEntity.Id);

			entity.Should().Be(arrangedEntity);
		}

		[Fact]
		public void Given_Entity_When_Executing_With_Unknown_Id_Then_Null_Is_Returned()
		{
			var arrangedEntity = CreateEntity();
			arrangedEntity.Id = new Guid("11111111-1111-1111-1111-111111111111");
			new AddCommand<T>(_session).Execute(arrangedEntity);

			var entity = _getEntityByIdQuery.Execute(new Guid("22222222-2222-2222-2222-222222222222"));

			entity.Should().BeNull();
		}
	}

	public class GetEntityByIdQueryTest_Player : GetEntityByIdQueryTest<Player>
	{
		protected override Player CreateEntity()
		{
			return new Player();
		}
	}
}
