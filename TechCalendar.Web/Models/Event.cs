using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechCalendar.Web.Models
{
    public class Event
    {
        public int EventId { get; set; }
        public string Title { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Url { get; set; }
        public string BackgroundColor { get; set; }
        public string BorderColor { get; set; }
        public string TextColor { get; set; }
    }
}
