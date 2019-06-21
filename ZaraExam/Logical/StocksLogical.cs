using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


    }
}
