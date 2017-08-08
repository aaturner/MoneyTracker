using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace MoneyTracker.Models.Allocations
{
    public abstract class Allocation
    {


        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public Nullable<bool> IsMonthly { get; set; }

        [DisplayName("Day of Month")]
        public int RecuranceDayNumber { get; set; }
        [DataType(DataType.Date)]
        [DisplayName("Recur End")]
        public DateTime? RecuranceEndDate { get; set; }

        [DisplayName("Allocation Amount")]
        public decimal Amount { get; set; }

        [DisplayName("Source Account")]
        public int? AccountId { get; set; }
        [DisplayName("Source Account")]
        public virtual Account FromAccount { get; set; }

    }
}