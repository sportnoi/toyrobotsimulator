using System;
using ToyRobotSimulator.Entities.Commands;
using ToyRobotSimulator.Exceptions;

namespace ToyRobotSimulator.Entities
{
    public class ToyRobot
    {
        #region Singleton
        private static ToyRobot instance;
        private ToyRobot() { }
        public static ToyRobot Instance
        {
            get
            {
                if (instance == null)
                    instance = new ToyRobot();
                return instance;
            }
        }
        #endregion

        private Tuple<int, int> CurrentPosition { get; set; }
        private FacesEnum Face { get; set; }

        public string GetCurrentPosition()
        {
            if (!this.HasBeenPlaced())
            {
                throw new ToyRobotHasNotBeenPlacedException();
            }

            return this.CurrentPosition.Item1 + "," +
                   this.CurrentPosition.Item2 + "," +
                   this.Face;
        }
        public bool HasBeenPlaced()
        {
            return this.CurrentPosition != null;
        }
        public void Execute(ICommand command)
        {
            command.Execute(this);
        }

        public void Place(Tuple<int, int> newPosition, FacesEnum newFace)
        {
            this.CurrentPosition = newPosition;
            this.Face = newFace;
        }
    }
}