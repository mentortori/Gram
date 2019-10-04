using System.ComponentModel.DataAnnotations;

namespace Gram.Application.Partners.Models
{
    public class PartnerCreateModel
    {
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Is partner active?")]
        public bool IsActive { get; set; }
    }
}
