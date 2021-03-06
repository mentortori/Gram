﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Gram.Application.Employees.Models
{
    public class EmployeeDeleteModel
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

        [Display(Name = "Date of employment")]
        [DataType(DataType.Date)]
        public DateTime? DateOfEmployment { get; set; }

        [Display(Name = "Employment expiry date")]
        [DataType(DataType.Date)]
        public DateTime? EmploymentExpiryDate { get; set; }
    }
}
