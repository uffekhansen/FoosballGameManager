using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GLL.Dataclasses
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
