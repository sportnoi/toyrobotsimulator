using System;
using ToyRobotSimulator.Entities.Commands;
using ToyRobotSimulator.Exceptions;

namespace ToyRobotSimulator.Entities
{
    public class ToyRobot
    {
        private Tuple<int, int> CurrentPosition { get; set; }
        private FacesEnum Face { get; set; }

        public Tuple<int, int, FacesEnum> GetCurrentPosition()
        {
            if (!this.HasBeenPlaced())
            {
                throw new ToyRobotHasNotBeenPlacedException("As the robot has not been placed, you cannot use any other command than PLACE.");
            }

            return Tuple.Create(CurrentPosition.Item1,
                   this.CurrentPosition.Item2,
                   this.Face);
        }

        public bool HasBeenPlaced()
        {
            return this.CurrentPosition != null;
        }
        public void Execute(ICommand command)
        {
            command.Execute(this);
        }

        public void UpdatePosition(Tuple<int, int> newPosition)
        {
            this.CurrentPosition = newPosition;
        }

        public void UpdateFace(FacesEnum newFace)
        {
            this.Face = newFace;
        }
    }
}