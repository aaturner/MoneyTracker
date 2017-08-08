namespace MoneyTracker.DAL.PrimaryMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _0802a : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ChangeEvents", "Recurance", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ChangeEvents", "Recurance");
        }
    }
}
