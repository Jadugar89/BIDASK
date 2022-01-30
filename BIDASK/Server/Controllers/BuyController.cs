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
    [ApiController]
    [Route("[controller]")]

    public class BuyController : ControllerBase
    {
        private static xAPI.Sync.Server serverData = Servers.DEMO;

        private readonly ILogger<PriceController> _logger;

        public BuyController(ILogger<PriceController> logger)
        {
            _logger = logger;
        }



        [HttpPost]
        public TradeTransactionResponse BuyNow([FromBody] CommandtoApi commandtoApi)
        {
            SyncAPIConnector connector = new SyncAPIConnector(serverData);
            Credentials credentials = new Credentials(commandtoApi.ID, commandtoApi.Password);
            APICommandFactory.ExecuteLoginCommand(connector, credentials);
            connector.Streaming.Connect();
            SymbolResponse symbolResponse = APICommandFactory.ExecuteSymbolCommand(connector, commandtoApi.Symbol);

            double price = symbolResponse.Symbol.Ask.GetValueOrDefault();
            long order = 0;
            string customComment = "my comment";
            long expiration = 0;
            TradeTransInfoRecord ttOpenInfoRecord = new TradeTransInfoRecord(
                TRADE_OPERATION_CODE.BUY,
                TRADE_TRANSACTION_TYPE.ORDER_OPEN,
                price, commandtoApi.sl, commandtoApi.tp, symbolResponse.Symbol.Symbol, commandtoApi.volume, order, customComment, expiration);
            TradeTransactionResponse tradeTransactionResponse = APICommandFactory.ExecuteTradeTransactionCommand(connector, ttOpenInfoRecord);
            if(tradeTransactionResponse.Status.Value)



            connector.Streaming.Disconnect();
            APICommandFactory.ExecuteLogoutCommand(connector);
            return tradeTransactionResponse;
        }
        
    }
}
