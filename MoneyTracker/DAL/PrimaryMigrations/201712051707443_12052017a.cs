namespace MoneyTracker.DAL.PrimaryMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _12052017a : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Allocations", "isPayrollDeduction", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Allocations", "isPayrollDeduction");
        }
    }
}
