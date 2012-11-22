using Domain.Entities;
using FluentNHibernate.Mapping;

namespace DAL.Mappings
{
	public class TeamMapping : ClassMap<Team>
	{
		public TeamMapping()
		{
			Id(team => team.Id).GeneratedBy.Assigned();
			HasManyToMany(x => x.Players);
		}
	}
}
