namespace MoneyTracker.DAL.PrimaryMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _12052017b : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PayrollDeductions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Description = c.String(),
                        isPreTax = c.Boolean(nullable: false),
                        IncomeId = c.Int(nullable: false),
                        AccountId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.AccountId, cascadeDelete: false)
                .ForeignKey("dbo.Allocations", t => t.IncomeId, cascadeDelete: false)
                .Index(t => t.IncomeId)
                .Index(t => t.AccountId);
            
            DropColumn("dbo.Allocations", "isPayrollDeduction");
            DropColumn("dbo.Allocations", "isPreTax");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Allocations", "isPreTax", c => c.Boolean());
            AddColumn("dbo.Allocations", "isPayrollDeduction", c => c.Boolean(nullable: false));
            DropForeignKey("dbo.PayrollDeductions", "IncomeId", "dbo.Allocations");
            DropForeignKey("dbo.PayrollDeductions", "AccountId", "dbo.Accounts");
            DropIndex("dbo.PayrollDeductions", new[] { "AccountId" });
            DropIndex("dbo.PayrollDeductions", new[] { "IncomeId" });
            DropTable("dbo.PayrollDeductions");
        }
    }
}
