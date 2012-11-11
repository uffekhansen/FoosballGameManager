using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Domain.Exceptions;

namespace Domain.Services
{
	public class TournamentCreator : ITournamentCreator
	{
		public IEnumerable<Team> Teams;

		public Tournament CreateTournament()
		{
			GuardAgainstWrongSettings();

			return new Tournament
			{
				Teams = Teams,
			};
		}

		private void GuardAgainstWrongSettings()
		{
			ThrowsIfNoTeams();
			ThrowsIfOnlyOneTeam();
		}

		private void ThrowsIfNoTeams()
		{
			if (!Teams.Any())
			{
				throw new TournamentCreationException("No teams to create tournament from");
			}
		}

		private void ThrowsIfOnlyOneTeam()
		{
			if(Teams.Count() == 1)
			{
				throw new TournamentCreationException("Only 1 team supplied - need at least 2 to create a tournament");	
			}
		}
	}
}
