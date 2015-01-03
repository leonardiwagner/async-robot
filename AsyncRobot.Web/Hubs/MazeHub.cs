using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using AsyncRobot.Core;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Land = AsyncRobot.Web.Models.Land;

namespace AsyncRobot.Web.Hubs {
    public class MazeHub : Hub {
        private Stopwatch Stopwatch = new Stopwatch();

        public void Run(string json) {
            var robots = readRobotsFromJson(json);
            var runner = new AsyncRobot.Core.Runners.BasicRunner(robots);
            runner.Moved += runner_Moved;
            runner.Reached += runner_Reached;

            Stopwatch.Start();
            runner.Run();
        }

        public void RunThread(string json, int threadCount) {
            var robots = readRobotsFromJson(json);
            var runner = new AsyncRobot.Core.Runners.ThreadRunner(robots, threadCount);
            runner.Moved += runner_Moved;
            runner.Reached += runner_Reached;

            Stopwatch.Start();
            runner.Run();
        }

        public async Task RunAsync(string json) {
            var robots = readRobotsFromJson(json);
            var runner = new AsyncRobot.Core.Runners.AsyncRunner(robots);
            runner.Moved += runner_Moved;
            runner.Reached += runner_Reached;

            Stopwatch.Start();
            await runner.Run();
        }

        void runner_Reached(object sender, RobotMoveArgs e) {
            Stopwatch.Stop();
            Clients.All.reached(Stopwatch.ElapsedMilliseconds);
        }

        void runner_Moved(object sender, RobotMoveArgs e) {
            Clients.All.setRobotPosition(e.RobotId, e.X, e.Y);
        }

        private IEnumerable<Robot> readRobotsFromJson(string json) {
            var landClient = new JavaScriptSerializer().Deserialize<Land>(json);
            var land = new AsyncRobot.Core.Land(landClient.width, landClient.height);
            foreach (var track in landClient.track) {
                land.SetPositionType(track.x, track.y, LandPositionType.SPACE);
            }

            var robots = new List<AsyncRobot.Core.Robot>();
            for (int i = 1; i <= 50; i++) {
                robots.Add(new Robot(land, i, 1, 18));
            }

            return robots;
        }

        
    }
}