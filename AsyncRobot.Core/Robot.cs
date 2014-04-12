using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncRobot.Core
{
    public enum RobotMovement
    {
        Right,
        Left
    }

    public class MyEventArgs : EventArgs
    {
        public int Id { get; private set; }
        public int X { get; private set; }
        public int Y { get; private set; }
        public MyEventArgs(int id, int x, int y)
        {
            this.Id = id;
            this.X = x;
            this.Y = y;
        }
    }

    public class Robot
    {
        public event EventHandler<MyEventArgs> Moved;
        protected virtual async Task OnMove(int id, int x, int y)
        {
            EventHandler<MyEventArgs> handler = Moved;
            if (handler != null)
            {
                handler(this, new MyEventArgs(id, x, y));
            }
        }

        private LandPosition CurrentPosition;
        private List<LandPosition> breadcrumb = new List<LandPosition>(); 
        private Land land;
        private int id;
        
        private char[] compass = new char[]{'W','N','E','S'};
        public char Direction { get; set; }

        public Robot(Land land, int id, int positionX, int positionY)
        {
            this.id = id;
            this.land = land;
            CurrentPosition = new LandPosition(positionX ,positionY,' ');
            Direction = 'N';
        }

        public LandPosition SeeLand(char direction)
        {
            int seeX = CurrentPosition.x;
            int seeY = CurrentPosition.y;
            
            if (direction == 'W') --seeX;
            if (direction == 'E') ++seeX;
            if (direction == 'N') --seeY;
            if (direction == 'S') ++seeY;

            return new LandPosition(seeX, seeY, this.land.Point(seeX, seeY));
        }

        public async Task ExploreLand()
        {
            for (int i = 0; i < 100; i++)
            {
               await Task.Run(() => Move());
            }
        }
        public void Move()
        {
            List<char> avaiableMoves = new List<char>();

            foreach (char direction in compass)
            {
                if (SeeLand(direction).value == ' ')
                {
                    avaiableMoves.Add(direction);
                }
            }

            char moveTo = ' ';
            for(int i = 0; i <avaiableMoves.Count();i++)
            {
                var seeLand = SeeLand(avaiableMoves[i]);
                List<LandPosition> wasThere = this.breadcrumb
                    .Where(horizontal => horizontal.x == seeLand.x)
                    .Where(vertical => vertical.y == seeLand.y).ToList();

                if (wasThere == null || wasThere.Count == 0)
                {
                    moveTo = avaiableMoves[i];
                    break;
                }

                if (i == avaiableMoves.Count() - 1)
                {
                    moveTo = avaiableMoves[i];
                    break;
                }
            }

            int moveX = CurrentPosition.x;
            int moveY = CurrentPosition.y;
            
            if (moveTo == 'W') --moveX;
            if (moveTo == 'E') ++moveX;
            if (moveTo == 'N') --moveY;
            if (moveTo == 'S') ++moveY;

            CurrentPosition = new LandPosition(moveX, moveY, 'R');
            breadcrumb.Add(CurrentPosition);

            int a = 0;
            for (int i = 0; i < 400000; i ++)
            {
                a++;
            }

            Moved(this, new MyEventArgs(this.id, CurrentPosition.x, CurrentPosition.y));
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
