using System;
using System.ComponentModel.DataAnnotations;

namespace Gram.Application.Events.Models
{
    public class EventsListViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Event name")]
        public string EventName { get; set; }

        [Display(Name = "Status")]
        public string EventStatus { get; set; }

        [Display(Name = "Event date")]
        [DataType(DataType.Date)]
        public DateTime? EventDate { get; set; }
    }
}
