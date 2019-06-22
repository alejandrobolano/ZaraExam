
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Resources;
using System.Threading;
using ZaraExam.Dao;
using ZaraExam.Logical;

namespace ZaraExam.Visual
{
    class Program
    {
        private static readonly log4net.ILog LOG = log4net.LogManager.GetLogger
        (MethodBase.GetCurrentMethod().DeclaringType);
        private static Stopwatch timer;
        static void Main(string[] args)
        {
            Assembly a = Assembly.Load("ZaraExam");
            ResourceManager rm = new ResourceManager("ZaraExam.Strings", a);
            timer = Stopwatch.StartNew();
            LOG.Info(rm.GetString("loading"));

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
            LOG.InfoFormat("Application completed in {0}ms", timer.ElapsedMilliseconds);
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
