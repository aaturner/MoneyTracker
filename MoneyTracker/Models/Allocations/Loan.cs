using System.Collections.Generic;

namespace MoneyTracker.Models.Allocations
{
    public class Loan: Allocation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Loan()
        {
            this.Transactions = new HashSet<Transaction>();
            this.LoanBalanceEntries = new HashSet<LoanBalanceEntry>();
            this.ChangeEvents = new HashSet<ChangeEvent>();
        }

        public decimal Apr { get; set; }
        public decimal AssetCurrentValue { get; set; }




        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Transaction> Transactions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LoanBalanceEntry> LoanBalanceEntries { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChangeEvent> ChangeEvents { get; set; }

    }
}