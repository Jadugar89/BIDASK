using Microsoft.AspNetCore.Http;
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

namespace BIDASK.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class PriceController : ControllerBase
    {
        

        private readonly ILogger<PriceController> _logger;

        public PriceController(ILogger<PriceController> logger)
        {
            _logger = logger;
        }


        [HttpGet]
        public PriceXTB Get([FromQuery] string userId, [FromQuery] string password,[FromQuery] string symbol)
        {
            xAPI.Sync.Server serverData = Servers.DEMO;
            Console.WriteLine(serverData.MainPort);
            SyncAPIConnector connector = new SyncAPIConnector(serverData);
             Credentials credentials = new Credentials(userId, password);
             APICommandFactory.ExecuteLoginCommand(connector, credentials);
            connector.Streaming.Connect();
            

            SymbolResponse symbolResponse= APICommandFactory.ExecuteSymbolCommand(connector, symbol);
            connector.Streaming.Disconnect();
             APICommandFactory.ExecuteLogoutCommand(connector);
            PriceXTB pricemoje = new PriceXTB();
            pricemoje.Ask = (double) symbolResponse.Symbol.Ask;
            pricemoje.Bid = (double) symbolResponse.Symbol.Bid;
            pricemoje.Symbol = symbolResponse.Symbol.Symbol;
            return pricemoje;

        }

        [HttpGet("symbols")]
        public IEnumerable<string> Get([FromQuery] string userId, [FromQuery] string password)
        {
            xAPI.Sync.Server serverData = Servers.DEMO;
            SyncAPIConnector connector = new SyncAPIConnector(serverData);
            Credentials credentials = new Credentials(userId, password);
            APICommandFactory.ExecuteLoginCommand(connector, credentials);
            connector.Streaming.Connect();

            AllSymbolsResponse allSymbolsResponse= APICommandFactory.ExecuteAllSymbolsCommand(connector, true);
            
            connector.Streaming.Disconnect();
            APICommandFactory.ExecuteLogoutCommand(connector);
            return allSymbolsResponse.SymbolRecords.Where(x=>x.CategoryName=="IND").Select(x => x.Symbol).ToArray(); 
        }

        

    }
}
