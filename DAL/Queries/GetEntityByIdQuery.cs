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
		// 9 december s�ndag hos gitte- mormors f�dselsdag 83 300,- for mig 10.30
		public T Execute(Guid id)
		{
			return _session.Get<T>(id);
		}
	}
}