using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncRobot.Core.Runners {
    public class AsyncRunner : AbstractRunner
    {
        public AsyncRunner(IEnumerable<Robot> robots) {
            this.Robots = robots;
        }

        public async Task Run()
        {
            await RunRobotsAsync();
            
            await Task.Run(() => base.OnReached());
        }

        private async Task RunRobotsAsync()
        {
            var tasks = new List<Task>();

            foreach (var robotR in Robots)
            {
                var robot = robotR;

                tasks.Add(new Task(() =>
                {
                    while (!robot.HasReachedExit)
                    {
                        robot.Move();
                        if (robot.CurrentPosition.X == 18 || robot.CurrentPosition.X == 19)
                        {
                            base.MoveRobot(Thread.CurrentThread.ManagedThreadId, robot);
                        }
                    }
                }));
            }

            tasks.ForEach(x => x.Start());
            await Task.WhenAll(tasks);
        }
        

    }
}
