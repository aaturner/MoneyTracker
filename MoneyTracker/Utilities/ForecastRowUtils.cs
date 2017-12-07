using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using MoneyTracker.AppModels;
using MoneyTracker.DAL;
using MoneyTracker.Models;
using MoneyTracker.Models.Allocations;
using MoneyTracker.Models.ChangeEvents;
using MoneyTracker.Models.Enums;
using MoneyTracker.Extensions;

namespace MoneyTracker.Utilities
{
    public static class ForecastRowUtils
    {
       
        public static List<ForecastRow> BuildForecastRows()
        {
            PrimaryContext db = new PrimaryContext();
            IEnumerable<Account> checkingAccounts = db.Accounts.Where(x => x.AccountType ==Enums.AccountType.Checking).ToList();
            IEnumerable<Account> loanAccounts = db.Accounts.Where(x => x.AccountType == Enums.AccountType.Loan).ToList();
            IEnumerable<Account> creditCards = db.Accounts.Where(x => x.AccountType == Enums.AccountType.CreditCard).ToList();
            IEnumerable<Account> savingsAccounts = db.Accounts.Where(x => x.AccountType == Enums.AccountType.Savings).ToList();
            IEnumerable<Account> investmentAccounts = db.Accounts.Where(x => x.AccountType == Enums.AccountType.Investment).ToList();
            IEnumerable<PayrollDeduction> deductions = db.PayrollDeductions.ToList();
            List<ForecastRow> retList = new List<ForecastRow>();

            BuildAccountRows(checkingAccounts,retList, Enums.AccountType.Checking);
            BuildAccountRows(loanAccounts, retList, Enums.AccountType.Loan);
            BuildAccountRows(creditCards, retList, Enums.AccountType.CreditCard);
            BuildAccountRows(savingsAccounts, retList, Enums.AccountType.Savings);
            BuildAccountRows(investmentAccounts, retList, Enums.AccountType.Investment);
            //BuildPayrollDeductions(deductions, retList);


            return retList;
        }



