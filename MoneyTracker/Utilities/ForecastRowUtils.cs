using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            IEnumerable<Account> accounts = db.Accounts.ToList();
            IEnumerable<Loan> loans = db.Loans.ToList();
            List<ForecastRow> retList = new List<ForecastRow>();

            BuildAccountRows(accounts,retList);
            BuildLoanRows(loans, retList);


            return retList;
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

        private static void BuildAccountRows(IEnumerable<Account> accounts, List<ForecastRow> retList)
        {
            retList.Add(BuildHeader1("Bank Accounts"));
            foreach (var account in accounts)
            {
                PrimaryContext db = new PrimaryContext();
                //Calculate current balance
                decimal curentBal = GetCurrentBalance(account);

                //Calculate monthly change
                decimal monthDelta = GetMonthlyDelta(account);

                //Collect Allocation change events  -- Not yet implimented
                IEnumerable<AllocationChange> allocationChangeList = db.ChangeEvents.OfType<AllocationChange>().Where(x => x.Allocation.AccountId == account.Id);

                //Collect Account change events
                IEnumerable<AccountChange> accountChangeList = db.ChangeEvents.OfType<AccountChange>().Where(x => x.AccountId == account.Id);

                //Build Projections
                Dictionary<int, decimal> forecast = BuildAccountProjections(retList, accountChangeList, curentBal, monthDelta);

                //Build rows
                retList.Add(new ForecastRow(Enums.TableRowType.expense, account.Name, forecast[0].ToString(), forecast[1].ToString(), forecast[2].ToString(),
                    forecast[3].ToString(), forecast[4].ToString(), forecast[5].ToString(), forecast[6].ToString(),
                    forecast[7].ToString(), forecast[8].ToString(), forecast[9].ToString()));

            }
        }

        public static ForecastRow BuildHeader1(string headerTitle)
        {
            ForecastRow retRow = new ForecastRow();
            retRow.AccountType = headerTitle;
            retRow.RowType = Enums.TableRowType.header1;
            return retRow;
        }


        private static Dictionary<int, decimal> BuildAccountProjections(List<ForecastRow> retList,IEnumerable<ChangeEvent> accountChangeList, 
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
                var changeEventSum = GetChangeEventSum(accountChangeList, DateTime.Now.AddYears(i-6), curentBal, false);
                runningTotalDecimal += (monthDelta * 12) + changeEventSum;
                retDictionary.Add(i,runningTotalDecimal);
            }
            return retDictionary;
        }

        private static decimal GetChangeEventSum(IEnumerable<ChangeEvent> changeList, DateTime date, decimal currentAmount, bool byMonth = true)
        {
            decimal retDecimal = decimal.Zero;
            foreach (ChangeEvent change in changeList)
            {
                if(byMonth)
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
                    && change.EffectiveDateTime.Date >= new DateTime(date.Year -1, date.Month, DateTime.DaysInMonth(date.Year -1, date.Month)))
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

        private static decimal GetMonthlyDelta(Account account)
        {
            PrimaryContext db = new PrimaryContext();
            decimal monthDelta = decimal.Zero;

            decimal incomeSum = 0;
            if (db.Allocations.OfType<Income>().Where(x => x.AccountId == account.Id).Any())
            {
                incomeSum = db.Allocations.OfType<Income>().Where(x => x.AccountId == account.Id).Sum(x => x.Amount);
            }

            decimal expenseSum = 0;
            if (db.Allocations.OfType<Expense>().Where(x => x.AccountId == account.Id).Any())
            {
                expenseSum = db.Allocations.OfType<Expense>().Where(x => x.AccountId == account.Id).Sum(x => x.Amount);
            }

            decimal loanSum = 0;
            if (db.Allocations.OfType<Loan>().Where(x => x.AccountId == account.Id).Any())
            {
                loanSum = db.Allocations.OfType<Loan>().Where(x => x.AccountId == account.Id).Sum(x => x.Amount);
            }

            decimal siDeductionSum = 0;
            if (db.Allocations.OfType<SavingsInvestment>().Where(x => x.AccountId == account.Id).Any())
            {
                siDeductionSum =
                    db.Allocations.OfType<SavingsInvestment>().Where(x => x.AccountId == account.Id).Sum(x => x.Amount);
            }

            decimal siDepositSum = 0;
            if (db.Allocations.OfType<SavingsInvestment>().Where(x => x.DestinationAccountId == account.Id).Any())
            {
                siDepositSum =
                    db.Allocations.OfType<SavingsInvestment>().Where(x => x.DestinationAccountId == account.Id).Sum(x => x.Amount);
            }

            monthDelta = (incomeSum + siDepositSum) - (expenseSum + loanSum + siDeductionSum);
            return monthDelta;
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
            if(db.LoanBalanceEntries.Any(x => x.LoanId == loan.Id))
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
            TimeSpan span = new TimeSpan(System.DateTime.Now.Ticks - balEntry.Date.Ticks);
            decimal interest = Decimal.Zero;
            if(balEntry != null) Financial.GetMonthlyLoanInterest(balEntry.Amount, loan.Apr, (int)(span.Days / 30));

            decimal initialBal = balEntry.Amount - transactionsAmount + interest;
            decimal workingBal = initialBal;
            
            //Current Month
            retDictionary.Add(0,workingBal);

            //For 6 months
            for (int i = 1; i <= 6; i++)
            {
                interest = Financial.GetMonthlyLoanInterest(workingBal, loan.Apr);
                //TODO: Change Events
                workingBal = workingBal - payment + interest;
                retDictionary.Add(i,workingBal);
            }

            //For 3 Years
            workingBal = initialBal;
            for (int i = 7; i <= 10; i++)
            {
                interest = Financial.GetYearlyLoanInterest(workingBal, loan.Apr, payment);
                //TODO: Change Events
                workingBal = workingBal - payment * 12 + interest;
                retDictionary.Add(i, workingBal);
            }

            return retDictionary;
        }

        private static Dictionary<int, decimal> NoBalEntry(Dictionary<int, decimal> retDictionary)
        {
            retDictionary.Add(0, .00m);
            retDictionary.Add(1, .00m);
            retDictionary.Add(2, .00m);
            retDictionary.Add(3, .00m);
            retDictionary.Add(4, .00m);
            retDictionary.Add(5, .00m);
            retDictionary.Add(6, .00m);
            retDictionary.Add(7, .00m);
            retDictionary.Add(8, .00m);
            retDictionary.Add(9, .00m);
            return retDictionary;
        }

        private static decimal GetCurrentBalance(Account account)
        {
            PrimaryContext db = new PrimaryContext();
            //This statement is simplified, will not always work
            AccountBalanceEntry lastEntry = db.AccountBalanceEntries.Where(x => x.AccountId == account.Id).FirstOrDefault();
            // AccountBalanceEntry lastEntry = db.AccountBalanceEntries.Where(x => x.AccountId == account.Id).Last();
            return lastEntry.Amount;
        }





    }
}