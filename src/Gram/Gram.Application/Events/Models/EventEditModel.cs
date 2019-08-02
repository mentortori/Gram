using Gram.Application.GeneralTypes.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Gram.Application.Events.Models
{
    public class EventEditModel
    {
        public int Id { get; set; }
        public byte[] RowVersion { get; set; }
        public List<GeneralTypeDropDownItemModel> Statuses { get; set; }

        [Display(Name = "Event name")]
        public string EventName { get; set; }

        [Display(Name = "Status")]
        public int EventStatusId { get; set; }

        [Display(Name = "Event description")]
        [DataType(DataType.MultilineText)]
        public string EventDescription { get; set; }

        [Display(Name = "Event date")]
        [DataType(DataType.Date)]
        public DateTime? EventDate { get; set; }
    }
}
