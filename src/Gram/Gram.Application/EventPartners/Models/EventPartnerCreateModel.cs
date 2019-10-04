using System.ComponentModel.DataAnnotations;

namespace Gram.Application.EventPartners.Models
{
    public class EventPartnerCreateModel
    {
        [Display(Name = "Partner")]
        public int PartnerId { get; set; }
    }
}
