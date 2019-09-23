using System.ComponentModel.DataAnnotations;

namespace Gram.Application.Partners.Models
{
    public class PartnerCreateModel
    {
        [Display(Name = "Person")]
        public int PersonId { get; set; }

        [Display(Name = "Is guide active?")]
        public bool IsActive { get; set; }
    }
}
