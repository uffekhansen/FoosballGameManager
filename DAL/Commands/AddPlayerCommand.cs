using DAL.Queries;
using Domain.Entities;
using Domain.Exceptions;

namespace DAL.Commands
{
	public class AddPlayerCommand : IAddPlayerCommand
	{
		private readonly IAddCommand<Player> _addCommand;
		private readonly IIsPlayerNameUniqueQuery _isPlayerNameUniqueQuery;

		public AddPlayerCommand(IAddCommand<Player> addCommand, IIsPlayerNameUniqueQuery isPlayerNameUniqueQuery)
		{
			_isPlayerNameUniqueQuery = isPlayerNameUniqueQuery;
			_addCommand = addCommand;
		}

		public void Execute(Player player)
		{
			if (!_isPlayerNameUniqueQuery.Execute(player.Name))
			{
				throw new AlreadyExistsException("Player name not unique");
			}

			_addCommand.Execute(player);
		}
	}
}
