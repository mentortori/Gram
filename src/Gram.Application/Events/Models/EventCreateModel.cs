using System;
using System.ComponentModel.DataAnnotations;

namespace Gram.Application.Events.Models
{
    public class EventCreateModel
    {
        [Display(Name = "Event name")]
        public string EventName { get; set; }

        [Display(Name = "Status")]
        public int EventStatusId { get; set; }

        [Display(Name = "Event description")]
        [DataType(DataType.MultilineText)]
        public string EventDescription { get; set; }

        [Display(Name = "Event date")]
        [DataType(DataType.Date)]
        public DateTime? EventDate { get; set; }
    }
}
