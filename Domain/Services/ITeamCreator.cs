using System.Collections.Generic;
using Domain.Entities;

namespace Domain.Services
{
	public interface ITeamCreator
	{
		IList<Team> CreateTeams();

		int PlayersPerTeam { get; set; }

		IEnumerable<Player> Players { get; set; }
	}
}
