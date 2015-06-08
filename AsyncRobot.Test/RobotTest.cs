using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AsyncRobot.Core;
using NUnit.Framework;
using Should;

namespace AsyncRobot.Test
{
    [TestFixture]
    public class RobotTest {
        private Land Land;
        private Robot Robot;

        [SetUp]
        public void Setup() {
            /* 012345
             *0######
             *1### ##
             *2#    #
             *3## ###
             *4# X###
             *5# ####
             *6# ####
             */
            Land = new Land(6, 7);
            Land.SetPositionType(1, 3, LandPositionType.SPACE);
            Land.SetPositionType(2, 1, LandPositionType.SPACE);
            Land.SetPositionType(2, 2, LandPositionType.SPACE);
            Land.SetPositionType(2, 3, LandPositionType.SPACE);
            Land.SetPositionType(2, 4, LandPositionType.SPACE);
            Land.SetPositionType(3, 2, LandPositionType.SPACE);
            Land.SetPositionType(4, 2, LandPositionType.SPACE);
            Land.SetPositionType(4, 1, LandPositionType.SPACE);
            Land.SetPositionType(5, 1, LandPositionType.SPACE);
            Land.SetPositionType(6, 1, LandPositionType.SPACE);

            Robot = new Robot(0);
            /*
            while (!Robot.HasReachedExit) {
                Robot.FindExit();
            }*/
        }

        [Test]
        public void RobotShouldMoveAtLeastOnce() {
            //Robot.Breadcrumb.Count.ShouldBeGreaterThan(1);
        }

        [Test]
        public void RobotShouldMoveUntilFindExit() {
            //Robot.Breadcrumb.Count.ShouldEqual(25);
        }

        [Test]
        public void RobotShouldBackWhenDoesntFindExit() {
            /*
            Dictionary<LandPosition, int> visitedSamePositionManyTimes = Robot.Breadcrumb.GroupBy(position => new {position.X, position.Y})
                                                                                         .Where(position => position.Count() > 1)
                                                                                         .ToDictionary(position => new LandPosition(position.Key.X, position.Key.Y), position => position.Count());
            visitedSamePositionManyTimes.Count().ShouldEqual(8);
             */
        }

       

    }
}
