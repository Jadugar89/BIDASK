using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xAPI.Sync;
using xAPI.Responses;
using xAPI.Commands;
using xAPI.Records;
using xAPI.Codes;

namespace BIDASK.Shared
{
    public class PriceXTB
    {

        public string Symbol { get; set; }
        public double Bid { get; set; }
        public double Ask { get; set; }
    }
}
