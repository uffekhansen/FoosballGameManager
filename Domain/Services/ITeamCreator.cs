using System.Collections.Generic;
using Domain.Entities;

namespace Domain.Services
{
	public interface ITeamCreator
	{
		List<Team> CreateTeams();

		int PlayersPerTeam { get; set; }

		IEnumerable<Player> Players { get; set; }
	}
}
