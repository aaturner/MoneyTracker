using System.Collections.Generic;
using System.Linq;
using MoneyTracker.AppModels;
using MoneyTracker.DAL;
using MoneyTracker.Models;
using MoneyTracker.Models.Allocations;
using MoneyTracker.Models.ChangeEvents;
using MoneyTracker.Models.DataObjects;
using System;
//using MoneyTracker.Models.ChangeEvents;

namespace MoneyTracker.Utilities
{
    public static class BudgetRowUtils
    {
        
        public static void ExpenseLines(List<BudgetRow> retList, int selectedMonth)  //need to distinguish year
        {
            PrimaryContext db = new PrimaryContext();
            IEnumerable<ExpenseCategory> Categories = db.ExpenseCategories.ToList();
            decimal budgetSubTotalDecimal = Decimal.Zero;
            decimal actualSubTotalDecimal = Decimal.Zero;
            foreach (ExpenseCategory category in Categories)
            {
                retList.Add(new BudgetRow("", category.Name, Enums.TableRowType.header2));
                foreach (Expense expense in category.Expenses)
                {
                    //Apply change events
                    decimal newAmount = General.GetAllocationWithChangeEventsByMonth(expense, selectedMonth);
                    
                    //Calculate Actuals
                    decimal transactSum = GetMonthTransactionActuals(expense, selectedMonth);

                    //Calculate Budget running total for sysSetting to date
                    decimal residual = GetResidualToDate(expense);

                    //Add budget row to retList
                    retList.Add(new BudgetRow("", "", expense.Name, "", newAmount, transactSum,
                        residual, Enums.TableRowType.expense, expense.Id));

                    //Sub Totals
                    budgetSubTotalDecimal += newAmount;
                    actualSubTotalDecimal += transactSum;

                }
            }
            retList.Find(x => x.Column1.Contains("Expense")).MoneyCol1 = budgetSubTotalDecimal;
            retList.Find(x => x.Column1.Contains("Expense")).MoneyCol2 = actualSubTotalDecimal;
        }

        public static void IncomeLines(List<BudgetRow> retList, int selectedMonth)
        {
            PrimaryContext db = new PrimaryContext();
            IEnumerable<IncomeSource> Sources = db.IncomeSources.ToList();
            decimal budgetSubTotalDecimal = Decimal.Zero;
            decimal actualSubTotalDecimal = Decimal.Zero;
            foreach (IncomeSource source in Sources)
            {
                retList.Add(new BudgetRow("", source.Name, Enums.TableRowType.header2));
                foreach (Income income in source.Incomes)
                {
                    //Apply change events
                    decimal newAmount = General.GetAllocationWithChangeEventsByMonth(income,selectedMonth);

                    //Calculate actuals
                    decimal transactSum = GetMonthTransactionActuals(income, selectedMonth);

                    retList.Add(new BudgetRow("", "", income.Name, "", newAmount, transactSum,
                        Enums.TableRowType.income, income.Id));
                    budgetSubTotalDecimal += newAmount;
                    actualSubTotalDecimal += transactSum;
                }
            }
            retList.Find(x => x.Column1.Contains("Income")).MoneyCol1 = budgetSubTotalDecimal;
            retList.Find(x => x.Column1.Contains("Income")).MoneyCol2 = actualSubTotalDecimal;
        }

        public static void LoanLines(List<BudgetRow> retList, int selectedMonth)
        {
            PrimaryContext db = new PrimaryContext();
            IEnumerable<Loan> Loans = db.Loans.ToList();
            decimal budgetSubTotalDecimal = Decimal.Zero;
            decimal actualSubTotalDecimal = Decimal.Zero;
            foreach (Loan loan in Loans)
            {
                //Apply change events
                decimal newAmount = General.GetAllocationWithChangeEventsByMonth(loan, selectedMonth);

                //Calculate Actuals
                decimal transactSum = GetMonthTransactionActuals(loan, selectedMonth);

                retList.Add(new BudgetRow("", loan.Name, loan.Description, "loan bal", newAmount, transactSum,
                    Enums.TableRowType.loan, loan.Id));
                budgetSubTotalDecimal += newAmount;
                actualSubTotalDecimal += transactSum;
            }
            retList.Find(x => x.Column1.Contains("Loans")).MoneyCol1 = budgetSubTotalDecimal;
            retList.Find(x => x.Column1.Contains("Loans")).MoneyCol2 = actualSubTotalDecimal;
        }

        public static void SaveInvestLines(List<BudgetRow> retList, int selectedMonth)
        {
            PrimaryContext db = new PrimaryContext();
            IEnumerable<SavingsInvestment> siEnumerable = db.SavingsInvestments.ToList();
            decimal subTotalDecimal = decimal.Zero;
            decimal actualSubTotalDecimal = decimal.Zero;
            foreach (var si in siEnumerable)
            {
                //Apply change events 
                decimal newAmount = General.GetAllocationWithChangeEventsByMonth(si, selectedMonth);
                decimal transactSum = GetMonthTransactionActuals(si, selectedMonth);

                retList.Add(new BudgetRow("", si.Name, si.Description, "loan bal", newAmount, transactSum,
                    Enums.TableRowType.si, si.Id));
                subTotalDecimal += newAmount;
                actualSubTotalDecimal += transactSum;
            }
            retList.Find(x => x.Column1.Contains("Save/Invest")).MoneyCol1 = subTotalDecimal;
            retList.Find(x => x.Column1.Contains("Save/Invest")).MoneyCol2 = actualSubTotalDecimal;
        }


