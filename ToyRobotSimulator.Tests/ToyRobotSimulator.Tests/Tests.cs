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
            toyRobot = ToyRobot.Instance;
            map = SimulatorMap.Instance;
        }

        [Test]
        public void PlaceRobotInMap()
        {
            /*
             * Should test each of the cases where a robot can be place in a map
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
    }
}
