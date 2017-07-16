namespace MoneyTracker.DAL.PrimaryMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _06023 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Allocations", "DestinationAccount_Id", "dbo.Accounts");
            RenameColumn(table: "dbo.Allocations", name: "DestinationAccount_Id", newName: "DestinationAccountId");
            RenameIndex(table: "dbo.Allocations", name: "IX_DestinationAccount_Id", newName: "IX_DestinationAccountId");
            AddForeignKey("dbo.Allocations", "DestinationAccountId", "dbo.Accounts", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Allocations", "DestinationAccountId", "dbo.Accounts");
            RenameIndex(table: "dbo.Allocations", name: "IX_DestinationAccountId", newName: "IX_DestinationAccount_Id");
            RenameColumn(table: "dbo.Allocations", name: "DestinationAccountId", newName: "DestinationAccount_Id");
            AddForeignKey("dbo.Allocations", "DestinationAccount_Id", "dbo.Accounts", "Id");
        }
    }
}
