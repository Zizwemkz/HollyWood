using System;
using System.Collections.Generic;

namespace HollywoodAssessment.Data.Models
{
    public partial class Event
    {
        public Event()
        {
            EventDetail = new HashSet<EventDetail>();
        }

        public long EventId { get; set; }
        public long? TournamentId { get; set; }
        public string EventName { get; set; }
        public short? EventNumber { get; set; }
        public DateTime? EventDateTime { get; set; }
        public DateTime? EventEndDateTime { get; set; }
        public bool? AutoClose { get; set; }

        public virtual Tournament Tournament { get; set; }
        public virtual ICollection<EventDetail> EventDetail { get; set; }
    }
}
