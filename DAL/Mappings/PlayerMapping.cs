using Domain.Entities;
using FluentNHibernate.Mapping;

namespace DAL.Mappings
{
	public class PlayerMapping : ClassMap<Player>
	{
		public PlayerMapping()
		{
			Id(player => player.Id).GeneratedBy.Assigned();

			Map(player => player.Name);
			Map(player => player.Affiliation);
		}
	}
}
