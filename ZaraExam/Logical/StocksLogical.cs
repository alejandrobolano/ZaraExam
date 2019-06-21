using LinqToExcel;
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
        public DateTime LastDayOfMonthBy(int year, int month, DayOfWeek day)
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
                dateNext = LastDayOfMonthBy(year, month, day).AddDays(1);
                if (dateNext == finish ||
                    LastDayOfMonthBy(year, month, day) == finish)
                {
                    b = true;
                }

                bool a = false;
                while (!a)
                {
                    dateNext = LastDayOfMonthBy(year, month, day).AddDays(1);
                    if (dateNext == finish ||
                        LastDayOfMonthBy(year, month, day) == finish)
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
