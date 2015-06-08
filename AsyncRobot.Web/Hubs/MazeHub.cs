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

        public void Run(string json)
        {
            var runRequest = new JavaScriptSerializer().Deserialize<RunRequest>(json);
            var land = new AsyncRobot.Core.Land(runRequest.land.width, runRequest.land.height);
            foreach (var track in runRequest.land.track)
            {
                land.SetPositionType(track.x, track.y, LandPositionType.SPACE);
            }

            var runner = new Core.Runners.BasicRunner(land);
            runRequest.robots.ForEach(r => runner.AddRobotToRunner(new Robot(r.id), new LandPosition(r.x,r.y)));
            runner.Run();
            //Stopwatch.Start();
            
        }


  

        
    }
}