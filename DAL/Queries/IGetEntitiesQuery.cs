using System.Collections.Generic;

namespace DAL.Queries
{
	public interface IGetEntitiesQuery<T>
	{
		IEnumerable<T> Execute();
	}
}
