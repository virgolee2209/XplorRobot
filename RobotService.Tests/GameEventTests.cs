using RobotService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
namespace RobotService.Tests
{
    [TestClass]
    public class GameEventTests
    {
        [TestMethod]
        public void GameEvent_RandomText_InvalidCommand()
        {
            string input = "test";
            Assert.ThrowsException<GameCommandInvalidException>(()=>{
                GameEvent gameEvent = new GameEvent(input);
            });
            
        }
        [TestMethod]
        public void GameEvent_EmptyText_InvalidCommand()
        {
            string input = "";
            Assert.ThrowsException<GameCommandEmptyException>(()=>{
                GameEvent gameEvent = new GameEvent(input);
            });
            
        }
        [TestMethod]
        public void GameEvent_3parts_InvalidCommand()
        {
            string input = "place 0,0,N invalid";
            Assert.ThrowsException<GameCommandInvalidFormatException>(()=>{
                GameEvent gameEvent = new GameEvent(input);
            },"wrong exception type");
            
        }
        [TestMethod]
        public void GameEvent_PlaceWithoutParam_InvalidCommand()
        {
            string input = "place";
            Assert.ThrowsException<GameCommandInvalidParameterException>(()=>{
                GameEvent gameEvent = new GameEvent(input);
            },"invalid");
            
        }
        [TestMethod]
        public void GameEvent_MoveWithParam_InvalidCommand()
        {
            string input = "move 0,0,N";
            Assert.ThrowsException<GameCommandInvalidParameterException>(()=>{
                GameEvent gameEvent = new GameEvent(input);
            },"wrong exception type");
            
        }
        [TestMethod]
        public void GameEvent_MOVE_ValidCommand()
        {
            string input = "move";
            GameEvent gameEvent = new GameEvent(input);
            Assert.AreEqual(Commands.MOVE, gameEvent.GetGameCommand());
        }
        [TestMethod]
        public void GameEvent_MOVE_UpperCase_ValidCommand()
        {
            string input = "MOVE";
            GameEvent gameEvent = new GameEvent(input);
            Assert.AreEqual(Commands.MOVE, gameEvent.GetGameCommand());
        }
        [TestMethod]
        public void GameEvent_MOVE_MixedCase_ValidCommand()
        {
            string input = "mOvE";
            GameEvent gameEvent = new GameEvent(input);
            Assert.AreEqual(Commands.MOVE, gameEvent.GetGameCommand());
        }

    }
}
