using System;
using System.ComponentModel.DataAnnotations;

namespace Gram.Application.Attendees.Models
{
    public class AttendeeListViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Attendee")]
        public string Attendee { get; set; }

        [Display(Name = "Attendance status")]
        public string AttendanceStatus { get; set; }

        [Display(Name = "Status date")]
        [DataType(DataType.Date)]
        public DateTime StatusDate { get; set; }

        [Display(Name = "Remarks")]
        public string Remarks { get; set; }
    }
}
