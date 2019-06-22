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
        private FileDao file;
        public StocksLogical()
        {
            file = FileDao.Instance;
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
        /// Method without use
        /// </summary>
        /// <param name="start"></param>
        /// <param name="finish"></param>
        /// <param name="day"></param>
        public void Method(DateTime start, DateTime finish, DayOfWeek day)
        {
            bool b = false;
            DateTime dateNext;
            int year = start.Year;
            int month = start.Month;
            var stocks = FileDao.GetStocks();

            //Reverse to list of stocks
            stocks.Reverse();

            while (!b)
            {
                dateNext = GetLastDayOfMonthBy(year, month, day).AddDays(1);
                if (dateNext == finish ||
                    GetLastDayOfMonthBy(year, month, day) == finish)
                {
                    b = true;
                }

                bool a = false;
                while (!a)
                {
                    dateNext = GetLastDayOfMonthBy(year, month, day).AddDays(1);
                    if (dateNext == finish ||
                        GetLastDayOfMonthBy(year, month, day) == finish)
                    {
                        b = true;
                        a = true;
                    }
                    //En caso de que esté pues tomar el valor de apertura, que ahora no se como usarlo
                    var stock = stocks.Where(x => x.Date == dateNext).FirstOrDefault();
                    if (stock != null)
                    {
                        Console.WriteLine(stock.Date.ToShortDateString());
                        Console.WriteLine(Convert.ToString(stock.Opening));
                        a = true;
                    }
                }

                month++;
                if (month == 13)
                {
                    month = 1;
                    year++;
                }
            }
        }

        /// <summary>
        /// GetLastDayOfMonthList
        /// </summary>
        /// <param name="start"></param>
        /// <param name="finish"></param>
        /// <param name="day"></param>
        /// <returns></returns>
        public List<DateTime> GetLastDayOfMonthList(DateTime start, DateTime finish, DayOfWeek day)
        {
            var selectedDates = new List<DateTime>();
            DateTime dateTimeExpected;
            for (var date = start; date < finish; date = date.AddMonths(1))
            {
                dateTimeExpected = GetLastDayOfMonthBy(date.Year, date.Month, DayOfWeek.Thursday);
                if (dateTimeExpected == finish)
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
            var stocks = FileDao.GetStocks();

            DateTime dateNext;
            var listLastDays = GetLastDayOfMonthList(start, finish, day);

            foreach (var item in listLastDays)
            {
                var stock = new Stock();
                dateNext = item.AddDays(1);

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
