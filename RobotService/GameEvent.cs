using System;

namespace RobotService
{
    public class GameEvent
    {
        private Commands command;
        private RobotPosition robotPosition;

        public GameEvent(string input)
        {
            if (string.IsNullOrEmpty(input) || input.Trim().Length == 0)
            {
                throw new GameCommandEmptyException();
            }
            string[] commandParts = input.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            if (commandParts.Length > 2)
            {
                throw new GameCommandInvalidFormatException();
            }

            string commandStr = commandParts[0];
            string parameters = String.Empty;
            if (commandParts.Length > 1)
            {
                parameters = commandParts[1];
            }

            if (!Enum.TryParse<Commands>(commandStr, true, out command))
            {
                throw new GameCommandInvalidException();
            }

            if (command == Commands.PLACE && string.IsNullOrEmpty(parameters))
            {
                throw new GameCommandInvalidParameterException("Command PLACE requires parameters");
            }
            if (command != Commands.PLACE && !string.IsNullOrEmpty(parameters))
            {
                throw new GameCommandInvalidParameterException("Commands other than PLACE do not require parameters");
            }

            if (!string.IsNullOrEmpty(parameters))
            {
                robotPosition = new RobotPosition(parameters);
            }

        }
        public Commands GetGameCommand()
        {
            return command;
        }
        public RobotPosition GetRobotPosition()
        {
            return robotPosition;
        }
    }
}