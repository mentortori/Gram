using System;
using System.ComponentModel.DataAnnotations;

namespace Gram.Application.Events.Models
{
    public class EventDetailModel
    {
        public int Id { get; set; }
        public byte[] RowVersion { get; set; }

        [Display(Name = "Event name")]
        public string EventName { get; set; }

        [Display(Name = "Status")]
        public string EventStatus { get; set; }

        [Display(Name = "Event description")]
        [DataType(DataType.MultilineText)]
        public string EventDescription { get; set; }

        [Display(Name = "Event date")]
        [DataType(DataType.Date)]
        public DateTime? EventDate { get; set; }
    }
}
