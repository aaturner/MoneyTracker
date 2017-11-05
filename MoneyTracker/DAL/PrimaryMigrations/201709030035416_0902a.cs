namespace MoneyTracker.DAL.PrimaryMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _0902a : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Allocations", "RecuranceId", c => c.Int(nullable: false));
            AddColumn("dbo.Allocations", "Recurance", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Allocations", "Recurance");
            DropColumn("dbo.Allocations", "RecuranceId");
        }
    }
}
