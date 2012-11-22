using System.Collections.Generic;
using Domain.Entities;

namespace Tests.Builders
{
	public class TeamBuilder : Builder<Team>
	{
		public IEnumerable<Player> Players;

		public TeamBuilder()
		{
		}

		public TeamBuilder(IPersister persister)
			: base(persister)
		{
		}
	}
}
