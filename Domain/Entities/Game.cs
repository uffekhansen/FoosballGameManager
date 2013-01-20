using System.Collections.Generic;

namespace Domain.Entities
{
    public class Game : AssignedIdEntity
    {
        public virtual IEnumerable<Player> Players { get; set; }

        public Game()
        {
            Players = new Player[] {};
        }
    }
}