using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using MoneyTracker.Models;
using MoneyTracker.Models.Allocations;

namespace MoneyTracker.AppModels
{
    public class TransactionsUpload
    {
        public TransactionsUpload()
        {
            TransactionTempsCollection = new List<TransactionTemp>();
            _accounts = DAL.GetData.GetAccounts();
            _allocations = DAL.GetData.GetAllocations();
        }


        public DataTable DataTable { get; set; }

        public ICollection<TransactionTemp> TransactionTempsCollection { get; set; }


        private readonly List<Account> _accounts;
        public string AccountName { get; set; }
        public Nullable<int> AccountId { get; set; }
        public IEnumerable<SelectListItem> AccountItems
        {
            get
            {
                var allAccounts = _accounts.Select(a => new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = a.Name
                });
                return allAccounts;
            }
        }

        public int SelectedId { get; set; }


        private readonly List<Allocation> _allocations;
        public string AllocationName { get; set; }
        public Nullable<int> AllocationId { get; set; }
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