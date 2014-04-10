using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;


using Microsoft.AspNet.SignalR;

using Owin;
using Microsoft.Owin;

[assembly: OwinStartup(typeof(AsyncRobot.Web.Api.AsyncRobotHub))]
namespace AsyncRobot.Web.Api
{
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

        public void AddRobot()
        {
            this.RobotList.Add(new Core.Robot(this.Land)
            {
                Direction = 'N'
            });
        }

        public void RequestMazeCreation()
        {
            var land = new Core.Land();
            this.Land = land;
            var serializer = new JavaScriptSerializer();

            var MazePositions = land.Read();
            var maze = serializer.Serialize(land.Read());

            Clients.All.mountMaze(maze.ToString());
        }
    }
}