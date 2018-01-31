using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Configuration;

namespace YahooFinance.Controllers
{
    public class HomeController : Controller
    {
        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            var DJI = new Dictionary<string, object>();
            WebRequest.GetMarketData("TIME_SERIES_INTRADAY", "DJI", "1min", JsonResponse =>
                {
                    DJI = JsonResponse;        
                }
            ).Wait();

            var SP500 = new Dictionary<string, object>();
            WebRequest.GetMarketData("TIME_SERIES_INTRADAY", "^GSPC", "1min", JsonResponse =>
                {
                    SP500 = JsonResponse;
                }
            ).Wait();

            var Nasdaq = new Dictionary<string, object>();
            WebRequest.GetMarketData("TIME_SERIES_INTRADAY", "^IXIC", "1min", JsonResponse =>
                {
                    Nasdaq = JsonResponse;
                }
            ).Wait();

            var RUT = new Dictionary<string, object>();
            WebRequest.GetMarketData("TIME_SERIES_INTRADAY", "RUT", "1min", JsonResponse =>
                {
                    RUT = JsonResponse;
                }
            ).Wait();

            ViewBag.DJI = DJI;
            ViewBag.SP500 = SP500;
            ViewBag.Nasdaq = Nasdaq;
            ViewBag.RUT = RUT; 

            // var MostActive = new List<KeyValuePair<string, string>>();
            // WebRequest.MostActive(JsonResponse => 
            //     {
            //         MostActive = JsonResponse;
            //     }
            // ).Wait();

            // ViewBag.MostActive = MostActive;
            return View();
        }
        [HttpPost]
        [Route("stock-index/find")]
        public object Find(string Search)
        {
            var AllIdx = new Dictionary<string, string>();
            WebRequest.FindStockData(JsonResponse =>
                {
                    AllIdx = JsonResponse;
                }
            ).Wait();
            return AllIdx.ContainsValue(Search);
        }
    }
}
