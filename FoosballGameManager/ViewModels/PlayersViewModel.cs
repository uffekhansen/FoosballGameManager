using System;
using System.Collections.Generic;
using Domain.Entities;
using Domain.Enums;

namespace FoosballGameManager.ViewModels
{
	public class PlayersViewModel
	{
		public IEnumerable<Player> Players { get; set; }

		public IEnumerable<Guid> SelectedPlayers { get; set; }

		public TeamGenerationMethod TeamGenerationMethod { get; set; }
	}
}
