using System;
using System.Linq;

namespace RobotService
{
    public class RobotPosition
    {
        public FacingDirection Face { get; private set; }
        public int PositionX { get; private set; }
        public int PositionY { get; private set; }
        public RobotPosition(string positionString)
        {
            string[] paramList = positionString.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            if (paramList.Length != 3)
            {
                throw new GameCommandParameterInvalidException();
            }
            int x = -1, y = -1;
            if (!int.TryParse(paramList[0], out x) || x < 0)
            {
                throw new GameCommandParameterInvalidException("Parameter X in (X,Y,F) must be a positive integer");
            }
            PositionX = x;
            if (!int.TryParse(paramList[1], out y) || y < 0)
            {
                throw new GameCommandParameterInvalidException("Parameter Y in (X,Y,F) must be a positive integer");
            }
            PositionY = y;
            FacingDirection facing;
            if (!Enum.TryParse<FacingDirection>(paramList[2], true, out facing))
            {
                throw new GameCommandParameterInvalidException("Parameter F in (X,Y,F) must be a valid direction (North, South, East, West");
            }
            Face = facing;
        }
        public void Move()
        {
            switch (Face)
            {
                case FacingDirection.EAST:
                    PositionY++;
                    break;
                case FacingDirection.NORTH:
                    PositionX++;
                    break;
                case FacingDirection.SOUTH:
                    PositionX--;
                    break;
                case FacingDirection.WEST:
                default:
                    PositionY--;
                    break;
            }
        }
        public void Turn(bool isClockwise=true)
        {
            int minFace = Enum.GetValues(typeof(FacingDirection)).Cast<int>().Min();
            int maxFace = Enum.GetValues(typeof(FacingDirection)).Cast<int>().Max();
            if (isClockwise)
            {
                if ((int)Face == maxFace)
                {
                    Face = (FacingDirection)minFace;
                }
                else
                {
                    Face = Face++;
                }
                
            }
            else
            {
                if ((int)Face == minFace)
                {
                    Face = (FacingDirection)maxFace;
                }
                else
                {
                    Face = Face--;
                }
            }
        }
        public override string ToString()
        {
            return $"{PositionX},{PositionY},{Face.ToString()}";
        }
    }
}
