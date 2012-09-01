using System.Collections.Generic;
using Domain.Entities;
using NHibernate;
using NHibernate.Linq;

namespace DAL.Queries
{
	public class GetPlayersQuery : IGetPlayersQuery
	{
		private readonly ISession _session;

		public GetPlayersQuery(ISession session)
		{
			_session = session;
		}

		public IEnumerable<Player> Execute()
		{
			return _session.Query<Player>();
		}
	}
}
