namespace CplexConnect.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class courses : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Groups", "Courses", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Groups", "Courses");
        }
    }
}
