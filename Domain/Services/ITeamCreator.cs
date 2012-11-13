using System.Collections.Generic;
using Domain.Entities;
using Domain.ValueObjects;

namespace Domain.Services
{
	public interface ITeamCreator
	{
		List<Team> CreateTeams();

		int PlayersPerTeam { get; set; }

		IEnumerable<Player> Players { get; set; }
	}
}
