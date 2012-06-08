using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Domain.Tools;

namespace Domain.Services
{
	public class RandomTeamCreator : TeamCreator
	{
		private IRandom _random;

		public RandomTeamCreator(IRandom random, int playersPerTeam, IEnumerable<Player> players)
			: base(playersPerTeam, players)
		{
			_random = random;
		}

		protected override List<Team> GenerateTeams()
		{
			var teams = new List<Team>();
			var players = _players.ToList();

			for (int i = 0; i < players.Count; i += _playersPerTeam)
			{
				var playersForTeam = players.GetRange(i, _playersPerTeam).ToList();
				teams.Add(new Team(playersForTeam));
			}

			return teams;
		}
	}
}
