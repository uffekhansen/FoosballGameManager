using System;
using System.Collections.Generic;
using Domain.Entities;

namespace DAL.Queries
{
	public interface IGetPlayersByIdsQuery
	{
		IEnumerable<Player> Execute(IEnumerable<Guid> playerIds);
	}
}
