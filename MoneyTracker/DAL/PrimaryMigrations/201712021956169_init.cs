namespace MoneyTracker.DAL.PrimaryMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
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
                        Name = c.String(nullable: false),
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
                        Description = c.String(),
                        IsMonthly = c.Boolean(),
                        RecurrenceId = c.Int(),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AccountId = c.Int(),
                        ExpenseCategoryId = c.Int(),
                        PersonId = c.Int(),
                        IncomeSourceId = c.Int(),
                        Apr = c.Decimal(precision: 18, scale: 2),
                        AssetCurrentValue = c.Decimal(precision: 18, scale: 2),
                        Institution = c.String(),
                        Notes = c.String(),
                        Apr1 = c.Decimal(precision: 18, scale: 2),
                        CurrentValue = c.Decimal(precision: 18, scale: 2),
                        DestinationAccountId = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        Account_Id = c.Int(),
                        Account_Id1 = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.AccountId)
                .ForeignKey("dbo.Recurrences", t => t.RecurrenceId)
                .ForeignKey("dbo.IncomeSources", t => t.IncomeSourceId, cascadeDelete: true)
                .ForeignKey("dbo.People", t => t.PersonId, cascadeDelete: true)
                .ForeignKey("dbo.Accounts", t => t.DestinationAccountId, cascadeDelete: true)
                .ForeignKey("dbo.ExpenseCategories", t => t.ExpenseCategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Accounts", t => t.Account_Id)
                .ForeignKey("dbo.Accounts", t => t.Account_Id1)
                .Index(t => t.RecurrenceId)
                .Index(t => t.AccountId)
                .Index(t => t.ExpenseCategoryId)
                .Index(t => t.PersonId)
                .Index(t => t.IncomeSourceId)
                .Index(t => t.DestinationAccountId)
                .Index(t => t.Account_Id)
                .Index(t => t.Account_Id1);
            
            CreateTable(
                "dbo.ChangeEvents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EffectiveDateTime = c.DateTime(nullable: false),
                        IsRecurring = c.Boolean(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ChangeTypeEnum = c.Int(nullable: false),
                        AccountId = c.Int(),
                        AllocationId = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        Allocation_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.AccountId, cascadeDelete: true)
                .ForeignKey("dbo.Allocations", t => t.Allocation_Id)
                .ForeignKey("dbo.Allocations", t => t.AllocationId)
                .Index(t => t.AccountId)
                .Index(t => t.AllocationId)
                .Index(t => t.Allocation_Id);
            
            CreateTable(
                "dbo.Recurrences",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RecurrenceFrequencyEnum = c.Int(nullable: false),
                        RecuranceStartDate = c.DateTime(nullable: false),
                        RecuranceEndDate = c.DateTime(),
                        RecuranceDayNumber = c.Int(),
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
                        AllocationId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.AccountId, cascadeDelete: true)
                .ForeignKey("dbo.Allocations", t => t.AllocationId)
                .Index(t => t.AccountId)
                .Index(t => t.AllocationId);
            
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
                "dbo.LoanBalanceEntries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        LoanId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Allocations", t => t.LoanId, cascadeDelete: true)
                .Index(t => t.LoanId);
            
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
            
            CreateTable(
                "dbo.SystemSettings",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Setting = c.Int(nullable: false),
                        SettingValue = c.String(),
                        SettingDate = c.DateTime(),
                        SettingDay = c.Int(),
                        SettingInt = c.Int(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.TransactionTemps",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TransactionType = c.Int(nullable: false),
                        EnteredDate = c.DateTime(nullable: false),
                        TransactionDate = c.DateTime(nullable: false),
                        Description = c.String(),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AccountId = c.Int(),
                        AllocationId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.AccountId)
                .ForeignKey("dbo.Allocations", t => t.AllocationId)
                .Index(t => t.AccountId)
                .Index(t => t.AllocationId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TransactionTemps", "AllocationId", "dbo.Allocations");
            DropForeignKey("dbo.TransactionTemps", "AccountId", "dbo.Accounts");
            DropForeignKey("dbo.Allocations", "Account_Id1", "dbo.Accounts");
            DropForeignKey("dbo.Allocations", "Account_Id", "dbo.Accounts");
            DropForeignKey("dbo.Allocations", "ExpenseCategoryId", "dbo.ExpenseCategories");
            DropForeignKey("dbo.ChangeEvents", "AllocationId", "dbo.Allocations");
            DropForeignKey("dbo.Allocations", "DestinationAccountId", "dbo.Accounts");
            DropForeignKey("dbo.SavingsInvestmentBalanceEntries", "SavingsInvestmentId", "dbo.Allocations");
            DropForeignKey("dbo.LoanBalanceEntries", "LoanId", "dbo.Allocations");
            DropForeignKey("dbo.Allocations", "PersonId", "dbo.People");
            DropForeignKey("dbo.Allocations", "IncomeSourceId", "dbo.IncomeSources");
            DropForeignKey("dbo.Transactions", "AllocationId", "dbo.Allocations");
            DropForeignKey("dbo.Transactions", "AccountId", "dbo.Accounts");
            DropForeignKey("dbo.Allocations", "RecurrenceId", "dbo.Recurrences");
            DropForeignKey("dbo.Allocations", "AccountId", "dbo.Accounts");
            DropForeignKey("dbo.ChangeEvents", "Allocation_Id", "dbo.Allocations");
            DropForeignKey("dbo.ChangeEvents", "AccountId", "dbo.Accounts");
            DropForeignKey("dbo.AccountBalanceEntries", "AccountId", "dbo.Accounts");
            DropIndex("dbo.TransactionTemps", new[] { "AllocationId" });
            DropIndex("dbo.TransactionTemps", new[] { "AccountId" });
            DropIndex("dbo.SavingsInvestmentBalanceEntries", new[] { "SavingsInvestmentId" });
            DropIndex("dbo.LoanBalanceEntries", new[] { "LoanId" });
            DropIndex("dbo.Transactions", new[] { "AllocationId" });
            DropIndex("dbo.Transactions", new[] { "AccountId" });
            DropIndex("dbo.ChangeEvents", new[] { "Allocation_Id" });
            DropIndex("dbo.ChangeEvents", new[] { "AllocationId" });
            DropIndex("dbo.ChangeEvents", new[] { "AccountId" });
            DropIndex("dbo.Allocations", new[] { "Account_Id1" });
            DropIndex("dbo.Allocations", new[] { "Account_Id" });
            DropIndex("dbo.Allocations", new[] { "DestinationAccountId" });
            DropIndex("dbo.Allocations", new[] { "IncomeSourceId" });
            DropIndex("dbo.Allocations", new[] { "PersonId" });
            DropIndex("dbo.Allocations", new[] { "ExpenseCategoryId" });
            DropIndex("dbo.Allocations", new[] { "AccountId" });
            DropIndex("dbo.Allocations", new[] { "RecurrenceId" });
            DropIndex("dbo.AccountBalanceEntries", new[] { "AccountId" });
            DropTable("dbo.TransactionTemps");
            DropTable("dbo.SystemSettings");
            DropTable("dbo.ExpenseCategories");
            DropTable("dbo.SavingsInvestmentBalanceEntries");
            DropTable("dbo.LoanBalanceEntries");
            DropTable("dbo.People");
            DropTable("dbo.IncomeSources");
            DropTable("dbo.Transactions");
            DropTable("dbo.Recurrences");
            DropTable("dbo.ChangeEvents");
            DropTable("dbo.Allocations");
            DropTable("dbo.Accounts");
            DropTable("dbo.AccountBalanceEntries");
        }
    }
}
