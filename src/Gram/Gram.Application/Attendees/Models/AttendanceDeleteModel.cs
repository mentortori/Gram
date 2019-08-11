using System;
using System.ComponentModel.DataAnnotations;

namespace Gram.Application.Attendees.Models
{
    public class AttendanceDeleteModel
    {
        public int Id { get; set; }
        public byte[] RowVersion { get; set; }
        public int EventId { get; set; }

        [Display(Name = "Attendee")]
        public string Attendee { get; set; }

        [Display(Name = "Attendance status")]
        public string Status { get; set; }

        [Display(Name = "Status date")]
        [DataType(DataType.Date)]
        public DateTime StatusDate { get; set; }

        [Display(Name = "Remarks")]
        public string Remarks { get; set; }
    }
}
