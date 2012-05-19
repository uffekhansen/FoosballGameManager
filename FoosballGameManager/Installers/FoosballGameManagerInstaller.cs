using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace FoosballGameManager.Installers
{
	public class FoosballGameManagerInstaller : IWindsorInstaller
	{
		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			container.Register(/*Classes.FromThisAssembly()
				.Pick()
				.LifestyleTransient()*/);
		}
	}
}
