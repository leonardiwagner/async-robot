using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncRobot.Core.Runners {
    public class BasicRunner : AbstractRunner
    {
        public BasicRunner(Land land) : base(land) { }

        public void Run() {
            foreach (var robotToRun in base.RobotsToRun) {
                robotToRun.Key.Moved += robot_Moved;
                robotToRun.Key.SearchForLandExit(base.RunnerLand, robotToRun.Value);
            }
        }


        void robot_Moved(object sender, RobotMoveArgs e)
        {
            base.OnMove(this, e);
        }


    }
}
