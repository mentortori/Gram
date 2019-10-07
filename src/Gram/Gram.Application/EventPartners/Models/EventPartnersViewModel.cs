using Gram.Application.SharedModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Gram.Application.EventPartners.Models
{
    public class EventPartnersViewModel
    {
        public EventPartnersViewModel()
        {
            Partners = new HashSet<ListItemWithRowVersionModel>();
        }

        public int EventId { get; set; }

        [Display(Name = "Partners")]
        public IEnumerable<ListItemWithRowVersionModel> Partners { get; set; }
    }
}
