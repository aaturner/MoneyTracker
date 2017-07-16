namespace MoneyTracker.DAL.PrimaryMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _0519_2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TransactionTemps",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TransactionType = c.Int(nullable: false),
                        EnteredDate = c.DateTime(nullable: false),
                        TransactionDate = c.DateTime(),
                        Description = c.String(),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AccountId = c.Int(nullable: false),
                        AllocationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.AccountId, cascadeDelete: true)
                .ForeignKey("dbo.Allocations", t => t.AllocationId, cascadeDelete: true)
                .Index(t => t.AccountId)
                .Index(t => t.AllocationId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TransactionTemps", "AllocationId", "dbo.Allocations");
            DropForeignKey("dbo.TransactionTemps", "AccountId", "dbo.Accounts");
            DropIndex("dbo.TransactionTemps", new[] { "AllocationId" });
            DropIndex("dbo.TransactionTemps", new[] { "AccountId" });
            DropTable("dbo.TransactionTemps");
        }
    }
}
