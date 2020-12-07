using System;
using System.Collections.Generic;

namespace RobotService
{
    public class Game
    {
        private const int TABLE_WIDTH=5;
        private const int TABLE_HEIGHT=5;
        private List<GameEvent> eventList;
        private RobotPosition currentPosition;
        public Game()
        {
            eventList = new List<GameEvent>();
        }
        public void SendCommand(GameEvent gameEvent)
        {
            eventList.Add(gameEvent);
            switch (gameEvent.GetGameCommand())
            {
                case Commands.PLACE:
                    {
                        DoPlace(gameEvent.GetRobotPosition());
                        break;
                    }
                case Commands.MOVE:
                    {
                        MakeAMove();
                        break;
                    }
                case Commands.LEFT:
                    {
                        TurnLeft();
                        break;
                    }
                case Commands.RIGHT:
                    {
                        TurnRight();
                        break;
                    }
                case Commands.REPORT:
                default:
                    {
                        Console.WriteLine(ReportCurrentPosition());
                        break;
                    }
            }
        }

        private void DoPlace(RobotPosition newPosition)
        {
            if (!isPositionValid(newPosition)) throw new GameRobotPlaceOutsideTableException();
            currentPosition = newPosition;
        }
        private void MakeAMove()
        {
            if (!isGameStarted())
            {
                throw new GameNotStartedException();
            }
            RobotPosition nextPosition = new RobotPosition(currentPosition.ToString());
            nextPosition.Move();
            if (!isPositionValid(nextPosition)) throw new GameRobotCannotMoveException();
            currentPosition = nextPosition;
        }

        private void TurnLeft()
        {
            if (!isGameStarted())
            {
                throw new GameNotStartedException();
            }
            //switch (currentPosition.Face)
            //{
            //}
            currentPosition.Turn(isClockwise:false);
        }
        private void TurnRight()
        {
            if (!isGameStarted())
            {
                throw new GameNotStartedException();
            }
            currentPosition.Turn();
        }
        public string ReportCurrentPosition()
        {
            if (!isGameStarted())
            {
                throw new GameNotStartedException();
            }
            return currentPosition.ToString();
        }

        private bool isGameStarted()
        {
            return currentPosition != null;
        }
        private bool isPositionValid(RobotPosition position)
        {
            if (position.PositionX < 0 || position.PositionX >= TABLE_HEIGHT
                || position.PositionY < 0 || position.PositionY >= TABLE_WIDTH)
            {
                return false;
            }
            return true;
        }

    }

    public class GameNotStartedException : Exception
    {
        public GameNotStartedException() : base("Robot has NOT been placed") { }
    }
    public class GameRobotPlaceOutsideTableException : Exception
    {
        public GameRobotPlaceOutsideTableException() : base("Robot can not be placed outside if table range") { }
        public GameRobotPlaceOutsideTableException(string message) : base(message) { }
    }
    public class GameRobotCannotMoveException : Exception
    {
        public GameRobotCannotMoveException() : base("Robot can not make a move because it is on the edge of the table") { }
        public GameRobotCannotMoveException(string message) : base(message) { }
    }
}
