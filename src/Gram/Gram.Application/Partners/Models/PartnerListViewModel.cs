using System;
using System.ComponentModel.DataAnnotations;

namespace Gram.Application.Partners.Models
{
    public class PartnerListViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Events partnered")]
        public int EventsCount { get; set; }

        [Display(Name = "Is partner active?")]
        public bool IsActive { get; set; }
    }
}
