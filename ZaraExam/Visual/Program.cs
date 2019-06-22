
using System;
using ZaraExam.Logical;

namespace ZaraExam.Visual
{
    class Program
    {
        static void Main(string[] args)
        {
            StocksLogical logical = new StocksLogical();
            //logical.Method(Convert.ToDateTime("01/05/2001"), Convert.ToDateTime("28/12/2017"), DayOfWeek.Thursday);
            
            var list = logical.GetLastDayOfMonthList(Convert.ToDateTime("01/05/2001"), Convert.ToDateTime("28/12/2017"), DayOfWeek.Thursday);
            var listStocks = logical.GetStockByQuotation(Convert.ToDateTime("01/05/2001"), Convert.ToDateTime("28/12/2017"), DayOfWeek.Thursday);
            foreach (var item in listStocks)
            {
                Console.WriteLine(Convert.ToString(item.Date));
            }

            Console.ReadLine();
        }
    }
}
