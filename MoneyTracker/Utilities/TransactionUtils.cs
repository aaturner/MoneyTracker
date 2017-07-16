using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MoneyTracker.Models;
using MoneyTracker.DAL;
using System.Web.Mvc;
using System.Data;
using MoneyTracker.AppModels;
using MoneyTracker.Models.Allocations;


namespace MoneyTracker.Utilities
{
    public static class TransactionUtils
    {
        private static PrimaryContext db = new PrimaryContext();


        //public static List<SelectListItem> GetBudgetItems()
        //{
        //    List<SelectListItem> retList = new List<SelectListItem>();
        //    List<Income> incomes = db.Incomes.ToList();
        //    retList.Add(new SelectListItem() { Text = "Income", Value = "" });
        //    foreach (var income in incomes)
        //    {
        //        retList.Add(new SelectListItem
        //        {
        //            Text = "   " + income.Description + ", " + income.IncomeSource,
        //            Value = income.Id.ToString()
        //        });
        //    }
        //    retList.Add(new SelectListItem() { Text = "Expense", Value = "" });
        //    List<Expense> expenses = db.Expenses.ToList();      //.OrderBy(expense => expense.ExpenseCategory)
        //    foreach (var expense in expenses)
        //    {
        //        retList.Add(new SelectListItem
        //        {
        //            Text = "   " + expense.Name + ", " + expense.ExpenseCategory,
        //            Value = expense.Id.ToString()
        //        });
        //    }
        //    retList.Add(new SelectListItem() { Text = "Loans", Value = "" });
        //    List<Loan> loans = db.Loans.ToList();
        //    foreach (var loan in loans)
        //    {
        //        retList.Add(new SelectListItem
        //        {
        //            Text = "   " + loan.Name + ", " + loan.Description,
        //            Value = loan.Id.ToString()
        //        });
        //    }
        //    return retList;
        //}

        //public static List<BudgetEntry> GetBudgetEntries()
        //{
        //    List<BudgetEntry> retList = new List<BudgetEntry>();
        //    int i = 0;
        //    List<Income> incomes = db.Incomes.ToList();
        //    foreach (var income in incomes)
        //    {
        //        retList.Add(new BudgetEntry(i, income.Id, income.Description, BudgetItemTypeEnum.Income));
        //        i++;
        //    }
        //    List<Expense> expenses = db.Expenses.ToList();      //.OrderBy(expense => expense.ExpenseCategory)
        //    foreach (var expense in expenses)
        //    {
        //        retList.Add(new BudgetEntry(i, expense.Id, expense.Name, BudgetItemTypeEnum.Expense));
        //        i++;
        //    }
        //    List<Loan> loans = db.Loans.ToList();
        //    foreach (var loan in loans)
        //    {
        //        retList.Add(new BudgetEntry(i, loan.Id,loan.Name,BudgetItemTypeEnum.Loan));
        //    }
        //    return retList;
        //}


        internal static ICollection<TransactionTemp> GetTransactions(DataTable csvTable, int accountId)
        {
            //Account accnt = db.Accounts.Where(a => a.Id == accountId).SingleOrDefault();  
            ICollection<TransactionTemp> retList = new List<TransactionTemp>();
            ValidateHeaders(csvTable);
            for (int i = 0; i < csvTable.Rows.Count; i++)
            {
                retList.Add(new TransactionTemp(Convert.ToDateTime(csvTable.Rows[i][0]), //Transaction Date
                    csvTable.Rows[i][1].ToString(), //Description
                    Convert.ToDecimal(csvTable.Rows[i][2]), //Amount
                    accountId
                )); //Rows[i].Field<double>("DoubleColumn")
            }
            return retList;
        }

        private static void ValidateHeaders(DataTable csvTable)
        {
            //first row appearantly doesn't  have headers
            bool matchExpected = true;
            if (!string.Equals(csvTable.Columns[0].ColumnName.ToLower(), "transaction date")) matchExpected = false;
            if (!string.Equals(csvTable.Columns[1].ColumnName.ToLower(), "description")) matchExpected = false;
            if (!string.Equals(csvTable.Columns[2].ColumnName.ToLower(), "amount")) matchExpected = false;
            if (!matchExpected) throw new Exception("Upload file headers do not match expected.");
        }

        public static void CommitTransactions(TransactionsUpload model)
        {
            throw new NotImplementedException();
        }
    }
}