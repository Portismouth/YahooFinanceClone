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

            ViewBag.DJI = DJI; 
            return View();
        }
    }
}
