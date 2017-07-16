using System.Collections.Generic;
using System.ComponentModel;

namespace MoneyTracker.Models.Allocations
{
    public class SavingsInvestment: Allocation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SavingsInvestment()
        {
            this.BalanceEntries = new HashSet<SavingsInvestmentBalanceEntry>();
            this.Transactions = new HashSet<Transaction>();
            this.ChangeEvents = new HashSet<ChangeEvent>();
        }

        public string Institution { get; set; }
        public string Notes { get; set; }
        public decimal Apr { get; set; }
        public decimal CurrentValue { get; set; }

        public int DestinationAccountId { get; set; }
        [DisplayName("Destination Account")]
        public virtual Account DestinationAccount { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SavingsInvestmentBalanceEntry> BalanceEntries { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Transaction> Transactions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChangeEvent> ChangeEvents { get; set; }

    }
}