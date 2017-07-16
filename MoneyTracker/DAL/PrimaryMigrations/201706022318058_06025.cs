namespace MoneyTracker.DAL.PrimaryMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _06025 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.LoanBalanceEntries", "Loan_Id", "dbo.Allocations");
            DropIndex("dbo.LoanBalanceEntries", new[] { "Loan_Id" });
            RenameColumn(table: "dbo.LoanBalanceEntries", name: "Loan_Id", newName: "LoanId");
            AlterColumn("dbo.LoanBalanceEntries", "LoanId", c => c.Int(nullable: false));
            CreateIndex("dbo.LoanBalanceEntries", "LoanId");
            AddForeignKey("dbo.LoanBalanceEntries", "LoanId", "dbo.Allocations", "Id", cascadeDelete: true);
            DropColumn("dbo.LoanBalanceEntries", "LoansId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.LoanBalanceEntries", "LoansId", c => c.Int(nullable: false));
            DropForeignKey("dbo.LoanBalanceEntries", "LoanId", "dbo.Allocations");
            DropIndex("dbo.LoanBalanceEntries", new[] { "LoanId" });
            AlterColumn("dbo.LoanBalanceEntries", "LoanId", c => c.Int());
            RenameColumn(table: "dbo.LoanBalanceEntries", name: "LoanId", newName: "Loan_Id");
            CreateIndex("dbo.LoanBalanceEntries", "Loan_Id");
            AddForeignKey("dbo.LoanBalanceEntries", "Loan_Id", "dbo.Allocations", "Id");
        }
    }
}
