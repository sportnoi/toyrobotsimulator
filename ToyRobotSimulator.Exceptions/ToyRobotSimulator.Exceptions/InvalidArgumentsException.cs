using System;

namespace ToyRobotSimulator.Exceptions
{
    public class InvalidArgumentsException : Exception
    {
        public InvalidArgumentsException() 
            : base ("Arguments for this Place command were invalid") { }
    }
}