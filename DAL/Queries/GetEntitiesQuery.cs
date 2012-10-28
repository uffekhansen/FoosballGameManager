using System.Collections.Generic;
using NHibernate;
using NHibernate.Linq;

namespace DAL.Queries
{
	public class GetEntitiesQuery<T> : IGetEntitiesQuery<T> where T : class
	{
		private readonly ISession _session;

		public GetEntitiesQuery(ISession session)
		{
			_session = session;
		}

		public IEnumerable<T> Execute()
		{
			return _session.Query<T>();
		}
	}
}
