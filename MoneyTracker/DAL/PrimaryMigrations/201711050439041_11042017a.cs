namespace MoneyTracker.DAL.PrimaryMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _11042017a : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.LoanPayments", "Loan_Id", "dbo.Allocations");
            DropIndex("dbo.LoanPayments", new[] { "Loan_Id" });
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
            
            AddColumn("dbo.Allocations", "Recurrence_Id", c => c.Int());
            AddColumn("dbo.ChangeEvents", "Recurrence_Id", c => c.Int());
            CreateIndex("dbo.Allocations", "Recurrence_Id");
            CreateIndex("dbo.ChangeEvents", "Recurrence_Id");
            AddForeignKey("dbo.ChangeEvents", "Recurrence_Id", "dbo.Recurrences", "Id");
            AddForeignKey("dbo.Allocations", "Recurrence_Id", "dbo.Recurrences", "Id");
            DropColumn("dbo.Allocations", "Recurance");
            DropColumn("dbo.Allocations", "ApplicableMonth");
            DropColumn("dbo.Allocations", "RecuranceDayNumber");
            DropColumn("dbo.Allocations", "RecuranceEndDate");
            DropColumn("dbo.ChangeEvents", "Recurance");
            DropTable("dbo.LoanPayments");
        }
        
        public override void Down()
        {
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
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.ChangeEvents", "Recurance", c => c.Int(nullable: false));
            AddColumn("dbo.Allocations", "RecuranceEndDate", c => c.DateTime());
            AddColumn("dbo.Allocations", "RecuranceDayNumber", c => c.Int(nullable: false));
            AddColumn("dbo.Allocations", "ApplicableMonth", c => c.DateTime(nullable: false));
            AddColumn("dbo.Allocations", "Recurance", c => c.Int(nullable: false));
            DropForeignKey("dbo.Allocations", "Recurrence_Id", "dbo.Recurrences");
            DropForeignKey("dbo.ChangeEvents", "Recurrence_Id", "dbo.Recurrences");
            DropIndex("dbo.ChangeEvents", new[] { "Recurrence_Id" });
            DropIndex("dbo.Allocations", new[] { "Recurrence_Id" });
            DropColumn("dbo.ChangeEvents", "Recurrence_Id");
            DropColumn("dbo.Allocations", "Recurrence_Id");
            DropTable("dbo.Recurrences");
            CreateIndex("dbo.LoanPayments", "Loan_Id");
            AddForeignKey("dbo.LoanPayments", "Loan_Id", "dbo.Allocations", "Id");
        }
    }
}
