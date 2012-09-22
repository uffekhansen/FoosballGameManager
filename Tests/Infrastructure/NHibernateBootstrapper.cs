using DAL.Mappings;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;

namespace Tests.Infrastructure
{
	public class NHibernateBootstrapper : INHibernateBootstrapper
	{
		private const string _databaseFilename = @"C:\Users\Uffe\Desktop\dbfiles\foosball_tests.db";

		public ISessionFactory Bootstrap()
		{
			return Fluently.Configure()
				.Database(SQLiteConfiguration.Standard.UsingFile(_databaseFilename))
				.Mappings(m => m.FluentMappings.AddFromAssemblyOf<PlayerMapping>())
				.BuildSessionFactory();
		}
	}
}