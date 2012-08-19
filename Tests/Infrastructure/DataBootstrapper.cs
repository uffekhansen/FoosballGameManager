using DAL.Mappings;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;

namespace Tests.Infrastructure
{
	public class DataBootstrapper
	{
		private const string _databaseFilename = @"C:\Users\Uffe\Desktop\dbfiles\foosballTests.db";

		public void Boostrap()
		{
			CreateSessionFactory();
		}

		private void CreateSessionFactory()
		{
			Fluently.Configure()
				.Database(SQLiteConfiguration.Standard.UsingFile(_databaseFilename))
				.Mappings(m => m.FluentMappings.Add<PlayerMapping>())
				.BuildSessionFactory();
		}
	}
}
