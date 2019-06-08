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
        private readonly Cache _cache;

        public EventController(EventDbContext context)
        {
            _context = context;

            _cache = new Cache();
        }

        [HttpGet]
        public ActionResult<ICollection<Event>> GetEvents(DateTime start, DateTime end)
        {
            var result = _cache.Get(start, end);
            if (result == null)
            {
                result = _context.Events
                    .Where(e => e.Start > start && e.End < end)
                    .ToList();

                _cache.Put(start, end, result);
            }

            return Ok(result);
        }

        [HttpPost]
        public void CreateEvent([FromBody] Event e)
        {
            _context.Events.Add(e);
            _context.SaveChangesAsync();
        }
    }

    internal class CacheItem
    {
        public DateTime Creation { get; private set; }
        public ICollection<Event> Item { get; private set; }

        public CacheItem(ICollection<Event> item)
        {
            Creation = DateTime.Now;
            Item = item;
        }
    }

    internal class Cache
    {
        private long _ttl;
        private Dictionary<(DateTime, DateTime), CacheItem> _cache;

        public Cache(long ttl = 3_600_000 /* 1 hour */)
        {
            _ttl = ttl;
            _cache = new Dictionary<(DateTime, DateTime), CacheItem>();
        }

        public ICollection<Event> Get(DateTime start, DateTime end)
        {
            if (!_cache.ContainsKey((start, end)))
            {
                return null;
            }
            var cacheItem = _cache[(start, end)];
            var now = DateTime.Now;

            if ((now - cacheItem.Creation).TotalMilliseconds > _ttl)
            {
                _cache[(start, end)] = null;
                return null;
            }

            return _cache[(start, end)].Item;
        }

        public void Put(DateTime start, DateTime end, ICollection<Event> item)
        {
            _cache[(start, end)] = new CacheItem(item);
        }
    }
}
