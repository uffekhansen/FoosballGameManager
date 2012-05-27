using System.Collections.Generic;

namespace Domain.Entities
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
