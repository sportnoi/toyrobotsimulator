using System;

namespace ToyRobotSimulator.Exceptions
{
    public class InvalidMoveCommandException : Exception
    {
        public InvalidMoveCommandException(string message)
            : base(message) { }
    }
}
