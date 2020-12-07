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
            if (currentPosition == null)
            {
                Console.WriteLine("No move is made");
            }
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
                    {
                        ReportCurrentPosition();
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

        private void DoPlace(RobotPosition newPosition)
        {
            currentPosition = newPosition;
        }
        private void MakeAMove()
        {
            if (!isGameStarted())
            {
                throw new GameNotStartedException();
            }
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
        }
        private void TurnRight()
        {
            if (!isGameStarted())
            {
                throw new GameNotStartedException();
            }
        }
        private void ReportCurrentPosition()
        {
            if (!isGameStarted())
            {
                throw new GameNotStartedException();
            }
        }

        private bool isGameStarted()
        {
            return currentPosition != null;
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
