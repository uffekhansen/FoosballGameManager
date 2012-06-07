using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Domain.Services;
using Xunit;
using Xunit.Extensions;

namespace Tests.UnitTests.Domain.Services
{
	public class RandomTeamCreatorTest
	{
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
			var _teamCreator = new RandomTeamCreator(playersPerTeam, playerList);

			var teams = _teamCreator.CreateTeams();

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
