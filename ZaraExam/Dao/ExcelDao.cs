using LinqToExcel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZaraExam.Logical;
using ZaraExam.Model;

namespace ZaraExam.Dao
{
    class ExcelDao
    {
        private static ExcelDao instance = null;
        private static readonly object padlock = new object();
        private static List<Stock> stocks;
        private StocksLogical logical;

        private ExcelDao()
        {
            logical = new StocksLogical();
            stocks = logical.GetStocks();
        }

        public static ExcelDao Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new ExcelDao();
                    }
                    return instance;
                }
            }
        }

        public static List<Stock> GetStocks()
        {
            return stocks;
        }

    }
}
