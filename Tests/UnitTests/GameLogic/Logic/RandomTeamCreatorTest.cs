using System.Collections.Generic;
using System.Linq;
using GameLogic.Entities;
using GameLogic.Logic;
using Xunit;
using Xunit.Extensions;

namespace Tests.UnitTests.GameLogic.Logic
{
	public class RandomTeamCreatorTest
	{
		private readonly RandomTeamCreator _teamCreator;

		public RandomTeamCreatorTest()
		{
			_teamCreator = new RandomTeamCreator();
		}

		[Theory]
		[InlineData(1, 1, 1)]
		[InlineData(1, 2, 2)]
		[InlineData(2, 2, 1)]
		[InlineData(1, 2, 2)]
		[InlineData(5, 5, 1)]
		[InlineData(3, 6, 2)]
		public void Given_players_When_creating_team_Then_the_expected_number_of_teams_are_returned(int expectedNumberOfTeams, int numberPlayersInlist, int playersPerTeam)
		{
			var playerList = ArrangePlayerList(numberPlayersInlist);

			var teams = _teamCreator.CreateTeams(playerList, playersPerTeam);

			Assert.Equal(expectedNumberOfTeams, teams.Count);
		}

		private List<Player> ArrangePlayerList(int numberPlayers)
		{
			return Enumerable.Repeat(ArrangePlayer(), numberPlayers)
				.ToList();
		}

		private Player ArrangePlayer()
		{
			return new Player();
		}
	}
}
