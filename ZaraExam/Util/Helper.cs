using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZaraExam.Model;

namespace ZaraExam.Util
{
    class Helper
    {
        public static string FILEPATH = ConfigurationManager.AppSettings.Get("File");

        public static Stock ConvertStringToStock(string stream)
        {
            Stock stock = new Stock();
            string[] streamList = stream.Split(';');
            stock.Date = DateTime.ParseExact(streamList[0], "dd-MMM-yyyy", CultureInfo.CreateSpecificCulture("es-US"));
            stock.Closing = Decimal.Parse(streamList[1], CultureInfo.CreateSpecificCulture("es-US"));
            stock.Opening = Decimal.Parse(streamList[2], CultureInfo.CreateSpecificCulture("es-US"));

            return stock;
        }
    }
}
