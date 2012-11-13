using System.Collections.Generic;
using Domain.Entities;

namespace Domain.ValueObjects
{
	public class Team
	{
		private readonly IEnumerable<Player> _players;

		public IEnumerable<Player> Players
		{
			get { return _players; }
		}

		public Team(IEnumerable<Player> players)
		{
			_players = players;
		}
	}
}