        #region Build Row Methods
        private static void BuildAccountRows(IEnumerable<Account> accounts, List<ForecastRow> retList, Enums.AccountType accntType)
        {
            retList.Add(BuildHeader1(accntType.ToDescription()));
            foreach (var account in accounts)
            {


                //Build rows
                decimal currentBal = GetCurrentBalance(account);
                decimal plusMonth1 = GetMonthAccountProjection(account, DateTime.Now.AddMonths(1)) + currentBal;
                decimal plusMonth2 = GetMonthAccountProjection(account, DateTime.Now.AddMonths(2)) + plusMonth1;
                decimal plusMonth3 = GetMonthAccountProjection(account, DateTime.Now.AddMonths(3)) + plusMonth2;
                decimal plusMonth4 = GetMonthAccountProjection(account, DateTime.Now.AddMonths(4)) + plusMonth3;
                decimal plusMonth5 = GetMonthAccountProjection(account, DateTime.Now.AddMonths(5)) + plusMonth4;
                decimal plusMonth6 = GetMonthAccountProjection(account, DateTime.Now.AddMonths(6)) + plusMonth5;
                decimal plusMonth7 = GetMonthAccountProjection(account, DateTime.Now.AddMonths(7)) + plusMonth6;
                decimal plusMonth8 = GetMonthAccountProjection(account, DateTime.Now.AddMonths(8)) + plusMonth7;
                decimal plusMonth9 = GetMonthAccountProjection(account, DateTime.Now.AddMonths(9)) + plusMonth8;
                decimal plusMonth10 = GetMonthAccountProjection(account, DateTime.Now.AddMonths(10)) + plusMonth9;
                decimal plusMonth11 = GetMonthAccountProjection(account, DateTime.Now.AddMonths(11)) + plusMonth10;

                decimal plusYear1 = GetMonthAccountProjection(account, DateTime.Now.AddYears(1)) + plusMonth11;
                decimal plusYear2 = GetMonthAccountProjection(account, DateTime.Now.AddYears(2))
                    + GetYearAccountProjections(account, 2);
                decimal plusYear3 = GetMonthAccountProjection(account, DateTime.Now.AddYears(3))
                    + GetYearAccountProjections(account, 3); ;

                retList.Add((new ForecastRow()
                {
                    AccountName = account.Name,
                    AccountType = account.AccountType.ToDescription(),

                    MonthCurrent = currentBal.ToString(CultureInfo.CurrentCulture),

                    Month1 = plusMonth1.ToString(),
                    Month2 = plusMonth2.ToString(),
                    Month3 = plusMonth3.ToString(),
                    Month4 = plusMonth4.ToString(),
                    Month5 = plusMonth5.ToString(),
                    Month6 = plusMonth6.ToString(),

                    Year1 = plusYear1.ToString(),
                    Year2 = plusYear2.ToString(),
                    Year3 = plusYear3.ToString()
                }
                ));
                #region old
                //PrimaryContext db = new PrimaryContext();
                ////Calculate current balance
                //decimal curentBal = GetCurrentBalance(account);

                ////Calculate monthly change
                //decimal monthDelta = GetMonthlyDelta(account);

                ////Collect Allocation change events  -- Not yet implimented
                //IEnumerable<AllocationChange> allocationChangeList = db.ChangeEvents.OfType<AllocationChange>().Where(x => x.Allocation.AccountId == account.Id);

                ////Collect Account change events
                //IEnumerable<AccountChange> accountChangeList = db.ChangeEvents.OfType<AccountChange>().Where(x => x.AccountId == account.Id);

                ////Build Projections
                //Dictionary<int, decimal> forecast = BuildAccountProjections(retList, accountChangeList, curentBal, monthDelta);
                //retList.Add(new ForecastRow(Enums.TableRowType.expense, account.Name, forecast[0].ToString(), 
                //    forecast[1].ToString(), forecast[2].ToString(),
                //    forecast[3].ToString(), forecast[4].ToString(), forecast[5].ToString(), forecast[6].ToString(),
                //    forecast[7].ToString(), forecast[8].ToString(), forecast[9].ToString()));
                #endregion
            }
        }
        private static void BuildLoanRows(IEnumerable<Loan> loans, List<ForecastRow> retList)
        {
            retList.Add(BuildHeader1("Loans"));
            foreach (var loan in loans)
            {
                var forecast = GetLoanProjections(loan);

                retList.Add(new ForecastRow(Enums.TableRowType.expense, loan.Name, forecast[0].ToString("F"), forecast[1].ToString("F"), 
                    forecast[2].ToString("F"), forecast[3].ToString("F"), forecast[4].ToString("F"), forecast[5].ToString("F"), 
                    forecast[6].ToString("F"), forecast[7].ToString("F"), forecast[8].ToString("F"), forecast[9].ToString("F")));

            }
        }

        private static void BuildCcRows(IEnumerable<Account> creditCards, List<ForecastRow> retList)
        {
            throw new NotImplementedException();
            //Maybe do this inside BuildAccount Rows
        }

        public static ForecastRow BuildHeader1(string headerTitle)
        {
            ForecastRow retRow = new ForecastRow();
            retRow.Header = headerTitle;
            retRow.RowType = Enums.TableRowType.header1;
            return retRow;
        }

        private static void BuildPayrollDeductions(IEnumerable<PayrollDeduction> deductions, List<ForecastRow> retList)
        {
            throw new NotImplementedException();
        }
        #endregion


        private static decimal GetYearAccountProjections(Account account, int yearsToAddFromNow)
        {
            decimal retVal = decimal.Zero;
            DateTime startDate = DateTime.Now.AddYears(yearsToAddFromNow - 1);
            for (int i = 1; i <= 12; i++)
            {
                retVal += GetMonthAccountProjection(account, startDate.AddMonths(i));
            }
            return retVal;
        }

