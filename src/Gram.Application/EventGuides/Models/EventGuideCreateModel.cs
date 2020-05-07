using System.ComponentModel.DataAnnotations;

namespace Gram.Application.EventGuides.Models
{
    public class EventGuideCreateModel
    {
        [Display(Name = "Guide")]
        public int GuideId { get; set; }
    }
}
