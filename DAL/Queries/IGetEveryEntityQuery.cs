using System.Collections.Generic;

namespace DAL.Queries
{
	public interface IGetEveryEntityQuery<T>
	{
		IEnumerable<T> Execute();
	}
}
