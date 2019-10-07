using System.ComponentModel.DataAnnotations;
using Gram.Application.SharedModels;

namespace Gram.Application.ContactDetails.Models
{
    public class ContactDetailsUpdateModel
    {
        public int MobileId { get; set; }

        [Display(Name = "Mobile")]
        [DataType(DataType.PhoneNumber)]
        public string Mobile { get; set; }

        public byte[] MobileRowVersion { get; set; }

        public int EmailId { get; set; }

        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public byte[] EmailRowVersion { get; set; }

        public int FacebookId { get; set; }

        [Display(Name = "Facebook")]
        [DataType(DataType.Url)]
        public string Facebook { get; set; }

        public byte[] FacebookRowVersion { get; set; }

        public int InstagramId { get; set; }

        [Display(Name = "Instagram")]
        [DataType(DataType.Url)]
        public string Instagram { get; set; }

        public byte[] InstagramRowVersion { get; set; }

        public int TwitterId { get; set; }

        [Display(Name = "Twitter")]
        [DataType(DataType.Url)]
        public string Twitter { get; set; }

        public byte[] TwitterRowVersion { get; set; }

        public int WebId { get; set; }

        [Display(Name = "Web")]
        [DataType(DataType.Url)]
        public string Web { get; set; }

        public byte[] WebRowVersion { get; set; }
    }
}
