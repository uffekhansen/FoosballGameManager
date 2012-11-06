using Domain.Entities;

namespace Tests.Builders
{
	public class PlayerBuilder : Builder<Player>
	{
		public string Name;

		public PlayerBuilder()
		{
		}

		public PlayerBuilder(IPersister persister)
			: base(persister)
		{
		}
	}
}
