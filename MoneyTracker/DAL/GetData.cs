using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MoneyTracker.Models;
using MoneyTracker.Models.Allocations;

namespace MoneyTracker.DAL
{
    public static class GetData
    {
        private static PrimaryContext db = new PrimaryContext();
        public static List<Account> GetAccounts()
        {
            return db.Accounts.ToList();
        }

        public static List<Allocation> GetAllocations()
        {
            return db.Allocations.ToList();
        }
    }
}