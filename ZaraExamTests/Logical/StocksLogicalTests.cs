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
        StocksLogical stocks;
        public StocksLogicalTests()
        {
            stocks = new StocksLogical();
        }
        [TestMethod()]
        [DataRow(2001, 05, DayOfWeek.Thursday)]
        public void LastDayOfMonthTest(int year, int month, DayOfWeek day)
        {
            DateTime expected = Convert.ToDateTime("30/04/2015");
            DateTime checkDate = stocks.GetLastDayOfMonthBy(year, month, day);
            bool b = false;
            int count = 0;
            DateTime date;

            while (!b)
            {
                date = stocks.GetLastDayOfMonthBy(year, month, DayOfWeek.Thursday).AddDays(1);
                if (date == Convert.ToDateTime("28/12/2017") ||
                    stocks.GetLastDayOfMonthBy(year, month, DayOfWeek.Thursday) == Convert.ToDateTime("28/12/2017"))
                {
                    b = true;
                }
                if (date.DayOfWeek == DayOfWeek.Friday)
                {
                    count++;
                }
                month++;
                if (month == 13)
                {
                    month = 1;
                    year++;
                }
            }
            Assert.AreEqual(checkDate, expected);

        }

    }
}