using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MoneyTracker.Models.Allocations
{
    public abstract class Allocation
    {
        public Allocation()
        {

        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        //Frequency
        public Nullable<bool> IsMonthly { get; set; }   //Depricated

        public int? RecurrenceId { get; set; }
        public virtual Recurrence Recurrence { get; set; }

        [DisplayName("Allocation Amount")]
        public decimal Amount { get; set; }

        [DisplayName("Source Account")]
        public int? AccountId { get; set; }
        [DisplayName("Source Account")]
        public virtual Account FromAccount { get; set; }

    }
}