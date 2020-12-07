using RobotService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace RobotService.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            string test = "test";
            Class1 class1 = new Class1(test);
            Assert.AreEqual(test, class1.str);
        }
    }
}
