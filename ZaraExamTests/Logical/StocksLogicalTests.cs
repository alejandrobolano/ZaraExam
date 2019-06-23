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
    public class StocksLogicalTests
    {
        FileDao fileDao;
        StocksLogical stocks;
        public StocksLogicalTests()
        {
            fileDao = FileDao.Instance;
            stocks = FileDao.GetStocksLogical;
        }

        [TestMethod()]
        //[DataRow(2012, 06, DayOfWeek.Thursday)]
        //[DataRow(2019, 03, DayOfWeek.Thursday)]
        //[DataRow(2017, 02, DayOfWeek.Thursday)]
        [DataRow(2015, 04, DayOfWeek.Thursday)]
        public void GetLastDayOfMonthByTest(int year, int month, DayOfWeek day)
        {
            //DateTime expected = Convert.ToDateTime("28/06/2012");
            //DateTime expected = Convert.ToDateTime("28/03/2019");
            //DateTime expected = Convert.ToDateTime("23/02/2017");
            DateTime expected = Convert.ToDateTime("30/04/2015");
            DateTime checkDate = stocks.GetLastDayOfMonthBy(year, month, day);
            Assert.AreEqual(checkDate, expected);
        }

        [TestMethod()]
        [DataRow("23/05/2001", "28/12/2017", DayOfWeek.Thursday)]
        public void GetStockByQuotationTest(string start, string finish, DayOfWeek day)
        {
            DateTime dateStart = Convert.ToDateTime(start);
            DateTime dateFinish = Convert.ToDateTime(finish);
            Assert.IsTrue(stocks.GetStockByQuotation(dateStart, dateFinish, day) != null);
        }
    }
}