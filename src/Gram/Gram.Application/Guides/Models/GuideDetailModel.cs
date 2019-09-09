using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Gram.Application.Guides.Models
{
    public class GuideDetailModel
    {
        public int Id { get; set; }
        public byte[] RowVersion { get; set; }
        public bool IsDeletable { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Date of birth")]
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        [Display(Name = "Nationality")]
        public string Nationality { get; set; }

        [Display(Name = "Events guided")]
        public IEnumerable<GuideEventModel> Events { get; set; }

        [Display(Name = "Is guide active?")]
        public bool IsActive { get; set; }

        public class GuideEventModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}
