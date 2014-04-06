using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace AsyncRobot.Core
{
    public enum RobotMovement
    {
        Right,
        Left
    }
    public class Robot
    {
        private MapPosition CurrentPosition;
        private List<MapPosition> breadcrumb = new List<MapPosition>(); 
        private Land land;
        
        private char[] compass = new char[]{'W','N','E','S'};
        public char Direction { get; set; }

        public Robot(Land land)
        {
            this.land = land;
            CurrentPosition = land.RobotStartPosition;
            Direction = 'N';
        }

        public char SeeLand(char direction)
        {
            int seeX = CurrentPosition.x;
            int seeY = CurrentPosition.y;
            
            if (direction == 'W') ++seeX;
            if (direction == 'E') --seeX;
            if (direction == 'N') ++seeY;
            if (direction == 'S') --seeY;

            return this.land.Point(seeX, seeY);
        }

        public void Move()
        {
            List<char> avaiableMoves = new List<char>();

            foreach (char direction in compass)
            {
                if (SeeLand(direction) == ' ')
                {
                    avaiableMoves.Add(direction);
                }
            }

            char moveTo = ' ';
            foreach (char availableMove in avaiableMoves)
            {
                moveTo = availableMove;
                break;
            }

            int moveX = CurrentPosition.x;
            int moveY = CurrentPosition.y;
            
            if (moveTo == 'W') ++moveX;
            if (moveTo == 'E') --moveX;
            if (moveTo == 'N') ++moveY;
            if (moveTo == 'S') --moveY;

            CurrentPosition = new MapPosition(moveX, moveY, 'R');
        }

        public void TurnRight() { Turn(RobotMovement.Right);}
        public void TurnLeft() { Turn(RobotMovement.Left); }

        private void Turn(RobotMovement movement)
        {
            int currentDirectionPosition = Array.IndexOf(compass, Direction);
            int changeDirectionPosition = 0;

            if (movement == RobotMovement.Right) changeDirectionPosition = currentDirectionPosition + 1;
            if (movement == RobotMovement.Left) changeDirectionPosition = currentDirectionPosition - 1;

            //rotate compass
            if (changeDirectionPosition == compass.Length) changeDirectionPosition = 0;
            if (changeDirectionPosition < 0) changeDirectionPosition = compass.Length - 1;

            Direction = compass[changeDirectionPosition];
        }

        

        public void x()
        {
            
        }

        private bool CanGoUp()
        {
            if (land.Point(CurrentPosition.x + 1, CurrentPosition.y) == ' ')
                return true;
            else
                return false;
        }

        private bool CanGoDown()
        {
            if (land.Point(CurrentPosition.x - 1, CurrentPosition.y) == ' ')
                return true;
            else
                return false;
        }

        private bool CanGoLeft()
        {
            if (land.Point(CurrentPosition.x, CurrentPosition.y - 1) == ' ')
                return true;
            else
                return false;
        }

        private bool CanGoRight()
        {
            if (land.Point(CurrentPosition.x, CurrentPosition.y + 1) == ' ')
                return true;
            else
                return false;
        }

    }
}
