using Domain.Entities;
using NHibernate;

namespace DAL.Queries
{
	public class IsPlayerNameUniqueQuery : IIsPlayerNameUniqueQuery
	{
		private readonly ISession _session;

		public IsPlayerNameUniqueQuery(ISession session)
		{
			_session = session;
		}

		public bool Execute(string name)
		{
			return _session.QueryOver<Player>()
				.Where(player => player.Name == name)
				.RowCount() == 0;
		}
	}
}
