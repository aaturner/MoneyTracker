namespace MoneyTracker.DAL.PrimaryMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _11162017c : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ChangeEvents", "IsRecurring", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ChangeEvents", "IsRecurring");
        }
    }
}
