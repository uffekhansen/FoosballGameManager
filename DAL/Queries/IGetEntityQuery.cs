using System;

namespace DAL.Queries
{
	public interface IGetEntityQuery<T>
	{
		T Execute(Guid id);
	}
}
