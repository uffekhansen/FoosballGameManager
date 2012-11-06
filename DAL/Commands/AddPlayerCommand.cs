using DAL.Queries;
using Domain.Entities;
using Domain.Exceptions;
using NHibernate;

namespace DAL.Commands
{
	public class AddPlayerCommand : AddCommand<Player>
	{
		private readonly IIsPlayerNameUniqueQuery _isPlayerNameUniqueQuery;

		public AddPlayerCommand(ISession session, IIsPlayerNameUniqueQuery isPlayerNameUniqueQuery)
			: base(session)
		{
			_isPlayerNameUniqueQuery = isPlayerNameUniqueQuery;
		}

		public void Execute(Player player)
		{
			if (!_isPlayerNameUniqueQuery.Execute(player.Name))
			{
				throw new AlreadyExistsException("Player name not unique");
			}

			base.Execute(player);
		}
	}
}
