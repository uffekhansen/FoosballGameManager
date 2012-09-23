using Domain.Entities;
using NHibernate;

namespace DAL.Queries
{
	public class AddPlayerQuery : IAddPlayerQuery
	{
		private readonly ISession _session;

		public AddPlayerQuery(ISession session)
		{
			_session = session;
		}

		public void Execute(Player player)
		{
			_session.Save(player);
		}
	}
}
