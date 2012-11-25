using Domain.Entities;
using FluentNHibernate.Mapping;

namespace DAL.Mappings
{
	public class TournamentMapping : ClassMap<Tournament>
	{
		public TournamentMapping()
		{
			Id(tournament => tournament.Id).GeneratedBy.Assigned();

			HasMany(tournament => tournament.Teams).Cascade.All();
		}
	}
}
