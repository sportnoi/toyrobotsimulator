using System;
using System.Collections.Generic;
using ToyRobotSimulator.Entities;
using ToyRobotSimulator.Entities.Commands;

namespace ToyRobotSimulator
{
    class Program
    {
        static void Main(string[] args)
        {
            var robot = new ToyRobot();

            Dictionary<string, Type> availableCommands = new Dictionary<string, Type>
            {
                { "PLACE", typeof(PlaceCommand) },
                { "MOVE", typeof(MoveCommand) },
                { "LEFT", typeof(LeftCommand) },
                { "RIGHT", typeof(RightCommand) },
                { "REPORT", typeof(ReportCommand) }
            };

            while (true)
            {
                var input = Console.ReadLine();
                var command = string.Empty;
                var arguments = string.Empty;

                if (!String.IsNullOrEmpty(input))
                {
                    var inputSplitted = input.Split(' ');

                    if (inputSplitted.Length > 0) command = inputSplitted[0];
                    if (inputSplitted.Length == 2) arguments = inputSplitted[1];

                    if (!availableCommands.ContainsKey(command.ToUpper())
                        || !availableCommands.TryGetValue(command.ToUpper(), out Type value))
                        continue;

                    try
                    {
                        dynamic cmd = Activator.CreateInstance(value, arguments);
                        robot.Execute(cmd);
                    }
                    catch (Exception ex)
                    {
                        if (ex.InnerException != null) Console.WriteLine(ex.InnerException.Message);
                        else Console.WriteLine(ex.Message);
                    }
                }
            }
        }
    }
}