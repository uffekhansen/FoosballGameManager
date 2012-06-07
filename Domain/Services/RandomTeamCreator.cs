using System.Collections.Generic;
using System.Linq;
using Domain.Entities;

namespace Domain.Services
{
	public class RandomTeamCreator : TeamCreator
	{
		public RandomTeamCreator(int playersPerTeam, List<Player> players)
			: base(playersPerTeam, players)
		{
		}

		protected override List<Team> GenerateTeams()
		{
			var teams = new List<Team>();

			for (int i = 0; i < _players.Count; i += _playersPerTeam)
			{
				teams.Add(new Team
				{
					Players = _players
						.GetRange(i, _playersPerTeam)
						.ToList()
				});
			}

			return teams;
		}
	}
}
