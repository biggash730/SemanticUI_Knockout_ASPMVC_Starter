namespace vls.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editedtheagentbranchtableagain : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AgentBranches", "CreatedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.AgentBranches", "UpdatedById", "dbo.AspNetUsers");
            DropIndex("dbo.AgentBranches", new[] { "CreatedById" });
            DropIndex("dbo.AgentBranches", new[] { "UpdatedById" });
            DropColumn("dbo.AgentBranches", "IsActive");
            DropColumn("dbo.AgentBranches", "IsDeleted");
            DropColumn("dbo.AgentBranches", "Created");
            DropColumn("dbo.AgentBranches", "Updated");
            DropColumn("dbo.AgentBranches", "CreatedById");
            DropColumn("dbo.AgentBranches", "UpdatedById");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AgentBranches", "UpdatedById", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.AgentBranches", "CreatedById", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.AgentBranches", "Updated", c => c.DateTime(nullable: false));
            AddColumn("dbo.AgentBranches", "Created", c => c.DateTime(nullable: false));
            AddColumn("dbo.AgentBranches", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.AgentBranches", "IsActive", c => c.Boolean(nullable: false));
            CreateIndex("dbo.AgentBranches", "UpdatedById");
            CreateIndex("dbo.AgentBranches", "CreatedById");
            AddForeignKey("dbo.AgentBranches", "UpdatedById", "dbo.AspNetUsers", "Id", cascadeDelete: false);
            AddForeignKey("dbo.AgentBranches", "CreatedById", "dbo.AspNetUsers", "Id", cascadeDelete: false);
        }
    }
}
