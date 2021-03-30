using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SharpenTheSaw.Models;
using SharpenTheSaw.Models.ViewModels;

namespace SharpenTheSaw.Controllers
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

        public IActionResult Index(long? teamid, string teamname, int pageNum = 0)
        {
            //return View(context.Recipes
            //    .FromSqlInterpolated($"SELECT * FROM Recipes WHERE RecipeClassId = {mealtypeid} OR {mealtypeid} IS NULL")
            //    .ToList());

            int pageSize = 5;

            return View(new IndexViewModel
            {
                Bowlers = (context.Bowlers
                .Where(m => m.TeamId == teamid || teamid == null)
                .OrderBy(m => m.BowlerFirstName)
                .Skip((pageNum-1)*pageSize)
                .Take(pageSize)
                .ToList()),

                 PageNumberingInfo = new PageNumberingInfo
                 {
                     NumItemsPerPage = pageSize,
                     CurrentPage = pageNum,

                     //if no team has been selected, then get the full count. Otherwise only count the number from the team name that has been selected
                     TotalNumItems = (teamid == null ? context.Bowlers.Count() :
                     context.Bowlers.Where(x => x.TeamId == teamid).Count())
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
