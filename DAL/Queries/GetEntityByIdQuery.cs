using System;
using NHibernate;

namespace DAL.Queries
{
	public class GetEntityByIdQuery<T> : IGetEntityByIdQuery<T>
	{
		private readonly ISession _session;

		public GetEntityByIdQuery(ISession session)
		{
			_session = session;
		}

		public T Execute(Guid id)
		{
			return _session.Get<T>(id);
		}
	}
}