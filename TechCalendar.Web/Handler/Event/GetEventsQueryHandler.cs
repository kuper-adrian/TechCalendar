using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TechCalendar.Web.Models;
using TechCalendar.Web.Persistence;
using TechCalendar.Web.Util;

namespace TechCalendar.Web.Handler.Event
{
    public class GetEventsQueryHandler : IQueryHandler<GetEventsQuery, ICollection<TechCalendar.Web.Models.Event>>
    {
        private readonly EventDbContext _dbContext;
        private readonly static Cache<GetEventsQuery, ICollection<TechCalendar.Web.Models.Event>> _cache = new Cache<GetEventsQuery, ICollection<Models.Event>>();
        public GetEventsQueryHandler(EventDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ICollection<TechCalendar.Web.Models.Event>> HandleAsync(GetEventsQuery query)
        {
            var events = _cache.Get(query);
            if (events == null)
            {
                events = await _dbContext
                    .Events
                    .Where(e => e.Start >= query.Start && e.End <= query.End)
                    .ToListAsync();
                
                _cache.Put(query, events);
            }

            return events;
        }
    }
}