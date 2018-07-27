namespace SupportSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addNewFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Address", c => c.String());
            AddColumn("dbo.AspNetUsers", "City", c => c.String());
            AddColumn("dbo.AspNetUsers", "Country", c => c.String());
            AddColumn("dbo.AspNetUsers", "isActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "isActive");
            DropColumn("dbo.AspNetUsers", "Country");
            DropColumn("dbo.AspNetUsers", "City");
            DropColumn("dbo.AspNetUsers", "Address");
        }
    }
}
