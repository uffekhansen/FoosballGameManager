using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Domain.Extensions;

namespace Domain.Strategies
{
	public class MixedAffiliationTeamCreationStrategy : IMixedAffiliationTeamCreationStrategy
	{
		public IEnumerable<Player> Players { get; set; }
		public int PlayersPerTeam { get; set; }

		private IList<Team> _teams;
		public List<List<Player>> _playersGroupedByAffiliation;

		public IList<Team> CreateTeams()
		{
			_teams = new List<Team>();

			CreateAffiliationGroupings();
			CreateMixedTeamsFromAffiliationGroupings();

			return _teams;
		}

		private void CreateAffiliationGroupings()
		{
			_playersGroupedByAffiliation = Players
				.GroupBy(player => player.Affiliation)
				.Select(grouping => grouping.ToList()).ToList();
		}

		private void CreateMixedTeamsFromAffiliationGroupings()
		{
			var players = GetPlayersFromLargestAffiliationGroupings();

			_teams.Add(new Team(players));
		}

		private List<Player> GetPlayersFromLargestAffiliationGroupings()
		{
			var players = new List<Player>();
			var usedAffiliations = new List<string>();

			PlayersPerTeam.TimesDo(() =>
			{
				var player = TakePlayerFromLargestAffiliationGrouping(usedAffiliations);
				players.Add(player);
				usedAffiliations.Add(player.Affiliation);
			});

			return null;
		}

		private Player TakePlayerFromLargestAffiliationGrouping(IList<string> usedAffiliations)
		{
			var groupings = GetGroupings(usedAffiliations);
			var largestGrouping = GetLargestGrouping(groupings);
			var player = largestGrouping.First();
			largestGrouping.Remove(player);
			return largestGrouping.First();
		}

		private List<List<Player>> GetGroupings(IList<string> usedAffiliations)
		{
			var groupings = GetUnusedGroupings(usedAffiliations);
			if (!groupings.Any())
			{
				groupings = _playersGroupedByAffiliation;
			}

			return groupings;
		}

		private List<List<Player>> GetUnusedGroupings(IList<string> usedAffiliations)
		{
			return _playersGroupedByAffiliation
				.Where(grouping => grouping.Any() && !GroupingContainsUsedAffiliation(grouping, usedAffiliations))
				.ToList();
		}

		private bool GroupingContainsUsedAffiliation(List<Player> grouping, IList<string> usedAffiliations)
		{
			return usedAffiliations.Contains(grouping.First().Affiliation);
		}

		private List<Player> GetLargestGrouping(List<List<Player>> groupings)
		{
			int largestGroupingCount = GetLargestGroupingCount(groupings);

			return groupings.First(grouping => grouping.Count() == largestGroupingCount);
		}

		private int GetLargestGroupingCount(List<List<Player>> groupings)
		{
			return groupings.Max(grouping => grouping.Count());
		}
	}
}
