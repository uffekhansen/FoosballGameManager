using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Domain.Extensions;
using Domain.Services;
using Domain.Tools;
using FluentAssertions;
using NSubstitute;
using Tests.Builders;
using Xunit.Extensions;

namespace Tests.UnitTests.Domain.Services
{
	public class GroupedAffiliationTeamCreatorTest
	{
		private IList<Player> _players = new List<Player>();
		private readonly GroupedAffiliationTeamCreator _groupedAffiliationTeamCreator;

		private const string _noAffiliation = "";
		private const string _mvnoAffiliation = "MVNO";
		private const string _salesAffiliation = "Sales";

		public GroupedAffiliationTeamCreatorTest()
		{
			_groupedAffiliationTeamCreator = new GroupedAffiliationTeamCreator(Substitute.For<IRandom>());
		}

		/*
		 * Given players size of 2
		 * 2 MVNO, 2 SALES			= 1 mvno only, 1 sales only
		 * 2 MVNO, 2 SALES, 2 N/A	= 1 mvno only, 1 sales only, 1 n/a only
		 * 1 MVNO, 3 SALES			= 1 sales only
		 * 1 MVNO, 4 SALES, 1 N/A	= 2 sales only
		 * 2 MVNO, 2 SALES, 6 N/A	= 1 mvno only, 1 sales only, 3 n/a only
		 */

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
			_groupedAffiliationTeamCreator.PlayersPerTeam = 2;
			_groupedAffiliationTeamCreator.Players = _players;

			var teams = _groupedAffiliationTeamCreator.CreateTeams();

			teams.Count().Should().Be(expectedPureMvnoTeams + expectedPureNotAffiliatedTeams + expectedPureSalesTeams);
			FindNumberTeamsOfPureAffiliation(teams, _mvnoAffiliation).Should().Be(expectedPureMvnoTeams);
			FindNumberTeamsOfPureAffiliation(teams, _salesAffiliation).Should().Be(expectedPureSalesTeams);
			FindNumberTeamsOfPureAffiliation(teams, _noAffiliation).Should().Be(expectedPureNotAffiliatedTeams);
		}

		/*
		 * Given players size of 3
		 * 3 MVNO, 3 SALES			= 1 mvno only, 1 sales only
		 * 3 MVNO, 2 SALES, 2 N/A	= 1 mvno only
		 * 1 MVNO, 4 SALES			= 1 sales only
		 * 1 MVNO, 6 SALES, 1 N/A	= 2 sales only
		 * 3 MVNO, 3 SALES, 6 N/A	= 1 mvno only, 1 sales only, 2 n/a only
		*/

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
			_groupedAffiliationTeamCreator.PlayersPerTeam = 3;
			_groupedAffiliationTeamCreator.Players = _players;

			var teams = _groupedAffiliationTeamCreator.CreateTeams();

			teams.Count().Should().Be(expectedPureMvnoTeams + expectedPureNotAffiliatedTeams + expectedPureSalesTeams);
			FindNumberTeamsOfPureAffiliation(teams, _mvnoAffiliation).Should().Be(expectedPureMvnoTeams);
			FindNumberTeamsOfPureAffiliation(teams, _salesAffiliation).Should().Be(expectedPureSalesTeams);
			FindNumberTeamsOfPureAffiliation(teams, _noAffiliation).Should().Be(expectedPureNotAffiliatedTeams);
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

		//TODO: Random teamcreator is used
	}
}
