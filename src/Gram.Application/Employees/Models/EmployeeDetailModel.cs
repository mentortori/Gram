using System;
using System.ComponentModel.DataAnnotations;

namespace Gram.Application.Employees.Models
{
    public class EmployeeDetailModel
    {
        public int Id { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Date of birth")]
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        [Display(Name = "Nationality")]
        public string Nationality { get; set; }

        [Display(Name = "Date of employment")]
        [DataType(DataType.Date)]
        public DateTime? DateOfEmployment { get; set; }

        [Display(Name = "Employment expiry date")]
        [DataType(DataType.Date)]
        public DateTime? EmploymentExpiryDate { get; set; }
    }
}
