using System;
using Domain.Entities;

namespace Tests.Builders
{
	public class PlayerBuilder : Builder<Player>
	{
		public Guid Id = Guid.NewGuid();

		public PlayerBuilder()
		{
		}

		public PlayerBuilder(IPersister persister)
			: base(persister)
		{
		}
	}
}
