using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncRobot.Core {
    public class Robot {
        public readonly int Id;
        public event EventHandler<RobotMoveArgs> Moved;
        public event EventHandler<RobotMoveArgs> ReachedExit;
        
        private readonly Compass compass = new Compass();
        private readonly char firstCompassDirection;
        //private readonly char lastCompassDirection;

        public Robot(int id)
        {
            Id = id;
            firstCompassDirection = compass.Directions[0];
            //lastCompassDirection = compass.Directions[compass.Directions.Length - 1];
        }

        public void SearchForLandExit(Land land, LandPosition startPosition)
        {
            SearchForLandExit(land, startPosition, new List<LandPosition>());
        }

        private void SearchForLandExit(Land land, LandPosition startPosition, ICollection<LandPosition> breadcrumb)
        {
            char directionToMove = FindTheBestDirectionToMove(land, startPosition, breadcrumb, firstCompassDirection);
            bool hasFoundExit = directionToMove == default(char);
            if (hasFoundExit)
            {
                //ReachedExit(this, new RobotMoveArgs(0, Id, 0, 0));
                return;
            }
            else
            {
                //do next move to find exit
                LandPosition newPosition = MovePosition(land, startPosition, directionToMove);
                var newBreadcrumb = new List<LandPosition>(breadcrumb) {newPosition};

                Moved(this, new RobotMoveArgs(Thread.CurrentThread.ManagedThreadId, Id, newPosition.X, newPosition.Y));
                SearchForLandExit(land, newPosition, newBreadcrumb);
            }
        }

        private char FindTheBestDirectionToMove(Land land, LandPosition currentPosition, ICollection<LandPosition> breadcrumb, char direction, int fewerVisitsInAPosition = -1, char shouldGoInThatDirection = 'N')
        {
            var whatRobotSeeInThatDirection = MovePosition(land, currentPosition, direction);

            if (whatRobotSeeInThatDirection.PositionType == LandPositionType.OUT_OF_LIMITS)
            {
                return default(char);
            }
            if (whatRobotSeeInThatDirection.PositionType == LandPositionType.SPACE)
            {
                var howManyTimesRobotWasHere = breadcrumb.Count(position => position.X == whatRobotSeeInThatDirection.X &&
                                                                            position.Y == whatRobotSeeInThatDirection.Y);

                if (fewerVisitsInAPosition == -1 || howManyTimesRobotWasHere < fewerVisitsInAPosition)
                {
                    fewerVisitsInAPosition = howManyTimesRobotWasHere;
                    shouldGoInThatDirection = direction;
                }
            }

            var nextDirection = compass.GetNextDirection(direction);
            if (nextDirection == firstCompassDirection)
            {
                //only return the actual direction to move when all directions was seen
                return shouldGoInThatDirection;
                
            }
            else
            {
                return FindTheBestDirectionToMove(land, currentPosition, breadcrumb, nextDirection, fewerVisitsInAPosition, shouldGoInThatDirection);
            }
        }


        private LandPosition MovePosition(Land land, LandPosition currentPosition, char directionToMove)
        {
            LandPosition newPosition;
            if (directionToMove == 'W')
                newPosition = new LandPosition(currentPosition.X - 1, currentPosition.Y);
            else if (directionToMove == 'E')
                newPosition = new LandPosition(currentPosition.X + 1, currentPosition.Y);
            else if (directionToMove == 'N')
                newPosition = new LandPosition(currentPosition.X, currentPosition.Y - 1);
            else if (directionToMove == 'S')
                newPosition = new LandPosition(currentPosition.X, currentPosition.Y + 1);
            else
                throw new InvalidDirectionException(directionToMove);

            return new LandPosition(newPosition.X, newPosition.Y, land.GetPositionType(newPosition));
        }

     

    }
}
