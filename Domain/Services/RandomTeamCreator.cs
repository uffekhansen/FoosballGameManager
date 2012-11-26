using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Domain.Tools;

namespace Domain.Services
{
	public class RandomTeamCreator : TeamCreator
	{
		private readonly IRandom _random;

		public RandomTeamCreator()
		{
		}

		public RandomTeamCreator(IRandom random)
		{
			_random = random;
		}

		protected override IList<Team> GenerateTeams()
		{
			var teams = new List<Team>();
			var players = _players.ToList();

			for (int i = 0; i < _players.Count(); i += PlayersPerTeam)
			{
				teams.Add(TakeTeam(players));
			}

			return teams;
		}

		private Team TakeTeam(List<Player> players)
		{
			var playersForTeam = new List<Player>();

			for (int i = 0; i < PlayersPerTeam; i++)
			{
				playersForTeam.Add(TakeRandomPlayer(players));
			}

			return new Team(playersForTeam);
		}

		private Player TakeRandomPlayer(List<Player> players)
		{
			int index = _random.Next(players.Count);
			var player = players.ElementAt(index);

			players.RemoveAt(index);

			return player;
		}
	}
}
