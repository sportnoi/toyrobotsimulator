using System;

namespace ToyRobotSimulator.Entities.Commands
{
    public class ReportCommand : ICommand
    {
        public ReportCommand(string args) { }

        void ICommand.Execute(ToyRobot toyRobot)
        {
            Tuple<int, int, FacesEnum> position = toyRobot.GetCurrentPosition();
            Console.WriteLine(position.Item1 + "," + position.Item2 + "," + position.Item3);
        }
    }
}