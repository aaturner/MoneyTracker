namespace MoneyTracker.DAL.PrimaryMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _12052018c : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Accounts", "Apr", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Allocations", "LoanAccountId", c => c.Int());
            CreateIndex("dbo.Allocations", "LoanAccountId");
            AddForeignKey("dbo.Allocations", "LoanAccountId", "dbo.Accounts", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Allocations", "LoanAccountId", "dbo.Accounts");
            DropIndex("dbo.Allocations", new[] { "LoanAccountId" });
            DropColumn("dbo.Allocations", "LoanAccountId");
            DropColumn("dbo.Accounts", "Apr");
        }
    }
}
