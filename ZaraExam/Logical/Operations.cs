﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZaraExam.Dao;

namespace ZaraExam.Logical
{
    public class Operations
    {
        
        public Operations()
        {

        }

        /// <summary>
        /// Get total of actions GetTotalActionsMath
        /// </summary>
        /// <param name="start"></param>
        /// <param name="finish"></param>
        /// <param name="day"></param>
        /// <param name="input"></param>
        /// <param name="broken"></param>
        /// <returns></returns>
        private decimal GetTotalActionsMath(DateTime start, DateTime finish, DayOfWeek day, float input, float broken)
        {
            decimal action = 0;
            decimal temp;    
            
            var stocks = FileDao.GetStocksLogical.GetStockByQuotation(start, finish, day);
            foreach (var item in stocks)
            {
                temp = Convert.ToDecimal(input) / item.Opening;
                action += decimal.Round(temp - (temp * Convert.ToDecimal(broken)), 3);
            }
            return action;
        }

        /// <summary>
        /// Total earn
        /// </summary>
        /// <param name="start"></param>
        /// <param name="finish"></param>
        /// <param name="day"></param>
        /// <param name="input"></param>
        /// <param name="broken"></param>
        /// <returns></returns>
        public decimal TotalEarnings(DateTime start, DateTime finish, DayOfWeek day, float input, float broken)
        {
            var actions = GetTotalActionsMath(start, finish, day, input, broken);
            var lastStock = FileDao.GetStocks.Where(x => x.Date == finish).FirstOrDefault();
            return decimal.Round(actions * lastStock.Closing,3);
        }
    }
}
