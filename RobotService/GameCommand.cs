using System;

namespace RobotService
{
    public enum Commands
    {
        PLACE,
        MOVE,
        LEFT,
        RIGHT,
        REPORT
    }
    public enum FacingDirection
    {
        SOUTH=0,
        NORTH=2,
        EAST=3,
        WEST=1
    }
    #region Exceptions
    public class GameCommandInvalidException:Exception
    {
        public GameCommandInvalidException()
        :base("Command is not recognised"){}
    }
    public class GameCommandInvalidFormatException:Exception
    {
        public GameCommandInvalidFormatException()
        :base("Command should only have maximum 2 parts (command and parameters)"){}
    }
    public class GameCommandEmptyException:Exception
    {
        public GameCommandEmptyException()
        :base("Command cannot be empty string"){}
    }
    public class GameCommandInvalidParameterException:Exception
    {
        public GameCommandInvalidParameterException()
        :base("Invalid parameters for entered command"){}
        public GameCommandInvalidParameterException(string message)
        :base(message){}
    }
    public class GameCommandParameterInvalidException : Exception
    {
        public GameCommandParameterInvalidException()
        : base("Parameters for PLACE command should have 3 parts (int, int, String)") { }
        public GameCommandParameterInvalidException(string message)
        : base(message) { }
    }
    #endregion
}