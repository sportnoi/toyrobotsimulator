using System;

namespace ToyRobotSimulator.Exceptions
{
    public class InvalidPlaceCommandException : Exception
    {
        public InvalidPlaceCommandException()
            : base("Invalid arguments. New position should be between the limits of the tablemap.") { }
    }
}
