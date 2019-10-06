using Gram.Application.ContactDetails.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Gram.Application.People.Models
{
    public class PersonDetailModel
    {
        public PersonDetailModel()
        {
            AttendedEvents = new HashSet<PersonAttendanceModel>();
        }

        public int Id { get; set; }

        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Display(Name = "Date of birth")]
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        [Display(Name = "Nationality")]
        public string Nationality { get; set; }

        public ContactDetailsViewModel ContactDetails { get; set; }

        [Display(Name = "Events attended")]
        public IEnumerable<PersonAttendanceModel> AttendedEvents { get; set; }

        public class PersonAttendanceModel
        {
            public int EventId { get; set; }

            [Display(Name = "Event name")]
            public string EventName { get; set; }

            [Display(Name = "Attendance status")]
            public string AttendanceStatus { get; set; }

            [Display(Name = "Status date")]
            [DataType(DataType.Date)]
            public DateTime StatusDate { get; set; }

            [Display(Name = "Remarks")]
            public string Remarks { get; set; }
        }
    }
}
