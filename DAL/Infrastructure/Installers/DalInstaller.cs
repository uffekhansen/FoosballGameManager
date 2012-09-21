using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace DAL.Infrastructure.Installers
{
	public class DalInstaller : IWindsorInstaller
	{
		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			container.Register(
				Classes.FromThisAssembly()
				.Pick()
				.WithServiceDefaultInterfaces()
				.LifestylePerWebRequest());
		}
	}
}
