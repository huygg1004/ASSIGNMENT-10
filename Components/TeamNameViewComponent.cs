using Microsoft.AspNetCore.Mvc;
using SharpenTheSaw.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharpenTheSaw.Components
{
    public class TeamNameViewComponent : ViewComponent
    {
        private BowlingLeagueContext context;
        public TeamNameViewComponent (BowlingLeagueContext ctx)
        {
            context = ctx;
        }

        public IViewComponentResult Invoke()
        {
            //return View(context.RecipeClasses
            //    .Select(x => x.RecipeClassDescription)
            //    .Distinct()
            //    .OrderBy(x => x)
            //    .ToList());

            ViewBag.SelectedTeamName = RouteData?.Values["TeamName"];
            return View(context.Teams
                .Distinct()
                .OrderBy(x => x));
        }
    }
}
