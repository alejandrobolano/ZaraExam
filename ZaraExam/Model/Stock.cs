using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZaraExam.Model
{
    public class Stock
    {
        public DateTime Date { get; set; }
        public decimal Opening { get; set; }
        public decimal Closing { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Stock stock &&
                   Date == stock.Date &&
                   Opening == stock.Opening &&
                   Closing == stock.Closing;
        }

        public override int GetHashCode()
        {
            var hashCode = -1991093572;
            hashCode = hashCode * -1521134295 + Date.GetHashCode();
            hashCode = hashCode * -1521134295 + Opening.GetHashCode();
            hashCode = hashCode * -1521134295 + Closing.GetHashCode();
            return hashCode;
        }
    }
}
