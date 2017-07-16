using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoneyTracker.Utilities
{
    public static class Enums
    {
        public enum AllocationType
        {
            Expense,
            Income,
            Loan,
            Savings,
            Investment
        };

        public enum TransactionTypeEnum
        {
            Debit,
            Credit,
            Transfer
        };

        public enum ChangeTypeEnum
        {
            LumpSum,
            Percentage
        }
        public enum TableRowType
        {
            header1,
            header2,
            income,
            expense,
            summary,
            total
        }

        public enum SysSetting
        {
            AllocationOverUnderCalcDate
        }
    }
}