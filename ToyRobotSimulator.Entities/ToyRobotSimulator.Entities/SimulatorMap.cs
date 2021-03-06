﻿using System;

namespace ToyRobotSimulator.Entities
{
    public class SimulatorMap
    {
        #region Singleton
        private static SimulatorMap instance;
        private SimulatorMap() { }
        public static SimulatorMap Instance
        {
            get
            {
                if (instance == null)
                    instance = new SimulatorMap();
                return instance;
            }
        }
        #endregion
        private const int MAX_X = 4;
        private const int MAX_Y = 4;

        public bool IsPotentialPositionInOfBounds(Tuple<int, int> potentialPosition)
        {
            return 0 <= potentialPosition.Item1 
                   && potentialPosition.Item1 <= MAX_X
                   && 0 <= potentialPosition.Item2 
                   && potentialPosition.Item2 <= MAX_Y;
        }
    }
}