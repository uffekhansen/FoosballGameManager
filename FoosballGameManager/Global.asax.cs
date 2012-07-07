using System;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Domain.Installers;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FoosballGameManager.Installers;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace FoosballGameManager
{
	public class MvcApplication : System.Web.HttpApplication
	{
		private static IWindsorContainer _container;
		private static IServiceProvider _serviceProvider;
		private static ISessionFactory _sessionFactory;

		private const string _databaseFileName = "foosball.db";

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

			_sessionFactory = CreateSessionFactory();
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

		private static ISessionFactory CreateSessionFactory()
		{
			return Fluently.Configure()
				.Database(
				SQLiteConfiguration.Standard
				.UsingFile(_databaseFileName)
				)
				.Mappings(m =>
							m.FluentMappings.AddFromAssemblyOf<MvcApplication>())
				.ExposeConfiguration(BuildSchema)
				.BuildSessionFactory();
		}

		private static void BuildSchema(Configuration config)
		{
			new SchemaExport(config).Create(false, true);
		}
	}
}
