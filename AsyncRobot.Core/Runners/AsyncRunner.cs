using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncRobot.Core.Runners {
    
    public class AsyncRunner : AbstractRunner
    {
        public AsyncRunner(Land land) : base(land) { }

        public async Task Run()
        {
            var tasks = new List<Task>();

            foreach (var robotToRun in base.RobotsToRun) {
                
                tasks.Add(new Task(() =>
                {
                    robotToRun.Key.Moved += robot_Moved;
                    robotToRun.Key.SearchForLandExit(base.RunnerLand, robotToRun.Value);
                }));

               
            }

            tasks.ForEach(x => x.Start());
            await Task.WhenAll(tasks);
        }

        

        void robot_Moved(object sender, RobotMoveArgs e)
        {
            base.OnMove(this, e);
        }
    }
}
