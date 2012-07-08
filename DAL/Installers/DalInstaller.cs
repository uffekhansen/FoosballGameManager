using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace FoosballGameManager.Installers
{
	public class DalInstaller : IWindsorInstaller
	{
		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			container.Register(Classes.FromThisAssembly()
				.InSameNamespaceAs<DalInstaller>()
				.LifestyleTransient());
		}
	}
}
