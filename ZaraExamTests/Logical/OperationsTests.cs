using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZaraExam.Logical;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZaraExam.Logical.Tests
{
    [TestClass()]
    public class OperationsTests
    {
        Operations operations;
        public OperationsTests()
        {
            operations = new Operations();
        }

        [TestMethod()]
        [DataRow("01/05/2001", "28/12/2017", DayOfWeek.Thursday)]
        public void GetTotalActionsMathTest(string start, string finish, DayOfWeek day)
        {
            var result = operations.TotalEarnings(Convert.ToDateTime(start), Convert.ToDateTime(finish),
                day, 50f, 0.02f);
            Console.WriteLine(result);
            Assert.IsTrue(result > 0);
        }

    }
}