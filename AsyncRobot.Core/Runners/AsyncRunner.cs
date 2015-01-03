using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncRobot.Core.Runners {
    public class AsyncRunner {
        private IEnumerable<Robot> Robots;
        public event EventHandler<RobotMoveArgs> Moved;
        public event EventHandler<RobotMoveArgs> Reached;

        public AsyncRunner(IEnumerable<Robot> robots) {
            this.Robots = robots;
        }

        public async Task Run() {
            foreach (var robot in Robots) {
                while (!robot.HasReachedExit) {
                    await this.MoveRobotAsync(robot);
                }
            }

            Reached(this,new RobotMoveArgs(0, 0, 0));
        }

        private async Task MoveRobotAsync(Robot robot) {
            await Task.Run(() => {
                robot.Move();
                Moved(this, new RobotMoveArgs(robot.Id, robot.CurrentPosition.X, robot.CurrentPosition.Y));
            });
        }
    }
}
