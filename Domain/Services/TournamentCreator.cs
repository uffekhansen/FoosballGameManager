using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Domain.Exceptions;

namespace Domain.Services
{
	public class TournamentCreator : ITournamentCreator
	{
		private IEnumerable<Team> _teams;

		public Tournament CreateTournament(IEnumerable<Team> teams)
		{
			_teams = teams;
			GuardAgainstWrongSettings();

			return new Tournament
			{
				Teams = _teams,
			};
		}

		private void GuardAgainstWrongSettings()
		{
			ThrowsIfNoTeams();
			ThrowsIfOnlyOneTeam();
		}

		private void ThrowsIfNoTeams()
		{
			if (!_teams.Any())
			{
				throw new TournamentCreationException("No teams to create tournament from");
			}
		}

		private void ThrowsIfOnlyOneTeam()
		{
			if(_teams.Count() == 1)
			{
				throw new TournamentCreationException("Only 1 team supplied - need at least 2 to create a tournament");	
			}
		}
	}
}
