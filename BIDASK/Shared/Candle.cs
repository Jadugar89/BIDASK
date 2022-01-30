using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIDASK.Shared
{
    public class Candle
    {
        public long lTime { get; set; }
        public double Open { get; set; }
        public double Close { get; set; }
        public double MinPrice { get; set; }
        public double MaxPrice { get; set; }
        public double Volume { get; set; }
    }
}
