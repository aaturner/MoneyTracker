using System.Collections.Generic;
using System.ComponentModel;

namespace MoneyTracker.Models.Allocations
{
    public class Loan: Allocation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Loan()
        {
             this.LoanBalanceEntries = new HashSet<LoanBalanceEntry>();
        }

        [DisplayName("Destination Account")]
        public int? LoanAccountId { get; set; }
        [DisplayName("Destination Account")]
        public virtual Account LoanAccount { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LoanBalanceEntry> LoanBalanceEntries { get; set; }




    }
}