        public static BudgetRow BuildHeader1(string headerTitle)
        {
            BudgetRow retRow = new BudgetRow();
            retRow.Column1 = headerTitle;
            retRow.RowType = Enums.TableRowType.header1;
            return retRow;
        }

        public static BudgetRow BuildSummaryRow(List<BudgetRow> retList)
        {
            BudgetRow retRow = new BudgetRow();
            retRow.Column1 = "Balance";
            //retRow.Column3 = "";
            retRow.MoneyCol1 = FindBalance(retList,1);
            retRow.MoneyCol2 = FindBalance(retList,2);
            retRow.RowType = Enums.TableRowType.total;
            return retRow;

        }
        /// <summary>
        /// Get balance of a list of budget rows
        /// </summary>
        /// <param name="retList"></param>
        /// <param name="column"> the colum number to sum (1 = Budget, 2 = Actual)s</param>
        /// <returns></returns>
        public static decimal FindBalance(List<BudgetRow> retList, int column)
        {
            decimal? balDecimal = Decimal.Zero;
            switch (column)
            {
                case 1:
                    foreach (var line in retList)
                    {
                        if (line.RowType == Enums.TableRowType.income)
                        {
                            balDecimal += line.MoneyCol1;
                        }
                        else if (line.RowType == Enums.TableRowType.expense)
                        {
                            balDecimal -= line.MoneyCol1;
                        }
                    }
                    if (!balDecimal.HasValue) balDecimal = decimal.Zero;
                    break;
                case 2:
                    foreach (var line in retList)
                    {
                        if (line.RowType == Enums.TableRowType.income)
                        {
                            balDecimal += line.MoneyCol2;
                        }
                        else if (line.RowType == Enums.TableRowType.expense)
                        {
                            balDecimal -= line.MoneyCol2;
                        }
                    }
                    if (!balDecimal.HasValue) balDecimal = decimal.Zero;
                    break;
            }

            return (decimal)balDecimal;
        }

        public static void BuildTotalLine(List<BudgetRow> retList)
        {
            BudgetRow retRow = new BudgetRow();
            retRow.Column1 = "Total";
            decimal? budgetBal = retList.Find(x => x.Column1.Contains("Bal")).MoneyCol1;
            decimal? actualBal = retList.Find(x => x.Column1.Contains("Bal")).MoneyCol2;
            decimal? budgetLoans = retList.Find(x => x.Column1.Contains("Loan")).MoneyCol1;
            decimal? actualLoans = retList.Find(x => x.Column1.Contains("Loan")).MoneyCol2;
            decimal? budgetSaveInvest = retList.Find(x => x.Column1.Contains("Save")).MoneyCol1;
            decimal? actualSaveInvest = retList.Find(x => x.Column1.Contains("Save")).MoneyCol2;
            retRow.MoneyCol1 = budgetBal-(budgetLoans + budgetSaveInvest);
            retRow.MoneyCol2 = actualBal-(actualLoans + actualSaveInvest);
            retRow.RowType = Enums.TableRowType.total;
            retList.Add(retRow);
        }

        private static decimal GetMonthTransactionActuals(Allocation allocation, int selectedMonth, int selectedYear = 0)
        {
            PrimaryContext db = new PrimaryContext();
            if (selectedYear == 0) selectedYear = System.DateTime.Now.Year;
            var transactions =
                db.Transactions.Where(x => x.AllocationId == allocation.Id &&
                                           x.TransactionDate.Month == selectedMonth &&
                                           x.TransactionDate.Year == System.DateTime.Now.Year); 
                                            //need to make year selectable and assign variable
            decimal transactSum = decimal.Zero;
            if (transactions.Any())
            {
                transactSum = transactions.Sum(x => x.Amount);
            }
            return transactSum;
        }

        private static decimal GetResidualToDate(Allocation allocation)
        {
            PrimaryContext db = new PrimaryContext();
            //Get List of Months
            SystemSetting setting = db.SystemSettings.FirstOrDefault(x => x.Setting == Enums.SysSetting.AllocationOverUnderCalcDate);
            DateTime start = setting.SettingDate;
            List<Month> Months = Utilities.General.GetMonthList(start, 
                DateTime.Now);

            //calculate total spent from cut off to now and total allocated
            decimal spentSum = decimal.Zero;
            decimal allocatedSum = decimal.Zero;
            foreach (Month m in Months)
            {
                spentSum += GetMonthTransactionActuals(allocation, m.MonthNumber, m.Year);
                allocatedSum += General.GetAllocationWithChangeEventsByMonth(allocation, m.MonthNumber, m.Year);
            }
            return allocatedSum - spentSum;
        }

    }
}