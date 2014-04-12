using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncRobot.Core
{
    public class RobotMoveArgs : EventArgs
    {
        public int RobotId { get; private set; }
        public int X { get; private set; }
        public int Y { get; private set; }
        public RobotMoveArgs(int id, int x, int y)
        {
            this.RobotId = id;
            this.X = x;
            this.Y = y;
        }
    }

    public class Robot
    {
        private Land Land;
        private LandPosition CurrentPosition;
        private List<LandPosition> Breadcrumb = new List<LandPosition>(); 
        private int Id;
        private char[] Compass = {'W','N','E','S'};
        private bool HasReachedExit = false;
        public char Direction { get; set; }

        public event EventHandler<RobotMoveArgs> Moved;
        public event EventHandler<RobotMoveArgs> Reached;

        public Robot(Land land, int id, int positionX, int positionY)
        {
            this.Id = id;
            this.Land = land;
            CurrentPosition = new LandPosition(positionX ,positionY);
            Direction = 'N';
        }

        /// <summary>
        /// Look at near land object
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        private LandPosition SeeLand(char direction)
        {
            int seeX = CurrentPosition.X;
            int seeY = CurrentPosition.Y;
            
            if (direction == 'W') --seeX;
            if (direction == 'E') ++seeX;
            if (direction == 'N') --seeY;
            if (direction == 'S') ++seeY;

            return new LandPosition(seeX, seeY, this.Land.GetPosition(seeX, seeY));
        }

        /// <summary>
        /// Make a movement in the land
        /// </summary>
        public void Move()
        {
            //Look where it can goes in the land
            List<char> avaiableMoves = new List<char>();
            foreach (char direction in Compass)
            {
                char seeLand = SeeLand(direction).Value;

                if (seeLand == default(char))
                {
                    HasReachedExit = true;
                    Reached(this, new RobotMoveArgs(this.Id, this.CurrentPosition.X, this.CurrentPosition.Y));
                }
                else if (seeLand == ' ')
                {
                    avaiableMoves.Add(direction);
                }
            }

            //Choose the best position to go
            char moveTo = ' ';
            for (int i = 0; i < avaiableMoves.Count(); i++)
            {
                var seeLand = SeeLand(avaiableMoves[i]);
                List<LandPosition> wasThereBefore = this.Breadcrumb
                    .Where(horizontal => horizontal.X == seeLand.X)
                    .Where(vertical => vertical.Y == seeLand.Y).ToList();

                if (wasThereBefore.Count == 0)
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

            //Set move to chosen position
            int moveX = CurrentPosition.X;
            int moveY = CurrentPosition.Y;

            if (moveTo == 'W') --moveX;
            if (moveTo == 'E') ++moveX;
            if (moveTo == 'N') --moveY;
            if (moveTo == 'S') ++moveY;

            this.CurrentPosition = new LandPosition(moveX, moveY);
            this.Breadcrumb.Add(CurrentPosition);

            Moved(this, new RobotMoveArgs(this.Id, CurrentPosition.X, CurrentPosition.Y));
        }

        /// <summary>
        /// Explore land untill it reaches a exit
        /// </summary>
        /// <returns></returns>
        public async Task ExploreLandAsync()
        {
            do
            {
                await Task.Run(() => Move());
            } while (!HasReachedExit);
        }
    }
}
