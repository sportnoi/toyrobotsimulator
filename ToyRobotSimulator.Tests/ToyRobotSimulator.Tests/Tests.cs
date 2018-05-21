using NUnit.Framework;
using System;
using ToyRobotSimulator.Entities;
using ToyRobotSimulator.Entities.Commands;
using ToyRobotSimulator.Exceptions;

namespace ToyRobotSimulator.Tests
{
    [TestFixture]
    [Category("ToyRobotSimulator Tests")]
    public class Tests
    {
        ToyRobot toyRobot;
        [SetUp]
        public void Setup()
        {
            toyRobot = new ToyRobot();
        }

        [Test]
        public void PlaceRobotInMap()
        {
            /*
             * Should test each of the cases where a robot can be placed in a map
             * 1. Test initial state
             * 2. Out of bounds
             * 3. Between (0,0) and (4,4)
             */
            
            //1
            Assert.Throws<ToyRobotHasNotBeenPlacedException>(() => toyRobot.GetCurrentPosition());
            Assert.IsFalse(toyRobot.HasBeenPlaced());

            //2
            var args = "-1,0,WEST";
            PlaceCommand placeCommand = new PlaceCommand(args);
            Assert.Throws<InvalidPlaceCommandException>(() => toyRobot.Execute(placeCommand));

            //2.1
            args = "2,5,WEST";
            placeCommand = new PlaceCommand(args);
            Assert.Throws<InvalidPlaceCommandException>(() => toyRobot.Execute(placeCommand));

            //3
            args = "0,0,NORTH";
            placeCommand = new PlaceCommand(args);
            toyRobot.Execute(placeCommand);

            //3.1
            args = "3,4,NORTH";
            placeCommand = new PlaceCommand(args);
            toyRobot.Execute(placeCommand);

        }

        [Test]
        public void MoveRobot()
        {
            /*
             * Should move the Robot within the Map.
             * The robot should not fall -> Ignore the movemevent.
             * 1. Test initial state
             * 2. Place the robot in places where moving the robot should be ignored
             * 3. Normal use cases
             * 4. Nested move commands till fall
             */

            //1
            Assert.Throws<ToyRobotHasNotBeenPlacedException>(() => toyRobot.GetCurrentPosition());
            Assert.IsFalse(toyRobot.HasBeenPlaced());

            //2.0: Starting in south-west corner
            var args = "0,0,WEST";
            PlaceCommand placeCommand = new PlaceCommand(args);
            toyRobot.Execute(placeCommand);
            MoveCommand moveCommand = new MoveCommand(string.Empty);
            Assert.Throws<InvalidMoveCommandException>(() => toyRobot.Execute(moveCommand));

            //2.0 South-west corner
            args = "0,0,SOUTH";
            placeCommand = new PlaceCommand(args);
            toyRobot.Execute(placeCommand);
            Assert.Throws<InvalidMoveCommandException>(() => toyRobot.Execute(moveCommand));

            //2.1 South-east corner
            args = "4,0,EAST";
            placeCommand = new PlaceCommand(args);
            toyRobot.Execute(placeCommand);
            Assert.Throws<InvalidMoveCommandException>(() => toyRobot.Execute(moveCommand));

            //2.1 South-east corner
            args = "4,0,SOUTH";
            placeCommand = new PlaceCommand(args);
            toyRobot.Execute(placeCommand);
            Assert.Throws<InvalidMoveCommandException>(() => toyRobot.Execute(moveCommand));

            //2.2 North-east corner
            args = "4,4,EAST";
            placeCommand = new PlaceCommand(args);
            toyRobot.Execute(placeCommand);
            Assert.Throws<InvalidMoveCommandException>(() => toyRobot.Execute(moveCommand));

            //2.2 North-east corner
            args = "4,4,NORTH";
            placeCommand = new PlaceCommand(args);
            toyRobot.Execute(placeCommand);
            Assert.Throws<InvalidMoveCommandException>(() => toyRobot.Execute(moveCommand));

            //2.3 North-west corner
            args = "0,4,WEST";
            placeCommand = new PlaceCommand(args);
            toyRobot.Execute(placeCommand);
            Assert.Throws<InvalidMoveCommandException>(() => toyRobot.Execute(moveCommand));

            //2.3 North-west corner
            args = "0,4,NORTH";
            placeCommand = new PlaceCommand(args);
            toyRobot.Execute(placeCommand);
            Assert.Throws<InvalidMoveCommandException>(() => toyRobot.Execute(moveCommand));

            //3
            args = "0,0,NORTH";
            placeCommand = new PlaceCommand(args);
            toyRobot.Execute(placeCommand);
            toyRobot.Execute(moveCommand);

            //3.1
            args = "2,2,EAST";
            placeCommand = new PlaceCommand(args);
            toyRobot.Execute(placeCommand);
            toyRobot.Execute(moveCommand);

            //4
            args = "3,0,NORTH";
            placeCommand = new PlaceCommand(args);
            toyRobot.Execute(placeCommand);
            toyRobot.Execute(moveCommand);
            toyRobot.Execute(moveCommand);
            toyRobot.Execute(moveCommand);
            toyRobot.Execute(moveCommand);
            Assert.Throws<InvalidMoveCommandException>(() => toyRobot.Execute(moveCommand));
        }

