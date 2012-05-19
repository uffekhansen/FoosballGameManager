using System.Collections.Generic;
using System.Linq;
using GameLogic.Entities;
using GameLogic.Exceptions;
using GameLogic.Logic;
using GameLogic.ValueObjects;
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
		public void GivenPlayers_WhenCreatingTeam_ThenTheExpectedNumberOfTeamsAreReturned(int expectedNumberOfTeams, int numberPlayersInlist, int playersPerTeam)
		{
			var playerList = ArrangePlayerList(numberPlayersInlist);
			_teamCreator.TeamSettings = ArrangeTeamSettings(playersPerTeam);

			var teams = _teamCreator.CreateTeams(playerList);

			Assert.Equal(expectedNumberOfTeams, teams.Count);
		}

		[Theory]
		[InlineData(1, 2)]
		[InlineData(2, 3)]
		[InlineData(3, 10)]
		public void GivenFeverPlayerThanPlayersPerTeam_WhenCreatingTeam_ThenTeamGenerationExceptionIsThrown(int numberPlayersInlist, int playersPerTeam)
		{
			var playerList = ArrangePlayerList(numberPlayersInlist);
			_teamCreator.TeamSettings = ArrangeTeamSettings(playersPerTeam);

			Assert.ThrowsDelegate act = () => _teamCreator.CreateTeams(playerList);

			Assert.Throws<TeamGenerationException>(act);
		}

		[Theory]
		[InlineData(3, 2)]
		[InlineData(5, 6)]
		[InlineData(11, 5)]
		[InlineData(101, 10)]
		public void GivenPlayersNotDivisableWithNumberPlayersPerTeam_WhenCreatingTeam_ThenTeamGenerationExceptionIsThrown(int numberPlayersInlist, int playersPerTeam)
		{
			var playerList = ArrangePlayerList(numberPlayersInlist);
			_teamCreator.TeamSettings = ArrangeTeamSettings(playersPerTeam);

			Assert.ThrowsDelegate act = () => _teamCreator.CreateTeams(playerList);

			Assert.Throws<TeamGenerationException>(act);
		}

		[Fact]
		public void GivenZeroPlayers_WhenCreatingTeam_ThenTeamGenerationExceptionIsThrown()
		{
			const int aPositiveNumber = 4;
			var playerList = ArrangePlayerList(0);
			_teamCreator.TeamSettings = ArrangeTeamSettings(playersPerTeam: aPositiveNumber);

			Assert.ThrowsDelegate act = () => _teamCreator.CreateTeams(playerList);

			Assert.Throws<TeamGenerationException>(act);
		}

		private TeamSettings ArrangeTeamSettings(int playersPerTeam)
		{
			return new TeamSettings
			{
				NumberPlayers = playersPerTeam,
			};
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
