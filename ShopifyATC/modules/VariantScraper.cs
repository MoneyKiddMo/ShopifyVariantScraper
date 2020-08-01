using System;
using System.Reflection;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Text;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace ShopifyATC.modules
{
    public class VariantScraper: ModuleBase<SocketCommandContext>
    {
        
        [Command("variant", RunMode = RunMode.Async)]
        public async Task Variant(string varUrl)
        {

            var shopifyId = await GrabShopifyVar(varUrl);
            var bip = "";



            bip += string.Join("\n", Product.Variants.Select(a => a.Id));


            static async Task<Temperatures> GrabShopifyVar(string varUrl)
            {
                var httpClient = new HttpClient();

                var variantGrabRequest = new HttpRequestMessage
                {
                    RequestUri = new Uri(varUrl += ".json"),
                    Method = HttpMethod.Get
                };
                variantGrabRequest.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/84.0.4147.105 Safari/537.36");

                var variantGrabResponse = await httpClient.SendAsync(variantGrabRequest);

                var variantGrabContent = 
                    await variantGrabResponse.Content.ReadAsStringAsync();

                var variantGrabResponseData =
                    JsonConvert.DeserializeObject<Temperatures>(variantGrabContent);
                return variantGrabResponseData;
            }

            await Context.Channel.SendMessageAsync(bip);



        }
    }


}