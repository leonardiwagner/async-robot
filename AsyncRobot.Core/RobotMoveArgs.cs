using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncRobot.Core {
    public class RobotMoveArgs {
        public int RobotId { get; private set; }
        public int X { get; private set; }
        public int Y { get; private set; }
        public RobotMoveArgs(int robotId, int x, int y) {
            this.RobotId = robotId;
            this.X = x;
            this.Y = y;
        }
    }
}
