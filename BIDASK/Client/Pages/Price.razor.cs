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

        private PriceXTB prices;
        private bool Inited = false;
        private string[] ArrSymbols;
        private string Content;

        private bool TableOK = false;

        public Price()
        {

        }

        
        protected override async Task OnInitializedAsync()
        {
            ArrSymbols = await Http.GetFromJsonAsync<string[]>("price/symbols");
            Inited = true;
        }
        

        public async Task Testowa()
        {
            try
            {
                prices = await Http.GetFromJsonAsync<PriceXTB>("price?&symbol=US100");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }
        public async Task Buy()
        {
           
            HttpResponseMessage httpResponseMessage;
            CommandtoApi commandtoApi = new CommandtoApi("US100");
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
