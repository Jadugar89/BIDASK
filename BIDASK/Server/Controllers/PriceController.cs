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
using BIDASK.Server.Services;

namespace BIDASK.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class PriceController : ControllerBase
    {
        

        private readonly ILogger<PriceController> _logger;
        private readonly IUtilityService _UtilityService;

        public PriceController(ILogger<PriceController> logger,IUtilityService utilityService)
        {
            _logger = logger;
            _UtilityService = utilityService;
        }


        [HttpGet]
        public async Task<PriceXTB> Get(string symbol)
        {
            xAPI.Sync.Server serverData = Servers.DEMO;

            SyncAPIConnector connector = await _UtilityService.GetConnected();

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
        public async Task<IEnumerable<string>> Get()
        {
            SyncAPIConnector connector = await _UtilityService.GetConnected();

            AllSymbolsResponse allSymbolsResponse= APICommandFactory.ExecuteAllSymbolsCommand(connector, true);
            
            connector.Streaming.Disconnect();
            APICommandFactory.ExecuteLogoutCommand(connector);
            return allSymbolsResponse.SymbolRecords.Where(x=>x.CategoryName=="IND").Select(x => x.Symbol).ToArray(); 
        }

    }
}
