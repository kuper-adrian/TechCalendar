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
using TechCalendar.Web.Util;

namespace TechCalendar.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly EventDbContext _context;
        private readonly Cache<(DateTime, DateTime), List<Event>> _eventsCache;

        public HomeController(EventDbContext configuration)
        {
            _context = configuration;
            _eventsCache = new Cache<(DateTime, DateTime), List<Event>>();
        }

        public IActionResult Index()
        {            
            return View();
        }

        [HttpPost]
        public async Task<ObjectResult> GetEvents(DateTime start, DateTime end)
        {
            var events = _eventsCache.Get((start, end));
            if (events == null)
            {
                events = await _context
                    .Events
                    .Where(e => e.Start >= start && e.End <= end)
                    .ToListAsync();
            }

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
