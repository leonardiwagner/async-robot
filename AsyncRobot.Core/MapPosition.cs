using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncRobot.Core
{
    public class MapPosition
    {
        public int x { get; private set; }
        public int y { get; private set; }
        public char value { get; private set; }

        public MapPosition(int x, int y, char value)
        {
            this.x = x;
            this.y = y;
            this.value = value;
        }
    }
}
