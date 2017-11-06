namespace MoneyTracker.DAL.PrimaryMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _11052017a : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Allocations", name: "Recurrence_Id", newName: "RecurrenceId");
            RenameIndex(table: "dbo.Allocations", name: "IX_Recurrence_Id", newName: "IX_RecurrenceId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Allocations", name: "IX_RecurrenceId", newName: "IX_Recurrence_Id");
            RenameColumn(table: "dbo.Allocations", name: "RecurrenceId", newName: "Recurrence_Id");
        }
    }
}
