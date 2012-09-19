using NHibernate;
using Tests.Builders;
using Tests.Infrastructure;

namespace Tests
{
	public class TestBase
	{
		protected readonly IPersister _persister;

		public TestBase()
		{
			_persister = new Persister(TestContainer.Resolve<ISession>());
		}
	}
}
