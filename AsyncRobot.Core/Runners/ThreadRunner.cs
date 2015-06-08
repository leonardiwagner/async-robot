using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncRobot.Core.Runners {
  
    public class ThreadRunner : AbstractRunner
    {
        public ThreadRunner(Land land) : base(land) { }

        public void Run(int threadCount)
        {
            var threadRobots = new List<Dictionary<Robot, LandPosition>>();
            var robotsClone = new Dictionary<Robot, LandPosition>(base.RobotsToRun);
            var robotsPerThread = base.RobotsToRun.Count / threadCount;
            for (int i = 0; i < robotsPerThread; i++)
            {
                //moves robots to thread robots
                Dictionary<Robot, LandPosition> slicedRobots = robotsClone.Take(robotsPerThread).ToDictionary(x => x.Key, x => x.Value);
                foreach (var robot in slicedRobots.Keys) robotsClone.Remove(robot);
                
                threadRobots.Add(slicedRobots);
            }

            var threads = new List<Thread>();
            foreach (var threadRobot in threadRobots)
            {
                threads.Add(new Thread(() =>
                {
                    foreach (var robot in threadRobot)
                    {
                        robot.Key.Moved += robot_Moved;
                        robot.Key.SearchForLandExit(base.RunnerLand, robot.Value);
                    }
                }));
            }

            foreach (var thread in threads)
            {
                thread.Start();
            }
        }

       
        void robot_Moved(object sender, RobotMoveArgs e)
        {
            base.OnMove(this, e);
        }
        

    }
}
