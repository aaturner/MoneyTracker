namespace MoneyTracker.DAL.PrimaryMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccountBalanceEntries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AccountId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.AccountId)
                .Index(t => t.AccountId);
            
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Institution = c.String(),
                        Website = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Allocations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        IsMonthly = c.Boolean(),
                        RecuranceDayNumber = c.Int(nullable: false),
                        RecuranceEndDate = c.DateTime(),
                        AllocationType = c.Int(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ExpenseCategoryId = c.Int(),
                        PersonId = c.Int(),
                        IncomeSourceId = c.Int(),
                        Apr = c.Decimal(precision: 18, scale: 2),
                        AssetCurrentValue = c.Decimal(precision: 18, scale: 2),
                        Institution = c.String(),
                        Notes = c.String(),
                        Apr1 = c.Decimal(precision: 18, scale: 2),
                        CurrentValue = c.Decimal(precision: 18, scale: 2),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        Account_Id = c.Int(),
                        Account_Id1 = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.IncomeSources", t => t.IncomeSourceId, cascadeDelete: true)
                .ForeignKey("dbo.People", t => t.PersonId, cascadeDelete: true)
                .ForeignKey("dbo.ExpenseCategories", t => t.ExpenseCategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Accounts", t => t.Account_Id)
                .ForeignKey("dbo.Accounts", t => t.Account_Id1)
                .Index(t => t.ExpenseCategoryId)
                .Index(t => t.PersonId)
                .Index(t => t.IncomeSourceId)
                .Index(t => t.Account_Id)
                .Index(t => t.Account_Id1);
            
            CreateTable(
                "dbo.ExpenseChangeEvents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        EffectiveDate = c.DateTime(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ChangeType = c.Int(nullable: false),
                        AllocationId = c.Int(nullable: false),
                        Expense_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Allocations", t => t.AllocationId, cascadeDelete: true)
                .ForeignKey("dbo.Allocations", t => t.Expense_Id)
                .Index(t => t.AllocationId)
                .Index(t => t.Expense_Id);
            
            CreateTable(
                "dbo.IncomeChangeEvents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        EffeciveDate = c.DateTime(nullable: false),
                        ChangeType = c.Int(nullable: false),
                        AllocationId = c.Int(nullable: false),
                        Income_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Allocations", t => t.AllocationId, cascadeDelete: true)
                .ForeignKey("dbo.Allocations", t => t.Income_Id)
                .Index(t => t.AllocationId)
                .Index(t => t.Income_Id);
            
            CreateTable(
                "dbo.IncomeSources",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.People",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FullName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TransactionType = c.Int(nullable: false),
                        TransactionDate = c.DateTime(nullable: false),
                        EnteredDate = c.DateTime(),
                        Description = c.String(),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AccountId = c.Int(nullable: false),
                        AllocationBaseId = c.Int(),
                        Allocation_Id = c.Int(),
                        Income_Id = c.Int(),
                        Expense_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.AccountId, cascadeDelete: true)
                .ForeignKey("dbo.Allocations", t => t.Allocation_Id)
                .ForeignKey("dbo.Allocations", t => t.Income_Id)
                .ForeignKey("dbo.Allocations", t => t.Expense_Id)
                .Index(t => t.AccountId)
                .Index(t => t.Allocation_Id)
                .Index(t => t.Income_Id)
                .Index(t => t.Expense_Id);
            
            CreateTable(
                "dbo.LoanBalanceEntries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        LoansId = c.Int(nullable: false),
                        Loan_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Allocations", t => t.Loan_Id)
                .Index(t => t.Loan_Id);
            
            CreateTable(
                "dbo.LoanPayments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LoansId = c.Int(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PostDate = c.DateTime(nullable: false),
                        Loan_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Allocations", t => t.Loan_Id)
                .Index(t => t.Loan_Id);
            
            CreateTable(
                "dbo.SavingsInvestmentBalanceEntries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SavingsInvestmentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Allocations", t => t.SavingsInvestmentId, cascadeDelete: true)
                .Index(t => t.SavingsInvestmentId);
            
            CreateTable(
                "dbo.ExpenseCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Allocations", "Account_Id1", "dbo.Accounts");
            DropForeignKey("dbo.Allocations", "Account_Id", "dbo.Accounts");
            DropForeignKey("dbo.Transactions", "Expense_Id", "dbo.Allocations");
            DropForeignKey("dbo.Allocations", "ExpenseCategoryId", "dbo.ExpenseCategories");
            DropForeignKey("dbo.ExpenseChangeEvents", "Expense_Id", "dbo.Allocations");
            DropForeignKey("dbo.ExpenseChangeEvents", "AllocationId", "dbo.Allocations");
            DropForeignKey("dbo.SavingsInvestmentBalanceEntries", "SavingsInvestmentId", "dbo.Allocations");
            DropForeignKey("dbo.LoanPayments", "Loan_Id", "dbo.Allocations");
            DropForeignKey("dbo.LoanBalanceEntries", "Loan_Id", "dbo.Allocations");
            DropForeignKey("dbo.Transactions", "Income_Id", "dbo.Allocations");
            DropForeignKey("dbo.Transactions", "Allocation_Id", "dbo.Allocations");
            DropForeignKey("dbo.Transactions", "AccountId", "dbo.Accounts");
            DropForeignKey("dbo.Allocations", "PersonId", "dbo.People");
            DropForeignKey("dbo.Allocations", "IncomeSourceId", "dbo.IncomeSources");
            DropForeignKey("dbo.IncomeChangeEvents", "Income_Id", "dbo.Allocations");
            DropForeignKey("dbo.IncomeChangeEvents", "AllocationId", "dbo.Allocations");
            DropForeignKey("dbo.AccountBalanceEntries", "AccountId", "dbo.Accounts");
            DropIndex("dbo.SavingsInvestmentBalanceEntries", new[] { "SavingsInvestmentId" });
            DropIndex("dbo.LoanPayments", new[] { "Loan_Id" });
            DropIndex("dbo.LoanBalanceEntries", new[] { "Loan_Id" });
            DropIndex("dbo.Transactions", new[] { "Expense_Id" });
            DropIndex("dbo.Transactions", new[] { "Income_Id" });
            DropIndex("dbo.Transactions", new[] { "Allocation_Id" });
            DropIndex("dbo.Transactions", new[] { "AccountId" });
            DropIndex("dbo.IncomeChangeEvents", new[] { "Income_Id" });
            DropIndex("dbo.IncomeChangeEvents", new[] { "AllocationId" });
            DropIndex("dbo.ExpenseChangeEvents", new[] { "Expense_Id" });
            DropIndex("dbo.ExpenseChangeEvents", new[] { "AllocationId" });
            DropIndex("dbo.Allocations", new[] { "Account_Id1" });
            DropIndex("dbo.Allocations", new[] { "Account_Id" });
            DropIndex("dbo.Allocations", new[] { "IncomeSourceId" });
            DropIndex("dbo.Allocations", new[] { "PersonId" });
            DropIndex("dbo.Allocations", new[] { "ExpenseCategoryId" });
            DropIndex("dbo.AccountBalanceEntries", new[] { "AccountId" });
            DropTable("dbo.ExpenseCategories");
            DropTable("dbo.SavingsInvestmentBalanceEntries");
            DropTable("dbo.LoanPayments");
            DropTable("dbo.LoanBalanceEntries");
            DropTable("dbo.Transactions");
            DropTable("dbo.People");
            DropTable("dbo.IncomeSources");
            DropTable("dbo.IncomeChangeEvents");
            DropTable("dbo.ExpenseChangeEvents");
            DropTable("dbo.Allocations");
            DropTable("dbo.Accounts");
            DropTable("dbo.AccountBalanceEntries");
        }
    }
}
