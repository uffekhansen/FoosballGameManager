using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Tests.Infrastructure.Installers;

namespace Tests.Infrastructure
{
	internal static class TestContainer
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

		public static void Initialize()
		{
			var installers = new IWindsorInstaller[]
			{
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
