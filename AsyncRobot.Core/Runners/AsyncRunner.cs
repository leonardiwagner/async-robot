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

        public async Task Run()
        {
            await RunRobotsAsync();

            await Task.Run(() => Reached(this, new RobotMoveArgs(0, 0, 0)));
        }

        public async Task RunRobotsAsync()
        {
            var tasks = new List<Task>();
            
            foreach (var robotR in Robots)
            {
                var robot = robotR;

                tasks.Add(new Task(() => {
                    while (!robot.HasReachedExit)
                    {
                        robot.Move();
                        if (robot.CurrentPosition.X == 18 || robot.CurrentPosition.X == 19)
                        {
                            Moved(this,
                                new RobotMoveArgs(robot.Id, robot.CurrentPosition.X, robot.CurrentPosition.Y));
                        }
                    }
                }));
            }

            tasks.ForEach(x => x.Start());
            await Task.WhenAll(tasks);
        }

    }
}
