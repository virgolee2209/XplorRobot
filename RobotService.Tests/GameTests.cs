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
                game.SendCommand(new GameEvent("move"));
            });
        }

        [TestMethod]
        public void GameTests_NoPlace_Left_GameNotStartedException()
        {
            Game game = new Game();
            Assert.ThrowsException<GameNotStartedException>(() => {
                game.SendCommand(new GameEvent("left"));
            });
        }

        [TestMethod]
        public void GameTests_NoPlace_Right_GameNotStartedException()
        {
            Game game = new Game();
            Assert.ThrowsException<GameNotStartedException>(() => {
                game.SendCommand(new GameEvent("right"));
            });
        }

        [TestMethod]
        public void GameTests_NoPlace_Report_GameNotStartedException()
        {
            Game game = new Game();
            Assert.ThrowsException<GameNotStartedException>(() => {
                game.SendCommand(new GameEvent("report"));
            });
        }

        [TestMethod]
        public void GameTests_PlaceRobotOutOfRange_GameRobotPlaceOutsideTableException()
        {
            Game game = new Game();
            GameEvent outOfRangeEvent = new GameEvent("PLACE 6,0,EAST");
            Assert.ThrowsException<GameRobotPlaceOutsideTableException>(() => {
                game.SendCommand(outOfRangeEvent);
            });

            outOfRangeEvent = new GameEvent("PLACE 0,6,EAST");
            Assert.ThrowsException<GameRobotPlaceOutsideTableException>(() => {
                game.SendCommand(outOfRangeEvent);
            });
        }

        [DataRow(0, 0, FacingDirection.WEST)]
        [DataRow(2, 0, FacingDirection.WEST)]
        [DataRow(4, 0, FacingDirection.WEST)]
        [DataRow(0, 0, FacingDirection.SOUTH)]
        [DataRow(0, 2, FacingDirection.SOUTH)]
        [DataRow(0, 4, FacingDirection.SOUTH)]
        [DataRow(4, 0, FacingDirection.NORTH)]
        [DataRow(4, 2, FacingDirection.NORTH)]
        [DataRow(4, 4, FacingDirection.NORTH)]
        [DataRow(0, 4, FacingDirection.EAST)]
        [DataRow(2, 4, FacingDirection.EAST)]
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
            game.SendCommand(placeRobotOnTheEdge);
            GameEvent moveEvent = new GameEvent("MOVE");
            Assert.ThrowsException<GameRobotCannotMoveException>(() => {
                game.SendCommand(moveEvent);
            });
            Assert.AreEqual(placeRobotOnTheEdge.GetRobotPosition().ToString(),game.ReportCurrentPosition());
            //placeRobotOnTheEdge = new GameEvent("PLACE 0,2,WEST");
            //Assert.ThrowsException<GameRobotCannotMoveException>(() => {
            //    game.SendCommand(moveEvent);
            //});

            //placeRobotOnTheEdge = new GameEvent("PLACE 0,5,WEST");
            //Assert.ThrowsException<GameRobotCannotMoveException>(() => {
            //    game.SendCommand(moveEvent);
            //});
        }

    }
}
