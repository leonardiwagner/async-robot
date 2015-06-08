using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncRobot.Core.Runners {
    /*
    public class ThreadRunner : AbstractRunner
    {
        private readonly int ThreadCount;

        public ThreadRunner(IEnumerable<Robot> robots, int threadCount) {
            base.Robots = robots;
            this.ThreadCount = threadCount;
        }

        public void Run()
        {
            var threads = new List<Thread>();
            var robotsPerThreads = Robots.Count() / ThreadCount;
            for (int i = 0; i < this.ThreadCount; i++)
            {
                var robotsPerThreadsList = Robots.Skip(i * robotsPerThreads).Take(robotsPerThreads).ToList();
                threads.Add(new Thread(() => Run(robotsPerThreadsList)));
            }

            foreach (var thread in threads) {
                thread.Start();
            }

            foreach (var thread in threads)
            {
                thread.Join();
            }

            base.OnReached();
        }

        private void Run(List<Robot> robots) {
            foreach (var robot in robots)
            {
                while (!robot.HasReachedExit)
                {
                    base.MoveRobot(Thread.CurrentThread.ManagedThreadId, robot);
                }
            }
        }

        


    }*/
}
