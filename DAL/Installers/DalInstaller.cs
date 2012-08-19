using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using DAL.Queries;

namespace DAL.Installers
{
	public class DalInstaller : IWindsorInstaller
	{
		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			container.Register(Component.For<IGetPlayersQuery>()
				.ImplementedBy<GetPlayersQuery>()
				.LifestyleTransient());
		}
	}
}
