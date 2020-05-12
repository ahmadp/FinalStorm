namespace Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class intiDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.City",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CityName = c.String(),
                        CountryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Country", t => t.CountryId, cascadeDelete: true)
                .Index(t => t.CountryId);
            
            CreateTable(
                "dbo.Country",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SubscriptionType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SubsrciptionDescription = c.String(),
                        Cost = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Topic",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TopicName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        PhoneNumber = c.String(),
                        Password = c.String(),
                        NickName = c.String(),
                        JoiningDate = c.DateTime(nullable: false),
                        VerificationCode = c.String(),
                        VerificationCodeExpiry = c.DateTime(nullable: false),
                        CityId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.City", t => t.CityId, cascadeDelete: true)
                .Index(t => t.CityId);
            
            CreateTable(
                "dbo.UserSubscrption",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        SubscriptionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SubscriptionType", t => t.SubscriptionId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.SubscriptionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserSubscrption", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserSubscrption", "SubscriptionId", "dbo.SubscriptionType");
            DropForeignKey("dbo.Users", "CityId", "dbo.City");
            DropForeignKey("dbo.City", "CountryId", "dbo.Country");
            DropIndex("dbo.UserSubscrption", new[] { "SubscriptionId" });
            DropIndex("dbo.UserSubscrption", new[] { "UserId" });
            DropIndex("dbo.Users", new[] { "CityId" });
            DropIndex("dbo.City", new[] { "CountryId" });
            DropTable("dbo.UserSubscrption");
            DropTable("dbo.Users");
            DropTable("dbo.Topic");
            DropTable("dbo.SubscriptionType");
            DropTable("dbo.Country");
            DropTable("dbo.City");
        }
    }
}
