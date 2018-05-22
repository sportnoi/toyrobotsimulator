using System;
using ToyRobotSimulator.Exceptions;

namespace ToyRobotSimulator.Entities.Commands
{
    public class MoveCommand : ICommand
    {
        private const int unitsToMove = 1;
        private readonly SimulatorMap map;

        public MoveCommand(SimulatorMap map,string args)
        {
            this.map = map;
        }

        void ICommand.Execute(ToyRobot toyRobot)
        {
            Tuple<int, int> newPotentialPosition = GetPotentialPosition(toyRobot);
            if (!this.map.IsPotentialPositionInOfBounds(newPotentialPosition))
            {
                throw new InvalidMoveCommandException();
            }

            toyRobot.UpdatePosition(newPotentialPosition);
        }

        private Tuple<int, int> GetPotentialPosition(ToyRobot toyRobot)
        {
            Tuple<int, int, FacesEnum> currentPosition = toyRobot.GetCurrentPosition();
            switch (currentPosition.Item3)
            {
                case FacesEnum.NORTH:
                    return Tuple.Create(currentPosition.Item1, currentPosition.Item2 + unitsToMove);
                case FacesEnum.SOUTH:
                    return Tuple.Create(currentPosition.Item1, currentPosition.Item2 - unitsToMove);
                case FacesEnum.WEST:
                    return Tuple.Create(currentPosition.Item1 - unitsToMove, currentPosition.Item2);
                case FacesEnum.EAST:
                    return Tuple.Create(currentPosition.Item1 + unitsToMove, currentPosition.Item2);
                default:
                    throw new InvalidToyRobotFaceException();
            }
        }
    }
}
