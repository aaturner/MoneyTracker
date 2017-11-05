namespace MoneyTracker.DAL.PrimaryMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _0902b : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Allocations", "Recurance");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Allocations", "Recurance", c => c.Int(nullable: false));
        }
    }
}
