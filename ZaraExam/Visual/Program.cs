
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

        static void Main(string[] args)
        {
            Assembly a = Assembly.Load("ZaraExam");
            ResourceManager rm = new ResourceManager("ZaraExam.Strings", a);

            Console.WriteLine(rm.GetString("loading"));

            //FileDao f = FileDao.Instance;
            //FileDao.GetStocks();

            Operations operations;
            DateTime start, finish;
            DayOfWeek day;
            float input, broker;
            DataZara(out operations, out start, out finish, out day, out input, out broker);
            WriteConsoleResult(rm, operations, start, finish, day, input, broker);
        }

        private static void WriteConsoleResult(ResourceManager rm, Operations operations, DateTime start, DateTime finish, DayOfWeek day, float input, float broker)
        {
            Console.WriteLine(rm.GetString("date_start") + start.ToShortDateString());
            Console.WriteLine(rm.GetString("date_finish") + finish.ToShortDateString());
            Console.WriteLine();
            Console.WriteLine(rm.GetString("earn_total") +
                operations.TotalEarnings(start, finish, day, input, broker));
            Console.ReadKey();
        }

        private static void DataZara(out Operations operations, out DateTime start, out DateTime finish, out DayOfWeek day, out float input, out float broker)
        {
            operations = new Operations();
            start = Convert.ToDateTime("01/05/2001");
            finish = Convert.ToDateTime("28/12/2017");
            day = DayOfWeek.Thursday;
            input = 50f;
            broker = 0.02f;
        }
    }
}
