using System.Collections.Generic;

namespace GameLogic.Entities
{
    public class Team
    {
        public List<Player> Players;

        public Team()
        {
            Players = new List<Player>();
        }
    }
}
