using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Domain.Exceptions;

namespace DAL.Queries
{
	public class GetPlayersQuery : IGetPlayersQuery
	{
		private readonly IGetEntityByIdQuery<Player> _getPlayerEntityByIdQuery;
		private readonly IList<Guid> _unrecognizedPlayerIds = new List<Guid>();

		public GetPlayersQuery(IGetEntityByIdQuery<Player> getPlayerEntityByIdQuery)
		{
			_getPlayerEntityByIdQuery = getPlayerEntityByIdQuery;
		}

		public IEnumerable<Player> Execute(IEnumerable<Guid> playerIds)
		{
			var players = playerIds.Select(GetPlayer).ToList();

			if (_unrecognizedPlayerIds.Any())
			{
				throw new NotFoundException(GenerateNotFoundErrorMessage());
			}

			return players;
		}

		private string GenerateNotFoundErrorMessage()
		{
			var unrecognizedIdStrings = _unrecognizedPlayerIds
				.Select(id => string.Format(" {0},", id));

			return unrecognizedIdStrings
				.Aggregate("Players with the following ids did not exist:", (current, id) => current + id);
		}

		private Player GetPlayer(Guid id)
		{
			var player = _getPlayerEntityByIdQuery.Execute(id);

			if (player == null)
			{
				_unrecognizedPlayerIds.Add(id);
			}

			return player;

		}
	}
}
