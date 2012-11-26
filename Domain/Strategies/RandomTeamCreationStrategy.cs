using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Domain.Tools;

namespace Domain.Strategies
{
	public class RandomTeamCreationStrategy : IRandomTeamCreationStrategy
	{
		public int PlayersPerTeam { get; set; }
		public IEnumerable<Player> Players { get; set; }

		private readonly IRandom _random;

		public RandomTeamCreationStrategy(IRandom random)
		{
			_random = random;
		}

		public IList<Team> CreateTeams()
		{
			var teams = new List<Team>();
			var players = Players.ToList();

			for (int i = 0; i < Players.Count(); i += PlayersPerTeam)
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
