namespace MoneyTracker.DAL.PrimaryMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _11122017b : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.ChangeEvents", new[] { "Recurrence_Id" });
            DropForeignKey("dbo.ChangeEvents", "RecurrenceId", "dbo.Recurrences");
            DropColumn("dbo.ChangeEvents", "RecurrenceId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ChangeEvents", "RecurrenceId", c => c.Int());
            CreateIndex("dbo.ChangeEvents", "RecurrenceId");
            AddForeignKey("dbo.ChangeEvents", "RecurrenceId", "dbo.Recurrences", "Id");
        }
    }
}
