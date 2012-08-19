using Tests.Infrastructure;

namespace Tests.UnitTests
{
	public class InDatabaseTest
	{
		public InDatabaseTest()
		{
			var dataBootstrapper = new DataBootstrapper();
			dataBootstrapper.Boostrap();
		}
	}
}
