using System;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using DAL.Infrastructure.Installers;
using Domain.Infrastructure.Installers;
using FoosballGameManager.Infrastructure.Installers;

namespace FoosballGameManager.Infrastructure.DI
{
	public class FoosballContainer
	{
		private static IWindsorContainer _container;

		private static IWindsorContainer WindsorContainer
		{
			get
			{
				if (_container == null)
				{
					Initialize();
				}

				return _container;
			}
			set
			{
				_container = value;
			}
		}

		public static IKernel Kernel
		{
			get
			{
				return WindsorContainer.Kernel;
			}
		}

		public static void Initialize()
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

		public static T Resolve<T>()
		{
			return WindsorContainer.Resolve<T>();
		}

		public static object Resolve(Type service)
		{
			return WindsorContainer.Resolve(service);
		}

		public static void Dispose()
		{
			_container.Dispose();
		}
	}
}
