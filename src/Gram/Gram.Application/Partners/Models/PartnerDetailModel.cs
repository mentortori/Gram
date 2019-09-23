using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Gram.Application.Partners.Models
{
    public class PartnerDetailModel
    {
        public int Id { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Date of birth")]
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        [Display(Name = "Nationality")]
        public string Nationality { get; set; }

        [Display(Name = "Events guided")]
        public IEnumerable<PartnerEventModel> Events { get; set; }

        [Display(Name = "Is guide active?")]
        public bool IsActive { get; set; }

        public class PartnerEventModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}
