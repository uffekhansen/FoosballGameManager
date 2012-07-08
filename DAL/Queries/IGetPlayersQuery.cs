using System.Collections.Generic;
using Domain.Entities;

namespace DAL.Queries
{
	public interface IGetPlayersQuery
	{
		IEnumerable<Player> Execute();
	}
}