        private static decimal GetMonthAccountProjection(Account account, DateTime targetMonth)
        {
            PrimaryContext db = new PrimaryContext();
            List<Allocation> accountAllocations;
            decimal? retVal = decimal.Zero;

            if (db.Allocations.Any(x => x.AccountId == account.Id))
            {
                accountAllocations = db.Allocations.Where(x => x.AccountId == account.Id).ToList();
                foreach (var allocation in accountAllocations)
                {
                    allocation.TempAmountDecimal = General.GetAllocationWithChangeEventsByMonth(allocation,targetMonth.Month,targetMonth.Year);
                }
                retVal += accountAllocations.OfType<Income>().Sum(x => x.TempAmountDecimal);
                retVal -= accountAllocations.OfType<Expense>().Sum(x => x.TempAmountDecimal);
                retVal -= accountAllocations.OfType<Loan>().Sum(x => x.TempAmountDecimal);
                retVal -= accountAllocations.OfType<SavingsInvestment>().Sum(x => x.Amount);
                if (account.AccountType != Enums.AccountType.Checking) retVal += GetMonthInterest(account, retVal);
            }
            return (decimal)retVal;
        }

        private static decimal? GetMonthInterest(Account account, decimal? amount)
        {
            decimal? retVal = amount * (1 + (account.Apr / 12));
            return retVal;
        }

        private static Dictionary<int, decimal> GetLoanProjections(Loan loan)
        {
            PrimaryContext db = new PrimaryContext();
            Dictionary<int, decimal> retDictionary = new Dictionary<int, decimal>();

            //Check that loan entry is valid
            //bool valid = true;
            LoanBalanceEntry balEntry = new LoanBalanceEntry();
            decimal payment = decimal.Zero;
            List<Transaction> transactionsResult;
            decimal transactionsAmount = decimal.Zero;

            //Still need to select most recent entry
            if (db.LoanBalanceEntries.Any(x => x.LoanId == loan.Id))
            {
                balEntry = db.LoanBalanceEntries.First(x => x.LoanId == loan.Id);
            }
            if (db.Allocations.Any(x => x.Id == loan.Id))
            {
                payment = db.Allocations.First(x => x.Id == loan.Id).Amount;
            }
            if (db.Transactions.Any(x => x.AllocationId == loan.Id &&
                                         x.TransactionDate > balEntry.Date))
            {
                transactionsResult = db.Transactions.Where(x => x.AllocationId == loan.Id &&
                                                                x.TransactionDate > balEntry.Date).ToList();
                transactionsAmount = transactionsResult.AsQueryable().Sum(x => x.Amount);
            }

            //TODO: This section needs cleaned up
            //try
            //{
            //    balEntry = db.LoanBalanceEntries.First(x => x.LoanId == loan.Id);
            //}
            //catch (Exception e)
            //{
            //    throw new Exception("Invalid balance entry for loan:" + loan.Name + Environment.NewLine+ e.Message);
            //}
            //try
            //{
            //    payment = db.Allocations.First(x => x.Id == loan.Id).Amount;
            //}
            //catch (Exception e)
            //{
            //    throw new Exception("Invalid payment entry for loan:" + loan.Name + Environment.NewLine + e.Message);
            //}
            //try
            //{
            //    transactionsResult = db.Transactions.Where(x => x.AllocationId == loan.Id &&
            //                                                    x.TransactionDate > balEntry.Date).ToList();
            //    if(transactionsResult.Any()) transactionsAmount = transactionsResult.AsQueryable().Sum(x => x.Amount);
            //}
            //catch (Exception e)
            //{
            //    throw new Exception("Invalid transactions result for loan:" + loan.Name + Environment.NewLine + e.Message);
            //}
            //in future log exception and continue, use NoBalEntry method 


            //Correct for time passed since entry = workingBal
            //________Diferent way of getting APR
            //TimeSpan span = new TimeSpan(System.DateTime.Now.Ticks - balEntry.Date.Ticks);
            //decimal interest = Decimal.Zero;
            //if(balEntry != null) Financial.GetMonthlyLoanInterest(balEntry.Amount, loan.Apr, (int)(span.Days / 30));

            //decimal initialBal = balEntry.Amount - transactionsAmount + interest;
            //decimal workingBal = initialBal;

            ////Current Month
            //retDictionary.Add(0,workingBal);

            ////For 6 months
            //for (int i = 1; i <= 6; i++)
            //{
            //    interest = Financial.GetMonthlyLoanInterest(workingBal, loan.Apr);
            //    //TODO: Change Events
            //    workingBal = workingBal - payment + interest;
            //    if (workingBal < 1) workingBal = 0;
            //    retDictionary.Add(i,workingBal);
            //}

            ////For 3 Years
            //workingBal = initialBal;
            //for (int i = 7; i <= 10; i++)
            //{
            //    interest = Financial.GetYearlyLoanInterest(workingBal, loan.Apr, payment);
            //    //TODO: Change Events
            //    workingBal = workingBal - payment * 12 + interest;
            //    if (workingBal < 1) workingBal = 0;
            //    retDictionary.Add(i, workingBal);
            //}

            return retDictionary;
        }

