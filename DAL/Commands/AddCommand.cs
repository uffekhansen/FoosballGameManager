using NHibernate;

namespace DAL.Commands
{
	public class AddCommand<T> :IAddCommand<T> where T : class
	{
		private readonly ISession _session;

		public AddCommand(ISession session)
		{
			_session = session;
		}

		public void Execute(T t)
		{
			_session.Save(t);
		}
	}
}
