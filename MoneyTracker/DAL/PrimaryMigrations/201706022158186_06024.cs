namespace MoneyTracker.DAL.PrimaryMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _06024 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Allocations", "AllocationType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Allocations", "AllocationType", c => c.Int(nullable: false));
        }
    }
}
