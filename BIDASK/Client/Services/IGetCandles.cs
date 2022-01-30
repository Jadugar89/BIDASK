using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BIDASK.Shared; 

namespace BIDASK.Client.Services
{
    public interface IGetCandles
    {
        public Task<IEnumerable<Candle>> GetCandlesFromServer(HttpClient Http,string userId, string password, string symbol, int period, long time);
    }
}
