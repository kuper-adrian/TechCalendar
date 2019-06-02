using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TechCalendar.Api.Models;
using TechCalendar.Api.Persistence;

namespace TechCalendar.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly EventDbContext _context;

        public EventController(EventDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<ICollection<Event>> GetEvents(DateTime start, DateTime end)
        {
            return _context
                .Events
                .Where(e => e.StartDate > start && e.EndDate < end)
                .ToList();
        }

        [HttpPost]
        public void CreateEvent([FromBody] Event e)
        {
            _context.Events.Add(e);
            _context.SaveChangesAsync();
        }
    }
}
