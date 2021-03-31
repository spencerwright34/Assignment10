using Assignment10.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment10.Components
{
    public class BowlingTeamViewComponent : ViewComponent
    {
        private BowlingLeagueContext context;

        public BowlingTeamViewComponent(BowlingLeagueContext ctx)
        {
            context = ctx;
        }

        public IViewComponentResult Invoke()
        {
            //This will highlight the selected team
            ViewBag.SelectedCategory = RouteData?.Values["TeamName"];

            return View(context.Teams
                .Distinct()
                .OrderBy(x => x));
        }
    }
}
