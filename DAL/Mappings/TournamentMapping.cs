using Domain.Entities;
using FluentNHibernate.Mapping;

namespace DAL.Mappings
{
	public class TournamentMapping : ClassMap<Tournament>
	{
		public TournamentMapping()
		{
			Id(tournament => tournament.Id).GeneratedBy.Assigned();
			HasMany(tournament => tournament.Teams)
				.Component(team => team.Map(x => x.Players));
		}
	}
}
