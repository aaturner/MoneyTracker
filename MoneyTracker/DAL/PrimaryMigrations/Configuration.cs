using MoneyTracker.Models;
using MoneyTracker.Models.Allocations;
using MoneyTracker.Models.Enums;
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



        //    context.Persons.AddOrUpdate(x => x.FullName,
        //            new Person() { Id = 1, FullName = "Aaron" },
        //            new Person() { Id = 2, FullName = "Rachel" }
        //           );

        //    context.Accounts.AddOrUpdate(x => x.Name,
        //        new Account() { Name = "Primary Checking", Institution = "Capital One" },
        //        new Account() { Name = "Primary Savings", Institution = "Capital One" }
        //        );

        //    context.ExpenseCategories.AddOrUpdate(x => x.Name,
        //        new ExpenseCategory() { Name = "Food" },
        //        new ExpenseCategory() { Name = "Donations" },
        //        new ExpenseCategory() { Name = "Home" },
        //        new ExpenseCategory() { Name = "Personal" },
        //        new ExpenseCategory() { Name = "Utilities" },
        //        new ExpenseCategory() { Name = "Auto" }
        //        );

        //    context.IncomeSources.AddOrUpdate(x => x.Name,
        //        new IncomeSource() { Name = "TJX" },
        //        new IncomeSource() { Name = "LCSD1" }
        //        );

        //    context.SaveChanges();

        //    #region Food
        //    context.Allocations.AddOrUpdate(x => x.Id,

        //        new Expense()
        //        {
        //            Name = "Groceries",
        //            Description = "Household supplies and food",
        //            Amount = 300,
        //            ExpenseCategory = context.ExpenseCategories.Single(x => x.Name == "Food"),
        //            FromAccount = context.Accounts.Single( x => x.Name == "Primary Checking"),
        //            Recurrence = new Recurrence()
        //            {
        //                RecurrenceFrequencyEnum = RecurrenceEnum.Weekly,
        //                RecuranceStartDate = DateTime.Now
        //            }
        //        },
        //        new Expense()
        //        {
        //            Name = "Eat out",
        //            Description = "Restourant and meals eaten out of the home",
        //            Amount = 100,
        //            ExpenseCategory = context.ExpenseCategories.Single(x => x.Name == "Food"),
        //            FromAccount = context.Accounts.Single(x => x.Name == "Primary Checking"),
        //            Recurrence = new Recurrence()
        //            {
        //                RecurrenceFrequencyEnum = RecurrenceEnum.Monthly,
        //                RecuranceStartDate = DateTime.Now
        //            }
        //        },
        //        new Expense()
        //        {
        //            Name = "Suppliment",
        //            Description = "Purchased on the internet",
        //            ExpenseCategoryId = 1,
        //            Amount = 250,
        //            ExpenseCategory = context.ExpenseCategories.Single(x => x.Name == "Food"),
        //            FromAccount = context.Accounts.Single(x => x.Name == "Primary Checking"),
        //            Recurrence = new Recurrence()
        //            {
        //                RecurrenceFrequencyEnum = RecurrenceEnum.Weekly,
        //                RecuranceStartDate = DateTime.Now
        //            }
        //        });

        //    #endregion

        //    context.SaveChanges();

        //    #region Home and Utilities
        //    context.Allocations.AddOrUpdate(x => x.Id,
        //        new Expense()
        //        {
        //            Name = "Basement Work",
        //            Description = "Home improvment related",
        //            IsMonthly = true,
        //            Amount = 400,
        //            ExpenseCategory = context.ExpenseCategories.Single(x => x.Name == "Home"),
        //            FromAccount = context.Accounts.Single(x => x.Name == "Primary Checking"),
        //            Recurrence = new Recurrence()
        //            {
        //                RecurrenceFrequencyEnum = RecurrenceEnum.Monthly,
        //                RecuranceStartDate = DateTime.Now
        //            }
        //        },
        //        new Expense()
        //        {
        //            Name = "Gas/Electric",
        //            Description = "Black Hills",
        //            Amount = 160,
        //            ExpenseCategory = context.ExpenseCategories.Single(x => x.Name == "Home"),
        //            FromAccount = context.Accounts.Single(x => x.Name == "Primary Checking"),
        //            Recurrence = new Recurrence()
        //            {
        //                RecurrenceFrequencyEnum = RecurrenceEnum.Monthly,
        //                RecuranceStartDate = DateTime.Now
        //            }
        //        },
        //        new Expense()
        //        {
        //            Name = "Garbage/Water",
        //            Description = "Cheyenne City",
        //            Amount = 75,
        //            ExpenseCategory = context.ExpenseCategories.Single(x => x.Name == "Home"),
        //            FromAccount = context.Accounts.Single(x => x.Name == "Primary Checking"),
        //            Recurrence = new Recurrence()
        //            {
        //                RecurrenceFrequencyEnum = RecurrenceEnum.Monthly,
        //                RecuranceStartDate = DateTime.Now
        //            }
        //        },
        //        new Expense()
        //        {
        //            Name = "HOA",
        //            Description = "Dakota Crossing",
        //            Amount = 130,
        //            ExpenseCategory = context.ExpenseCategories.Single(x => x.Name == "Home"),
        //            FromAccount = context.Accounts.Single(x => x.Name == "Primary Checking"),
        //            Recurrence = new Recurrence()
        //            {
        //                RecurrenceFrequencyEnum = RecurrenceEnum.Monthly,
        //                RecuranceStartDate = DateTime.Now
        //            }
        //        },

        //    #endregion

        //    #region Donations

        //        new Expense()
        //        {
        //            Name = "Tithing",
        //            Description = "Church donations",
        //            Amount = 700,
        //            ExpenseCategory = context.ExpenseCategories.Single(x => x.Name == "Donations"),
        //            FromAccount = context.Accounts.Single(x => x.Name == "Primary Checking"),
        //            Recurrence = new Recurrence()
        //            {
        //                RecurrenceFrequencyEnum = RecurrenceEnum.Monthly,
        //                RecuranceStartDate = DateTime.Now
        //            }
        //        },
        //        new Expense()
        //        {
        //            Name = "Fast Offering",
        //            Description = "Church donations",
        //            Amount = 100,
        //            ExpenseCategory = context.ExpenseCategories.Single(x => x.Name == "Donations"),
        //            FromAccount = context.Accounts.Single(x => x.Name == "Primary Checking"),
        //            Recurrence = new Recurrence()
        //            {
        //                RecurrenceFrequencyEnum = RecurrenceEnum.Monthly,
        //                RecuranceStartDate = DateTime.Now
        //            }
        //        });

        //    #endregion
        //    context.SaveChanges();

        //    #region Personal
        //    context.Allocations.AddOrUpdate(x => x.Id,
        //        new Expense()
        //        {
        //            Name = "Haircuts",
        //            Description = "Personal Care",
        //            Amount = 50,
        //            ExpenseCategory = context.ExpenseCategories.Single(x => x.Name == "Personal"),
        //            FromAccount = context.Accounts.Single(x => x.Name == "Primary Checking"),
        //            Recurrence = new Recurrence()
        //            {
        //                RecurrenceFrequencyEnum = RecurrenceEnum.Monthly,
        //                RecuranceStartDate = DateTime.Now
        //            }
        //        },
        //        new Expense()
        //        {
        //            Name = "Clothing",
        //            Description = "Personal Care",
        //            Amount = 50,
        //            ExpenseCategory = context.ExpenseCategories.Single(x => x.Name == "Personal"),
        //            FromAccount = context.Accounts.Single(x => x.Name == "Primary Checking"),
        //            Recurrence = new Recurrence()
        //            {
        //                RecurrenceFrequencyEnum = RecurrenceEnum.Monthly,
        //                RecuranceStartDate = DateTime.Now
        //            }
        //        },
        //        new Expense()
        //        {
        //            Name = "Kids",
        //            Description = "",
        //            Amount = 100,
        //            ExpenseCategory = context.ExpenseCategories.Single(x => x.Name == "Personal"),
        //            FromAccount = context.Accounts.Single(x => x.Name == "Primary Checking"),
        //            Recurrence = new Recurrence()
        //            {
        //                RecurrenceFrequencyEnum = RecurrenceEnum.Monthly,
        //                RecuranceStartDate = DateTime.Now
        //            }
        //        },
        //        new Expense()
        //        {
        //            Name = "Aaron",
        //            Description = "",
        //            Amount = 50,
        //            ExpenseCategory = context.ExpenseCategories.Single(x => x.Name == "Personal"),
        //            FromAccount = context.Accounts.Single(x => x.Name == "Primary Checking"),
        //            Recurrence = new Recurrence()
        //            {
        //                RecurrenceFrequencyEnum = RecurrenceEnum.Monthly,
        //                RecuranceStartDate = DateTime.Now
        //            }
        //        },
        //        new Expense()
        //        {
        //            Name = "Rachel",
        //            Description = "",
        //            Amount = 50,
        //            ExpenseCategory = context.ExpenseCategories.Single(x => x.Name == "Personal"),
        //            FromAccount = context.Accounts.Single(x => x.Name == "Primary Checking"),
        //            Recurrence = new Recurrence()
        //            {
        //                RecurrenceFrequencyEnum = RecurrenceEnum.Monthly,
        //                RecuranceStartDate = DateTime.Now
        //            }
        //        },
        //        new Expense()
        //        {
        //            Name = "Misc",
        //            Description = "",
        //            Amount = 100,
        //            ExpenseCategory = context.ExpenseCategories.Single(x => x.Name == "Personal"),
        //            FromAccount = context.Accounts.Single(x => x.Name == "Primary Checking"),
        //            Recurrence = new Recurrence()
        //            {
        //                RecurrenceFrequencyEnum = RecurrenceEnum.Monthly,
        //                RecuranceStartDate = DateTime.Now
        //            }
        //        },
        //        new Expense()
        //        {
        //            Name = "Entertainment",
        //            Description = "",
        //            Amount = 40,
        //            ExpenseCategory = context.ExpenseCategories.Single(x => x.Name == "Personal"),
        //            FromAccount = context.Accounts.Single(x => x.Name == "Primary Checking"),
        //            Recurrence = new Recurrence()
        //            {
        //                RecurrenceFrequencyEnum = RecurrenceEnum.Monthly,
        //                RecuranceStartDate = DateTime.Now
        //            }
        //        },
        //        new Expense()
        //        {
        //            Name = "Travel",
        //            Description = "",
        //            Amount = 50,
        //            ExpenseCategory = context.ExpenseCategories.Single(x => x.Name == "Personal"),
        //            FromAccount = context.Accounts.Single(x => x.Name == "Primary Checking"),
        //            Recurrence = new Recurrence()
        //            {
        //                RecurrenceFrequencyEnum = RecurrenceEnum.Monthly,
        //                RecuranceStartDate = DateTime.Now
        //            }
        //        },
        //        new Expense()
        //        {
        //            Name = "Gifts",
        //            Description = "",
        //            Amount = 50,
        //            ExpenseCategory = context.ExpenseCategories.Single(x => x.Name == "Personal"),
        //            FromAccount = context.Accounts.Single(x => x.Name == "Primary Checking"),
        //            Recurrence = new Recurrence()
        //            {
        //                RecurrenceFrequencyEnum = RecurrenceEnum.Monthly,
        //                RecuranceStartDate = DateTime.Now
        //            }
        //        });

        //    #endregion
        //    context.SaveChanges();

        //    #region Auto
        //    context.Allocations.AddOrUpdate(x => x.Id,
        //        new Expense()
        //        {
        //            Name = "Gas",
        //            Description = "Auto fuel",
        //            Amount = 250,
        //            ExpenseCategory = context.ExpenseCategories.Single(x => x.Name == "Auto"),
        //            FromAccount = context.Accounts.Single(x => x.Name == "Primary Checking"),
        //            Recurrence = new Recurrence()
        //            {
        //                RecurrenceFrequencyEnum = RecurrenceEnum.Monthly,
        //                RecuranceStartDate = DateTime.Now
        //            }
        //        },
        //        new Expense()
        //        {
        //            Name = "Insurance",
        //            Description = "Auto insurance",
        //            Amount = 150,
        //            ExpenseCategory = context.ExpenseCategories.Single(x => x.Name == "Auto"),
        //            FromAccount = context.Accounts.Single(x => x.Name == "Primary Checking"),
        //            Recurrence = new Recurrence()
        //            {
        //                RecurrenceFrequencyEnum = RecurrenceEnum.Monthly,
        //                RecuranceStartDate = DateTime.Now
        //            }
        //        },
        //        new Expense()
        //        {
        //            Name = "Maintenance",
        //            Description = "Auto maintenance",
        //            Amount = 100,
        //            ExpenseCategory = context.ExpenseCategories.Single(x => x.Name == "Auto"),
        //            FromAccount = context.Accounts.Single(x => x.Name == "Primary Checking"),
        //            Recurrence = new Recurrence()
        //            {
        //                RecurrenceFrequencyEnum = RecurrenceEnum.Monthly,
        //                RecuranceStartDate = DateTime.Now
        //            }
        //        });

        //    #endregion
        //    context.SaveChanges();

        //    #region Income
        //    context.Allocations.AddOrUpdate(x => x.Id,
        //        new Income()
        //        {
        //            Name = "TJX",
        //            Description = "Primary Income",
        //            Amount = 1400,
        //            Person = context.Persons.Single(x => x.FullName == "Aaron"),
        //            IncomeSource = context.IncomeSources.Single(x => x.Name == "TJX"),
        //            FromAccount = context.Accounts.Single(x => x.Name == "Primary Checking"),
        //            Recurrence = new Recurrence()
        //            {
        //                RecurrenceFrequencyEnum = RecurrenceEnum.Weekly,
        //                RecuranceStartDate = DateTime.Now
        //            }

        //        },
        //        new Income()
        //        {
        //            Name = "LCSD 1st 1/2",
        //            Description = "LCSD",
        //            Amount = 1500,
        //            Person = context.Persons.Single(x => x.FullName == "Rachel"),
        //            IncomeSource = context.IncomeSources.Single(x => x.Name == "LCSD1"),
        //            FromAccount = context.Accounts.Single(x => x.Name == "Primary Checking"),
        //            Recurrence = new Recurrence()
        //            {
        //                RecurrenceFrequencyEnum = RecurrenceEnum.Monthly,
        //                RecuranceStartDate = DateTime.Now,
        //                RecuranceDayNumber = 1
        //            }

        //        },
        //        new Income()
        //        {
        //            Name = "LCSD 2nd 1/2",
        //            Description = "LCSD",
        //            Amount = 1500,
        //            Person = context.Persons.Single(x => x.FullName == "Rachel"),
        //            IncomeSource = context.IncomeSources.Single(x => x.Name == "LCSD1"),
        //            FromAccount = context.Accounts.Single(x => x.Name == "Primary Checking"),
        //            Recurrence = new Recurrence()
        //            {
        //                RecurrenceFrequencyEnum = RecurrenceEnum.Monthly,
        //                RecuranceStartDate = DateTime.Now,
        //                RecuranceDayNumber = 15
        //            }

        //        });

        //    #endregion
        //    context.SaveChanges();

        //    #region Loans
        //    context.Allocations.AddOrUpdate(x => x.Id,
        //    new Loan()
        //    {
        //        Name = "Mortgage",
        //        Description = "",
        //        Amount = 1600,
        //        FromAccount = context.Accounts.Single(x => x.Name == "Primary Checking"),
        //        Apr = 4.50m,
        //        Recurrence = new Recurrence()
        //        {
        //            RecurrenceFrequencyEnum = RecurrenceEnum.Monthly,
        //            RecuranceStartDate = DateTime.Now,
        //            RecuranceDayNumber = 5
        //        }

        //    },
        //    new Loan()
        //    {
        //        Name = "Pilot",
        //        Description = "Public Service CU",
        //        Amount = 380,
        //        FromAccount = context.Accounts.Single(x => x.Name == "Primary Checking"),
        //        Apr = 4.50m,
        //        Recurrence = new Recurrence()
        //        {
        //            RecurrenceFrequencyEnum = RecurrenceEnum.Monthly,
        //            RecuranceStartDate = DateTime.Now,
        //            RecuranceDayNumber = 1
        //        }

        //    },
        //    new Loan()
        //    {
        //        Name = "Cap One  CC",
        //        Description = "Capital One",
        //        Amount = 200,
        //        FromAccount = context.Accounts.Single(x => x.Name == "Primary Checking"),
        //        Apr = 19.50m,
        //        Recurrence = new Recurrence()
        //        {
        //            RecurrenceFrequencyEnum = RecurrenceEnum.Monthly,
        //            RecuranceStartDate = DateTime.Now,
        //            RecuranceDayNumber = 15
        //        }
        //    },
        //    new Loan()
        //    {
        //        Name = "Amex  CC",
        //        Description = "American Express",
        //        Amount = 200,
        //        FromAccount = context.Accounts.Single(x => x.Name == "Primary Checking"),
        //        Apr = 19.50m,
        //        Recurrence = new Recurrence()
        //        {
        //            RecurrenceFrequencyEnum = RecurrenceEnum.Monthly,
        //            RecuranceStartDate = DateTime.Now,
        //            RecuranceDayNumber = 15
        //        }
        //    },

        //    #endregion

        //new SavingsInvestment()
        //{

        //    Name = "Savings",
        //    Description = "",
        //    Amount = 500,
        //    FromAccount = context.Accounts.Single(x => x.Name == "Primary Checking"),
        //    DestinationAccount = context.Accounts.Single(x => x.Name == "Primary Savings"),
        //    Apr = 1.50m,
        //    Recurrence = new Recurrence()
        //    {
        //        RecurrenceFrequencyEnum = RecurrenceEnum.Monthly,
        //        RecuranceStartDate = DateTime.Now,
        //        RecuranceDayNumber = 15
        //    }


        //});
        //    context.SaveChanges();
        }
    }
}
