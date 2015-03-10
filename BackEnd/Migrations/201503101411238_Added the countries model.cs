namespace BackEnd.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addedthecountriesmodel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ShortCode = c.String(nullable: false, maxLength: 128),
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Countries", "UpdatedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.Countries", "CreatedById", "dbo.AspNetUsers");
            DropIndex("dbo.Countries", new[] { "UpdatedById" });
            DropIndex("dbo.Countries", new[] { "CreatedById" });
            DropTable("dbo.Countries");
        }
    }
}
