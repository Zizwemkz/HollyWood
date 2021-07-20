using System;
using System.Collections.Generic;

namespace HollywoodAssessment.Data.Models
{
    public partial class EventDetail
    {
      //TODO Validation
        public long EventDetailId { get; set; }
        public long? EventId { get; set; }
        public short? EventDetailStatusId { get; set; }
        public string EventDetailName { get; set; }
        public short? EventDetailNumber { get; set; }
        public decimal? EventDetailOdd { get; set; }
        public short? FinishingPosition { get; set; }
        public bool? FirstTimer { get; set; }

        public virtual Event Event { get; set; }
        public virtual EventDetailStatus EventDetailStatus { get; set; }
    }
}
