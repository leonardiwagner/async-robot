using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using AsyncRobot.Core;
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



        public async Task RunRobots(int robotCount)
        {
            var land = new Core.Land();
            var robotList = new List<Core.Robot>();
            for (int i = 0; i < robotCount; i++)
            {
                var robot = new Robot(land, i);
                robot.Moved += robot_Moved;
                robotList.Add(robot);
                
            }

            foreach(var robot in robotList)
                robot.MoveX();
        }

        void robot_Moved(object sender, MyEventArgs e)
        {
            Clients.All.printTask(e.Log);
        }
       

        public void TaskMock()
        {
            for(int i =0;i<10;i++)
            { 
                Thread.Sleep(2000);
                PrintTask("ae " + i);
            }
        }

        public void PrintTask(string taskString)
        {
            Clients.All.printTask(taskString);
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