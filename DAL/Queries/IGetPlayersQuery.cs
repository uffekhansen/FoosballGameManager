using System;
using System.Collections;
using System.Collections.Generic;

namespace DAL.Queries
{
	public interface IGetPlayersQuery
	{
		IEnumerable Execute(IEnumerable<Guid> playerIds);
	}
}
