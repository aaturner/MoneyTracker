namespace MoneyTracker.DAL.PrimaryMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _06045 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ChangeEvents", "AccountId", c => c.Int());
            AddColumn("dbo.ChangeEvents", "AllocationId", c => c.Int());
            AddColumn("dbo.ChangeEvents", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.ChangeEvents", "AccountId");
            CreateIndex("dbo.ChangeEvents", "AllocationId");
            AddForeignKey("dbo.ChangeEvents", "AccountId", "dbo.Accounts", "Id", cascadeDelete: false);
            AddForeignKey("dbo.ChangeEvents", "AllocationId", "dbo.Allocations", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ChangeEvents", "AllocationId", "dbo.Allocations");
            DropForeignKey("dbo.ChangeEvents", "AccountId", "dbo.Accounts");
            DropIndex("dbo.ChangeEvents", new[] { "AllocationId" });
            DropIndex("dbo.ChangeEvents", new[] { "AccountId" });
            DropColumn("dbo.ChangeEvents", "Discriminator");
            DropColumn("dbo.ChangeEvents", "AllocationId");
            DropColumn("dbo.ChangeEvents", "AccountId");
        }
    }
}
