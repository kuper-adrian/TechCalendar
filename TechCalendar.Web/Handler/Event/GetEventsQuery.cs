using System;

namespace TechCalendar.Web.Handler.Event
{
    public class GetEventsQuery
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public override bool Equals(object obj)
        {
            var other = obj as GetEventsQuery;
            if (other == null)
            {
                return false;
            }
            return this.Start == other.Start && this.End == other.End;
        }

        public override int GetHashCode()
        {
            unchecked 
            {
                int hash = 17;
                hash = hash * 23 + Start.GetHashCode();
                hash = hash * 23 + End.GetHashCode();
                return hash;
            }
        }
    }
}