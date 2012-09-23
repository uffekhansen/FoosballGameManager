using System.Collections.Generic;
using System.Linq;
using NHibernate;
using Tests.Extensions;

namespace Tests.Builders
{
	class Persister : IPersister
	{
		private readonly ISession _session;
		private readonly IList<object> _objects;

		public Persister(ISession session)
		{
			_session = session;
			_objects = new List<object>();
		}

		public void Add(object obj)
		{
			_objects.Add(obj);
		}

		public void Persist()
		{
			_session.SaveWithTransaction(_objects.ToArray());
		}
	}
}
