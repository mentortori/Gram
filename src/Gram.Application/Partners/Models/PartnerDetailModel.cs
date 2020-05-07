using Gram.Application.ContactDetails.Models;
using Gram.Application.SharedModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Gram.Application.Partners.Models
{
    public class PartnerDetailModel
    {
        public PartnerDetailModel()
        {
            Events = new HashSet<ListItemModel>();
        }

        public int Id { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        public ContactDetailsViewModel ContactDetails { get; set; }

        [Display(Name = "Events partnered")]
        public IEnumerable<ListItemModel> Events { get; set; }

        [Display(Name = "Is partner active?")]
        public bool IsActive { get; set; }
    }
}
