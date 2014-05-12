using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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
        private async Task<LandPosition> SeeLandAsync(char direction)
        {
            await Task.Yield();

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
        public async Task Move()
        {
            await Task.Yield();

            int lastThereBefore = -1;
            char shouldGo = ' ';
            foreach (char direction in Compass)
            {
                LandPosition seeLand = await SeeLandAsync(direction);

                if (seeLand.Value == default(char))
                {
                    HasReachedExit = true;
                    Reached(this, new RobotMoveArgs(this.Id, this.CurrentPosition.X, this.CurrentPosition.Y));
                }
                else if (seeLand.Value == ' ')
                {
                    var wasThereBeforeCount = this.Breadcrumb
                     .Where(horizontal => horizontal.X == seeLand.X)
                     .Where(vertical => vertical.Y == seeLand.Y).ToList().Count();

                    if (lastThereBefore == -1 || wasThereBeforeCount < lastThereBefore)
                    {
                        lastThereBefore = wasThereBeforeCount;
                        shouldGo = direction;
                    }
                }
            }

            char moveTo = shouldGo;


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
