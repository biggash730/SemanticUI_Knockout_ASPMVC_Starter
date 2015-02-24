namespace vls.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedidtypeandpaymentmodels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.IdTypes",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 64),
                        Description = c.String(maxLength: 512),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        Created = c.DateTime(nullable: false),
                        Updated = c.DateTime(nullable: false),
                        CreatedById = c.String(nullable: false, maxLength: 128),
                        UpdatedById = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedById, cascadeDelete: false)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedById, cascadeDelete: false)
                .Index(t => t.CreatedById)
                .Index(t => t.UpdatedById);
            
            CreateTable(
                "dbo.Payments",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        AgentId = c.String(nullable: false, maxLength: 128),
                        TransactionId = c.Long(nullable: false),
                        IdTypeId = c.Long(nullable: false),
                        IdNumber = c.String(nullable: false, maxLength: 128),
                        IdExpiryDate = c.DateTime(nullable: false),
                        FullName = c.String(nullable: false, maxLength: 128),
                        PhoneNumber = c.String(maxLength: 128),
                        AmountPaid = c.Double(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        Created = c.DateTime(nullable: false),
                        Updated = c.DateTime(nullable: false),
                        CreatedById = c.String(nullable: false, maxLength: 128),
                        UpdatedById = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.AgentId, cascadeDelete: false)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedById, cascadeDelete: false)
                .ForeignKey("dbo.IdTypes", t => t.IdTypeId, cascadeDelete: false)
                .ForeignKey("dbo.Transactions", t => t.TransactionId, cascadeDelete: false)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedById, cascadeDelete: false)
                .Index(t => t.AgentId)
                .Index(t => t.TransactionId)
                .Index(t => t.IdTypeId)
                .Index(t => t.CreatedById)
                .Index(t => t.UpdatedById);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Payments", "UpdatedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.Payments", "TransactionId", "dbo.Transactions");
            DropForeignKey("dbo.Payments", "IdTypeId", "dbo.IdTypes");
            DropForeignKey("dbo.Payments", "CreatedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.Payments", "AgentId", "dbo.AspNetUsers");
            DropForeignKey("dbo.IdTypes", "UpdatedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.IdTypes", "CreatedById", "dbo.AspNetUsers");
            DropIndex("dbo.Payments", new[] { "UpdatedById" });
            DropIndex("dbo.Payments", new[] { "CreatedById" });
            DropIndex("dbo.Payments", new[] { "IdTypeId" });
            DropIndex("dbo.Payments", new[] { "TransactionId" });
            DropIndex("dbo.Payments", new[] { "AgentId" });
            DropIndex("dbo.IdTypes", new[] { "UpdatedById" });
            DropIndex("dbo.IdTypes", new[] { "CreatedById" });
            DropTable("dbo.Payments");
            DropTable("dbo.IdTypes");
        }
    }
}
