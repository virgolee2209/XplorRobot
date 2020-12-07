using RobotService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
namespace RobotService.Tests
{
    [TestClass]
    public class GameTests
    {
        [TestMethod]
        public void GameTests_NoPlace_Move_GameNotStartedException()
        {
            Game game = new Game();
            Assert.ThrowsException<GameNotStartedException>(() => {
                game.ReceiveEvent(new GameEvent("move"));
            });
        }

        [TestMethod]
        public void GameTests_NoPlace_Left_GameNotStartedException()
        {
            Game game = new Game();
            Assert.ThrowsException<GameNotStartedException>(() => {
                game.ReceiveEvent(new GameEvent("left"));
            });
        }

        [TestMethod]
        public void GameTests_NoPlace_Right_GameNotStartedException()
        {
            Game game = new Game();
            Assert.ThrowsException<GameNotStartedException>(() => {
                game.ReceiveEvent(new GameEvent("right"));
            });
        }

        [TestMethod]
        public void GameTests_NoPlace_Report_GameNotStartedException()
        {
            Game game = new Game();
            Assert.ThrowsException<GameNotStartedException>(() => {
                game.ReceiveEvent(new GameEvent("report"));
            });
        }

        [TestMethod]
        public void GameTests_PlaceRobotOutOfRange_GameRobotPlaceOutsideTableException()
        {
            Game game = new Game();
            GameEvent outOfRangeEvent = new GameEvent("PLACE 6,0,EAST");
            Assert.ThrowsException<GameRobotPlaceOutsideTableException>(() => {
                game.ReceiveEvent(outOfRangeEvent);
            });

            outOfRangeEvent = new GameEvent("PLACE 0,6,EAST");
            Assert.ThrowsException<GameRobotPlaceOutsideTableException>(() => {
                game.ReceiveEvent(outOfRangeEvent);
            });
        }

        [DataRow(0, 0, FacingDirection.WEST)]
        [DataRow(0, 2, FacingDirection.WEST)]
        [DataRow(0, 4, FacingDirection.WEST)]
        [DataRow(0, 0, FacingDirection.SOUTH)]
        [DataRow(2, 0, FacingDirection.SOUTH)]
        [DataRow(4, 0, FacingDirection.SOUTH)]
        [DataRow(0, 4, FacingDirection.NORTH)]
        [DataRow(2, 4, FacingDirection.NORTH)]
        [DataRow(4, 4, FacingDirection.NORTH)]
        [DataRow(4, 0, FacingDirection.EAST)]
        [DataRow(4, 2, FacingDirection.EAST)]
        [DataRow(4, 4, FacingDirection.EAST)]
        [DataTestMethod]
        //[TestMethod]
        public void GameTests_PlaceRobotOnTheEdgeThenMove_GameRobotCannotMoveException(int x, int y, FacingDirection face)
        //public void GameTests_PlaceRobotOnTheEdgeThenMove_GameRobotCannotMoveException()
        {
            //int x=0,y=0;
            //FacingDirection face=FacingDirection.WEST;
            Game game = new Game();
            string placeCommand = $"PLACE {x},{y},{face.ToString()}";
            //GameEvent placeRobotOnTheEdge = new GameEvent("PLACE 0,0,WEST");
            GameEvent placeRobotOnTheEdge = new GameEvent(placeCommand);
            game.ReceiveEvent(placeRobotOnTheEdge);
            GameEvent moveEvent = new GameEvent("MOVE");
            Assert.ThrowsException<GameRobotCannotMoveException>(() => {
                game.ReceiveEvent(moveEvent);
            });
            
            Assert.AreEqual(placeRobotOnTheEdge.GetRobotPosition().ToString(),game.ReportCurrentPosition());
            
        }
        [DataRow(new string[]{
            "PLACE 0,0,NORTH",
            "MOVE",
            "REPORT"
        }, "0,1,NORTH")]//example a
        [DataRow(new string[]{
            "PLACE 0,0,NORTH",
            "LEFT",
            "REPORT"
        }, "0,0,WEST")]//example b
        [DataRow(new string[]{
            "PLACE 1,2,EAST",
            "MOVE",
            "MOVE",
            "LEFT",
            "MOVE",
            "REPORT"
        },"3,3,NORTH")]//example c
        [DataRow(new string[]{
            "PLACE 2,4,EAST",
            "MOVE",
            "RIGHT",
            "MOVE",
            "RIGHT",
            "MOVE",
            "MOVE",
            "LEFT",
            "MOVE",
            "REPORT"
        }, "1,2,SOUTH")]
        [DataRow(new string[]{
            "PLACE 2,2,NORTH",
            "MOVE",
            "REPORT"
        }, "2,3,NORTH")]
        [DataRow(new string[]{
            "PLACE 2,2,SOUTH",
            "MOVE",
            "REPORT"
        }, "2,1,SOUTH")]
        [DataRow(new string[]{
            "PLACE 2,2,EAST",
            "MOVE",
            "REPORT"
        }, "3,2,EAST")]
        [DataRow(new string[]{
            "PLACE 2,2,WEST",
            "MOVE",
            "REPORT"
        }, "1,2,WEST")]
        [DataTestMethod]
        public void GameTests_RandomMove_CheckExpectedPosition_Equal(string[] events,string expectedPosition)
        {
            //int x=0,y=0;
            //FacingDirection face=FacingDirection.WEST;
            Game game = new Game();
            foreach(string gameEvent in events)
            {
                GameEvent temp = new GameEvent(gameEvent);
                game.ReceiveEvent(temp);
            }
            RobotPosition expectedPos= new RobotPosition(expectedPosition);
            Assert.AreEqual(expectedPos.ToString(), game.ReportCurrentPosition());
        }
    }
}
