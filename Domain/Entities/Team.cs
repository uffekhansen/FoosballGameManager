using System.Collections.Generic;

namespace Domain.Entities
{
	public class Team : AssignedIdEntity
	{
		public virtual IEnumerable<Player> Players
		{
			get;
			set;
		}

		public Team()
		{
		}

		public Team(IEnumerable<Player> players)
		{
			Players = players;
		}
	}
}
