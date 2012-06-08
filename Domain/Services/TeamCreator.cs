using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Domain.Exceptions;

namespace Domain.Services
{
	public abstract class TeamCreator : ITeamCreator
	{
		protected readonly int _playersPerTeam;
		protected readonly IEnumerable<Player> _players;

		protected TeamCreator(int playersPerTeam, IEnumerable<Player> players)
		{
			_playersPerTeam = playersPerTeam;
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
