using System;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Domain.Installers;
using FoosballGameManager.Installers;

namespace FoosballGameManager
{
	public class MvcApplication : System.Web.HttpApplication
	{
		private static IWindsorContainer _container;
		private static IServiceProvider _serviceProvider;

		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
		}

		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
				name: "Default",
				url: "{controller}/{action}/{id}",
				defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
			);
		}

		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();

			RegisterGlobalFilters(GlobalFilters.Filters);
			RegisterRoutes(RouteTable.Routes);

			BootstrapContainer();
		}

		private static void BootstrapContainer()
		{
			var installers = new IWindsorInstaller[]
			{
				new FoosballGameManagerInstaller(),
				new DomainInstaller(),
			};

			_container = new WindsorContainer();
			_container.Install(installers);

			_serviceProvider = new WindsorServiceProvider(_container);
		}
	}
}
