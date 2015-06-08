using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncRobot.Core.Runners
{
    public abstract class AbstractRunner
    {
        public event EventHandler<RobotMoveArgs> OnRobotMove;
        protected readonly Land RunnerLand;
        protected readonly IDictionary<Robot, LandPosition> RobotsToRun = new Dictionary<Robot, LandPosition>();

        protected AbstractRunner(Land land)
        {
            RunnerLand = land;
        }

        public void AddRobotToRunner(Robot robot, LandPosition startPosition)
        {
            RobotsToRun.Add(robot, startPosition);
        }

        protected void OnMove(object sender, RobotMoveArgs e)
        {
            OnRobotMove(sender, e);
        }




    }
}
