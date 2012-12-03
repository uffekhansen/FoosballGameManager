using DAL.Mappings;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace Tests.Infrastructure
{
	public class NHibernateBootstrapper : INHibernateBootstrapper
	{
		//private const string _databaseFilename = @"C:\Users\Uffe\Desktop\dbfiles\foosball_tests.db";
		private const string _databaseFilename = "foosball_tests.db";

		public ISessionFactory Bootstrap()
		{
			return Fluently.Configure()
				.Database(SQLiteConfiguration.Standard.UsingFile(_databaseFilename))
				.Mappings(m => m.FluentMappings.AddFromAssemblyOf<PlayerMapping>())
				.ExposeConfiguration(BuildSchema)
				.BuildSessionFactory();
		}

		private void BuildSchema(Configuration cfg)
		{
			var schemaExport = new SchemaExport(cfg);

			schemaExport.Drop(script: false, export: true);
			schemaExport.Create(script: false, export: true);
		}
	}
}
