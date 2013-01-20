using Domain.Entities;
using FluentNHibernate.Mapping;

namespace DAL.Mappings
{
	public class GameMapping : ClassMap<Game>
	{
		public GameMapping()
		{
			Id(game  => game.Id).GeneratedBy.Assigned();

            HasMany(game => game.Players).Cascade.All();
		}
	}
}
