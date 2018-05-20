using System;

namespace ToyRobotSimulator.Entities.Commands
{
    public class LeftCommand : ICommand
    {
        private readonly SimulatorMap map;

        public LeftCommand(SimulatorMap map, string args)
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
            return Faces.TurnLeft(currentPosition.Item3);
        }
    }
}