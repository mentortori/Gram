using System;
using System.ComponentModel.DataAnnotations;

namespace Gram.Application.Partners.Models
{
    public class PartnerEditModel
    {
        public int Id { get; set; }
        public byte[] RowVersion { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Is partner active?")]
        public bool IsActive { get; set; }
    }
}
