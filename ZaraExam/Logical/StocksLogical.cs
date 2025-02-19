﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZaraExam.Dao;
using ZaraExam.Model;
using ZaraExam.Util;

namespace ZaraExam.Logical
{
    public class StocksLogical
    {
        
        public StocksLogical()
        {
        }

        /// <summary>
        /// Obtain last day of month ----------
        /// For this exam is DayOfWeek.Thursday
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <returns></returns>
        public DateTime GetLastDayOfMonthBy(int year, int month, DayOfWeek day)
        {
            DateTime lastDay = new DateTime(year, month, DateTime.DaysInMonth(year, month));
            while (lastDay.DayOfWeek != day)
                lastDay = lastDay.AddDays(-1);
            return lastDay;
        }              

        /// <summary>
        /// GetLastDayOfMonthList
        /// </summary>
        /// <param name="dateStart"></param>
        /// <param name="dateFinish"></param>
        /// <param name="dayOfWeek"></param>
        /// <returns></returns>
        private List<DateTime> GetLastDayOfMonthList(DateTime dateStart, DateTime dateFinish, DayOfWeek dayOfWeek)
        {
            var selectedDates = new List<DateTime>();
            DateTime dateTimeExpected;
            for (var date = dateStart; date < dateFinish; date = date.AddMonths(1))
            {
                dateTimeExpected = GetLastDayOfMonthBy(date.Year, date.Month, DayOfWeek.Thursday);
                if (dateTimeExpected == dateFinish)
                {
                    break;
                }
                selectedDates.Add(dateTimeExpected);
            }
            return selectedDates;
        }

        /// <summary>
        /// Get all stock that i need for the math formula
        /// </summary>
        /// <param name="start"></param>
        /// <param name="finish"></param>
        /// <param name="day"></param>
        /// <returns></returns>
        public List<Stock> GetStockByQuotation(DateTime start, DateTime finish, DayOfWeek day)
        {
            List<Stock> stocksReduced = new List<Stock>();
            var stocks = FileDao.GetStocks;
            DateTime dateNext;
            var listLastDays = GetLastDayOfMonthList(start, finish, day);
            foreach (var date in listLastDays)
            {
                var stock = new Stock();
                dateNext = date.AddDays(1);
                while (true)
                {
                    stock = stocks.Where(x => x.Date == dateNext).FirstOrDefault();
                    if (stock != null)
                    {
                        stocksReduced.Add(stock);
                        break;
                    }
                    else
                    {
                        dateNext = dateNext.AddDays(1);
                    }
                }
            }
            return stocksReduced;
        }
    }
}
