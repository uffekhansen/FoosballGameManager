using System.Web.Mvc;
using System.Web.Routing;
using FoosballGameManager.Infrastructure.DI;

namespace FoosballGameManager
{
	public class MvcApplication : System.Web.HttpApplication
	{
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
			FoosballContainer.Initialize();

			var windsorControllerFactory = new WindsorControllerFactory(FoosballContainer.Kernel);
			ControllerBuilder.Current.SetControllerFactory(windsorControllerFactory);
		}
	}
}
