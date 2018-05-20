using System;

namespace ToyRobotSimulator.Entities
{
    public enum FacesEnum
    {
        NORTH = 1,
        WEST,
        SOUTH,
        EAST
    }

    public static class Faces
    {
        public static FacesEnum TurnLeft(FacesEnum currentFace)
        {
            return Turn((int)currentFace + 1, FacesEnum.NORTH);
        }

        public static FacesEnum TurnRight(FacesEnum currentFace)
        {
            return Turn((int)currentFace - 1, FacesEnum.EAST);
        }

        private static FacesEnum Turn(int newFace, FacesEnum defaultFace)
        {
            if (Enum.IsDefined(typeof(FacesEnum), newFace))
                return (FacesEnum)newFace;
            else
                return defaultFace;
        }
    }
}
