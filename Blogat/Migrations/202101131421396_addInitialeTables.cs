namespace Blogat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addInitialeTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Blogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Heading = c.String(nullable: false, maxLength: 100),
                        Body = c.String(nullable: false),
                        TimeBloged = c.DateTime(nullable: false),
                        BlogLikes = c.Int(nullable: false),
                        PersonId = c.Int(nullable: false),
                        CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.People", t => t.PersonId, cascadeDelete: true)
                .Index(t => t.PersonId)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Body = c.String(nullable: false, maxLength: 500),
                        TimeCommented = c.DateTime(nullable: false),
                        CommentLikes = c.Int(nullable: false),
                        PersonId = c.Int(nullable: false),
                        Blog_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.People", t => t.PersonId, cascadeDelete: true)
                .ForeignKey("dbo.Blogs", t => t.Blog_Id)
                .Index(t => t.PersonId)
                .Index(t => t.Blog_Id);
            
            CreateTable(
                "dbo.People",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        DateOfBirth = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Blogs", "PersonId", "dbo.People");
            DropForeignKey("dbo.Comments", "Blog_Id", "dbo.Blogs");
            DropForeignKey("dbo.Comments", "PersonId", "dbo.People");
            DropForeignKey("dbo.Blogs", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Comments", new[] { "Blog_Id" });
            DropIndex("dbo.Comments", new[] { "PersonId" });
            DropIndex("dbo.Blogs", new[] { "CategoryId" });
            DropIndex("dbo.Blogs", new[] { "PersonId" });
            DropTable("dbo.People");
            DropTable("dbo.Comments");
            DropTable("dbo.Categories");
            DropTable("dbo.Blogs");
        }
    }
}
