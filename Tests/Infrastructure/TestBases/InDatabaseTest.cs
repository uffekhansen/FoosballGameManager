using NHibernate;
using Tests.Builders;

namespace Tests.Infrastructure.TestBases
{
	public class InDatabaseTest
	{
		protected ISession _session;
		protected readonly IPersister _persister;

		private static ISessionFactory _sessionFactory;

		public InDatabaseTest()
		{
			var dataBootstrapper = new DataBootstrapper();
			dataBootstrapper.Boostrap();

			_sessionFactory = TestContainer.Resolve<ISessionFactory>();
			_session = _sessionFactory.OpenSession();
			_persister = new Persister(_session);
		}
	}
}
