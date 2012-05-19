using System.Collections.Generic;

namespace GLL.Entities
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
