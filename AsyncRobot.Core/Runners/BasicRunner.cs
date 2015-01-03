using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncRobot.Core.Runners {
    public class BasicRunner {
        private IEnumerable<Robot> Robots;
        public event EventHandler<RobotMoveArgs> Moved;
        public event EventHandler<RobotMoveArgs> Reached;

        public BasicRunner(IEnumerable<Robot> robots) {
            this.Robots = robots;
        }

        public void Run() {
            foreach (var robot in Robots) {
                while (!robot.HasReachedExit) {
                    this.MoveRobot(robot);
                }
            }

            Reached(this, new RobotMoveArgs(0, 0, 0));
        }

        private void MoveRobot(Robot robot) {
            robot.Move();
            Moved(this, new RobotMoveArgs(robot.Id, robot.CurrentPosition.X, robot.CurrentPosition.Y));
        }

    }
}
