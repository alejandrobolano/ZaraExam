﻿
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading;
using ZaraExam.Dao;
using ZaraExam.Util;

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
            FileDao f = FileDao.Instance;

            DateTime start, finish;
            DayOfWeek day;
            float input, broker;
            DataZara(out start, out finish, out day, out input, out broker);

            Console.WriteLine(rm.GetString("type_operation"));
            int operation = Convert.ToInt32(Console.ReadLine());
            if (operation == 1)
            {
                ManualOperation(rm, start, finish, day);
            }
            else
            {
                WriteConsoleResult(rm, start, finish, day, input, broker);
            }
            LOG.WarnFormat("Application completed in {0}ms", timer.ElapsedMilliseconds);
            Console.ReadKey();

        }

        private static void ManualOperation(ResourceManager rm, DateTime start, DateTime finish, DayOfWeek day)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("Fecha").Append(";")
                .Append("Cierre").Append(";")
                .Append("Apertura").Append(";");
            var path = Helper.FILEFILTERPATH;
            if (File.Exists(path))
                File.Delete(path);
            FileDao.GetFileX.AddRow(builder.ToString());

            foreach (var item in FileDao.GetStocksLogical.GetStockByQuotation(start, finish, day))
            {
                FileDao.GetFileX.AddRow(Helper.ConvertStockToString(item));
            }
            LOG.Info(rm.GetString("new_file"));
        }

        private static void WriteConsoleResult(ResourceManager rm, DateTime start, DateTime finish, DayOfWeek day, float input, float broker)
        {
            var operations = FileDao.GetOperations;
            Console.WriteLine(rm.GetString("date_start") + start.ToShortDateString());
            Console.WriteLine(rm.GetString("date_finish") + finish.ToShortDateString());
            Console.WriteLine();
            Console.WriteLine(rm.GetString("earn_total") +
                operations.TotalEarnings(start, finish, day, input, broker));
        }

        private static void DataZara(out DateTime start, out DateTime finish, out DayOfWeek day, out float input, out float broker)
        {
            start = Convert.ToDateTime("01/05/2001");
            finish = Convert.ToDateTime("28/12/2017");
            day = DayOfWeek.Thursday;
            input = 50f;
            broker = 0.02f;
        }

    }
}
