using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using NHibernate;

namespace Tests.Infrastructure.Installers
{
	public class SessionInstaller : IWindsorInstaller
	{
		private readonly INHibernateBootstrapper _nHibernateBootstrapper;

		public SessionInstaller(INHibernateBootstrapper nHibernateBootstrapper)
		{
			_nHibernateBootstrapper = nHibernateBootstrapper;
		}

		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			container.Register(
				Component.For<ISessionFactory>()
				.LifeStyle.Singleton
				.UsingFactoryMethod(_nHibernateBootstrapper.Bootstrap));

			container.Register(Component.For<ISession>()
				.LifeStyle.Transient
				.UsingFactoryMethod(kernel => kernel.Resolve<ISessionFactory>().OpenSession()));
		}
	}
}
