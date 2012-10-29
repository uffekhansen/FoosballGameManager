using System;
using System.Collections.Generic;
using Domain.Entities;

namespace FoosballGameManager.ViewModels
{
	public class PlayersViewModel
	{
		public Player NewPlayer;
		public Guid SelectedPlayerId;
		public IEnumerable<Player> Players;
	}
}
