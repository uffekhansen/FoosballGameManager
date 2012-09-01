using Domain.Entities;

namespace Tests.Builders
{
	public class PlayerBuilder : Builder<Player>
	{
		public PlayerBuilder() {}

		public PlayerBuilder(IPersister persister)
			: base(persister)
		{
		}
	}
}
