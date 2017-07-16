using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MoneyTracker.Models
{
    public class AccountBalanceEntry
    {
        public int Id { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public System.DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public Nullable<int> AccountId { get; set; }

        public virtual Account Account { get; set; }
    }
}