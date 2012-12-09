using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Domain.Extensions;
using Domain.Strategies;
using Domain.Tools;
using FluentAssertions;
using NSubstitute;
using Tests.Builders;
using Xunit.Extensions;

namespace Tests.UnitTests.Domain.Strategies
{
	public class GroupedAffiliationTeamCreationStrategyTest
	{
		private readonly IRandom _random = Substitute.For<IRandom>();
		private IList<Player> _players = new List<Player>();
		private readonly GroupedAffiliationTeamCreationStrategy _groupedAffiliationTeamCreationStrategy;
		private const string _noAffiliation = "";
		private const string _mvnoAffiliation = "MVNO";
		private const string _salesAffiliation = "Sales";

		public GroupedAffiliationTeamCreationStrategyTest()
		{
			_groupedAffiliationTeamCreationStrategy = new GroupedAffiliationTeamCreationStrategy(Substitute.For<IRandomTeamCreationStrategy>(), _random);
		}

		[Theory]
		[InlineData(2, 2, 0, 1, 1, 0)]
		[InlineData(2, 2, 2, 1, 1, 1)]
		[InlineData(1, 3, 0, 0, 1, 0)]
		[InlineData(1, 4, 1, 0, 2, 0)]
		[InlineData(2, 2, 6, 1, 1, 3)]
		public void Given_Players_And_A_Team_Size_Of_2_When_CreateTeams_Then_Teams_Are_Grouped_By_Affiliation(int mvnoAffiliatedPlayers, int salesAffiliatedPlayers, int notAffiliatedPlayers, int expectedPureMvnoTeams, int expectedPureSalesTeams, int expectedPureNotAffiliatedTeams)
		{
			ArrangePlayers(mvnoAffiliatedPlayers, _mvnoAffiliation);
			ArrangePlayers(salesAffiliatedPlayers, _salesAffiliation);
			ArrangePlayers(notAffiliatedPlayers, _noAffiliation);
			_players = _players.OrderBy(x => x.Id).ToList();
			_groupedAffiliationTeamCreationStrategy.PlayersPerTeam = 2;
			_groupedAffiliationTeamCreationStrategy.Players = _players;

			var teams = _groupedAffiliationTeamCreationStrategy.CreateTeams();

			FindNumberTeamsOfPureAffiliation(teams, _mvnoAffiliation).Should().Be(expectedPureMvnoTeams);
			FindNumberTeamsOfPureAffiliation(teams, _salesAffiliation).Should().Be(expectedPureSalesTeams);
			FindNumberTeamsOfPureAffiliation(teams, _noAffiliation).Should().Be(expectedPureNotAffiliatedTeams);
		}

		[Theory]
		[InlineData(3, 3, 0, 1, 1, 0)]
		[InlineData(3, 2, 1, 1, 0, 0)]
		[InlineData(2, 4, 0, 0, 1, 0)]
		[InlineData(1, 6, 2, 0, 2, 0)]
		[InlineData(3, 3, 6, 1, 1, 2)]
		public void Given_Players_And_A_Team_Size_Of_3_When_CreateTeams_Then_Teams_Are_Grouped_By_Affiliation(int mvnoAffiliatedPlayers, int salesAffiliatedPlayers, int notAffiliatedPlayers, int expectedPureMvnoTeams, int expectedPureSalesTeams, int expectedPureNotAffiliatedTeams)
		{
			ArrangePlayers(mvnoAffiliatedPlayers, _mvnoAffiliation);
			ArrangePlayers(salesAffiliatedPlayers, _salesAffiliation);
			ArrangePlayers(notAffiliatedPlayers, _noAffiliation);
			_players = _players.OrderBy(x => x.Id).ToList();
			_groupedAffiliationTeamCreationStrategy.PlayersPerTeam = 3;
			_groupedAffiliationTeamCreationStrategy.Players = _players;

			var teams = _groupedAffiliationTeamCreationStrategy.CreateTeams();

			FindNumberTeamsOfPureAffiliation(teams, _mvnoAffiliation).Should().Be(expectedPureMvnoTeams);
			FindNumberTeamsOfPureAffiliation(teams, _salesAffiliation).Should().Be(expectedPureSalesTeams);
			FindNumberTeamsOfPureAffiliation(teams, _noAffiliation).Should().Be(expectedPureNotAffiliatedTeams);
		}

		[Theory]
		[InlineData(2, 2, 0, 1, 1, 0)]
		[InlineData(2, 2, 2, 1, 1, 1)]
		[InlineData(1, 3, 0, 0, 1, 0)]
		[InlineData(1, 4, 1, 0, 2, 0)]
		[InlineData(2, 2, 6, 1, 1, 3)]
		public void Given_Players_And_A_Team_Size_Of_2_When_CreateTeams_Then_Random_Is_Executed_Once_For_Each_Player_In_Pure_Affiliation_Team(int mvnoAffiliatedPlayers, int salesAffiliatedPlayers, int notAffiliatedPlayers, int expectedPureMvnoTeams, int expectedPureSalesTeams, int expectedPureNotAffiliatedTeams)
		{
			const int playersPerTeam = 2;
			ArrangePlayers(mvnoAffiliatedPlayers, _mvnoAffiliation);
			ArrangePlayers(salesAffiliatedPlayers, _salesAffiliation);
			ArrangePlayers(notAffiliatedPlayers, _noAffiliation);
			_players = _players.OrderBy(x => x.Id).ToList();
			_groupedAffiliationTeamCreationStrategy.PlayersPerTeam = playersPerTeam;
			_groupedAffiliationTeamCreationStrategy.Players = _players;

			_groupedAffiliationTeamCreationStrategy.CreateTeams();

			var expectedNumberOfPureTeams = expectedPureMvnoTeams + expectedPureNotAffiliatedTeams + expectedPureSalesTeams;
			_random.Received(expectedNumberOfPureTeams * playersPerTeam).Next(Arg.Any<int>());
		}

		private void ArrangePlayers(int count, string affiliation)
		{
			count.TimesDo(() => 
				_players.Add(new PlayerBuilder
				{
					Affiliation = affiliation,
				}.Build()));
		}

		private int FindNumberTeamsOfPureAffiliation(IEnumerable<Team> teams, string affiliation)
		{
			return teams.Count(team => team.Players
				.All(player => player.Affiliation == affiliation));
		}
	}
}
