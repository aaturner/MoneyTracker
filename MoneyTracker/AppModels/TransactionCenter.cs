using MoneyTracker.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MoneyTracker.AppModels
{
    public class TransactionCenter
    {
        public TransactionCenter()
        {
            List<Transaction> TransactionList = new List<Transaction>();
        }

        public List<Transaction> TransactionList { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime SelectedPeriod { get; set; }

        public int SelectedMonth { get; set; }
        public int SelectedYear { get; set; }



    }
}
