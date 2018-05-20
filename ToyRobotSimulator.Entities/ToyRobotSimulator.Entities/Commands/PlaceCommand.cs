using System;
using ToyRobotSimulator.Exceptions;

namespace ToyRobotSimulator.Entities.Commands
{
    public class PlaceCommand : ICommand
    {
        private readonly SimulatorMap map;
        private readonly Tuple<int, int> potentialPosition;
        private readonly FacesEnum potentialFace;

        public PlaceCommand(SimulatorMap map, string args)
        {
            if (String.IsNullOrEmpty(args)) { throw new InvalidArgumentsException("Arguments for this Place command were invalid"); }

            string[] argumentsSplitted = args.Split(',');
            if (argumentsSplitted.Length != 3)
            {
                throw new InvalidArgumentsException("Arguments for this Place command were invalid");
            }

            this.map = map;
            this.potentialPosition = Tuple.Create(int.Parse(argumentsSplitted[0]), int.Parse(argumentsSplitted[1]));
            this.potentialFace = (FacesEnum)Enum.Parse(typeof(FacesEnum), argumentsSplitted[2]);
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