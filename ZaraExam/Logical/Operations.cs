using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZaraExam.Dao;

namespace ZaraExam.Logical
{
    class Operations
    {
        FileDao excelDao = FileDao.Instance;

        public decimal GetTotalActionsMath(DateTime start, DateTime finish, DayOfWeek day, float input, float broken)
        {
            decimal action = 0;
            decimal temp = 0;
            
            var stocks = GetStockByQuotation(start, finish, day);
            foreach (var item in stocks)
            {
                temp = Convert.ToDecimal(input) / item.Opening;
                action += temp - (temp * Convert.ToDecimal(broken));
            }
            return action;
        }
    }
}
