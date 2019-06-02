using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TechCalendar.Web.Models;
using TechCalendar.Client;

namespace TechCalendar.Web.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var client = new EventClient("http://localhost:5000", new System.Net.Http.HttpClient());
            var events = await client.GetEventsAsync(DateTime.Parse("2018-01-01"), DateTime.Parse("2019-12-30"));
            
            return View(events);
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
