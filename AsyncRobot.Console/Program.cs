using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AsyncRobot.Core;

namespace AsyncRobot.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            var land = new Land();
            var robot = new Robot(land);

            Console.WriteLine(land.PrintLand());

            for(int i =0;i< 100;i++)
            {
                robot.Move();
                Console.Clear();
                Console.WriteLine(land.PrintLand());
                Thread.Sleep(100);
            }

            Console.Read();
        }
    }
}
