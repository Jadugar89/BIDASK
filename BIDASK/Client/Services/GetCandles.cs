using BIDASK.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;


namespace BIDASK.Client.Services
{
    public class GetCandles : IGetCandles
    {
        

        public async Task<IEnumerable<Candle>> GetCandlesFromServer(HttpClient Http, string userId, string password, string symbol, int period, long time)
        {
            Console.WriteLine("Task Get Candles");
           
            IEnumerable<Candle> Candles = null;
            
            try
            {
               Candles = await Http.GetFromJsonAsync<Candle[]>("Candles?userId="+userId+"&password="+ password + "&symbol="+ symbol + "&period="+ period + "&startTime="+ time);
                Console.WriteLine("Funkcja Get Candles");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return (IEnumerable<Candle>)Candles;

        }
    }
}
