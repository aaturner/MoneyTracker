namespace MoneyTracker.DAL.PrimaryMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _12072017a : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Allocations", "Apr");
            DropColumn("dbo.Allocations", "AssetCurrentValue");
            DropColumn("dbo.Allocations", "Apr1");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Allocations", "Apr1", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.Allocations", "AssetCurrentValue", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.Allocations", "Apr", c => c.Decimal(precision: 18, scale: 2));
        }
    }
}
