namespace vls.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editedtheagentbranchtable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AgentBranches", "Agent_Id", "dbo.AspNetUsers");
            DropIndex("dbo.AgentBranches", new[] { "Agent_Id" });
            DropColumn("dbo.AgentBranches", "AgentId");
            RenameColumn(table: "dbo.AgentBranches", name: "Agent_Id", newName: "AgentId");
            AlterColumn("dbo.AgentBranches", "AgentId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.AgentBranches", "AgentId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.AgentBranches", "AgentId");
            AddForeignKey("dbo.AgentBranches", "AgentId", "dbo.AspNetUsers", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AgentBranches", "AgentId", "dbo.AspNetUsers");
            DropIndex("dbo.AgentBranches", new[] { "AgentId" });
            AlterColumn("dbo.AgentBranches", "AgentId", c => c.String(maxLength: 128));
            AlterColumn("dbo.AgentBranches", "AgentId", c => c.Long(nullable: false));
            RenameColumn(table: "dbo.AgentBranches", name: "AgentId", newName: "Agent_Id");
            AddColumn("dbo.AgentBranches", "AgentId", c => c.Long(nullable: false));
            CreateIndex("dbo.AgentBranches", "Agent_Id");
            AddForeignKey("dbo.AgentBranches", "Agent_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
