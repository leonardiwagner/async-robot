using System;
using AsyncRobot.Core;
using NUnit.Framework;


namespace AsyncRobot.Test
{
    public class RobotTest
    {
        private Land land;
        private Robot robot;
        
        [SetUp]
        public void Init()
        {
            land = new Land();
            robot = new Robot(land);
        }
        
        [Test]
        [TestCase('W', 'N')]
        [TestCase('N', 'E')]
        [TestCase('E', 'S')]
        [TestCase('S', 'W')]
        public void TurnRight(char initial, char expected)
        {
            robot.Direction = initial;
            robot.TurnRight();

            Assert.AreEqual(expected, robot.Direction);
        }

        [Test]
        [TestCase('W', 'S')]
        [TestCase('N', 'W')]
        [TestCase('E', 'N')]
        [TestCase('S', 'E')]

        public void TurnLeft(char initial,char expected)
        {
            robot.Direction = initial;
            robot.TurnLeft();

            Assert.AreEqual(expected, robot.Direction);
        }
    }
}
