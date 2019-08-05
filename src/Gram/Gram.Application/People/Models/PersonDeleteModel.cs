using System;
using System.ComponentModel.DataAnnotations;

namespace Gram.Application.People.Models
{
    public class PersonDeleteModel
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
    }
}
