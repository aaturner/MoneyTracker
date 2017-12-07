namespace MoneyTracker.DAL.PrimaryMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _12052018d : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Allocations", "LoanAccountId", "dbo.Accounts");
            AddForeignKey("dbo.Allocations", "LoanAccountId", "dbo.Accounts", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Allocations", "LoanAccountId", "dbo.Accounts");
            AddForeignKey("dbo.Allocations", "LoanAccountId", "dbo.Accounts", "Id", cascadeDelete: true);
        }
    }
}
