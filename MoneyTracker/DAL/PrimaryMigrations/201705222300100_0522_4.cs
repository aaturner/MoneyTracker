namespace MoneyTracker.DAL.PrimaryMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _0522_4 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ChangeEvents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AllocationId = c.Int(nullable: false),
                        EffectiveDateTime = c.DateTime(nullable: false),
                        AmountDecimal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ChangeTypeEnum = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Allocations", t => t.AllocationId, cascadeDelete: true)
                .Index(t => t.AllocationId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ChangeEvents", "AllocationId", "dbo.Allocations");
            DropIndex("dbo.ChangeEvents", new[] { "AllocationId" });
            DropTable("dbo.ChangeEvents");
        }
    }
}
