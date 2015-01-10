using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncRobot.Core {
    public class Robot {
        private Land Land;
        public int Id { get; private set; }
        public LandPosition CurrentPosition { get; private set; }
        public ConcurrentBag<LandPosition> Breadcrumb { get; private set; }

        public bool HasReachedExit { get; private set; }
        private char[] Compass;
        public char Direction { get; set; }

        public event EventHandler<RobotMoveArgs> Moved;
        public event EventHandler<RobotMoveArgs> Reached;

        public Robot(Land land, int id, int startPositionX, int startPositionY)
        {
            this.Id = id;
            this.Land = land;
            this.CurrentPosition = new LandPosition(startPositionX, startPositionY);
            this.Breadcrumb = new ConcurrentBag<LandPosition> { };
            AddBreadcrumb();
            
            this.Compass = new char[]{ 'W', 'N', 'E', 'S'};
            this.Direction = 'N';

            
        }

        public void Move() {
            char directionToMove = this.FindTheBestDirectionToMove();
            if (directionToMove != default(char))
            {
                
            
           
                if (directionToMove == 'W') this.CurrentPosition.X--;
                if (directionToMove == 'E') this.CurrentPosition.X++;
                if (directionToMove == 'N') this.CurrentPosition.Y--;
                if (directionToMove == 'S') this.CurrentPosition.Y++;

                AddBreadcrumb();
            }

            /*
            if (this.Moved != null) {
                Moved(this, new RobotMoveArgs(this.Id, CurrentPosition.X, CurrentPosition.Y));
            }*/
        }

        public char FindTheBestDirectionToMove()
        {
            int fewerVisitsInAPosition  = -1;
            char shouldGoInThatDirection = default(char);

            foreach (char direction in Compass)
            {
                var whatRobotSeeInThatDirection = SeeLandPosition(direction);

                if (whatRobotSeeInThatDirection.PositionType == LandPositionType.OUT_OF_LIMITS)
                {
                    HasReachedExit = true;
                    return default(char);
                    Reached(this, new RobotMoveArgs(this.Id, this.CurrentPosition.X, this.CurrentPosition.Y));
                }
                else if (whatRobotSeeInThatDirection.PositionType == LandPositionType.SPACE)
                {

                    lock (this.Breadcrumb) {


                        var howManyTimesRobotWasHere = this.Breadcrumb.Count(position => position.X == whatRobotSeeInThatDirection.X && position.Y == whatRobotSeeInThatDirection.Y);

                        if (fewerVisitsInAPosition == -1 || howManyTimesRobotWasHere < fewerVisitsInAPosition) {
                            fewerVisitsInAPosition = howManyTimesRobotWasHere;
                            shouldGoInThatDirection = direction;
                        }
                    }
                    

                }
            }
            return shouldGoInThatDirection;
        }

        private LandPosition SeeLandPosition(char direction)
        {
            int seeX = CurrentPosition.X;
            int seeY = CurrentPosition.Y;
            
            if (direction == 'W') --seeX;
            if (direction == 'E') ++seeX;
            if (direction == 'N') --seeY;
            if (direction == 'S') ++seeY;

            return new LandPosition(seeX, seeY, this.Land.GetPositionType(seeX, seeY));
        }

        private void AddBreadcrumb() {
                this.Breadcrumb.Add(new LandPosition(CurrentPosition.X, CurrentPosition.Y));
                
        }

        public async Task ExploreLandAsync()
        {
            do
            {
                await Task.Run(() => Move());
            } while (!HasReachedExit);
        }

    }
}
