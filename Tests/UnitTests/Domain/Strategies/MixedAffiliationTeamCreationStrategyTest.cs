using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Domain.Strategies;
using Domain.Extensions;
using FluentAssertions;
using Tests.Builders;
using Xunit.Extensions;

namespace Tests.UnitTests.Domain.Strategies
{
	public class MixedAffiliationTeamCreationStrategyTest
	{
		private IList<Player> _players = new List<Player>();
		private readonly MixedAffiliationTeamCreationStrategy _mixedAffiliationTeamCreationStrategy;

		private const string _noAffiliation = "";
		private const string _mvnoAffiliation = "MVNO";
		private const string _salesAffiliation = "Sales";

		public MixedAffiliationTeamCreationStrategyTest()
		{
			_mixedAffiliationTeamCreationStrategy = new MixedAffiliationTeamCreationStrategy();
		}

		[Theory]
		[InlineData(2, 2, 0, 2)]
		[InlineData(2, 2, 2, 3)]
		[InlineData(1, 3, 0, 1)]
		[InlineData(1, 4, 1, 2)]
		[InlineData(2, 2, 6, 4)]
		[InlineData(3, 3, 10, 6)]
		public void Given_Players_And_A_Team_Size_Of_2_When_CreateTeams_Then_Teams_Are_Mixed_As_Much_As_Possible(int mvnoAffiliatedPlayers, int salesAffiliatedPlayers, int notAffiliatedPlayers, int expectedNumberOfMixedTeams)
		{
			ArrangePlayers(mvnoAffiliatedPlayers, _mvnoAffiliation);
			ArrangePlayers(salesAffiliatedPlayers, _salesAffiliation);
			ArrangePlayers(notAffiliatedPlayers, _noAffiliation);
			_players = _players.OrderBy(x => x.Id).ToList();
			_mixedAffiliationTeamCreationStrategy.PlayersPerTeam = 2;
			_mixedAffiliationTeamCreationStrategy.Players = _players;

			var teams = _mixedAffiliationTeamCreationStrategy.CreateTeams();

			FindNumberTeamsOfPureMixedAffiliation(teams).Should().Be(expectedNumberOfMixedTeams);
		}

		private int FindNumberTeamsOfPureMixedAffiliation(IEnumerable<Team> teams)
		{
			int pureMixedTeams = 0;

			teams.Each(team =>
			{
				var groupings = team.Players.GroupBy(player => player.Affiliation);
				if (groupings.Count() == team.Players.Count())
				{
					++pureMixedTeams;
				}
			});

			return pureMixedTeams;
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
