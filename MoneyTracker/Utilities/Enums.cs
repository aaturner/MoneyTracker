using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MoneyTracker.Utilities
{
    public static class Enums
    {

        public enum TableRowType
        {
            header1,
            header2,
            income,
            expense,
            loan,
            si,
            summary,
            total
        }

        public enum SysSetting
        {
            [Display(Name = "Calc Residual From Date")]
            AllocationOverUnderCalcDate,
            [Display(Name = "Week Start Day")]
            WeekStartDay,
            [Display(Name = "# Yr Displayed")]
            NumYearsToDisplay
        }

        public enum AllocationType
        {
            Income,
            Expense,
            [Display(Name = "Loans")]
            Loan,
            [Display(Name="Save/Invest")]
            Si
        }


    }
}