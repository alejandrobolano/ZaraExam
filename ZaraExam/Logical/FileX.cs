using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using ZaraExam.Model;
using ZaraExam.Util;

namespace ZaraExam.Logical
{
    class FileX
    {
        private static readonly log4net.ILog LOG = log4net.LogManager.GetLogger
        (MethodBase.GetCurrentMethod().DeclaringType);
        private ResourceManager rm;

        public FileX()
        {
            Assembly a = Assembly.Load("ZaraExam");
            rm = new ResourceManager("ZaraExam.Strings", a);
        }

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
                LOG.Info(rm.GetString("file_succesfull"));
            }
            catch (FileLoadException f)
            {
                LOG.Error(f.Message);
                throw;
            }
            catch(AccessViolationException a)
            {
                LOG.Error(a.Message);
                throw;
            }
            catch(FileNotFoundException f)
            {
                LOG.Error(f.Message);
                throw;
            }
            
            return stocks;
        }
    }
}
