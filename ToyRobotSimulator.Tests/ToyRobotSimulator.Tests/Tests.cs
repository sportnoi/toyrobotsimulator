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
        SimulatorMap map;
        [SetUp]
        public void Setup()
        {
            toyRobot = new ToyRobot();
            map = new SimulatorMap();
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
            PlaceCommand placeCommand = new PlaceCommand(map, new Tuple<int, int>(-1, 0), FacesEnum.WEST);
            Assert.Throws<InvalidPlaceCommandException>(() => toyRobot.Execute(placeCommand));

            //2.1
            placeCommand = new PlaceCommand(map, new Tuple<int, int>(2, 5), FacesEnum.WEST);
            Assert.Throws<InvalidPlaceCommandException>(() => toyRobot.Execute(placeCommand));

            //3
            placeCommand = new PlaceCommand(map, new Tuple<int, int>(0, 0), FacesEnum.NORTH);
            toyRobot.Execute(placeCommand);

            //3.1
            placeCommand = new PlaceCommand(map, new Tuple<int, int>(3, 4), FacesEnum.NORTH);
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
            PlaceCommand placeCommand = new PlaceCommand(map, new Tuple<int, int>(0, 0), FacesEnum.WEST);
            toyRobot.Execute(placeCommand);
            MoveCommand moveCommand = new MoveCommand(map);
            Assert.Throws<InvalidMoveCommandException>(() => toyRobot.Execute(moveCommand));

            //2.0 South-west corner
            placeCommand = new PlaceCommand(map, new Tuple<int, int>(0, 0), FacesEnum.SOUTH);
            toyRobot.Execute(placeCommand);
            Assert.Throws<InvalidMoveCommandException>(() => toyRobot.Execute(moveCommand));

            //2.1 South-east corner
            placeCommand = new PlaceCommand(map, new Tuple<int, int>(4, 0), FacesEnum.EAST);
            toyRobot.Execute(placeCommand);
            Assert.Throws<InvalidMoveCommandException>(() => toyRobot.Execute(moveCommand));

            //2.1 South-east corner
            placeCommand = new PlaceCommand(map, new Tuple<int, int>(4, 0), FacesEnum.SOUTH);
            toyRobot.Execute(placeCommand);
            Assert.Throws<InvalidMoveCommandException>(() => toyRobot.Execute(moveCommand));

            //2.2 North-east corner
            placeCommand = new PlaceCommand(map, new Tuple<int, int>(4, 4), FacesEnum.EAST);
            toyRobot.Execute(placeCommand);
            Assert.Throws<InvalidMoveCommandException>(() => toyRobot.Execute(moveCommand));

            //2.2 North-east corner
            placeCommand = new PlaceCommand(map, new Tuple<int, int>(4, 4), FacesEnum.NORTH);
            toyRobot.Execute(placeCommand);
            Assert.Throws<InvalidMoveCommandException>(() => toyRobot.Execute(moveCommand));

            //2.3 North-west corner
            placeCommand = new PlaceCommand(map, new Tuple<int, int>(0, 4), FacesEnum.WEST);
            toyRobot.Execute(placeCommand);
            Assert.Throws<InvalidMoveCommandException>(() => toyRobot.Execute(moveCommand));

            //2.3 North-west corner
            placeCommand = new PlaceCommand(map, new Tuple<int, int>(0, 4), FacesEnum.NORTH);
            toyRobot.Execute(placeCommand);
            Assert.Throws<InvalidMoveCommandException>(() => toyRobot.Execute(moveCommand));

            //3
            placeCommand = new PlaceCommand(map, new Tuple<int, int>(0, 0), FacesEnum.NORTH);
            toyRobot.Execute(placeCommand);
            toyRobot.Execute(moveCommand);

            //3.1
            placeCommand = new PlaceCommand(map, new Tuple<int, int>(2, 2), FacesEnum.EAST);
            toyRobot.Execute(placeCommand);
            toyRobot.Execute(moveCommand);

            //4
            placeCommand = new PlaceCommand(map, new Tuple<int, int>(3,0), FacesEnum.NORTH);
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
            PlaceCommand placeCommand = new PlaceCommand(map, new Tuple<int, int>(0, 0), FacesEnum.WEST);
            toyRobot.Execute(placeCommand);

            //3
            LeftCommand leftCommand = new LeftCommand(map);
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
            PlaceCommand placeCommand = new PlaceCommand(map, new Tuple<int, int>(0, 0), FacesEnum.WEST);
            toyRobot.Execute(placeCommand);

            //3
            RightCommand rightCommand = new RightCommand(map);
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
