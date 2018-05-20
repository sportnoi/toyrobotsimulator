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
            SimulatorMap map = new SimulatorMap();
            ToyRobot robot = new ToyRobot();

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
                string input = Console.ReadLine();
                string command = string.Empty;
                string arguments = string.Empty;
                string[] inputSplitted = input.Split(' ');

                if (inputSplitted.Length > 0) command = inputSplitted[0];
                if (inputSplitted.Length == 2) arguments = inputSplitted[1];

                if (availableCommands.ContainsKey(command.ToUpper()))
                {
                    if (availableCommands.TryGetValue(command.ToUpper(), out Type value))
                    {
                        try
                        {
                            dynamic cmd = Activator.CreateInstance(value, new object[] { map, arguments });
                            robot.Execute(cmd);
                        } catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
            }
        }
    }
}