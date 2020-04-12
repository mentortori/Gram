using System;
using System.ComponentModel.DataAnnotations;

namespace Gram.Application.Attendees.Models
{
    public class CreateDto
    {
        [Display(Name = "Attendee")]
        public int PersonId { get; set; }

        [Display(Name = "Attendance status")]
        public int StatusId { get; set; }

        [Display(Name = "Status date")]
        [DataType(DataType.Date)]
        public DateTime StatusDate { get; set; }

        [Display(Name = "Remarks")]
        public string Remarks { get; set; }
    }
}
