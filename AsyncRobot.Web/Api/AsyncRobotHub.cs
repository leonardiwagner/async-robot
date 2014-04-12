using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Script.Serialization;
using System.Web.WebPages.Instrumentation;
using AsyncRobot.Core;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;
using Owin;
using Microsoft.Owin;

[assembly: OwinStartup(typeof(AsyncRobot.Web.Api.AsyncRobotHub))]
namespace AsyncRobot.Web.Api
{
    public class LandJson
    {
        public int width { get; set; }
        public int height { get; set; }
        public List<Position> track { get; set; }
    }

    public class Position
    {
        public int id { get; set; }
        public int x { get; set; }
        public int y { get; set; }
    }

    public class AsyncRobotHub : Hub
    {
        private Core.Land Land;
        private List<Core.Robot> RobotList = new List<Core.Robot>();

        public void Configuration(IAppBuilder app)
        {
            // Any connection or hub wire up and configuration should go here
            app.MapSignalR();
        }

        public void Hello()
        {
            Clients.All.hello();
        }

        public void RunRobot(string landJson, string robotJson)
        {
            var landClient = JsonConvert.DeserializeObject<LandJson>(landJson);
            var robotClient = JsonConvert.DeserializeObject<List<Position>>(robotJson);

            Core.Land land = new Land(landClient.width, landClient.height);
            foreach(var landPoint in landClient.track)
                land.AddTrackPoint(landPoint.x, landPoint.y);
            
            List<Core.Robot> robotList = new List<Robot>();
            foreach (var robot in robotClient)
            {
                var r = new Robot(land, robot.id, robot.x, robot.y);
                r.Moved += r_Moved;
                r.ExploreLand();
            }

        }

        void r_Moved(object sender, MyEventArgs e)
        {
            Clients.All.setRobot(e.Id,e.X, e.Y);
        }

       
  

    }
}