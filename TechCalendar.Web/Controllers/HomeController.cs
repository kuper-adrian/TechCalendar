using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TechCalendar.Web.Models;
using TechCalendar.Client;
using Microsoft.Extensions.Configuration;

namespace TechCalendar.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;


        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {
            var client = new EventClient(_configuration["ApiBaseUrl"], new System.Net.Http.HttpClient());
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
