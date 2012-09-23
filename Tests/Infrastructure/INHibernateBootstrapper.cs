using NHibernate;

namespace Tests.Infrastructure
{
	public interface INHibernateBootstrapper
	{
		ISessionFactory Bootstrap();
	}
}
