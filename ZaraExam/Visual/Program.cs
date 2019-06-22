
using System;
using System.ComponentModel;
using System.Reflection;
using System.Resources;
using System.Threading;
using ZaraExam.Dao;
using ZaraExam.Logical;

namespace ZaraExam.Visual
{
    class Program
    {
        private ResXResourceReader resxReader;

        static void Main(string[] args)
        {
            Assembly a = Assembly.Load("ZaraExam");
            ResourceManager rm = new ResourceManager("ZaraExam.Strings", a);


            Console.WriteLine(rm.GetString("loading"));
            //FileDao f = FileDao.Instance;
            //FileDao.GetStocks();
            Operations operations = new Operations();
            DateTime start = Convert.ToDateTime("01/05/2001");
            DateTime finish = Convert.ToDateTime("28/12/2017");
            DayOfWeek day = DayOfWeek.Thursday;
            float input = 50f;
            float broker = 0.02f;

            Console.WriteLine(rm.GetString("date_start") + start.ToShortDateString());
            Console.WriteLine(rm.GetString("date_finish") + finish.ToShortDateString());
            Console.WriteLine();
            Console.WriteLine(rm.GetString("earn_total") +
                operations.TotalEarnings(start, finish, day, input, broker));
            Console.ReadKey();
        }

    }
}
