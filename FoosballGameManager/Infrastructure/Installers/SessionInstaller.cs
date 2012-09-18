using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using DAL.Mappings;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;

namespace FoosballGameManager.Infrastructure.Installers
{
	public class SessionInstaller : IWindsorInstaller
	{
		private const string _databaseFilename = @"C:\Users\Uffe\Desktop\dbfiles\foosball.db";

		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			container.Register(
				Component.For<ISessionFactory>()
				.LifeStyle.Singleton
				.UsingFactoryMethod(CreateSessionFactory));

			container.Register(Component.For<ISession>()
				.LifeStyle.PerWebRequest
				.UsingFactoryMethod(kernel => kernel.Resolve<ISessionFactory>().OpenSession()));
		}

		private static ISessionFactory CreateSessionFactory()
		{
			return Fluently.Configure()
				.Database(SQLiteConfiguration.Standard.UsingFile(_databaseFilename))
				.Mappings(m => m.FluentMappings.AddFromAssemblyOf<PlayerMapping>())
				.BuildSessionFactory();
		}
	}
}
