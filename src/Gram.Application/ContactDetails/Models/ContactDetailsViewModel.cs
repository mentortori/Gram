using System.ComponentModel.DataAnnotations;

namespace Gram.Application.ContactDetails.Models
{
    public class ContactDetailsViewModel
    {
        [Display(Name = "Mobile")]
        [DataType(DataType.PhoneNumber)]
        public string Mobile { get; set; }

        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Facebook")]
        [DataType(DataType.Url)]
        public string Facebook { get; set; }

        [Display(Name = "Instagram")]
        [DataType(DataType.Url)]
        public string Instagram { get; set; }

        [Display(Name = "Twitter")]
        [DataType(DataType.Url)]
        public string Twitter { get; set; }

        [Display(Name = "Web")]
        [DataType(DataType.Url)]
        public string Web { get; set; }
    }
}
