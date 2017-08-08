using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MoneyTracker.Models.Allocations;

namespace MoneyTracker.Models

{
    public class LoanPayment
    {
        public int Id { get; set; }
        public int LoansId { get; set; }
        public decimal Amount { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public System.DateTime PostDate { get; set; }

        public virtual Loan Loan { get; set; }
    }
}