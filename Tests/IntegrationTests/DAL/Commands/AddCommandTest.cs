using System.Linq;
using DAL.Commands;
using Domain.Entities;
using FluentAssertions;
using NHibernate.Linq;
using Tests.Extensions;
using Tests.Infrastructure.TestBases;
using Xunit;

namespace Tests.IntegrationTests.DAL.Commands
{
	public abstract class AddCommandTest<T> : InDatabaseTest where T : class
	{
		protected IAddCommand<T> _addCommand;

		[Fact]
		public void Given_Entity_When_Calling_Execute_Then_Player_Is_Persisted()
		{
			var entity = CreateEntity();

			_session.ExecuteWithTransactionAndClear(() => _addCommand.Execute(entity));

			var persistedPlayer = _session.Query<Player>().First();
			persistedPlayer.Should().NotBeNull();
		}

		protected abstract T CreateEntity();
	}

	public class AddCommandTest_WithPlayerEntity : AddCommandTest<Player>
	{
		public AddCommandTest_WithPlayerEntity() 
		{
			_addCommand = new AddCommand<Player>(_session);
		}

		protected override Player CreateEntity()
		{
			return new Player();
		}
	}
}
