using System.Collections.Generic;
using System.Linq;
using GameLogic.Entities;
using GameLogic.Exceptions;
using GameLogic.Logic;
using Xunit;
using Xunit.Extensions;

namespace Tests.UnitTests.GameLogic.Logic
{
	public class TeamCreatorTest
	{
		private readonly TeamCreator _teamCreator;

		public TeamCreatorTest()
		{
			_teamCreator = new TeamCreator();
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

		[Theory]
		[InlineData(1, 2)]
		[InlineData(2, 3)]
		[InlineData(3, 10)]
		public void Given_fewer_players_than_players_per_team_When_creating_team_Then_teamgenerationexception_is_thrown(int numberPlayersInlist, int playersPerTeam)
		{
			var playerList = ArrangePlayerList(numberPlayersInlist);

			Assert.ThrowsDelegate act = () => _teamCreator.CreateTeams(playerList, playersPerTeam);

			Assert.Throws<TeamGenerationException>(act);
		}

		[Theory]
		[InlineData(3, 2)]
		[InlineData(5, 6)]
		[InlineData(11, 5)]
		[InlineData(101, 10)]
		public void Given_players_not_divisable_with_number_players_per_team_When_creating_team_Then_teamgenerationException_is_thrown(int numberPlayersInlist, int playersPerTeam)
		{
			var playerList = ArrangePlayerList(numberPlayersInlist);

			Assert.ThrowsDelegate act = () => _teamCreator.CreateTeams(playerList, playersPerTeam);

			Assert.Throws<TeamGenerationException>(act);
		}

		[Fact]
		public void Given_zero_players_When_creating_team_Then_teamgenerationException_is_thrown()
		{
			const int any = 2;
			var playerList = ArrangePlayerList(0);

			Assert.ThrowsDelegate act = () => _teamCreator.CreateTeams(playerList, playersPerTeam: any);

			Assert.Throws<TeamGenerationException>(act);
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
