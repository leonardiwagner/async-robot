using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace AsyncRobot.Core
{
    public class InvalidDirectionException : Exception
    {
        public readonly char Direction;
        public InvalidDirectionException(char direction)
        {
            Direction = direction;
        }
    }
}
