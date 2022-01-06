namespace Robots.Tests
{
    using NUnit.Framework;
    using System;

    public class RobotsTests
    {
        [Test]
        public void ConstructorShouldWorkProperly()
        {
            RobotManager robotManager = new RobotManager(100);

            Assert.AreEqual(100, robotManager.Capacity);
        }

        [Test]
        public void CapacityShouldThrowExceptionWhenInvalid()
        {
            Assert.Throws<ArgumentException>(() => new RobotManager(-100));
        }

        [Test]
        public void RobotCountShouldCountProperly()
        {
            RobotManager robotManager = new RobotManager(100);
            Robot robot = new Robot("Gosho", 100);
            robotManager.Add(robot);

            Assert.AreEqual(1, robotManager.Count);
        }

        [Test]
        public void RobotManagerAddMethodShouldThrowInvalidOperationExceptionWhenRobotNamesDuplicate()
        {
            Robot robot = new Robot("Gosho", 100);
            RobotManager robotManager = new RobotManager(10);
            robotManager.Add(robot);

            Assert.Throws<InvalidOperationException>(() => robotManager.Add(robot));
        }

        [Test]
        public void RobotManagerShouldThrowExceptionForInvalidCapacity()
        {
            Robot robot = new Robot("Gosho", 100);
            RobotManager robotManager = new RobotManager(1);

            robotManager.Add(robot);
            Assert.Throws<InvalidOperationException>(() => robotManager.Add(robot));
        }

        [Test]
        public void RobotManagerRemoveMethodShouldWorkProperly()
        {
            Robot robot = new Robot("Pesho", 100);
            RobotManager robotManager = new RobotManager(10);
            robotManager.Add(robot);
            robotManager.Remove("Pesho");

            Assert.AreEqual(0, robotManager.Count);
        }

        [Test]
        public void RobotManagerRemoveMethodShouldThrowExceptionForInvalidRobotName()
        {
            RobotManager robotManager = new RobotManager(2);

            Assert.Throws<InvalidOperationException>(() => robotManager.Remove("Gosho"));
        }

        [Test]
        public void WorkShouldDecreaseRobotBattery()
        {
            Robot robot = new Robot("Ivan", 100);
            RobotManager robotManager = new RobotManager(2);

            robotManager.Add(robot);
            robotManager.Work("Ivan", "Cleaning", 20);

            Assert.AreEqual(80, robot.Battery);
        }

        [Test]
        public void WorkShouldThrowExceptionForInvalidRobot()
        {
            RobotManager robotManager = new RobotManager(2);

            Assert.Throws<InvalidOperationException>(() => robotManager.Work("Ivan", "Cleaning", 20));
        }

        [Test]
        public void WorkShouldThrowExceptionWhenBatteryIsNotEnough()
        {
            Robot robot = new Robot("Ivan", 10);
            RobotManager robotManager = new RobotManager(2);

            robotManager.Add(robot);

            Assert.Throws<InvalidOperationException>(() => robotManager.Work("Ivan", "Cleaning", 20));
        }

        [Test]
        public void ChargeShouldThrowExceptionForInvalidRobot()
        {
            RobotManager robotManager = new RobotManager(1);

            Assert.Throws<InvalidOperationException>(() => robotManager.Charge("Invalid"));
        }

        [Test]
        public void ChargeShouldSetToMaximumBattery()
        {
            Robot robot = new Robot("Pesho", 80);
            RobotManager robotManager = new RobotManager(1);

            robotManager.Add(robot);
            robotManager.Work("Pesho", "clean", 20);

            robotManager.Charge("Pesho");

            Assert.AreEqual(80, robot.Battery);

        }
    }
}
