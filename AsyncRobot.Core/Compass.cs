using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncRobot.Core
{
    public class Compass
    {
        public readonly char[] Directions = new char[] { 'W', 'N', 'E', 'S' };

        public char GetNextDirection(char currentDirection)
        {
            int currentDirectionInCompass = Array.IndexOf(Directions, currentDirection);
            if (currentDirectionInCompass == -1) throw new InvalidDirectionException(currentDirection);

            int nextDirection = currentDirectionInCompass + 1;
            if (nextDirection >= Directions.Length)
            {
                return Directions[0];
            }
            else
            {
                return Directions[nextDirection];
            }
        }
    }
}
