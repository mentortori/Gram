using System;
using System.ComponentModel.DataAnnotations;

namespace Gram.Application.Partners.Models
{
    public class PartnerEditModel
    {
        public int Id { get; set; }
        public byte[] RowVersion { get; set; }
        public int PersonId { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Is guide active?")]
        public bool IsActive { get; set; }
    }
}
