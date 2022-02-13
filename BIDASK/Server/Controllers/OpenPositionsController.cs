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
using BIDASK.Server.Services;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BIDASK.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OpenPositionsController : ControllerBase
    {

        private static xAPI.Sync.Server serverData = Servers.DEMO;

        private readonly ILogger<OpenPositionsController> _logger;
        private readonly IUtilityService _UtilityService;

        public OpenPositionsController(ILogger<OpenPositionsController> logger,IUtilityService utilityService)
        {
            _logger = logger;
            _UtilityService = utilityService;
        }

        [HttpGet]
        public async Task<IEnumerable<TradeOpen>> Get()
        {

            SyncAPIConnector connector =  await _UtilityService.GetConnected();
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
                tradeOpen.dateTime = UnixTimeToDateTime((long)item.Open_time);
                if (item.Profit == null)
                {
                    tradeOpen.Profit = 0.0;
                }
                else
                {
                    tradeOpen.Profit = (double)item.Profit;
                }
                tradeOpens.Add(tradeOpen);
                }
               return tradeOpens.ToArray();
        }

        /// <summary>
        /// Convert Unix time value to a DateTime object.
        /// </summary>
        /// <param name="unixtime">The Unix time stamp you want to convert to DateTime.</param>
        /// <returns>Returns a DateTime object that represents value of the Unix time.</returns>
        public DateTime UnixTimeToDateTime(long unixtime)
        {
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddMilliseconds(unixtime).ToLocalTime();

            return dtDateTime;
        }

    }
}
