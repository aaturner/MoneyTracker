namespace MoneyTracker.DAL.PrimaryMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _0604 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.ChangeEvents", new[] { "AllocationId" });
            AddColumn("dbo.Accounts", "AccountChange_Id", c => c.Int());
            AddColumn("dbo.ChangeEvents", "AccountId", c => c.Int());
            AddColumn("dbo.ChangeEvents", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.ChangeEvents", "AllocationId", c => c.Int());
            CreateIndex("dbo.Accounts", "AccountChange_Id");
            CreateIndex("dbo.ChangeEvents", "AllocationId");
            AddForeignKey("dbo.Accounts", "AccountChange_Id", "dbo.ChangeEvents", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Accounts", "AccountChange_Id", "dbo.ChangeEvents");
            DropIndex("dbo.ChangeEvents", new[] { "AllocationId" });
            DropIndex("dbo.Accounts", new[] { "AccountChange_Id" });
            AlterColumn("dbo.ChangeEvents", "AllocationId", c => c.Int(nullable: false));
            DropColumn("dbo.ChangeEvents", "Discriminator");
            DropColumn("dbo.ChangeEvents", "AccountId");
            DropColumn("dbo.Accounts", "AccountChange_Id");
            CreateIndex("dbo.ChangeEvents", "AllocationId");
        }
    }
}
