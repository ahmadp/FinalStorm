namespace Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Comment = c.String(),
                        CommentOwner = c.Int(),
                        TopicId = c.Int(nullable: false),
                        ParentCommentId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Comments", t => t.ParentCommentId)
                .ForeignKey("dbo.Topic", t => t.TopicId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.CommentOwner)
                .Index(t => t.CommentOwner)
                .Index(t => t.TopicId)
                .Index(t => t.ParentCommentId);
            
            CreateTable(
                "dbo.Notification",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserFollower",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        FollowerId = c.Int(nullable: false),
                        Isblocked = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserNotificationSetting",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AdminstrationTopics = c.Boolean(nullable: false),
                        NewComment = c.Boolean(nullable: false),
                        NewLikes = c.Boolean(nullable: false),
                        NewFriendRequest = c.Boolean(nullable: false),
                        NewTopicsAdded = c.Boolean(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            AddColumn("dbo.Topic", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.Topic", "UserId");
            AddForeignKey("dbo.Topic", "UserId", "dbo.Users", "id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserNotificationSetting", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserFollower", "UserId", "dbo.Users");
            DropForeignKey("dbo.Notification", "UserId", "dbo.Users");
            DropForeignKey("dbo.Comments", "CommentOwner", "dbo.Users");
            DropForeignKey("dbo.Comments", "TopicId", "dbo.Topic");
            DropForeignKey("dbo.Topic", "UserId", "dbo.Users");
            DropForeignKey("dbo.Comments", "ParentCommentId", "dbo.Comments");
            DropIndex("dbo.UserNotificationSetting", new[] { "UserId" });
            DropIndex("dbo.UserFollower", new[] { "UserId" });
            DropIndex("dbo.Notification", new[] { "UserId" });
            DropIndex("dbo.Topic", new[] { "UserId" });
            DropIndex("dbo.Comments", new[] { "ParentCommentId" });
            DropIndex("dbo.Comments", new[] { "TopicId" });
            DropIndex("dbo.Comments", new[] { "CommentOwner" });
            DropColumn("dbo.Topic", "UserId");
            DropTable("dbo.UserNotificationSetting");
            DropTable("dbo.UserFollower");
            DropTable("dbo.Notification");
            DropTable("dbo.Comments");
        }
    }
}
