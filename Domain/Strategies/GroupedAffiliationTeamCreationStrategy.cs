using System.Collections.Generic;
using Domain.Entities;

namespace Domain.Strategies
{
	public class GroupedAffiliationTeamCreationStrategy : IGroupedAffiliationTeamCreationStrategy
	{
		public int PlayersPerTeam { get; set; }

		public IEnumerable<Player> Players { get; set; }

		public GroupedAffiliationTeamCreationStrategy(IRandomTeamCreationStrategy randomTeamCreationStrategy)
		{
		}

		public IList<Team> CreateTeams()
		{
			throw new System.NotImplementedException();
		}
	}
}
