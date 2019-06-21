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
    public class StocksLogicalTests
    {
        [TestMethod()]
        [DataRow(2015,04,DayOfWeek.Thursday)]
        public void LastDayOfMonthTest(int year, int month, DayOfWeek day)
        {
            DateTime expected = Convert.ToDateTime("30/04/2015");
            StocksLogical stocks = new StocksLogical();
            DateTime checkDate = stocks.LastDayOfMonth(year, month, day);
            Assert.AreEqual(checkDate, expected);

        }
    }
}