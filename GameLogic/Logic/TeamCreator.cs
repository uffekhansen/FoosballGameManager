using System.Collections.Generic;
using System.Linq;
using GameLogic.Entities;
using GameLogic.Exceptions;

namespace GameLogic.Logic
{
	public class TeamCreator
	{
		public TeamSettings TeamSettings;

		public List<Team> CreateTeams(List<Player> players)
		{
			ThrowsIfEmpty(players);
			ThrowsIfNotEnoughPlayersForOneTeam(players);
			ThrowsIfNumberPlayersDoesNotMatchTeamSettings(players);

			return GenerateTeams(players);
		}

		private void ThrowsIfEmpty(List<Player> players)
		{
			if (!players.Any())
			{
				throw new TeamGenerationException("Zero players in list");
			}
		}

		private void ThrowsIfNotEnoughPlayersForOneTeam(List<Player> players)
		{
			if (players.Count < TeamSettings.NumberPlayers)
			{
				throw new TeamGenerationException("Not enough players for a single team");
			}
		}

		private void ThrowsIfNumberPlayersDoesNotMatchTeamSettings(List<Player> players)
		{
			if (players.Count % TeamSettings.NumberPlayers != 0)
			{
				throw new TeamGenerationException("Number players not divisable with number players per team in team settings");
			}
		}

		private List<Team> GenerateTeams(List<Player> players)
		{
			var teams = new List<Team>();
			int playersPerTeam = TeamSettings.NumberPlayers;

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
