using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using MoneyTracker.Models;
using MoneyTracker.Models.Allocations;
using System.Data.Entity.Validation;
using System.Linq;

namespace MoneyTracker.DAL
{
    public class PrimaryContext : DbContext
    {
        public PrimaryContext()
            : base("name=PrimaryContext")
        {
            Database.SetInitializer<PrimaryContext>(new MigrateDatabaseToLatestVersion<PrimaryContext, PrimaryMigrations.Configuration>() );
            //Database.SetInitializer<PrimaryContext>(new DropCreateDatabaseIfModelChanges<PrimaryContext>());
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountBalanceEntry> AccountBalanceEntries { get; set; }
        public DbSet<Allocation> Allocations { get; set; }
        public DbSet<ChangeEvent> ChangeEvents { get; set; }
        public DbSet<ExpenseCategory> ExpenseCategories { get; set; }
        public DbSet<IncomeSource> IncomeSources { get; set; }
        public DbSet<LoanBalanceEntry> LoanBalanceEntries { get; set; }
        public DbSet<LoanPayment> LoanPayments { get; set; }
        public virtual DbSet<Person> Persons { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionTemp> TransactionTemps { get; set; }
        public DbSet<SystemSetting> SystemSettings { get; set; }

        //Identity Models
        





        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                string errorMessages = string.Join("; ", ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.PropertyName + ": " + x.ErrorMessage));
                throw new DbEntityValidationException(errorMessages);
            }
        }

        public System.Data.Entity.DbSet<MoneyTracker.Models.Allocations.SavingsInvestment> SavingsInvestments { get; set; }

        public System.Data.Entity.DbSet<MoneyTracker.Models.Allocations.Loan> Loans { get; set; }
    }
}