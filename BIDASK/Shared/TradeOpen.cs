using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIDASK.Shared
{
    public class TradeOpen
    {
        public string  Symbol { get; set; }
        public int Typ { get; set; }    
        public double Open_price { get; set; }
        public DateTime dateTime { get; set; }
        public double Profit { get; set; }

        public string GetTypTransaction()
        {
            switch (Typ)
            {
                case 0:
                    return "Buy";
                case 1:
                    return "Sell";
                default:
                    return "N/N";

            }

        }
    }
}
