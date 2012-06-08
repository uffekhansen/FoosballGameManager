using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Domain.Tools;

namespace Domain.Services
{
	public class RandomTeamCreator : TeamCreator
	{
		private readonly IRandom _random;

		public RandomTeamCreator(IRandom random, int playersPerTeam, IEnumerable<Player> players)
			: base(playersPerTeam, players)
		{
			_random = random;
		}

		protected override List<Team> GenerateTeams()
		{
			var teams = new List<Team>();
			var players = _players.ToList();

			for (int i = 0; i < _players.Count(); i += _playersPerTeam)
			{
				teams.Add(TakeTeam(players));
			}

			return teams;
		}

		private Team TakeTeam(List<Player> players)
		{
			var playersForTeam = new List<Player>();

			for (int i = 0; i < _playersPerTeam; i++)
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
