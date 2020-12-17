using System;
namespace RobotService
{
    public class StringCalculator
    {
        

        public int Add(string numbers)
        {
            int result = 0;
            string[] numberArr = numbers.Split(new string[] { ",","\n" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string numberStr in numberArr)
            {
                int tempValue = -1;
                if (int.TryParse(numberStr, out tempValue))
                {
                    result += tempValue;
                }
            }

            return result;
        }
    }
}
