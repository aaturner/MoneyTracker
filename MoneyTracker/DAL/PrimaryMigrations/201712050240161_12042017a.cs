namespace MoneyTracker.DAL.PrimaryMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _12042017a : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Allocations", "isPreTax", c => c.Boolean());
            DropColumn("dbo.Allocations", "Institution");
            DropColumn("dbo.Allocations", "CurrentValue");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Allocations", "CurrentValue", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.Allocations", "Institution", c => c.String());
            DropColumn("dbo.Allocations", "isPreTax");
        }
    }
}
