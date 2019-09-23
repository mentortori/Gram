using System;
using System.ComponentModel.DataAnnotations;

namespace Gram.Application.Partners.Models
{
    public class PartnerDeleteModel
    {
        public int Id { get; set; }
        public byte[] RowVersion { get; set; }
        public bool IsDeletable { get; set; }

        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Display(Name = "Date of birth")]
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        [Display(Name = "Nationality")]
        public string Nationality { get; set; }

        [Display(Name = "Events guided")]
        public int EventsCount { get; set; }

        [Display(Name = "Is guide active?")]
        public bool IsActive { get; set; }
    }
}
