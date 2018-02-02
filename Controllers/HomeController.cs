using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Globalization;
using YahooFinance.Controllers;
using YahooFinance.Models;

namespace YahooFinance.Controllers
{
    public class HomeController : Controller
    {
        private YahooFinanceContext _context;

        public HomeController(YahooFinanceContext context)
        {
            _context = context;
        }

        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
             int? id = HttpContext.Session.GetInt32("UserId");
          TempData["userName"] = HttpContext.Session.GetString("UserName");


           User UserInfo = _context.Users.SingleOrDefault(user => user.UserId == id);
            ViewBag.UserInfo = UserInfo;

            



            // var DJI = new Dictionary<string, object>();
            // WebRequest.GetMarketData("TIME_SERIES_INTRADAY", "DJI", "1min", JsonResponse =>
            //     {
            //         DJI = JsonResponse;        
            //     }
            // ).Wait();

            // var SP500 = new Dictionary<string, object>();
            // WebRequest.GetMarketData("TIME_SERIES_INTRADAY", "^GSPC", "1min", JsonResponse =>
            //     {
            //         SP500 = JsonResponse;
            //     }
            // ).Wait();

            // var Nasdaq = new Dictionary<string, object>();
            // WebRequest.GetMarketData("TIME_SERIES_INTRADAY", "^IXIC", "1min", JsonResponse =>
            //     {
            //         Nasdaq = JsonResponse;
            //     }
            // ).Wait();

            // var RUT = new Dictionary<string, object>();
            // WebRequest.GetMarketData("TIME_SERIES_INTRADAY", "RUT", "1min", JsonResponse =>
            //     {
            //         RUT = JsonResponse;
            //     }
            // ).Wait();

            // ViewBag.DJI = DJI;
            // ViewBag.SP500 = SP500;
            // ViewBag.Nasdaq = Nasdaq;
            // ViewBag.RUT = RUT; 

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
        public object Find(string query)
        {
            System.Console.WriteLine(query);
            var AllIdx = new List<Dictionary<string, string>>();
            WebRequest.FindStockData(query, JsonResponse =>
                {
                    AllIdx = JsonResponse;
                }
            ).Wait();

            List<Dictionary<string,string>> Matches = new List<Dictionary<string,string>>();
            
            foreach(var idx in AllIdx)
            {
                foreach(KeyValuePair<string,string> pair in idx)
                {
                    TextInfo myTI = new CultureInfo("en-US").TextInfo;
                    if(pair.Value.Contains(query) || pair.Value.Contains(query.ToUpperInvariant()) )
                    {
                        Matches.Add(new Dictionary<string, string>(idx));
                    }
                    else if(pair.Value.StartsWith(query.ToUpper()))
                    {
                        Matches.Add(new Dictionary<string,string>(idx));
                        break;
                    }
                }
            }

            List<Dictionary<string, object>> RelMatches = new List<Dictionary<string, object>>();
            
            foreach(var idx in Matches)
            {
                Dictionary<string,object> match = new Dictionary<string,object>();
                WebRequest.IndyCapInfo(idx["symbol"], JsonResponse =>
                    {
                        match = JsonResponse;
                    }
                ).Wait();
                RelMatches.Add(new Dictionary<string,object>(match));
            }

            return RelMatches.OrderByDescending(i => i["marketCap"]);
        }
    }
}
