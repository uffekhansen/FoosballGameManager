using System.Collections.Generic;
using System.Linq;
using GameLogic.Entities;
using GameLogic.Exceptions;

namespace GameLogic.Logic
{
	public class TeamCreator
	{
		public List<Team> CreateTeams(List<Player> players, int playersPerTeam)
		{
			GuardAgainstWrongSettings(players, playersPerTeam);

			return GenerateTeams(players, playersPerTeam);
		}

		private void GuardAgainstWrongSettings(List<Player> players, int playersPerTeam)
		{
			ThrowsIfEmpty(players);
			ThrowsIfNotEnoughPlayersForOneTeam(players, playersPerTeam);
			ThrowsIfNumberPlayersDoesNotMatchTeamSettings(players, playersPerTeam);
		}

		private void ThrowsIfEmpty(List<Player> players)
		{
			if (!players.Any())
			{
				throw new TeamGenerationException("Zero players in list");
			}
		}

		private void ThrowsIfNotEnoughPlayersForOneTeam(List<Player> players, int playersPerTeam)
		{
			if (players.Count < playersPerTeam)
			{
				throw new TeamGenerationException("Not enough players for a single team");
			}
		}

		private void ThrowsIfNumberPlayersDoesNotMatchTeamSettings(List<Player> players, int playersPerTeam)
		{
			if (players.Count % playersPerTeam != 0)
			{
				throw new TeamGenerationException("Number players not divisable with number players per team in team settings");
			}
		}

		private List<Team> GenerateTeams(List<Player> players, int playersPerTeam)
		{
			var teams = new List<Team>();

			for (int i = 0; i < players.Count; i += playersPerTeam)
			{
				teams.Add(new Team
				{
					Players = players
						.GetRange(i, playersPerTeam)
						.ToList()
				});
			}

			return teams;
		}
	}
}
