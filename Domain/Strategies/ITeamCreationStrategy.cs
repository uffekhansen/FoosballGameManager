using System.Collections.Generic;
using Domain.Entities;

namespace Domain.Strategies
{
	public interface ITeamCreationStrategy
	{
		IEnumerable<Player> Players { get; set; }

		int PlayersPerTeam { get; set; }

		IList<Team> CreateTeams();
	}
}
