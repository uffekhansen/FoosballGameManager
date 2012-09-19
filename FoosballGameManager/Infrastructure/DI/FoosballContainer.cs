using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using DAL.Infrastructure.Installers;
using Domain.Infrastructure.Installers;
using FoosballGameManager.Infrastructure.Installers;

namespace FoosballGameManager.Infrastructure.DI
{
	internal static class FoosballContainer
	{
		private static IWindsorContainer WindsorContainer
		{
			get
			{
				if(WindsorContainer == null)
				{
					Initialize();
				}

				return WindsorContainer;
			}
			set
			{
				WindsorContainer = value;
			}
		}

		public static IKernel Kernel
		{
			get
			{
				return WindsorContainer.Kernel;
			}
		}

		internal static void Initialize()
		{
			var installers = new IWindsorInstaller[]
			{
				new ControllerInstaller(),
				new DomainInstaller(),
				new DalInstaller(),
				new SessionInstaller()
			};

			WindsorContainer = new WindsorContainer();
			WindsorContainer.Install(installers);
		}

		internal static T Resolve<T>()
		{
			return WindsorContainer.Resolve<T>();
		}
	}
}
