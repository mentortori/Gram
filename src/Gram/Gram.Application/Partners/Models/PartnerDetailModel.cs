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

        [Display(Name = "Events partnered")]
        public IEnumerable<PartnerEventModel> Events { get; set; }

        [Display(Name = "Is partner active?")]
        public bool IsActive { get; set; }

        public class PartnerEventModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}
