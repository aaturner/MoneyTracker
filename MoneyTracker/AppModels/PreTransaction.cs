using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using MoneyTracker.Models;
using System.Web.Mvc;


namespace MoneyTracker.AppModels
{
    public class PreTransaction
    {
        public PreTransaction()
        {
            
        }
        public PreTransaction(DateTime transDate, string description, decimal amount, int accountID)
        : this()
        {
            TransactionDate = transDate.Date;
           // EnteredDate = DateTime.Now;
            Description = description;
            Amount = amount;
            AccountId = accountID;
        }

        public System.DateTime? TransactionDate { get; set; }

        public string DisplayTransactionDate
        {
            get
            {
                if (TransactionDate.HasValue)
                {
                    DateTime temp = (DateTime)TransactionDate;
                    return temp.ToString("d");
                }
                else return "None Entered";
            } 
        }

        public DateTime? EnteredDate { get { return System.DateTime.Now.Date; } }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public int AccountId { get; set; }

        public Nullable<int> AllocationId { get; set; }





    }
}