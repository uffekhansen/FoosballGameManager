using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Domain.Services;

namespace Domain.Infrastructure.Installers
{
	public class DomainInstaller : IWindsorInstaller
	{
		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			container.Register(
				Classes.FromThisAssembly()
				.Pick()
				.WithServiceDefaultInterfaces());
		}
	}
}
