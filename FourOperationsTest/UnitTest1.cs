using System;
using System.Collections.Generic;
using FourOperations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FourOperationsTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ComboTest()
        {
            Dictionary<string, int> dict = new Dictionary<string, int>();
            dict.Add("((15/(7-(1-1)))*3)-(2+(1+1))", ((15 / (7 - (1 - 1))) * 3) - (2 + (1 + 1)));
            dict.Add("(5+16)*(10+10)", (5 + 16) * (10 + 10));
            dict.Add("1+2*5*20-(10*(3+6))", 1 + 2 * 5 * 20 - (10 * (3 + 6)));
            dict.Add("(5+16*(2-1))+3", (5 + 16 * (2 - 1)) + 3);
            dict.Add("(20-16/(1*2+2))+3", (20 - 16 / (1 * 2 + 2)) + 3);
            dict.Add("200-80-2*(1+6)*5/3+20", 200-80-2*(1+6)*5/3+20);
            dict.Add("(5)+55", (5)+55);

            foreach (var kvp in dict)
            {
                Program.BinaryExpressionTree newTree = Program.GetExpressionTree(kvp.Key);
                int result = newTree.root.CalculateLeftAndRight();
                Assert.AreEqual(kvp.Value, result, String.Format("Case{0}: Expect = {1} And Actual = {2} are not equal", kvp.Key,kvp.Value, result) );
            }
        }
    }
}
