using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncRobot.Core.Runners {
    public class ThreadRunner {
        private IEnumerable<Robot> Robots;
        private int ThreadCount;
        public event EventHandler<RobotMoveArgs> Moved;
        public event EventHandler<RobotMoveArgs> Reached;

        public ThreadRunner(IEnumerable<Robot> robots, int threadCount) {
            this.Robots = robots;
            this.ThreadCount = threadCount;
        }

        public void Run() {
            var threads = new List<Thread>();
            for (int i = 0; i < this.ThreadCount; i++) {
                threads.Add(new Thread(() => Run(i * (ThreadCount / 50), (i + 1) == ThreadCount)));
            }

            foreach (var thread in threads) {
                thread.Start();
            }
        }

        private void Run(int skip, bool isLastThread) {
            foreach (var robot in Robots.Skip(skip).Take(10)) {
                while (!robot.HasReachedExit) {
                    this.MoveRobot(robot);
                }
            }

            if (isLastThread) {
                Reached(this, new RobotMoveArgs(0, 0, 0));
            }
        }

        

        private void MoveRobot(Robot robot) {
            lock (robot) {
                robot.Move();
                Moved(this, new RobotMoveArgs(robot.Id, robot.CurrentPosition.X, robot.CurrentPosition.Y));
            }
        }
    }
}
