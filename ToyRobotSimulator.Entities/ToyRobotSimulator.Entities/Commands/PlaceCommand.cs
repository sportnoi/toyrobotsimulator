using System;
using ToyRobotSimulator.Exceptions;

namespace ToyRobotSimulator.Entities.Commands
{
    public class PlaceCommand : ICommand
    {
        private readonly Tuple<int, int> potentialPosition;
        private readonly FacesEnum potentialFace;
        private readonly SimulatorMap map;

        public PlaceCommand(SimulatorMap map,string args)
        {
            if (String.IsNullOrEmpty(args)) { throw new InvalidArgumentsException(); }

            var argumentsSplitted = args.Split(',');
            if (argumentsSplitted.Length != 3)
            {
                throw new InvalidArgumentsException();
            }

            this.map = map;
            this.potentialPosition = Tuple.Create(int.Parse(argumentsSplitted[0]), int.Parse(argumentsSplitted[1]));
            this.potentialFace = (FacesEnum)Enum.Parse(typeof(FacesEnum), argumentsSplitted[2].ToUpper());
        }

        void ICommand.Execute(ToyRobot toyRobot)
        {
            if (!this.map.IsPotentialPositionInOfBounds(this.potentialPosition))
            {
                throw new InvalidPlaceCommandException();
            }

            toyRobot.UpdatePosition(this.potentialPosition);
            toyRobot.UpdateFace(this.potentialFace);
        }
    }
}