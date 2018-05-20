using System;

namespace ToyRobotSimulator.Entities.Commands
{
    public class RightCommand : ICommand
    {
        private readonly SimulatorMap map;

        public RightCommand(SimulatorMap map, String args)
        {
            this.map = map;
        }

        void ICommand.Execute(ToyRobot toyRobot)
        {
            FacesEnum newPotentialFace = GetPotentialFace(toyRobot);
            toyRobot.UpdateFace(newPotentialFace);
        }

        private FacesEnum GetPotentialFace(ToyRobot toyRobot)
        {
            Tuple<int, int, FacesEnum> currentPosition = toyRobot.GetCurrentPosition();
            return Faces.TurnRight(currentPosition.Item3);
        }
    }
}