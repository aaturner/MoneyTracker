using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MoneyTracker.Models.Allocations;
using MoneyTracker.Utilities;

namespace MoneyTracker.Models
{
    public abstract class ChangeEvent
    {
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime EffectiveDateTime { get; set; }

        public bool IsRecurring { get; set; }
        [Required]
        public decimal Amount { get; set; }
        public Enums.ChangeTypeEnum ChangeTypeEnum { get; set; }

    }
}