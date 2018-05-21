using System;

namespace ToyRobotSimulator.Exceptions
{
    public class InvalidToyRobotFaceException : Exception
    {
        public InvalidToyRobotFaceException() 
            : base("The robot is not facing anywhere") { }
    }
}