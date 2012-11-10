using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Domain.Exceptions;

namespace Domain.Services
{
	public abstract class TeamCreator : ITeamCreator
	{
		private int _playersPerTeam = 2;
		public int PlayersPerTeam
		{
			get { return _playersPerTeam; }
			set { _playersPerTeam = value; }
		}

		protected readonly IEnumerable<Player> _players;

		protected TeamCreator(IEnumerable<Player> players)
		{
			_players = players;
		}

		public List<Team> CreateTeams()
		{
			GuardAgainstWrongSettings();

			return GenerateTeams();
		}

		private void GuardAgainstWrongSettings()
		{
			ThrowsIfEmpty();
			ThrowsIfNotEnoughPlayersForOneTeam();
			ThrowsIfNumberPlayersDoesNotMatchTeamSettings();
		}

		private void ThrowsIfEmpty()
		{
			if (!_players.Any())
			{
				throw new TeamGenerationException("Zero players in list");
			}
		}

		private void ThrowsIfNotEnoughPlayersForOneTeam()
		{
			if (_players.Count() < _playersPerTeam)
			{
				throw new TeamGenerationException("Not enough players for a single team");
			}
		}

		private void ThrowsIfNumberPlayersDoesNotMatchTeamSettings()
		{
			if (_players.Count() % _playersPerTeam != 0)
			{
				throw new TeamGenerationException("Number players not divisable with number players per team in team settings");
			}
		}

		protected abstract List<Team> GenerateTeams();
	}
}
