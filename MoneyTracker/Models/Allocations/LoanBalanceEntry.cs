
using System.ComponentModel.DataAnnotations;

namespace MoneyTracker.Models.Allocations
{
    public class LoanBalanceEntry
    {
        public int Id { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public System.DateTime Date { get; set; }
        public decimal Amount { get; set; }

        public int LoanId { get; set; }
        public virtual Loan Loan { get; set; }
    }
}