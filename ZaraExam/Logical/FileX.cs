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
            List<Stock> stocks = new List<Stock>();
            try
            {
                List<string> lines = File.ReadLines(Helper.FILEPATH).ToList();
                lines.RemoveAt(0);
                foreach (var item in lines)
                {
                    stocks.Add(Helper.ConvertStringToStock(item));
                }
            }
            catch (FileLoadException f)
            {
                Console.WriteLine(f.Message);
                throw;
            }
            catch(AccessViolationException a)
            {
                Console.WriteLine(a.Message);
                throw;
            }
            catch(FileNotFoundException f)
            {
                Console.WriteLine(f.Message);
                throw;
            }
            
            return stocks;
        }
    }
}
