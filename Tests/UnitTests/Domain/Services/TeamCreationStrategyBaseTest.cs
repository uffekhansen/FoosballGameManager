using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Domain.Extensions;
using Domain.Strategies;
using Domain.Tools;
using NSubstitute;
using Xunit;
using Xunit.Extensions;

namespace Tests.UnitTests.Domain.Services
{
	public abstract class TeamCreationStrategyBaseTest<T> where T : ITeamCreationStrategy
	{
		[Theory]
		[InlineData(1, 1, 1)]
		[InlineData(1, 2, 2)]
		[InlineData(2, 2, 1)]
		[InlineData(1, 2, 2)]
		[InlineData(5, 5, 1)]
		[InlineData(3, 6, 2)]
		public void Given_TeamCreationStrategy_When_Creating_Teams_Then_The_Expected_Number_Of_Teams_Are_Returned(int expectedNumberOfTeams, int numberPlayersInlist, int numberPlayersPerTeam)
		{
			var teamCreationStrategy = ArrangeTeamCreationStrategy(numberPlayersInlist, numberPlayersPerTeam);

			var teams = teamCreationStrategy.CreateTeams();

			Assert.Equal(expectedNumberOfTeams, teams.Count);
		}

		[Theory]
		[InlineData(1, 1)]
		[InlineData(2, 2)]
		[InlineData(2, 1)]
		[InlineData(5, 1)]
		[InlineData(6, 2)]
		[InlineData(6, 3)]
		public void Given_RandomTeamCreationStrategy_When_Creating_Teams_Then_Generated_Teams_Contains_Expected_Number_Of_Players(int numberPlayersInlist, int numberPlayersPerTeam)
		{
			var teamCreationStrategy = ArrangeTeamCreationStrategy(numberPlayersInlist, numberPlayersPerTeam);

			var teams = teamCreationStrategy.CreateTeams();

			teams.Each(x => Assert.Equal(numberPlayersPerTeam, x.Players.Count()));
		}

		private ITeamCreationStrategy ArrangeTeamCreationStrategy(int numberPlayersInList, int numberPlayersPerTeam)
		{
			var playerList = ArrangePlayerList(numberPlayersInList);

			var teamCreationStrategy = CreateTeamCreationStrategy();
			teamCreationStrategy.Players = playerList;
			teamCreationStrategy.PlayersPerTeam = numberPlayersPerTeam;

			return teamCreationStrategy;
		}

		private IEnumerable<Player> ArrangePlayerList(int numberPlayers)
		{
			return Enumerable.Repeat(ArrangePlayer(), numberPlayers)
				.ToList();
		}

		private Player ArrangePlayer()
		{
			return new Player();
		}

		protected abstract ITeamCreationStrategy CreateTeamCreationStrategy();
	}

	public class RandomTeamCreationStrategyBaseTest : TeamCreationStrategyBaseTest<RandomTeamCreationStrategy>
	{
		protected override ITeamCreationStrategy CreateTeamCreationStrategy()
		{
			return new RandomTeamCreationStrategy(Substitute.For<IRandom>());
		}
	}

	public class GroupedAffiliationTeamCreationStrategyBaseTest : TeamCreationStrategyBaseTest<GroupedAffiliationTeamCreationStrategy>
	{
		protected override ITeamCreationStrategy CreateTeamCreationStrategy()
		{
			return new GroupedAffiliationTeamCreationStrategy(new RandomTeamCreationStrategy(Substitute.For<IRandom>()));
		}
	}
}
