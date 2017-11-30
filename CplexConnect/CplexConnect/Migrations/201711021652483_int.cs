namespace CplexConnect.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _int : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Instructors", "AssignCourseLoad", c => c.Int(nullable: false));
            AlterColumn("dbo.Instructors", "CourseDist", c => c.Int(nullable: false));
            AlterColumn("dbo.Instructors", "CourseDist2", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Instructors", "CourseDist2", c => c.String());
            AlterColumn("dbo.Instructors", "CourseDist", c => c.String());
            AlterColumn("dbo.Instructors", "AssignCourseLoad", c => c.String());
        }
    }
}
