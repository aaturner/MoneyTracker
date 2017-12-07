using MoneyTracker.Utilities;
using System;
using System.ComponentModel.DataAnnotations;
using MoneyTracker.Models.Allocations;
using MoneyTracker.Models.Enums;


namespace MoneyTracker.Models
{
    public class Transaction
    {
        public Transaction()
        {

        }
        public Transaction(DateTime transDate, string description, decimal amount, Account account )
        {
            TransactionDate = transDate;
            EnteredDate = DateTime.Now;
            Description = description;
            Amount = amount;
            Account = account;
            AccountId = account.Id;
            TransactionType = (Amount >= 0) ?  Enums.TransactionTypeEnum.Credit :  Enums.TransactionTypeEnum.Debit;

        }

        public int Id { get; set; }
        public TransactionTypeEnum TransactionType { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public System.DateTime TransactionDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime? EnteredDate { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }

        public int AccountId { get; set; }
        public virtual Account Account { get; set; }

        public int? AllocationId { get; set; }
        public virtual Allocation Allocation { get; set; }

    }


}