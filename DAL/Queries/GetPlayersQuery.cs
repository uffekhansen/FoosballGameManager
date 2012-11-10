using System;
using System.Collections.Generic;
using Domain.Entities;

namespace DAL.Queries
{
	public class GetPlayersQuery : IGetPlayersQuery
	{

		public GetPlayersQuery()
		{
			_session = session;
		}

		public IEnumerable<Player> Execute(IEnumerable<Guid> playerIds)
		{
			
		}
	}
}
