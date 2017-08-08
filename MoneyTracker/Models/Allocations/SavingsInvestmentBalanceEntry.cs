using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MoneyTracker.Models.Allocations
{
    public class SavingsInvestmentBalanceEntry
    {
        public int Id { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public System.DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public int SavingsInvestmentId { get; set; }

        public virtual SavingsInvestment SavingsInvestment { get; set; }
    }
}