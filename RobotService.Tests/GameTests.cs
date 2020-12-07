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

        [DataRow(0,0,FacingDirection.WEST)]
        [DataRow(0,2, FacingDirection.WEST)]
        //[DataRow(0, new object[] { 2, "WEST" }, "Middle Left Position")]
        [DataTestMethod]
        public void GameTests_PlaceRobotOnTheEdgeThenMove_GameRobotCannotMoveException(int x, int y, FacingDirection face)
        {
            Game game = new Game();
            string placeCommand = $"PLACE {x},{y},{face.ToString()}";
            //GameEvent placeRobotOnTheEdge = new GameEvent("PLACE 0,0,WEST");
            GameEvent placeRobotOnTheEdge = new GameEvent(placeCommand);
            game.SendCommand(placeRobotOnTheEdge);
            GameEvent moveEvent = new GameEvent("MOVE");
            Assert.ThrowsException<GameRobotCannotMoveException>(() => {
                game.SendCommand(moveEvent);
            });

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
