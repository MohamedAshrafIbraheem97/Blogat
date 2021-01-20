namespace Blogat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedFirstNameAndLastName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.People", "FirstName", c => c.String(nullable: false));
            AddColumn("dbo.People", "LastName", c => c.String(nullable: false));
            DropColumn("dbo.People", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.People", "Name", c => c.String(nullable: false));
            DropColumn("dbo.People", "LastName");
            DropColumn("dbo.People", "FirstName");
        }
    }
}
