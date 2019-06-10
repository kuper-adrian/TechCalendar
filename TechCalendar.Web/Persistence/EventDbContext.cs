using System;
using Microsoft.EntityFrameworkCore;
using TechCalendar.Web.Models;

namespace TechCalendar.Web.Persistence
{
    public class EventDbContext : DbContext
    {
        public EventDbContext(DbContextOptions<EventDbContext> options)
            : base(options)
        { }

        public DbSet<Event> Events { get; set; }
    }
}