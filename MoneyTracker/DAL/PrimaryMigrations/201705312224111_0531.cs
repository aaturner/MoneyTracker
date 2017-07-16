namespace MoneyTracker.DAL.PrimaryMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _0531 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ExpenseChangeEvents", "AllocationId", "dbo.Allocations");
            DropIndex("dbo.ExpenseChangeEvents", new[] { "AllocationId" });
            DropIndex("dbo.ExpenseChangeEvents", new[] { "Expense_Id" });
            AddColumn("dbo.ChangeEvents", "Amount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.ChangeEvents", "Expense_Id", c => c.Int());
            CreateIndex("dbo.ChangeEvents", "Expense_Id");
            DropColumn("dbo.ChangeEvents", "AmountDecimal");
            DropTable("dbo.ExpenseChangeEvents");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ExpenseChangeEvents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        EffectiveDate = c.DateTime(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ChangeType = c.Int(nullable: false),
                        AllocationId = c.Int(nullable: false),
                        Expense_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.ChangeEvents", "AmountDecimal", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropIndex("dbo.ChangeEvents", new[] { "Expense_Id" });
            DropColumn("dbo.ChangeEvents", "Expense_Id");
            DropColumn("dbo.ChangeEvents", "Amount");
            CreateIndex("dbo.ExpenseChangeEvents", "Expense_Id");
            CreateIndex("dbo.ExpenseChangeEvents", "AllocationId");
            AddForeignKey("dbo.ExpenseChangeEvents", "AllocationId", "dbo.Allocations", "Id", cascadeDelete: true);
        }
    }
}
