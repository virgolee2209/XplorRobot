using RobotService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
namespace RobotService.Tests
{
    [TestClass]
    public class RobotPositionTests
    {
        [TestMethod]
        public void RobotPosition_Constructor_2Parts_GameCommandParameterInvalidException()
        {
            string input = "1,1";
            Assert.ThrowsException<GameCommandParameterInvalidException>(()=>{
                RobotPosition position = new RobotPosition(input);
            });
            
        }
        [TestMethod]
        public void RobotPosition_Constructor_4Parts_GameCommandParameterInvalidException()
        {
            string input = "1,1,WEST,invalid";
            Assert.ThrowsException<GameCommandParameterInvalidException>(() => {
                RobotPosition position = new RobotPosition(input);
            });

        }
        [TestMethod]
        public void RobotPosition_Constructor_XNotValidInt_GameCommandParameterInvalidException()
        {
            string input = "x,1,WEST";
            Assert.ThrowsException<GameCommandParameterInvalidException>(() => {
                RobotPosition position = new RobotPosition(input);
            });

        }
        [TestMethod]
        public void RobotPosition_Constructor_XNegativeInt_GameCommandParameterInvalidException()
        {
            string input = "-2,1,WEST";
            Assert.ThrowsException<GameCommandParameterInvalidException>(() => {
                RobotPosition position = new RobotPosition(input);
            });

        }
        [TestMethod]
        public void RobotPosition_Constructor_YNotValidInt_GameCommandParameterInvalidException()
        {
            string input = "1,Y,WEST";
            Assert.ThrowsException<GameCommandParameterInvalidException>(() => {
                RobotPosition position = new RobotPosition(input);
            });

        }
        [TestMethod]
        public void RobotPosition_Constructor_YNegativeInt_GameCommandParameterInvalidException()
        {
            string input = "1,-2,WEST";
            Assert.ThrowsException<GameCommandParameterInvalidException>(() => {
                RobotPosition position = new RobotPosition(input);
            });

        }
        [TestMethod]
        public void RobotPosition_Constructor_FaceNotValidInt_GameCommandParameterInvalidException()
        {
            string input = "1,1,SUN";
            Assert.ThrowsException<GameCommandParameterInvalidException>(() => {
                RobotPosition position = new RobotPosition(input);
            });

        }

        [TestMethod]
        public void RobotPosition_Constructor_ValidData_ValidPosition()
        {
            string input = "1,1,WEST";
            RobotPosition position = new RobotPosition(input);
            Assert.AreEqual(input, position.ToString());
        }

        [TestMethod]
        public void RobotPosition_TurnClockwise_North1Time_East()
        { 
            string original = $"1,1,{FacingDirection.NORTH.ToString()}";
            string expected = $"1,1,{FacingDirection.EAST.ToString()}";
            RobotPosition position = new RobotPosition(original);
            position.Turn();
            RobotPosition expectedPosition = new RobotPosition(expected);

            Assert.AreEqual(expectedPosition.ToString(), position.ToString());
        }
        [TestMethod]
        public void RobotPosition_TurnClockwise_North4Time_North()
        {
            string original = $"1,1,{FacingDirection.NORTH.ToString()}";
            RobotPosition position = new RobotPosition(original);
            int count = 0;
            while (count < 4)
            {
                position.Turn();
                count++;
            }
            
            RobotPosition expectedPosition = new RobotPosition(original);
            Assert.AreEqual(expectedPosition.ToString(), position.ToString());
        }
        [TestMethod]
        public void RobotPosition_TurnAntiClockwise_North1Time_West()
        {
            string original = $"1,1,{FacingDirection.NORTH.ToString()}";
            string expected = $"1,1,{FacingDirection.WEST.ToString()}";
            RobotPosition position = new RobotPosition(original);
            position.Turn(isClockwise: false);
            RobotPosition expectedPosition = new RobotPosition(expected);

            Assert.AreEqual(expectedPosition.ToString(), position.ToString());
        }
        [TestMethod]
        public void RobotPosition_TurnAntiClockwise_North4Time_North()
        {
            string original = $"1,1,{FacingDirection.NORTH.ToString()}";
            RobotPosition position = new RobotPosition(original);
            int count = 0;
            while (count < 4)
            {
                position.Turn(isClockwise:false);
                count++;
            }

            RobotPosition expectedPosition = new RobotPosition(original);
            Assert.AreEqual(expectedPosition.ToString(), position.ToString());
        }
        [TestMethod]
        public void RobotPosition_Move_MiddleFacingNorth_XIncreaseYStay()
        {
            int x = 2;
            string original = $"{x},2,{FacingDirection.NORTH}";
            string expected = $"{x+1},2,{FacingDirection.NORTH}";
            RobotPosition position = new RobotPosition(original);
            position.Move();
            RobotPosition expectedPosition = new RobotPosition(expected);
            Assert.AreEqual(expectedPosition.ToString(), position.ToString());
        }
        [TestMethod]
        public void RobotPosition_Move_MiddleFacingEast_XStayYIncrease()
        {
            int y = 2;
            string original = $"2,{y},{FacingDirection.EAST}";
            string expected = $"2,{y+1},{FacingDirection.EAST}";
            RobotPosition position = new RobotPosition(original);
            position.Move();
            RobotPosition expectedPosition = new RobotPosition(expected);
            Assert.AreEqual(expectedPosition.ToString(), position.ToString());
        }
        [TestMethod]
        public void RobotPosition_Move_MiddleFacingSouth_XDecreaseYStay()
        {
            int x = 2;
            string original = $"{x},2,{FacingDirection.SOUTH}";
            string expected = $"{x - 1},2,{FacingDirection.SOUTH}";
            RobotPosition position = new RobotPosition(original);
            position.Move();
            RobotPosition expectedPosition = new RobotPosition(expected);
            Assert.AreEqual(expectedPosition.ToString(), position.ToString());
        }
        [TestMethod]
        public void RobotPosition_Move_MiddleFacingWest_XStayYDecrease()
        {
            int y = 2;
            FacingDirection face = FacingDirection.WEST;
            string original = $"2,{y},{face}";
            string expected = $"2,{y - 1},{face}";
            RobotPosition position = new RobotPosition(original);
            position.Move();
            RobotPosition expectedPosition = new RobotPosition(expected);
            Assert.AreEqual(expectedPosition.ToString(), position.ToString());
        }
    }
}
