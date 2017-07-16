namespace MoneyTracker.DAL.PrimaryMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _06022 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Allocations", "AccountId", c => c.Int());
            AddColumn("dbo.Allocations", "DestinationAccount_Id", c => c.Int());
            CreateIndex("dbo.Allocations", "AccountId");
            CreateIndex("dbo.Allocations", "DestinationAccount_Id");
            AddForeignKey("dbo.Allocations", "AccountId", "dbo.Accounts", "Id");
            AddForeignKey("dbo.Allocations", "DestinationAccount_Id", "dbo.Accounts", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Allocations", "DestinationAccount_Id", "dbo.Accounts");
            DropForeignKey("dbo.Allocations", "AccountId", "dbo.Accounts");
            DropIndex("dbo.Allocations", new[] { "DestinationAccount_Id" });
            DropIndex("dbo.Allocations", new[] { "AccountId" });
            DropColumn("dbo.Allocations", "DestinationAccount_Id");
            DropColumn("dbo.Allocations", "AccountId");
        }
    }
}
