using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TechCalendar.Web.Models;
using Microsoft.Extensions.Configuration;
using TechCalendar.Web.Persistence;
using Microsoft.EntityFrameworkCore;

namespace TechCalendar.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly EventDbContext _context;


        public HomeController(EventDbContext configuration)
        {
            _context = configuration;
        }

        public async Task<IActionResult> Index()
        {
            var events = await _context
                .Events
                .Where(e => e.Start >= DateTime.Parse("2018-01-01") && e.End <= DateTime.Parse("2019-12-30"))
                .ToListAsync();
            
            return View(events);
        }

        [HttpPost]
        public async Task<ObjectResult> GetEvents(DateTime start, DateTime end)
        {
            var events = await _context
                .Events
                .Where(e => e.Start >= DateTime.Parse("2018-01-01") && e.End <= DateTime.Parse("2019-12-30"))
                .ToListAsync();

            return Ok(events);
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
