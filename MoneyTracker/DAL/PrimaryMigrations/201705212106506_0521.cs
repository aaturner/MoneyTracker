namespace MoneyTracker.DAL.PrimaryMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _0521 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TransactionTemps", "AllocationId", "dbo.Allocations");
            DropIndex("dbo.TransactionTemps", new[] { "AllocationId" });
            AlterColumn("dbo.TransactionTemps", "AllocationId", c => c.Int());
            CreateIndex("dbo.TransactionTemps", "AllocationId");
            AddForeignKey("dbo.TransactionTemps", "AllocationId", "dbo.Allocations", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TransactionTemps", "AllocationId", "dbo.Allocations");
            DropIndex("dbo.TransactionTemps", new[] { "AllocationId" });
            AlterColumn("dbo.TransactionTemps", "AllocationId", c => c.Int(nullable: false));
            CreateIndex("dbo.TransactionTemps", "AllocationId");
            AddForeignKey("dbo.TransactionTemps", "AllocationId", "dbo.Allocations", "Id", cascadeDelete: true);
        }
    }
}
