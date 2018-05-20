using System;

namespace ToyRobotSimulator.Exceptions
{
    public class InvalidPlaceCommandException : Exception
    {
        public InvalidPlaceCommandException(string message)
            : base(message) { }
    }
}
