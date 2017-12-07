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
            
        }
        
        public string Notes { get; set; }
   
        [DisplayName("Destination Account")]
        public int DestinationAccountId { get; set; }
        [DisplayName("Destination Account")]
        public virtual Account DestinationAccount { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SavingsInvestmentBalanceEntry> BalanceEntries { get; set; }
        

    }
}