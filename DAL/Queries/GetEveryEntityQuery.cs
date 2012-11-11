using System.Collections.Generic;
using NHibernate;
using NHibernate.Linq;

namespace DAL.Queries
{
	public class GetEveryEntityQuery<T> : IGetEveryEntityQuery<T> where T : class
	{
		private readonly ISession _session;

		public GetEveryEntityQuery(ISession session)
		{
			_session = session;
		}

		public IEnumerable<T> Execute()
		{
			return _session.Query<T>();
		}
	}
}
