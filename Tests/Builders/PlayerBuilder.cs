using Domain.Entities;

namespace Tests.Builders
{
	public class PlayerBuilder : Builder<Player>
	{
		public string Name = "Default name";
		public string Affiliation;

		public PlayerBuilder()
		{
		}

		public PlayerBuilder(IPersister persister)
			: base(persister)
		{
		}
	}
}
