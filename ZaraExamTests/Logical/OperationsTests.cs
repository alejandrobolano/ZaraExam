using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZaraExam.Logical;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZaraExam.Dao;

namespace ZaraExam.Logical.Tests
{
    [TestClass()]
    public class OperationsTests
    {
        FileDao fileDao;
        Operations operations;
        public OperationsTests()
        {
            fileDao = FileDao.Instance;
            operations = FileDao.GetOperations;
        }

        [TestMethod()]
        [DataRow("01/05/2001", "28/12/2017", DayOfWeek.Thursday)]
        public void TotalEarningsTest(string start, string finish, DayOfWeek day)
        {
            var result = operations.TotalEarnings(Convert.ToDateTime(start), Convert.ToDateTime(finish),
                day, 50f, 0.02f);
            Console.WriteLine(result);
            Assert.IsTrue(result > 0);
        }

      
    }
}