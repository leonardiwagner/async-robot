using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using AsyncRobot.Core;
using AsyncRobot.Web.Models;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Land = AsyncRobot.Web.Models.Land;

namespace AsyncRobot.Web.Hubs {
    public class MazeHub : Hub {
        private Stopwatch Stopwatch = new Stopwatch();

        public async Task Run(string json)
        {
            var runRequest = new JavaScriptSerializer().Deserialize<RunRequest>(json);
            var land = new AsyncRobot.Core.Land(runRequest.land.width, runRequest.land.height);
            foreach (var track in runRequest.land.track)
            {
                land.SetPositionType(track.x, track.y, LandPositionType.SPACE);
            }

            if (runRequest.approach == "sync")
            {
                var runner = new Core.Runners.BasicRunner(land);
                runRequest.robots.ForEach(r => runner.AddRobotToRunner(new Robot(r.id), new LandPosition(r.x, r.y)));
                runner.OnRobotMove += runner_OnRobotMove;
                runner.Run();
            }
            else if (runRequest.approach == "async")
            {
                var runner = new Core.Runners.AsyncRunner(land);
                runRequest.robots.ForEach(r => runner.AddRobotToRunner(new Robot(r.id), new LandPosition(r.x, r.y)));
                runner.OnRobotMove += runner_OnRobotMove;
                await runner.Run();
            }
            else if (runRequest.approach == "multithread")
            {
                var runner = new Core.Runners.ThreadRunner(land);
                runRequest.robots.ForEach(r => runner.AddRobotToRunner(new Robot(r.id), new LandPosition(r.x, r.y)));
                runner.OnRobotMove += runner_OnRobotMove;
                runner.Run(runRequest.threadCount);
            }

            
            Stopwatch.Start();
            
        }

        void runner_OnRobotMove(object sender, RobotMoveArgs e)
        {
            Clients.All.setRobotPosition(e.RobotId, e.X, e.Y, e.ThreadId);
        }


  

        
    }
}