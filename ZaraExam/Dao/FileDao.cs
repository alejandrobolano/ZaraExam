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
    public class FileDao
    {
        private static FileDao instance = null;
        private static readonly object padlock = new object();
        private static List<Stock> stocks;
        private StocksLogical logical;

        private FileDao()
        {
            logical = new StocksLogical();
            stocks = logical.GetStocks();
        }

        public static FileDao Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new FileDao();
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
