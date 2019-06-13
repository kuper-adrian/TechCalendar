using System;

namespace TechCalendar.Web.Handler.Event
{
    public class EventSubmission
    {
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime EndTime { get; set; }
        public string StreamUrl { get; set; }
    }
}
