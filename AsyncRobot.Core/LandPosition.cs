using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncRobot.Core
{
    public class LandPosition {
        public int X { get; set; }
        public int Y { get; set; }
        public LandPositionType PositionType { get; set; }

        public LandPosition(int x, int y, LandPositionType positionType = LandPositionType.WALL) {
            this.X = x;
            this.Y = y;
            this.PositionType = positionType;
        }

    }
}
