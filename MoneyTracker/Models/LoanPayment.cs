using MoneyTracker.Models.Allocations;

namespace MoneyTracker.Models

{
    public class LoanPayment
    {
        public int Id { get; set; }
        public int LoansId { get; set; }
        public decimal Amount { get; set; }
        public System.DateTime PostDate { get; set; }

        public virtual Loan Loan { get; set; }
    }
}