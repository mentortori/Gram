using Gram.Application.SharedModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Gram.Application.EventGuides.Models
{
    public class EventGuidesViewModel
    {
        public EventGuidesViewModel()
        {
            Guides = new HashSet<ListItemWithRowVersionModel>();
        }

        public int EventId { get; set; }

        [Display(Name = "Guides")]
        public IEnumerable<ListItemWithRowVersionModel> Guides { get; set; }
    }
}
