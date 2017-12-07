using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MoneyTracker.Models.Allocations;

namespace MoneyTracker.Models
{
    public class PayrollDeduction
    {
        //Intended for deductions of Savings or investments that occur on each income event
        public int Id { get; set; }

        [DisplayName("Deduction")]
        [Required]
        public string Name { get; set; }

        public decimal Amount { get; set; }

        [DisplayName("Description")]
        public string Description { get; set; }
        [DisplayName("Pre Tax")]
        public bool isPreTax { get; set; }

        [DisplayName("Income Deducted")]
        public int IncomeId { get; set; }
        public virtual Income Income { get; set; }

        [DisplayName("Destination Account")]
        public int AccountId { get; set; }
        public virtual Account Account { get; set; }



    }
}