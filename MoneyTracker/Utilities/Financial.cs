using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoneyTracker.Utilities
{
    public class Financial
    {
        public static decimal GetMonthlyLoanInterest(decimal principle, decimal apr, int months = 1)
        {
            decimal retVal = decimal.Zero;
            decimal interest = decimal.Zero;
            decimal principleAndInterest = principle;
            for (int i = 1; i <= months; i++)
            {
                interest =  (principleAndInterest * apr / 1200);
                retVal += interest;
                principleAndInterest += interest;
            }

            return retVal;
        }
        public static decimal GetYearlyLoanInterest(decimal principle, decimal apr, decimal payment)
        {
            decimal retVal = decimal.Zero;
            decimal interest = decimal.Zero;
            decimal principleAndInterest = principle;
            for (int i = 1; i == 12; i++)
            {
                interest = (principleAndInterest * apr / 1200);
                retVal += interest;
                principleAndInterest += interest - payment;
            }
            
            return retVal;
        }
    }
}