using System.Collections.Generic;
using System.ComponentModel;

namespace MoneyTracker.Models.Allocations
{
    public class ExpenseCategory
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ExpenseCategory()
        {
            this.Expenses = new HashSet<Expense>();
        }

        public int Id { get; set; }
        [DisplayName("Category")]
        public string Name { get; set; }
        

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Expense> Expenses { get; set; }
    }
}