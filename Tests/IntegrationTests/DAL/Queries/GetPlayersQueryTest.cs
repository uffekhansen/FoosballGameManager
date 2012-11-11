using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Queries;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Extensions;
using FluentAssertions;
using NSubstitute;
using Xunit.Extensions;

namespace Tests.IntegrationTests.DAL.Queries
{
	public class GetPlayersQueryTest
	{
		private readonly IGetEntityByIdQuery<Player> _getPlayerEntityByIdQuery = Substitute.For<IGetEntityByIdQuery<Player>>();
		private readonly GetPlayersByIdsQuery _getPlayersByIdsQuery;

		public GetPlayersQueryTest()
		{
			_getPlayerEntityByIdQuery.Execute(Arg.Any<Guid>()).Returns(new Player());
			_getPlayersByIdsQuery = new GetPlayersByIdsQuery(_getPlayerEntityByIdQuery);
		}

		public static IEnumerable<object[]> UnrecognizedPlayerIdErrorMessageTestData
		{
			get
			{
				yield return new object[]
				{
					new List<Guid> { new Guid("00000000-0000-0000-0000-000000000000") },
					new List<Guid> { new Guid("11111111-1111-1111-1111-111111111111") },
					"Players with the following ids did not exist: 11111111-1111-1111-1111-111111111111,"
				};
				yield return new object[]
				{
					new List<Guid> { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("55555555-5555-5555-5555-555555555555") },
					new List<Guid> { new Guid("11111111-1111-1111-1111-111111111111"), new Guid("22222222-2222-2222-2222-222222222222") },
					"Players with the following ids did not exist: 11111111-1111-1111-1111-111111111111, 22222222-2222-2222-2222-222222222222,"
				};
				yield return new object[]
				{
					new List<Guid> { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("55555555-5555-5555-5555-555555555555") },
					new List<Guid> { new Guid("11111111-1111-1111-1111-111111111111"), new Guid("22222222-2222-2222-2222-222222222222"), new Guid("33333333-3333-3333-3333-333333333333") },
					"Players with the following ids did not exist: 11111111-1111-1111-1111-111111111111, 22222222-2222-2222-2222-222222222222, 33333333-3333-3333-3333-333333333333,"
				};
			}
		}

		[Theory]
		[PropertyData("UnrecognizedPlayerIdErrorMessageTestData")]
		public void Given_GetEntityQuery_Returns_Null_When_Execute_Then_NotFoundException_Is_Thrown_Containing_A_Message_For_Each_Unrecognized_Player_Id(IEnumerable<Guid> recognizedPlayerIds, IEnumerable<Guid> unrecognizedPlayerIds, string expectedExceptionMessage)
		{
			unrecognizedPlayerIds.Each(ArrangeGetPlayerQueryToReturnNullOnId);

			Action execute = () => _getPlayersByIdsQuery.Execute(unrecognizedPlayerIds.Concat(recognizedPlayerIds));

			execute.ShouldThrow<NotFoundException>().WithMessage(expectedExceptionMessage);
		}

		public static IEnumerable<object[]> RecognizedPlayerIdsTestData
		{
			get
			{
				yield return new object[]
				{
					new List<Guid> { new Guid("11111111-1111-1111-1111-111111111111") },
				};
				yield return new object[]
				{
					new List<Guid> { new Guid("11111111-1111-1111-1111-111111111111"), new Guid("22222222-2222-2222-2222-222222222222") },
				};
				yield return new object[]
				{
					new List<Guid> { new Guid("11111111-1111-1111-1111-111111111111"), new Guid("22222222-2222-2222-2222-222222222222"), new Guid("33333333-3333-3333-3333-333333333333") },
				};
			}
		}

		[Theory]
		[PropertyData("RecognizedPlayerIdsTestData")]
		public void Given_Known_Player_Ids_When_Execute_Then_Players_Are_Returned(IEnumerable<Guid> recognizedPlayerIds)
		{
			recognizedPlayerIds.Each(ArrangeGetPlayerQueryToReturnPlayerWithId);

			var players = _getPlayersByIdsQuery.Execute(recognizedPlayerIds);

			var retrievedIds = players.Select(x => x.Id);
			retrievedIds.Should().ContainInOrder(recognizedPlayerIds);
		}

		private void ArrangeGetPlayerQueryToReturnNullOnId(Guid id)
		{
			_getPlayerEntityByIdQuery.Execute(id).Returns((Player)null);
		}

		private void ArrangeGetPlayerQueryToReturnPlayerWithId(Guid id)
		{
			_getPlayerEntityByIdQuery.Execute(id).Returns(new Player{ Id = id });
		}
	}
}
