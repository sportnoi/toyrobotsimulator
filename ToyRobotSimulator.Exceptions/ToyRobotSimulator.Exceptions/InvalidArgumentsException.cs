using System;

namespace ToyRobotSimulator.Exceptions
{
    public class InvalidArgumentsException : Exception
    {
        public InvalidArgumentsException(string message) 
            : base (message) { }
    }
}
