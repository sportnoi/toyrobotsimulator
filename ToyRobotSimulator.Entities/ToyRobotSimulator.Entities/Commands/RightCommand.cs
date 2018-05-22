using System;

namespace ToyRobotSimulator.Entities.Commands
{
    public class RightCommand : ICommand
    {
        public RightCommand(SimulatorMap map, String args)
        {
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