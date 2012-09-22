using NHibernate;
using Tests.Builders;
using Tests.Infrastructure;

namespace Tests
{
	public class TestBase
	{
		protected readonly ISession _session;
		protected readonly IPersister _persister;

		public TestBase()
		{
			_session = TestContainer.Resolve<ISession>();
			_persister = new Persister(_session);
		}
	}
}
