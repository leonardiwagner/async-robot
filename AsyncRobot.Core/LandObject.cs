using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncRobot.Core
{
    public class LandObject
    {
        private readonly char name;

        public static readonly LandObject WALL = new LandObject ('#');
        public static readonly LandObject SPACE = new LandObject (' ');
        public static readonly LandObject ENDPOINT = new LandObject ('E');

        private LandObject(char name)
        {
            this.name = name;
        }

        public char ToChar()
        {
            return this.name;
        }
    }
}
