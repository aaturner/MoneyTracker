namespace MoneyTracker.DAL.PrimaryMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _06043 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ChangeEvents", "AllocationId", "dbo.Allocations");
            DropIndex("dbo.ChangeEvents", new[] { "AllocationId" });
            DropColumn("dbo.ChangeEvents", "AllocationId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ChangeEvents", "AllocationId", c => c.Int(nullable: false));
            CreateIndex("dbo.ChangeEvents", "AllocationId");
            AddForeignKey("dbo.ChangeEvents", "AllocationId", "dbo.Allocations", "Id", cascadeDelete: true);
        }
    }
}
