using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace RobotService.Tests
{
    [TestClass]
    public class StringCalculatorTests
    {
        StringCalculator calculator;
        [TestInitialize]
        public void Test_Ini()
        {
            calculator = new StringCalculator();
        }
        [TestMethod]
        public void Add_EmptyString_ReturnZero()
        {
            Assert.AreEqual(0, calculator.Add(String.Empty));
        }

        [TestMethod]
        public void Add_SingleNumber_ReturnIntValue()
        {
            int expectedResult = 5;
            string testString = expectedResult.ToString();
            Assert.AreEqual(expectedResult, calculator.Add(testString));
        }
        [TestMethod]
        [DataRow(3,"2,1")]
        [DataRow(7, "4,3")]
        [DataRow(5, "5")]
        [DataRow(6, "1,2,3")]
        public void Add_MultipleNumbers_ReturnTotalValue(int expectedResult, string numbers)
        {
            Assert.AreEqual(expectedResult, calculator.Add(numbers));
        }

        //[TestMethod]
        //public void Add_StringContainNewLineAtTheEnd_Invalid()
        //{
        //    int expectedResult = 1;
        //    string testString = "1,\n";
        //    Assert.AreEqual(expectedResult, calculator.Add(testString));
        //}
        [TestMethod]
        public void Add_StringContainNewLineNotAtTheEnd_Valid()
        {
            int expectedResult = 6;
            string testString = "1\n2,3";
            Assert.AreEqual(expectedResult, calculator.Add(testString));
        }
    }
}
