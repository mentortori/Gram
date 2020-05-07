using System.ComponentModel.DataAnnotations;

namespace Gram.Application.Guides.Models
{
    public class GuideCreateModel
    {
        [Display(Name = "Person")]
        public int PersonId { get; set; }

        [Display(Name = "Is guide active?")]
        public bool IsActive { get; set; }
    }
}
