using System;
using System.Linq;
using DAL.Queries;
using Domain.Entities;
using Domain.Extensions;
using FluentAssertions;
using Tests.Builders;
using Tests.Infrastructure.TestBases;
using Xunit.Extensions;

namespace Tests.IntegrationTests.DAL.Queries
{
	public class GetEntitiesQueryTest : InDatabaseTest
	{
		private readonly GetEntitiesQuery<Player> _getEntitiesQuery;

		public GetEntitiesQueryTest()
		{
			_getEntitiesQuery = new GetEntitiesQuery<Player>(_session);
		}

		[Theory]
		[InlineData(2)]
		[InlineData(0)]
		public void Given_Players_When_Calling_Execute_Then_All_Existing_Players_Are_Returned(int numberPlayers)
		{
			ArrangePlayers(numberPlayers);
			_persister.Persist();

			var players = _getEntitiesQuery.Execute();

			players.Count().Should().Be(numberPlayers);
		}

		private void ArrangePlayers(int numberPlayers)
		{
			var playerBuilder = new PlayerBuilder();

			numberPlayers.TimesDo(() =>
			{
				playerBuilder.Id = Guid.NewGuid();
				_persister.Add(playerBuilder.Build());
			});
		}
	}
}
