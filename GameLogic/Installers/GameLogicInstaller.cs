using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace GameLogic.Installers
{
	public class GameLogicInstaller : IWindsorInstaller
	{
		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			container.Register(/*Classes.FromThisAssembly()
				.Pick()
				.LifestyleTransient()*/);
		}
	}
}
