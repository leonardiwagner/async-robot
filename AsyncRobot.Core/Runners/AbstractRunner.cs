using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncRobot.Core.Runners
{
    public class AbstractRunner
    {
        protected IEnumerable<Robot> Robots;
        public event EventHandler<RobotMoveArgs> Moved;
        public event EventHandler<RobotMoveArgs> Reached;

        protected void MoveRobot(int threadId, Robot robot)
        {
            robot.Move();
            Moved(this, new RobotMoveArgs(threadId, robot.Id, robot.CurrentPosition.X, robot.CurrentPosition.Y));

        }

        protected void OnReached()
        {
            Reached(this, new RobotMoveArgs(0, 0, 0, 0));
        }
    }
}
