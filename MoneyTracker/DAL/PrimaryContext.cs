using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.ModelConfiguration.Conventions;
using MoneyTracker.Models;
using MoneyTracker.Models.Allocations;
using System.Data.Entity.Validation;
using System.Linq;
using MoneyTracker.Models.ChangeEvents;

namespace MoneyTracker.DAL
{
    public class PrimaryContext : DbContext
    {
        public PrimaryContext()
            : base("name=PrimaryContext")
        {
            //Database.SetInitializer<PrimaryContext>(new MigrateDatabaseToLatestVersion<PrimaryContext, PrimaryMigrations.Configuration>() );
            Database.SetInitializer<PrimaryContext>(new DropCreateDatabaseIfModelChanges<PrimaryContext>());

            //DbModelBuilder modelBuilder = new DbModelBuilder();
            //modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();   //For ChangeEvent to avoid multiple paths
            //modelBuilder.Entity<ChangeEvent>().WillCascadeOnDelete(false);


        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountBalanceEntry> AccountBalanceEntries { get; set; }
        public DbSet<Allocation> Allocations { get; set; }
        public DbSet<ChangeEvent> ChangeEvents { get; set; }
        public DbSet<ExpenseCategory> ExpenseCategories { get; set; }
        public DbSet<IncomeSource> IncomeSources { get; set; }
        public DbSet<LoanBalanceEntry> LoanBalanceEntries { get; set; }
        public virtual DbSet<Person> Persons { get; set; }
        public virtual DbSet<Recurrence> Recurrences  { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionTemp> TransactionTemps { get; set; }
        public DbSet<SystemSetting> SystemSettings { get; set; }
        public DbSet<PayrollDeduction> PayrollDeductions { get; set; }


        //Identity Models

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AllocationChange>()
                .HasRequired(a => a.Allocation).WithMany()
                .WillCascadeOnDelete(false);
            //c => (ICollection<AllocationChange>)c.ChangeEvents.OfType<AllocationChange>()
        }

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