using System.Collections.Generic;
using Domain.Entities;
using Domain.Enums;

namespace Domain.Services
{
	public interface ITeamCreator
	{
		IList<Team> CreateTeams(TeamGenerationMethod teamGenerationMethod);

		int PlayersPerTeam { get; set; }

		IEnumerable<Player> Players { get; set; }
	}
}
