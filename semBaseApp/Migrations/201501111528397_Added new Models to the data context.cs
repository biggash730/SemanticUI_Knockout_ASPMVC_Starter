namespace vls.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddednewModelstothedatacontext : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AgentBranches",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        AgentId = c.Long(nullable: false),
                        BranchId = c.Long(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        Created = c.DateTime(nullable: false),
                        Updated = c.DateTime(nullable: false),
                        CreatedById = c.String(nullable: false, maxLength: 128),
                        UpdatedById = c.String(nullable: false, maxLength: 128),
                        Agent_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Agent_Id)
                .ForeignKey("dbo.Branches", t => t.BranchId, cascadeDelete: false)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedById, cascadeDelete: false)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedById, cascadeDelete: false)
                .Index(t => t.BranchId)
                .Index(t => t.CreatedById)
                .Index(t => t.UpdatedById)
                .Index(t => t.Agent_Id);
            
            CreateTable(
                "dbo.Branches",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Telephone = c.String(maxLength: 64),
                        PostalAddress = c.String(maxLength: 512),
                        ResidentialAddress = c.String(maxLength: 512),
                        Email = c.String(maxLength: 64),
                        CityId = c.Long(nullable: false),
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
                .ForeignKey("dbo.Cities", t => t.CityId, cascadeDelete: false)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedById, cascadeDelete: false)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedById, cascadeDelete: false)
                .Index(t => t.CityId)
                .Index(t => t.CreatedById)
                .Index(t => t.UpdatedById);
            
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        CountryId = c.Long(nullable: false),
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
                .ForeignKey("dbo.Countries", t => t.CountryId, cascadeDelete: false)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedById, cascadeDelete: false)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedById, cascadeDelete: false)
                .Index(t => t.CountryId)
                .Index(t => t.CreatedById)
                .Index(t => t.UpdatedById);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AgentBranches", "UpdatedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.AgentBranches", "CreatedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.AgentBranches", "BranchId", "dbo.Branches");
            DropForeignKey("dbo.Branches", "UpdatedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.Branches", "CreatedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.Branches", "CityId", "dbo.Cities");
            DropForeignKey("dbo.Cities", "UpdatedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.Cities", "CreatedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.Cities", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.AgentBranches", "Agent_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Cities", new[] { "UpdatedById" });
            DropIndex("dbo.Cities", new[] { "CreatedById" });
            DropIndex("dbo.Cities", new[] { "CountryId" });
            DropIndex("dbo.Branches", new[] { "UpdatedById" });
            DropIndex("dbo.Branches", new[] { "CreatedById" });
            DropIndex("dbo.Branches", new[] { "CityId" });
            DropIndex("dbo.AgentBranches", new[] { "Agent_Id" });
            DropIndex("dbo.AgentBranches", new[] { "UpdatedById" });
            DropIndex("dbo.AgentBranches", new[] { "CreatedById" });
            DropIndex("dbo.AgentBranches", new[] { "BranchId" });
            DropTable("dbo.Cities");
            DropTable("dbo.Branches");
            DropTable("dbo.AgentBranches");
        }
    }
}
