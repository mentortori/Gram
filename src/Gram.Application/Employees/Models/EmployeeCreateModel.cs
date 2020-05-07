using System;
using System.ComponentModel.DataAnnotations;

namespace Gram.Application.Employees.Models
{
    public class EmployeeCreateModel
    {
        [Display(Name = "Person")]
        public int PersonId { get; set; }

        [Display(Name = "Date of employment")]
        [DataType(DataType.Date)]
        public DateTime? DateOfEmployment { get; set; }

        [Display(Name = "Employment expiry date")]
        [DataType(DataType.Date)]
        public DateTime? EmploymentExpiryDate { get; set; }
    }
}
