using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Tests.Infrastructure.Installers;

namespace Tests.Infrastructure
{
	public class DataBootstrapper
	{
		private IWindsorContainer _container;

		public void Boostrap()
		{
			var installers = new IWindsorInstaller[]
			{
				new SessionInstaller()
			};

			_container = new WindsorContainer();
			_container.Install(installers);
		}
	}
}
