using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Domain.Extensions;

namespace Domain.Strategies
{
	public class GroupedAffiliationTeamCreationStrategy : IGroupedAffiliationTeamCreationStrategy
	{
		public int PlayersPerTeam { get; set; }
		public IEnumerable<Player> Players { get; set; }

		protected IList<Team> _teams;
		private IList<Player> _excessPlayers;

		private readonly IRandomTeamCreationStrategy _randomTeamCreationStrategy;

		public GroupedAffiliationTeamCreationStrategy(IRandomTeamCreationStrategy randomTeamCreationStrategy)
		{
			_randomTeamCreationStrategy = randomTeamCreationStrategy;
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
				var playersForTeam = playersSharingAffiliation.Take(PlayersPerTeam).ToList();

				AddTeam(playersForTeam);
				playersSharingAffiliation.RemoveRange(0, PlayersPerTeam);
			}

			AddExcessPlayers(playersSharingAffiliation);
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
