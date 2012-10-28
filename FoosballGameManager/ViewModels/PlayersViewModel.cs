using System;
using System.Collections.Generic;
using Domain.Entities;

namespace FoosballGameManager.ViewModels
{
	public class PlayersViewModel
	{
		public Guid SelectedPlayerId;
		public IEnumerable<Player> Players;
	}
}