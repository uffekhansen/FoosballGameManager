using System.Collections.Generic;
using Domain.Extensions;
using NHibernate;

namespace Tests.Builders
{
	class Persister : IPersister
	{
		private readonly ISession _session;
		private readonly IList<object> _objects;

		public Persister(ISession session)
		{
			_session = session;
		}

		public void Add(object obj)
		{
			_objects.Add(obj);
		}

		public void Persist()
		{
			_objects.Each(x => _session.Save(x));
		}
	}
}
