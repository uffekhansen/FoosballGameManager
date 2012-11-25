using System.Web.Mvc;
using System.Web.Routing;
using FoosballGameManager.Infrastructure.DI;
using HibernatingRhinos.Profiler.Appender.NHibernate;
using NHibernate;

namespace FoosballGameManager
{
	public class MvcApplication : System.Web.HttpApplication
	{
		public MvcApplication()
		{
			EndRequest += (sender, args) => CommitTransaction();
		}

		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();

			RegisterGlobalFilters(GlobalFilters.Filters);
			RegisterRoutes(RouteTable.Routes);

			BootstrapContainer();
			NHibernateProfiler.Initialize();
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

		protected void Application_BeginRequest()
		{
			var session = FoosballContainer.Resolve<ISession>();
			session.BeginTransaction();
		}

		private void CommitTransaction()
		{
			var session = FoosballContainer.Resolve<ISession>();
			if (session == null)
			{
				return;
			}

			var transaction = session.Transaction;
			if (!transaction.IsActive)
			{
				return;
			}

			transaction.Commit();
		}

		protected void Application_End()
		{
			FoosballContainer.Dispose();
		}
	}
}
