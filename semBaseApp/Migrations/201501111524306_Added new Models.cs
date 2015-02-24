namespace vls.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddednewModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
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
            
            AddColumn("dbo.Currencies", "Rate", c => c.Double(nullable: false));
            AddColumn("dbo.Transactions", "Rate", c => c.Double(nullable: false));
            AddColumn("dbo.Transactions", "Total", c => c.Double(nullable: false));
            AddColumn("dbo.Transactions", "QuestionId", c => c.Long(nullable: false));
            AddColumn("dbo.Transactions", "Answer", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Transactions", "QuestionId");
            AddForeignKey("dbo.Transactions", "QuestionId", "dbo.Questions", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transactions", "QuestionId", "dbo.Questions");
            DropForeignKey("dbo.Questions", "UpdatedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.Questions", "CreatedById", "dbo.AspNetUsers");
            DropIndex("dbo.Questions", new[] { "UpdatedById" });
            DropIndex("dbo.Questions", new[] { "CreatedById" });
            DropIndex("dbo.Transactions", new[] { "QuestionId" });
            DropColumn("dbo.Transactions", "Answer");
            DropColumn("dbo.Transactions", "QuestionId");
            DropColumn("dbo.Transactions", "Total");
            DropColumn("dbo.Transactions", "Rate");
            DropColumn("dbo.Currencies", "Rate");
            DropTable("dbo.Questions");
        }
    }
}
