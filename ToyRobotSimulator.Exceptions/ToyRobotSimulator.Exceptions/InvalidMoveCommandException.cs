using System;

namespace ToyRobotSimulator.Exceptions
{
    public class InvalidMoveCommandException : Exception
    {
        public InvalidMoveCommandException()
            : base("With this move the robot may fall, so I will ignore it. Please try again.") { }
    }
}
