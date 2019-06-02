using System;
using Microsoft.EntityFrameworkCore;
using TechCalendar.Api.Models;

namespace TechCalendar.Api.Persistence
{
    public class EventDbContext : DbContext
    {
        public EventDbContext(DbContextOptions<EventDbContext> options)
            : base(options)
        { }

        public DbSet<Event> Events { get; set; }
    }
}