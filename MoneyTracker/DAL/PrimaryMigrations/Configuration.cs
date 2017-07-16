using MoneyTracker.Models;
using MoneyTracker.Models.Allocations;
using MoneyTracker.Utilities;


namespace MoneyTracker.DAL.PrimaryMigrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MoneyTracker.DAL.PrimaryContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            MigrationsDirectory = @"DAL\PrimaryMigrations";
            ContextKey = "MoneyTracker.DAL.PrimaryContext";
        }

        protected override void Seed(MoneyTracker.DAL.PrimaryContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.

            //context.Persons.AddOrUpdate(x => x.Id,
            //    new Person() { Id = 1, FullName = "Aaron" },
            //    new Person() { Id = 2, FullName = "Rachel" }
            //    );

            //context.Accounts.AddOrUpdate(x => x.Id,
            //    new Account() { Id = 1, Name = "Checking", Institution = "Capital One" },
            //    new Account() { Id = 2, Name = "Savings", Institution = "Capital One" },
            //    new Account() { Id = 3, Name = "Blue Checking", Institution = "Blue Credit Union" }
            //    );

            //context.ExpenseCategories.AddOrUpdate(x => x.Id,
            //    new ExpenseCategory() { Id = 1, Name = "Food" },
            //    new ExpenseCategory() { Id = 2, Name = "Donations" },
            //    new ExpenseCategory() { Id = 3, Name = "Home" },
            //    new ExpenseCategory() { Id = 4, Name = "Personal" },
            //    new ExpenseCategory() { Id = 5, Name = "Utilities" },
            //    new ExpenseCategory() { Id = 6, Name = "Auto" }
            //    );

            //context.IncomeSources.AddOrUpdate(x => x.Id,
            //    new IncomeSource() { Id = 1, Name = "SierraTradingPost", Description = "Main job" },
            //    new IncomeSource() { Id = 2, Name = "Other", Description = "other income" }
            //    );

            //context.Allocations.AddOrUpdate(x => x.Id,
            //    new Expense() { Id = 3, Name = "Groceries", Description = "Household supplies and food", IsMonthly = true, ExpenseCategoryId = 1, Amount = 1000 },
            //    new Expense() { Id = 4, Name = "Eat out", Description = "Restourant and meals eaten out of the home", IsMonthly = true, ExpenseCategoryId = 1, Amount = 100 },
            //    new Expense() { Id = 5, Name = "Web Purchased", Description = "Purchased on the internet", IsMonthly = true, ExpenseCategoryId = 1, Amount = 250 },
            //    new Expense() { Id = 6, Name = "Basement Work", Description = "Home improvment related", IsMonthly = true, ExpenseCategoryId = 3, Amount = 400 },
            //    new Expense() { Id = 7, Name = "Tithing", Description = "Church donations", IsMonthly = true, ExpenseCategoryId = 2, Amount = 700 },
            //    new Expense() { Id = 8, Name = "Haircuts", Description = "Personal Care", IsMonthly = true, ExpenseCategoryId = 4, Amount = 50 },
            //    new Expense() { Id = 9, Name = "Clothing", Description = "Adult and child clothing", IsMonthly = true, ExpenseCategoryId = 4, Amount = 50 },
            //    new Expense() { Id = 10, Name = "Gas/Electric", Description = "Black Hills", IsMonthly = true, RecuranceDayNumber = 6, ExpenseCategoryId = 5, Amount = 180 },
            //    new Expense() { Id = 11, Name = "Garbage/Water", Description = "Cheyenne City", IsMonthly = true, RecuranceDayNumber = 28, ExpenseCategoryId = 5, Amount = 75 },
            //    new Expense() { Id = 12, Name = "Gas", Description = "Auto fuel", IsMonthly = true, ExpenseCategoryId = 6, Amount = 250 },
            //    new Expense() { Id = 13, Name = "Insurance", Description = "Auto insurance", IsMonthly = true, ExpenseCategoryId = 6, Amount = 150 },
            //    new Expense() { Id = 14, Name = "Maintenance", Description = "Auto maintenance", IsMonthly = true, ExpenseCategoryId = 4, Amount = 100 },
            //    new Expense() { Id = 15, Name = "Pilot", Description = "Public Service Credit Union", IsMonthly = true, RecuranceDayNumber = 23, ExpenseCategoryId = 6, Amount = 400 },
            //    new Income() { Id = 16, Name = "1st 1/2", Description = "Primary Income", IsMonthly = true, RecuranceDayNumber = 1, Amount = 3200, PersonId = 1, IncomeSourceId = 3 },
            //    new Income() { Id = 17, Name = "2nd 1/2", Description = "Primary Income", IsMonthly = true, RecuranceDayNumber = 15, Amount = 3400, PersonId = 1, IncomeSourceId = 3 }
            //    );







        }
    }
}
