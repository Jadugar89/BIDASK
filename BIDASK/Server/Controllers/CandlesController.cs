using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BIDASK.Shared;
using xAPI.Sync;
using xAPI.Responses;
using xAPI.Commands;
using xAPI.Records;
using xAPI.Codes;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BIDASK.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CandlesController : ControllerBase
    {

        private static xAPI.Sync.Server server = xAPI.Sync.Servers.DEMO;

        [HttpGet]
        public IEnumerable<Candle> Get([FromQuery] string userId, [FromQuery] string password, [FromQuery] string Symbol, [FromQuery] int period, [FromQuery] long startTime )
        {
            SyncAPIConnector connector = new SyncAPIConnector(server);
            Credentials credentials = new Credentials(userId, password);
            APICommandFactory.ExecuteLoginCommand(connector, credentials);
            connector.Streaming.Connect();
            PERIOD_CODE pERIOD_CODE = new PERIOD_CODE(period);
            
            ChartLastInfoRecord info = new ChartLastInfoRecord(Symbol, PERIOD_CODE.PERIOD_D1, startTime);

            ChartLastResponse lastResponse = APICommandFactory.ExecuteChartLastCommand(connector, info);

            List<Candle> candles = new List<Candle>();

            foreach (var item in lastResponse.RateInfos)
            {
                Candle candle = new Candle();
                candle.lTime = (long)item.Ctm;
                candle.Open =(double)item.Open;
                candle.Close = candle.Open+(double)item.Close;
                candle.MaxPrice = candle.Open+(double)item.High;
                candle.MinPrice = candle.Open+(double)item.Low;
                candle.Volume = (double)item.Vol;
                candles.Add(candle);
            }


            connector.Streaming.Disconnect();
            APICommandFactory.ExecuteLogoutCommand(connector);
            return candles.ToArray();
        }


    }
}
