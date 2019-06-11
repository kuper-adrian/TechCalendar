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
using TechCalendar.Web.Handler.Event;

namespace TechCalendar.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly EventDbContext _context;

        public HomeController(EventDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {            
            return View();
        }

        [HttpPost]
        public async Task<ObjectResult> GetEvents([FromBody] GetEventsQuery query)
        {
            var handler = new GetEventsQueryHandler(_context);
            var events = await handler.HandleAsync(query);

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
