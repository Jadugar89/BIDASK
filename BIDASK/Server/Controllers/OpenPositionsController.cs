using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xAPI.Sync;
using xAPI.Responses;
using xAPI.Commands;
using xAPI.Records;
using xAPI.Codes;
using Microsoft.Extensions.Logging;
using BIDASK.Shared;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BIDASK.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OpenPositionsController : ControllerBase
    {
        private static xAPI.Sync.Server server = xAPI.Sync.Servers.DEMO;

        [HttpGet]
        public IEnumerable<TradeOpen> Get([FromQuery] string userId, [FromQuery] string password)
        {

                SyncAPIConnector connector = new SyncAPIConnector(server);
                Credentials credentials = new Credentials(userId, password);
                APICommandFactory.ExecuteLoginCommand(connector, credentials);
                connector.Streaming.Connect();
                TradesResponse tradesResponse = APICommandFactory.ExecuteTradesCommand(connector, true);
                connector.Streaming.Disconnect();
                APICommandFactory.ExecuteLogoutCommand(connector);

                List<TradeOpen> tradeOpens = new List<TradeOpen>();

                foreach (var item in tradesResponse.TradeRecords)
                {
                TradeOpen tradeOpen=new TradeOpen();
                tradeOpen.Symbol = item.Symbol;
                tradeOpen.Typ = (int)item.Cmd;
                tradeOpen.Open_price = (double)item.Open_price;
                tradeOpen.dateTime = new DateTime((long)item.Timestamp);
                tradeOpen.Profit = (double)item.Profit;
                tradeOpens.Add(tradeOpen);
                }
               return tradeOpens.ToArray();
        }

        private  DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }

    }
}
