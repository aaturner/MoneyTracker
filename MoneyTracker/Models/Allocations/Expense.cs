using System.Collections.Generic;

namespace MoneyTracker.Models.Allocations
{
    public class Expense: Allocation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Expense()
        {
        }


        public int ExpenseCategoryId { get; set; }
        public virtual ExpenseCategory ExpenseCategory { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Transaction> Transactions { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<ChangeEvent> ChangeEvents { get; set; }
    }
}