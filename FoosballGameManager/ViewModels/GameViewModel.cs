using System.Collections.Generic;
using Domain.Entities;

namespace FoosballGameManager.ViewModels
{
    public class GameViewModel
    {
        public IEnumerable<Player> Players { get; set; }
    }
}