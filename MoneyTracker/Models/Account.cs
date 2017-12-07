using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MoneyTracker.Models.Allocations;

namespace MoneyTracker.Models
{
    public class Account
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Account()
        {
            this.AccountBalanceEntries = new HashSet<AccountBalanceEntry>();
            this.Transactions = new HashSet<Transaction>();
            this.Incomes = new HashSet<Income>();
            this.Expenses = new HashSet<Expense>();
            AccountType = Utilities.Enums.AccountType.Checking;
            Apr = 0;
        }

        public int Id { get; set; }
        [Required]
        [DisplayName("Account")]
        public string Name { get; set; }
        [Required]
        [DisplayName("Account Type")]
        public Utilities.Enums.AccountType AccountType { get; set; }
        public decimal Apr { get; set; }
        public string Institution { get; set; }
        public string Website { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AccountBalanceEntry> AccountBalanceEntries { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Transaction> Transactions { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Income> Incomes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Expense> Expenses { get; set; }
    }
}