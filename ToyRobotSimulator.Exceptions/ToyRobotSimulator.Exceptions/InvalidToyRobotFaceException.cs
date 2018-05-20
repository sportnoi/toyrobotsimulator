using System;

namespace ToyRobotSimulator.Exceptions
{
    public class InvalidToyRobotFaceException : Exception
    {
        public InvalidToyRobotFaceException(string message) 
            : base(message) { }
    }
}