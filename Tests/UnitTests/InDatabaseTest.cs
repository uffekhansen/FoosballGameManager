using Castle.MicroKernel.Registration;
using Castle.Windsor;
using DAL.Mappings;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using Tests.Infrastructure;

namespace Tests.UnitTests
{
	public class InDatabaseTest
	{
		protected ISession _session;

		private IWindsorContainer _container;
		private static ISessionFactory _sessionFactory;

		private const string _databaseFilename = @"C:\Users\Uffe\Desktop\dbfiles\foosball_test.db";

		public InDatabaseTest()
		{
			var dataBootstrapper = new DataBootstrapper();
			dataBootstrapper.Boostrap();

			BootstrapContainer();
		}

		private void BootstrapContainer()
		{
			var installers = new IWindsorInstaller[]
			{
			};

			_container = new WindsorContainer();
			_container.Install(installers);

			_sessionFactory = CreateSessionFactory();
			_session = _sessionFactory.OpenSession();
		}

		private ISessionFactory CreateSessionFactory()
		{
			return Fluently.Configure()
				.Database(SQLiteConfiguration.Standard.UsingFile(_databaseFilename))
				.Mappings(m => m.FluentMappings.AddFromAssemblyOf<PlayerMapping>())
				.BuildSessionFactory();
		}
	}
}
