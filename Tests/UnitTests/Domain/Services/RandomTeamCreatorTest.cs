using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Domain.Extensions;
using Domain.Services;
using Domain.Tools;
using NSubstitute;
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
		public void Given_randomTeamCreator_When_creating_teams_Then_the_expected_number_of_teams_are_returned(int expectedNumberOfTeams, int numberPlayersInlist, int numberPlayersPerTeam)
		{
			var teamCreator = ArrangeTeamCreator(numberPlayersInlist, numberPlayersPerTeam);

			var teams = teamCreator.CreateTeams();

			Assert.Equal(expectedNumberOfTeams, teams.Count);
		}

		[Theory]
		[InlineData(1, 1)]
		[InlineData(2, 2)]
		[InlineData(2, 1)]
		[InlineData(5, 1)]
		[InlineData(6, 2)]
		[InlineData(6, 3)]
		public void Given_RandomTeamCreator_When_Creating_Teams_Then_Generated_Teams_Contains_Expected_Number_Of_Players(int numberPlayersInlist, int numberPlayersPerTeam)
		{
			var teamCreator = ArrangeTeamCreator(numberPlayersInlist, numberPlayersPerTeam);

			var teams = teamCreator.CreateTeams();

			teams.Each(x => Assert.Equal(numberPlayersPerTeam, x.Players.Count()));
		}

		[Theory]
		[InlineData(1, 1)]
		[InlineData(2, 2)]
		[InlineData(2, 1)]
		[InlineData(5, 1)]
		[InlineData(6, 2)]
		public void Given_RandomTeamCreator_When_Creating_Teams_Then_Next_Is_Called_Expected_Number_Of_Times(int numberPlayersInlist, int numberPlayersPerTeam)
		{
			var randomSubstitute = Substitute.For<IRandom>();
			var teamCreator = ArrangeTeamCreator(randomSubstitute, numberPlayersInlist, numberPlayersPerTeam);

			teamCreator.CreateTeams();

			randomSubstitute.Received(numberPlayersInlist).Next(Arg.Any<int>());
		}

		private RandomTeamCreator ArrangeTeamCreator(int numberPlayersInList, int numberPlayersPerTeam)
		{
			var playerList = ArrangePlayerList(numberPlayersInList);
			return new RandomTeamCreator(new FoosballRandom())
			{
				Players = playerList,
				PlayersPerTeam = numberPlayersPerTeam,
			};
		}

		private RandomTeamCreator ArrangeTeamCreator(IRandom random, int numberPlayersInList, int numberPlayersPerTeam)
		{
			var playerList = ArrangePlayerList(numberPlayersInList);
			return new RandomTeamCreator(random)
			{
				Players = playerList,
				PlayersPerTeam = numberPlayersPerTeam,
			};
		}

		private IEnumerable<Player> ArrangePlayerList(int numberPlayers)
		{
			return Enumerable.Repeat(ArrangePlayer(), numberPlayers);
		}

		private Player ArrangePlayer()
		{
			return new Player();
		}
	}
}
