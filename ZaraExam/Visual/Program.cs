
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
            ResourceManager resourceManager = new ResourceManager("ZaraExam.Strings", a);
            timer = Stopwatch.StartNew();

            //ThreadStart threadStart = delegate { LoadInstance(rm); };
            //Thread thread = new Thread(threadStart);
            //thread.Start();
            
            LoadInstance(resourceManager);

            DateTime dateStart, dateFinish;
            DayOfWeek day;
            float input, broker;
            DataZara(out dateStart, out dateFinish, out day, out input, out broker);

            Console.WriteLine(resourceManager.GetString("type_operation"));
            int operation = Convert.ToInt32(Console.ReadLine());
            if (operation == 1)
            {
                ManualOperation(resourceManager, dateStart, dateFinish, day);
            }
            else
            {
                WriteConsoleResult(resourceManager, dateStart, dateFinish, day, input, broker);
            }
            LOG.WarnFormat("Application completed in {0}ms", timer.ElapsedMilliseconds);
            Console.ReadKey();
        }

        private static void LoadInstance(ResourceManager rm)
        {
            LOG.Info(rm.GetString("loading"));
            FileDao f = FileDao.Instance;
        }

        private static void ManualOperation(ResourceManager rm, DateTime start, DateTime finish, DayOfWeek day)
        {
            StringBuilder builder = new StringBuilder();
            AppendFirstRow(builder);
            var path = Helper.FILEFILTERPATH;
            if (File.Exists(path))
                File.Delete(path);
            FileDao.GetFileX.AddRow(builder.ToString());
            var lastStock = FileDao.GetStocks.ToArray()[0];

            foreach (var item in FileDao.GetStocksLogical.GetStockByQuotation(start, finish, day))
            {
                FileDao.GetFileX.AddRow(Helper.ConvertStockToString(item));
            }
            builder = AppendLastRows(lastStock);
            LOG.Info(rm.GetString("new_file"));
        }

        private static StringBuilder AppendLastRows(Model.Stock lastStock)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("Fecha de salida").Append(";");
            FileDao.GetFileX.AddRow(builder.ToString());
            FileDao.GetFileX.AddRow(Helper.ConvertStockToString(lastStock));
            builder = new StringBuilder();
            builder.Append(";").Append(";").Append("Total").Append(";");
            FileDao.GetFileX.AddRow(builder.ToString());
            builder = new StringBuilder();
            builder.Append(";").Append(";").Append("Venta").Append(";");
            FileDao.GetFileX.AddRow(builder.ToString());
            return builder;
        }

        private static void AppendFirstRow(StringBuilder builder)
        {
            builder.Append("Fecha").Append(";")
                            .Append("Cierre").Append(";")
                            .Append("Apertura").Append(";")
                            .Append("Acciones sin broken").Append(";");
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