        [Test]
        public void RotateRobotToLeft()
        {
            /*
             * Should rotate the Robot without moving it
             * 1. Test initial state
             * 2. Place the robot
             * 3. 360 degrees rotation
             */

            //1
            Assert.Throws<ToyRobotHasNotBeenPlacedException>(() => toyRobot.GetCurrentPosition());
            Assert.IsFalse(toyRobot.HasBeenPlaced());

            //2
            var args = "0,0,WEST";
            PlaceCommand placeCommand = new PlaceCommand(args);
            toyRobot.Execute(placeCommand);

            //3
            LeftCommand leftCommand = new LeftCommand(string.Empty);
            toyRobot.Execute(leftCommand);
            Assert.IsTrue(toyRobot.GetCurrentPosition().Item3 == FacesEnum.SOUTH);
            toyRobot.Execute(leftCommand);
            Assert.IsTrue(toyRobot.GetCurrentPosition().Item3 == FacesEnum.EAST);
            toyRobot.Execute(leftCommand);
            Assert.IsTrue(toyRobot.GetCurrentPosition().Item3 == FacesEnum.NORTH);
            toyRobot.Execute(leftCommand);
            Assert.IsTrue(toyRobot.GetCurrentPosition().Item3 == FacesEnum.WEST);

        }

        [Test]
        public void RotateRobotToRight()
        {
            /*
             * Should rotate the Robot without moving it
             * 1. Test initial state
             * 2. Place the robot
             * 3. 360 degrees rotation
             */

            //1
            Assert.Throws<ToyRobotHasNotBeenPlacedException>(() => toyRobot.GetCurrentPosition());
            Assert.IsFalse(toyRobot.HasBeenPlaced());

            //2
            var args = "0,0,WEST";
            PlaceCommand placeCommand = new PlaceCommand(args);
            toyRobot.Execute(placeCommand);

            //3
            RightCommand rightCommand = new RightCommand(string.Empty);
            toyRobot.Execute(rightCommand);
            Assert.IsTrue(toyRobot.GetCurrentPosition().Item3 == FacesEnum.NORTH);
            toyRobot.Execute(rightCommand);
            Assert.IsTrue(toyRobot.GetCurrentPosition().Item3 == FacesEnum.EAST);
            toyRobot.Execute(rightCommand);
            Assert.IsTrue(toyRobot.GetCurrentPosition().Item3 == FacesEnum.SOUTH);
            toyRobot.Execute(rightCommand);
            Assert.IsTrue(toyRobot.GetCurrentPosition().Item3 == FacesEnum.WEST);

        }
    }
}
