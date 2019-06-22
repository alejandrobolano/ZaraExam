using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZaraExam.Model;
using ZaraExam.Util;

namespace ZaraExam.Logical
{
    class FileX
    {
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
    }
}
