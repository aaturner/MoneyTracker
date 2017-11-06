namespace MoneyTracker.DAL.PrimaryMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _11052017b : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.ChangeEvents", name: "Recurrence_Id", newName: "RecurrenceId");
            RenameIndex(table: "dbo.ChangeEvents", name: "IX_Recurrence_Id", newName: "IX_RecurrenceId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.ChangeEvents", name: "IX_RecurrenceId", newName: "IX_Recurrence_Id");
            RenameColumn(table: "dbo.ChangeEvents", name: "RecurrenceId", newName: "Recurrence_Id");
        }
    }
}
