using Gram.Application.ContactDetails.Models;
using System.ComponentModel.DataAnnotations;

namespace Gram.Application.Partners.Models
{
    public class PartnerDeleteModel
    {
        public int Id { get; set; }
        public byte[] RowVersion { get; set; }
        public bool IsDeletable { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        public ContactDetailsViewModel ContactDetails { get; set; }

        [Display(Name = "Events partnered")]
        public int EventsCount { get; set; }

        [Display(Name = "Is partner active?")]
        public bool IsActive { get; set; }
    }
}