        //Move to general?
        public static decimal GetCurrentBalance(Account account)
        {
            PrimaryContext db = new PrimaryContext();
            decimal retVal = decimal.Zero;
            if (db.AccountBalanceEntries.Any(x => x.AccountId == account.Id))
            {
                AccountBalanceEntry lastEntry = db.AccountBalanceEntries.Where(x => x.AccountId == account.Id)
                    .OrderByDescending(x => x.Date).First();
                retVal = lastEntry.Amount;
            }


            // AccountBalanceEntry lastEntry = db.AccountBalanceEntries.Where(x => x.AccountId == account.Id).Last();
            return retVal;
        }





        //Old
        private static Dictionary<int, decimal> BuildAccountProjections(List<ForecastRow> retList, IEnumerable<ChangeEvent> accountChangeList,
            decimal curentBal, decimal monthDelta)
        {
            var retDictionary = new Dictionary<int, decimal>();
            decimal runningTotalDecimal = curentBal;

            retDictionary.Add(0, curentBal);

            //Build Months
            for (int i = 1; i < 7; i++)
            {
                var changeEventSum = GetChangeEventSum(accountChangeList, DateTime.Now.AddMonths(i), runningTotalDecimal);
                runningTotalDecimal += monthDelta + changeEventSum;
                retDictionary.Add(i, runningTotalDecimal);
            }

            //Build Years
            runningTotalDecimal = curentBal;
            for (int i = 7; i < 10; i++)
            {
                var changeEventSum = GetChangeEventSum(accountChangeList, DateTime.Now.AddYears(i - 6), curentBal, false);
                runningTotalDecimal += (monthDelta * 12) + changeEventSum;
                retDictionary.Add(i, runningTotalDecimal);
            }
            return retDictionary;
        }
        private static decimal GetChangeEventSum(IEnumerable<ChangeEvent> changeList, DateTime date, decimal currentAmount, bool byMonth = true)
        {
            decimal retDecimal = decimal.Zero;
            foreach (ChangeEvent change in changeList)
            {
                if (byMonth)
                {
                    int lastMonth = date.Month == 1 ? 12 : date.Month - 1;
                    if (change.EffectiveDateTime.Date <= new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month))
                    && change.EffectiveDateTime.Date >= new DateTime(date.Year, lastMonth, DateTime.DaysInMonth(date.Year, lastMonth)))
                    {
                        //change amount of allocation 
                        if (change.ChangeTypeEnum.Equals(ChangeTypeEnum.LumpSum))
                        {
                            retDecimal += change.Amount;
                        }
                        if (change.ChangeTypeEnum.Equals(ChangeTypeEnum.Percentage))
                        {
                            retDecimal = currentAmount * (1 + change.Amount);
                        }
                    }
                }
                else  //by year
                {
                    if (change.EffectiveDateTime.Date <= new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month))
                    && change.EffectiveDateTime.Date >= new DateTime(date.Year - 1, date.Month, DateTime.DaysInMonth(date.Year - 1, date.Month)))
                    {
                        //change amount of allocation 
                        if (change.ChangeTypeEnum.Equals(ChangeTypeEnum.LumpSum))
                        {
                            retDecimal += change.Amount;
                        }
                        if (change.ChangeTypeEnum.Equals(ChangeTypeEnum.Percentage))
                        {
                            retDecimal = currentAmount * (1 + change.Amount);
                        }
                    }
                }

            }
            return retDecimal;

        }

    }
}