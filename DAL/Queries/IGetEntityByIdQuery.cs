using System;

namespace DAL.Queries
{
	public interface IGetEntityByIdQuery<T>
	{
		T Execute(Guid id);
	}
}
