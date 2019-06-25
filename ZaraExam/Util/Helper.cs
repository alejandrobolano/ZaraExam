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
        public static string FILEFILTERPATH = ConfigurationManager.AppSettings.Get("FileFiltered");

        public static Stock ConvertStringToStock(string stream)
        {
            Stock stock = new Stock();
            string[] streamList = stream.Split(';');
            //stock.Date = DateTime.ParseExact(streamList[0], "dd-MMM-yyyy", CultureInfo.CreateSpecificCulture("es-US"));
            stock.Date = ParseTime(streamList[0]);
            stock.Closing = Decimal.Parse(streamList[1], CultureInfo.CreateSpecificCulture("es-US"));
            stock.Opening = Decimal.Parse(streamList[2], CultureInfo.CreateSpecificCulture("es-US"));

            return stock;
        }

        private static DateTime ParseTime(string date)
        {
            var arrayDatos = date.Split(Convert.ToChar("-"));
            var auxDate = DatesDictionary()[arrayDatos[1]];
            return Convert.ToDateTime(arrayDatos[0] + "/" + auxDate + "/" + arrayDatos[2]); ;
        }

        private static IDictionary<string, int> DatesDictionary()
        {
            IDictionary<string, int> dateDictionary = new Dictionary<string, int>();
            dateDictionary.Add("ene", 1);
            dateDictionary.Add("feb", 2);
            dateDictionary.Add("mar", 3);
            dateDictionary.Add("abr", 4);
            dateDictionary.Add("may", 5);
            dateDictionary.Add("jun", 6);
            dateDictionary.Add("jul", 7);
            dateDictionary.Add("ago", 8);
            dateDictionary.Add("sep", 9);
            dateDictionary.Add("oct", 10);
            dateDictionary.Add("nov", 11);
            dateDictionary.Add("dic", 12);

            return dateDictionary;
        }

        public static string ConvertStockToString(Stock stock)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(stock.Date.ToShortDateString())
                .Append(";").Append(stock.Closing.ToString())
                .Append(";").Append(stock.Opening.ToString());

            return stringBuilder.ToString();
        }
    }
}
