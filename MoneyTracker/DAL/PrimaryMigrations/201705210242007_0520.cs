namespace MoneyTracker.DAL.PrimaryMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _0520 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TransactionTemps", "AccountId", "dbo.Accounts");
            DropIndex("dbo.TransactionTemps", new[] { "AccountId" });
            AlterColumn("dbo.TransactionTemps", "AccountId", c => c.Int());
            CreateIndex("dbo.TransactionTemps", "AccountId");
            AddForeignKey("dbo.TransactionTemps", "AccountId", "dbo.Accounts", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TransactionTemps", "AccountId", "dbo.Accounts");
            DropIndex("dbo.TransactionTemps", new[] { "AccountId" });
            AlterColumn("dbo.TransactionTemps", "AccountId", c => c.Int(nullable: false));
            CreateIndex("dbo.TransactionTemps", "AccountId");
            AddForeignKey("dbo.TransactionTemps", "AccountId", "dbo.Accounts", "Id", cascadeDelete: true);
        }
    }
}
