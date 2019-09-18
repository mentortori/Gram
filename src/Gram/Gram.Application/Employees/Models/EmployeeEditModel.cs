using System;
using System.ComponentModel.DataAnnotations;

namespace Gram.Application.Employees.Models
{
    public class EmployeeEditModel
    {
        public int Id { get; set; }
        public byte[] RowVersion { get; set; }
        public int PersonId { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Date of employment")]
        [DataType(DataType.Date)]
        public DateTime? DateOfEmployment { get; set; }

        [Display(Name = "Employment expiry date")]
        [DataType(DataType.Date)]
        public DateTime? EmploymentExpiryDate { get; set; }
    }
}
