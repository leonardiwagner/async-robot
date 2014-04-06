using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncApplication
{
    public class Robot
    {
        private MapPosition CurrentPosition;
        public Land Land;

        public Robot(Land land)
        {
            this.Land = land;
            CurrentPosition = land.RobotStartPosition;
        }

        public void x()
        {
            
        }

        private bool CanGoUp()
        {
            if (Land.Point(CurrentPosition.x + 1, CurrentPosition.y) == ' ')
                return true;
            else
                return false;
        }

        private bool CanGoDown()
        {
            if (Land.Point(CurrentPosition.x - 1, CurrentPosition.y) == ' ')
                return true;
            else
                return false;
        }

        private bool CanGoLeft()
        {
            if (Land.Point(CurrentPosition.x , CurrentPosition.y - 1) == ' ')
                return true;
            else
                return false;
        }

        private bool CanGoRight()
        {
            if (Land.Point(CurrentPosition.x , CurrentPosition.y + 1) == ' ')
                return true;
            else
                return false;
        }

    }
}
