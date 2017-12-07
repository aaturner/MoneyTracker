using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MoneyTracker.Models.Allocations
{
    public abstract class Allocation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Allocation()
        {
            this.Transactions = new HashSet<Transaction>();
            this.ChangeEvents = new HashSet<ChangeEvent>();
        }

        public int Id { get; set; }

        [Required]
        [DisplayName("Allocation")]
        public string Name { get; set; }

        [DisplayName("Description")]
        public string Description { get; set; }

        //Frequency
        public Nullable<bool> IsMonthly { get; set; }   //Depricated

        public int? RecurrenceId { get; set; }
        public virtual Recurrence Recurrence { get; set; }

        [DisplayName("Allocation Amount")]
        public decimal Amount { get; set; }

        //used in forecast calculations
        public decimal? TempAmountDecimal { get; set; }

       // [Required]
        [DisplayName("Source Account")]
        public int? AccountId { get; set; }
        [DisplayName("Source Account")]
        public virtual Account FromAccount { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Transaction> Transactions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChangeEvent> ChangeEvents { get; set; }

    }
}