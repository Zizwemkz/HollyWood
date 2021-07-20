using System;
using System.Collections.Generic;

namespace HollywoodAssessment.Data.Models
{
    public partial class Tournament
    {
        public Tournament()
        {
            Event = new HashSet<Event>();
        }
        //todo validation

        public long TournamentId { get; set; }
        public string TournamentName { get; set; }

        public virtual ICollection<Event> Event { get; set; }
    }
}
