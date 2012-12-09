using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Domain.Extensions;
using Domain.Tools;

namespace Domain.Strategies
{
	public class MixedAffiliationTeamCreationStrategy : IMixedAffiliationTeamCreationStrategy
	{
		public IEnumerable<Player> Players { get; set; }
		public int PlayersPerTeam { get; set; }

		private IList<Team> _teams;
		private readonly IRandom _random;
		private IDictionary<string, List<Player>> _playersGroupedByAffiliation;

		public MixedAffiliationTeamCreationStrategy(IRandom random)
		{
			_random = random;
		}

		public IList<Team> CreateTeams()
		{
			_teams = new List<Team>();

			CreateAffiliationDictionary();
			CreateMixedTeamsFromAffiliationDictionary();

			return _teams;
		}

		private void CreateAffiliationDictionary()
		{
			_playersGroupedByAffiliation = Players
				.GroupBy(player => player.Affiliation)
				.ToDictionary(grouping => grouping.Key, grouping => grouping.ToList());
		}

		private void CreateMixedTeamsFromAffiliationDictionary()
		{
			int numberOfTeams = Players.Count() / PlayersPerTeam;

			numberOfTeams.TimesDo(() => _teams.Add(GetTeamFromMostRepresentedAffiliation()));
		}

		private Team GetTeamFromMostRepresentedAffiliation()
		{
			var players = new List<Player>();
			var usedAffiliations = new List<string>();

			PlayersPerTeam.TimesDo(() =>
			{
				var player = TakePlayerFromMostRepresentedAffiliation(usedAffiliations);
				players.Add(player);
				usedAffiliations.Add(player.Affiliation);
				RemoveEmptyAffiliations();
			});

			return new Team(players);
		}

		private Player TakePlayerFromMostRepresentedAffiliation(IEnumerable<string> usedAffiliations)
		{
			var unusedAffiliations = GetUnusedAffiliations(usedAffiliations);
			var largestAffiliationName = GetLargestAffiliationName(unusedAffiliations);
			var player = TakePlayerWithAffiliation(largestAffiliationName);
			return player;
		}

		private IEnumerable<string> GetUnusedAffiliations(IEnumerable<string> usedAffiliations)
		{
			var unusedAffiliations = _playersGroupedByAffiliation
				.Where(pair => !usedAffiliations.Contains(pair.Key))
				.Select(pair => pair.Key);

			if (!unusedAffiliations.Any())
			{
				return GetEveryAffiliation();
			}

			return unusedAffiliations;
		}

		private IEnumerable<string> GetEveryAffiliation()
		{
			return _playersGroupedByAffiliation.Select(pair => pair.Key);
		}

		private string GetLargestAffiliationName(IEnumerable<string> unusedAffiliationNames)
		{
			int largestAffiliationCount = GetLargestAffiliationCount(unusedAffiliationNames);

			return _playersGroupedByAffiliation.First(pair => pair.Value.Count == largestAffiliationCount).Key;
		}

		private Player TakePlayerWithAffiliation(string affiliation)
		{
			var affiliationList = _playersGroupedByAffiliation
				.First(pair => pair.Value.First().Affiliation == affiliation)
				.Value;

			int index = _random.Next(affiliationList.Count);
			var player = affiliationList[index];

			affiliationList.RemoveAt(index);

			return player;
		}

		private int GetLargestAffiliationCount(IEnumerable<string> unusedAffiliationNames)
		{
			return _playersGroupedByAffiliation
				.Where(pair => unusedAffiliationNames.Contains(pair.Key))
				.Max(pair =>  pair.Value.Count);
		}

		private void RemoveEmptyAffiliations()
		{
			var emptyAffiliations = _playersGroupedByAffiliation
				.Where(pair => !pair.Value.Any())
				.Select(pair => pair.Key)
				.ToList();

			emptyAffiliations.Each(x => _playersGroupedByAffiliation.Remove(x));
		}
	}
}
