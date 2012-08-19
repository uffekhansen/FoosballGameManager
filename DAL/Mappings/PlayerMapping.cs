using Domain.Entities;
using FluentNHibernate.Mapping;

namespace DAL.Mappings
{
	public class PlayerMapping : ClassMap<Player>
	{
		public PlayerMapping()
		{
			Id(x => x.Id);
			Map(x => x.Name);
			Map(x => x.Affiliation);
		}
	}
}
