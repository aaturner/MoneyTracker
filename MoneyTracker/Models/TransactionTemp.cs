using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.ModelBinding;
using System.Web.Mvc;
using MoneyTracker.Utilities;
using MoneyTracker.Models.Allocations;

namespace MoneyTracker.Models
{
    public class TransactionTemp
    {
        public TransactionTemp()
        {
            _allocations = DAL.GetData.GetAllocations();
        }

        public TransactionTemp(DateTime transDate, string description, decimal amount, int accountID)
        {
            TransactionDate = transDate.Date;
            Description = description;
            Amount = amount;
            AccountId = accountID;
        }

        public int Id { get; set; }
        public Enums.TransactionTypeEnum TransactionType { get; set; }


        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime EnteredDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime TransactionDate { get; set; }
        
        public string Description { get; set; }

        private decimal _amount;
        public decimal Amount {
            get { return Decimal.Round(_amount, 2); }
            set { this._amount = value; }
        }

        public int? AccountId { get; set; }
        public virtual Account Account { get; set; }

        
        public int? AllocationId { get; set; }
        public virtual Allocation Allocation { get; set; }
        private readonly List<Allocation> _allocations;
        public IEnumerable<SelectListItem> AllocationListItems
        {
            get
            {
                var allAllocations = _allocations.Select(a => new SelectListItem
                {
                    Value = a.Name,
                    Text = a.Name
                });
                return allAllocations;
            }
        }

    }
}