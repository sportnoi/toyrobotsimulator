using System;
using ToyRobotSimulator.Exceptions;

namespace ToyRobotSimulator.Entities.Commands
{
    public class PlaceCommand : ICommand
    {
        private readonly SimulatorMap map;
        private readonly Tuple<int, int> potentialPosition;
        private readonly FacesEnum potentialFace;

        public PlaceCommand(SimulatorMap map, Tuple<int, int> potentialPosition, FacesEnum potentialFace)
        {
            this.map = map;
            this.potentialFace = potentialFace;
            this.potentialPosition = potentialPosition;
        }

        void ICommand.Execute(ToyRobot toyRobot)
        {
            if (!map.IsPotentialPositionInOfBounds(this.potentialPosition))
            {
                throw new InvalidPlaceCommandException("Invalid arguments. New position should be between the limits of the tablemap.");
            }

            toyRobot.UpdatePosition(this.potentialPosition);
            toyRobot.UpdateFace(this.potentialFace);
        }
    }
}