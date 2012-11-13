using System.Collections.Generic;
using Domain.Entities;

namespace Domain.Services
{
	public interface ITournamentCreator
	{
		Tournament CreateTournament(IEnumerable<Team> teams);
	}
}
