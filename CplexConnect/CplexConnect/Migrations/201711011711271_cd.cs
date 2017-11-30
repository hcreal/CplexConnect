namespace CplexConnect.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Instructors", "CourseDist2", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Instructors", "CourseDist2");
        }
    }
}
