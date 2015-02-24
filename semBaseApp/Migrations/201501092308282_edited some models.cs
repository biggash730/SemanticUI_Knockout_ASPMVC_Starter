namespace vls.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editedsomemodels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ShortCode = c.String(nullable: false, maxLength: 128),
                        CurrencyId = c.Long(nullable: false),
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
                .ForeignKey("dbo.Currencies", t => t.CurrencyId, cascadeDelete: false)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedById, cascadeDelete: false)
                .Index(t => t.CurrencyId)
                .Index(t => t.CreatedById)
                .Index(t => t.UpdatedById);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FullName = c.String(nullable: false, maxLength: 128),
                        DateOfBirth = c.DateTime(nullable: false),
                        PhoneNumber = c.String(nullable: false, maxLength: 128),
                        Email = c.String(nullable: false, maxLength: 256),
                        Created = c.DateTime(nullable: false),
                        Updated = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: false)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: false)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: false)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: false)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Currencies",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Symbol = c.String(nullable: false, maxLength: 1),
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
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        SenderName = c.String(nullable: false, maxLength: 128),
                        SenderPhoneNumber = c.String(nullable: false, maxLength: 128),
                        Amount = c.Double(nullable: false),
                        CurrencyId = c.Long(nullable: false),
                        UniqueCode = c.String(maxLength: 64),
                        Pin = c.String(maxLength: 64),
                        RecipientName = c.String(nullable: false, maxLength: 128),
                        RecipientPhoneNumber = c.String(nullable: false, maxLength: 128),
                        RecipientCountryId = c.Long(nullable: false),
                        Status = c.String(nullable: false),
                        Notes = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        Created = c.DateTime(nullable: false),
                        Updated = c.DateTime(nullable: false),
                        CreatedById = c.String(nullable: false, maxLength: 128),
                        UpdatedById = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedById, cascadeDelete: false)
                .ForeignKey("dbo.Currencies", t => t.CurrencyId, cascadeDelete: false)
                .ForeignKey("dbo.Countries", t => t.RecipientCountryId, cascadeDelete: false)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedById, cascadeDelete: false)
                .Index(t => t.CurrencyId)
                .Index(t => t.RecipientCountryId)
                .Index(t => t.CreatedById)
                .Index(t => t.UpdatedById);
            
            CreateTable(
                "dbo.TransferFees",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        MaximumAmount = c.Double(nullable: false),
                        MinimumAmount = c.Double(nullable: false),
                        Fee = c.Double(nullable: false),
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
            DropForeignKey("dbo.TransferFees", "UpdatedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.TransferFees", "CreatedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.Transactions", "UpdatedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.Transactions", "RecipientCountryId", "dbo.Countries");
            DropForeignKey("dbo.Transactions", "CurrencyId", "dbo.Currencies");
            DropForeignKey("dbo.Transactions", "CreatedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Countries", "UpdatedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.Countries", "CurrencyId", "dbo.Currencies");
            DropForeignKey("dbo.Currencies", "UpdatedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.Currencies", "CreatedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.Countries", "CreatedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.TransferFees", new[] { "UpdatedById" });
            DropIndex("dbo.TransferFees", new[] { "CreatedById" });
            DropIndex("dbo.Transactions", new[] { "UpdatedById" });
            DropIndex("dbo.Transactions", new[] { "CreatedById" });
            DropIndex("dbo.Transactions", new[] { "RecipientCountryId" });
            DropIndex("dbo.Transactions", new[] { "CurrencyId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Currencies", new[] { "UpdatedById" });
            DropIndex("dbo.Currencies", new[] { "CreatedById" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Countries", new[] { "UpdatedById" });
            DropIndex("dbo.Countries", new[] { "CreatedById" });
            DropIndex("dbo.Countries", new[] { "CurrencyId" });
            DropTable("dbo.TransferFees");
            DropTable("dbo.Transactions");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Currencies");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Countries");
        }
    }
}
