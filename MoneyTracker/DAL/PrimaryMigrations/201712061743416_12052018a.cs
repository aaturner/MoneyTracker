namespace MoneyTracker.DAL.PrimaryMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _12052018a : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Accounts", "AccountType", c => c.Int(nullable: false));
            AddColumn("dbo.Allocations", "TempAmountDecimal", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Allocations", "TempAmountDecimal");
            DropColumn("dbo.Accounts", "AccountType");
        }
    }
}
