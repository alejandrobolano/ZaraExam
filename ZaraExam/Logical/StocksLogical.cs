﻿using LinqToExcel;
using System;
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
        /// Get all stocks of file
        /// </summary>
        /// <returns></returns>
        public List<Stock> GetStocks()
        {
            List<string> lines = File.ReadLines(Helper.FILEPATH).ToList();
            List<Stock> stocks = new List<Stock>();
            lines.RemoveAt(0);
            foreach (var item in lines)
            {
                stocks.Add(Helper.ConvertStringToStock(item));
            }
            return stocks;
        }

        public void Method(DateTime start, DateTime finish, DayOfWeek day)
        {
            bool b = false;
            DateTime dateNext;
            int year = start.Year;
            int month = start.Month;
            var stocks = GetStocks();

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
                        Add(stock);
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

        public List<Stock> GetStockByQuotation(DateTime start, DateTime finish, DayOfWeek day)
        {
            List<Stock> stocksReduced = new List<Stock>();
            var stocks = GetStocks(); // Coger del singleton para cargar rapido
            
            DateTime dateNext;

            foreach (var item in GetLastDayOfMonthList(start, finish, day))
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

        public void Add(Stock stock)
        {
            StringBuilder stringStock = new StringBuilder();
            stringStock.Append(stock.Date.ToShortDateString()).Append("")
                .Append(Convert.ToString(stock.Opening));

            using (StreamWriter writer = File.AppendText("stocks.txt"))
            {
                Write(stringStock.ToString(), writer);
            }

        }

        void Write(string message, TextWriter w)
        {
            w.WriteLine("{0}", message);
        }


    }
}
