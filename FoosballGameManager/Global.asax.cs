using System.Web.Mvc;
using System.Web.Routing;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using DAL.Infrastructure.Installers;
using Domain.Infrastructure.Installers;
using FoosballGameManager.Infrastructure.DI;
using FoosballGameManager.Infrastructure.Installers;

namespace FoosballGameManager
{
	public class MvcApplication : System.Web.HttpApplication
	{
		private static IWindsorContainer _container;

		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();

			RegisterGlobalFilters(GlobalFilters.Filters);
			RegisterRoutes(RouteTable.Routes);

			BootstrapContainer();
		}

		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
		}

		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
			routes.IgnoreRoute("{*favicon}", new { favicon = @"(.*/)?favicon.ico(/.*)?" });

			routes.MapRoute(
				name: "Default",
				url: "{controller}/{action}/{id}",
				defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
			);
		}

		private static void BootstrapContainer()
		{
			var installers = new IWindsorInstaller[]
			{
				new ControllerInstaller(),
				new DomainInstaller(),
				new DalInstaller(),
				new SessionInstaller()
			};

			_container = new WindsorContainer();
			_container.Install(installers);

			var windsorControllerFactory = new WindsorControllerFactory(_container.Kernel);
			ControllerBuilder.Current.SetControllerFactory(windsorControllerFactory);
		}
	}
}
