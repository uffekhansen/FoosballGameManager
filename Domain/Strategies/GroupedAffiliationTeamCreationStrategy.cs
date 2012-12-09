using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Domain.Extensions;
using Domain.Tools;

namespace Domain.Strategies
{
	public class GroupedAffiliationTeamCreationStrategy : IGroupedAffiliationTeamCreationStrategy
	{
		public int PlayersPerTeam { get; set; }
		public IEnumerable<Player> Players { get; set; }

		private IList<Team> _teams;
		private IList<Player> _excessPlayers;

		private readonly IRandom _random;
		private readonly IRandomTeamCreationStrategy _randomTeamCreationStrategy;

		public GroupedAffiliationTeamCreationStrategy(IRandomTeamCreationStrategy randomTeamCreationStrategy, IRandom random)
		{
			_randomTeamCreationStrategy = randomTeamCreationStrategy;
			_random = random;
		}

		public IList<Team> CreateTeams()
		{
			InitListsAsEmpty();

			var playersGroupedByAffiliation = Players.GroupBy(player => player.Affiliation);

			playersGroupedByAffiliation.Each(playerGroup => CreateTeamsWithSameAffiliation(playerGroup.ToList()));

			CreateRandomTeamsFromExcessPlayers();

			return _teams;
		}

		private void InitListsAsEmpty()
		{
			_teams = new List<Team>();
			_excessPlayers = new List<Player>();
		}

		private void CreateTeamsWithSameAffiliation(List<Player> playersSharingAffiliation)
		{
			while (playersSharingAffiliation.Count >= PlayersPerTeam)
			{
				var playersForTeam = TakeRandomPlayersForTeam(playersSharingAffiliation);

				AddTeam(playersForTeam);
			}

			AddExcessPlayers(playersSharingAffiliation);
		}

		private IEnumerable<Player> TakeRandomPlayersForTeam(List<Player> players)
		{
			var randomPlayers = new List<Player>();

			PlayersPerTeam.TimesDo(() => randomPlayers.Add(TakeRandomPlayer(players)));

			return randomPlayers;
		}

		private Player TakeRandomPlayer(List<Player> players)
		{
			int index = _random.Next(players.Count);

			var player = players[index];
			players.RemoveAt(index);

			return player;
		}

		private void AddTeam(IEnumerable<Player> players)
		{
			_teams.Add(new Team { Players = players });
		}

		private void AddExcessPlayers(IEnumerable<Player> playersSharingAffiliation)
		{
			playersSharingAffiliation.Each(player => _excessPlayers.Add(player));
		}

		private void CreateRandomTeamsFromExcessPlayers()
		{
			_randomTeamCreationStrategy.Players = _excessPlayers;
			_randomTeamCreationStrategy.PlayersPerTeam = PlayersPerTeam;

			var excessTeams = _randomTeamCreationStrategy.CreateTeams();
			excessTeams.Each(team => _teams.Add(team));
		}
	}
}
