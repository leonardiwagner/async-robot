using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncRobot.Core.Runners {
    public class BasicRunner : AbstractRunner
    {
        public BasicRunner(IEnumerable<Robot> robots) {
            this.Robots = robots;
        }

        public void Run() {
            foreach (var robot in Robots) {
                while (!robot.HasReachedExit) {
                    base.MoveRobot(Thread.CurrentThread.ManagedThreadId, robot);
                }
            }

            base.OnReached();
        }


    }
}
