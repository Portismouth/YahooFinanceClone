using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace YahooFinance
{
    public class WebRequest
    {
        public static async Task GetMarketData(string TimeSeries, string Symbol, string Interval, Action<Dictionary<string, object>> Callback)
        {
            // Create a temporary HttpClient connection.
            using (var Client = new HttpClient())
            {
                try
                {
                    Client.BaseAddress = new Uri($"https://www.alphavantage.co/query?function={TimeSeries}&symbol={Symbol}&interval={Interval}&apikey=0W8NF5GTPHPJ30IL");
                    HttpResponseMessage Response = await Client.GetAsync(""); // Make the actual API call.
                    Response.EnsureSuccessStatusCode(); // Throw error if not successful.
                    string StringResponse = await Response.Content.ReadAsStringAsync(); // Read in the response as a string.

                    // Then parse the result into JSON and convert to a dictionary that we can use.
                    // DeserializeObject will only parse the top level object, depending on the API we may need to dig deeper and continue deserializing
                    Dictionary<string, object> JsonResponse = JsonConvert.DeserializeObject<Dictionary<string, object>>(StringResponse);

                    // Finally, execute our callback, passing it the response we got.
                    Callback(JsonResponse);
                }
                catch (HttpRequestException e)
                {
                    // If something went wrong, display the error.
                    Console.WriteLine($"Request exception: {e.Message}");
                }
            }
        }

        public static async Task FindStockData( Action<Dictionary<string, string>> Callback)
        {
            // Create a temporary HttpClient connection.
            using (var Client = new HttpClient())
            {
                try
                {
                    Client.BaseAddress = new Uri($"https://api.iextrading.com/1.0/ref-data/symbols");
                    HttpResponseMessage Response = await Client.GetAsync(""); // Make the actual API call.
                    Response.EnsureSuccessStatusCode(); // Throw error if not successful.
                    string StringResponse = await Response.Content.ReadAsStringAsync(); // Read in the response as a string.
                    System.Console.WriteLine(StringResponse);
                    // Then parse the result into JSON and convert to a dictionary that we can use.
                    // DeserializeObject will only parse the top level object, depending on the API we may need to dig deeper and continue deserializing
                    Dictionary<string, string> JsonResponse = JsonConvert.DeserializeObject<List<KeyValuePair<string,string>>>(StringResponse).ToDictionary(x => x.Key, y => y.Value);

                    // Finally, execute our callback, passing it the response we got.
                    Callback(JsonResponse);
                }
                catch (HttpRequestException e)
                {
                    // If something went wrong, display the error.
                    Console.WriteLine($"Request exception: {e.Message}");
                }
            }
        }
        // public static async Task MostActive(Action<List<KeyValuePair<string, string>>> Callback)
        // {
        //     using (var Client = new HttpClient())
        //     {
        //         try
        //         {
        //             Client.BaseAddress = new Uri($"https://api.iextrading.com/1.0/stock/market/list/mostactive");
        //             HttpResponseMessage Response = await Client.GetAsync(""); // Make the actual API call.
        //             Response.EnsureSuccessStatusCode(); // Throw error if not successful.
        //             string StringResponse = await Response.Content.ReadAsStringAsync(); // Read in the response as a string.

        //             // Then parse the result into JSON and convert to a dictionary that we can use.
        //             // DeserializeObject will only parse the top level object, depending on the API we may need to dig deeper and continue deserializing
        //             var JsonResponse = JsonConvert.DeserializeObject<List<KeyValuePair<string, string>>>(StringResponse);

        //             // Finally, execute our callback, passing it the response we got.
        //             Callback(JsonResponse);
        //         }
        //         catch (HttpRequestException e)
        //         {
        //             // If something went wrong, display the error.
        //             Console.WriteLine($"Request exception: {e.Message}");
        //         }
        //     }
        // }
    }
}