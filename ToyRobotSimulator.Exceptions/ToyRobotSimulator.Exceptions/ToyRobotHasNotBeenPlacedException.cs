using System;

namespace ToyRobotSimulator.Exceptions
{
    public class ToyRobotHasNotBeenPlacedException : Exception
    {
        public ToyRobotHasNotBeenPlacedException(string message) 
            : base(message) { }
    }
}
