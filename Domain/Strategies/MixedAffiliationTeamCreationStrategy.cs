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

		public IList<Team> CreateTeams()
		{
			_teams = new List<Team>();

			// Take a player from the largest grouping
			// Take another player from the largest grouping, not equal to first player
			// Create a team
			// Repeat until not enough players
		}
	}
}
