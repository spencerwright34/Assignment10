using Assignment10.Models;
using Assignment10.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment10.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private BowlingLeagueContext context { get; set; }

        public HomeController(ILogger<HomeController> logger, BowlingLeagueContext ctx)
        {
            _logger = logger;
            context = ctx;
        }

        public IActionResult Index(long? bowlingteamid, string teamname, int pageNum = 0)
        {
            //This variable can be changed to determine how many bowlers will be displayed on the page at once
            int pageSize = 5;

            return View(new IndexViewModel

            {
                Bowlers = (context.Bowlers
                    .Where(m => m.TeamId == bowlingteamid || bowlingteamid == null)
                    .OrderBy(m => m.BowlerFirstName)
                    .Skip((pageNum - 1) * pageSize)
                    .Take(pageSize)
                    .ToList()),

                PageNumberingInfo = new PageNumberingInfo
                {
                    NumItemsPerPage = pageSize,
                    CurrentPage = pageNum,
                    //If no meal has been selected, then get the full count
                    //Otherwise, only count the number of bowlers from team selected
                    TotalNumItems = (bowlingteamid == null ? context.Bowlers.Count() : 
                        context.Bowlers.Where(x => x.BowlerId == bowlingteamid).Count())
                },

                TeamName = teamname
            });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
