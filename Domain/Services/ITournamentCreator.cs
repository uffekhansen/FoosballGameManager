using System.Collections.Generic;
using Domain.Entities;
using Domain.ValueObjects;

namespace Domain.Services
{
	public interface ITournamentCreator
	{
		Tournament CreateTournament(IEnumerable<Team> teams);
	}
}
