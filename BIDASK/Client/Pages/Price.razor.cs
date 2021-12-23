using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using xAPI.Records;
using xAPI.Sync;
using xAPI.Responses;
using System.Net.Http;
using BIDASK.Shared;
using Newtonsoft.Json;

namespace BIDASK.Client.Pages
{
    public partial class Price
    {
        private static Server serverData = Servers.DEMO;
        private static string userId = "12836146";
        private static string password = "xoh67782";
        private PriceXTB prices;
        private bool Inited = false;
        private string[] ArrSymbols;
        private string Content;

        public Price()
        {

        }

        
        protected override async Task OnInitializedAsync()
        {
            ArrSymbols = await Http.GetFromJsonAsync<string[]>("price/symbols?userId=" + userId + "&password=" + password);
            Inited = true;
        }
        

        public async Task Testowa()
        {
            try
            {
                prices = await Http.GetFromJsonAsync<PriceXTB>("price?userId=" + userId + "&password=" + password + "&symbol=US100");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }
        public async Task Buy()
        {
           
            HttpResponseMessage httpResponseMessage;
            CommandtoApi commandtoApi = new CommandtoApi(userId,password,"US100");
            Console.WriteLine("Funkcja Buy");
            try
            {

                httpResponseMessage= await Http.PostAsJsonAsync("Buy", commandtoApi);
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    Content = "ok";
                }
                
                Console.WriteLine("ok");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            


        }

    }
}
