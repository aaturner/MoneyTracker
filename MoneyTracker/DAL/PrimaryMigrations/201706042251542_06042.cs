namespace MoneyTracker.DAL.PrimaryMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _06042 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Accounts", "AccountChange_Id", "dbo.ChangeEvents");
            DropIndex("dbo.Accounts", new[] { "AccountChange_Id" });
            DropIndex("dbo.ChangeEvents", new[] { "AllocationId" });
            AlterColumn("dbo.ChangeEvents", "AllocationId", c => c.Int(nullable: false));
            CreateIndex("dbo.ChangeEvents", "AllocationId");
            DropColumn("dbo.Accounts", "AccountChange_Id");
            DropColumn("dbo.ChangeEvents", "AccountId");
            DropColumn("dbo.ChangeEvents", "Discriminator");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ChangeEvents", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.ChangeEvents", "AccountId", c => c.Int());
            AddColumn("dbo.Accounts", "AccountChange_Id", c => c.Int());
            DropIndex("dbo.ChangeEvents", new[] { "AllocationId" });
            AlterColumn("dbo.ChangeEvents", "AllocationId", c => c.Int());
            CreateIndex("dbo.ChangeEvents", "AllocationId");
            CreateIndex("dbo.Accounts", "AccountChange_Id");
            AddForeignKey("dbo.Accounts", "AccountChange_Id", "dbo.ChangeEvents", "Id");
        }
    }
}
