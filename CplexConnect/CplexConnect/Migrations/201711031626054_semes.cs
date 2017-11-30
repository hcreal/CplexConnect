namespace CplexConnect.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class semes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InputDatas", "Semester_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Instructors", "AssignCourseLoad", c => c.String());
            AlterColumn("dbo.Instructors", "CourseDist", c => c.String());
            AlterColumn("dbo.Instructors", "CourseDist2", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Instructors", "CourseDist2", c => c.Int(nullable: false));
            AlterColumn("dbo.Instructors", "CourseDist", c => c.Int(nullable: false));
            AlterColumn("dbo.Instructors", "AssignCourseLoad", c => c.Int(nullable: false));
            DropColumn("dbo.InputDatas", "Semester_Id");
        }
    }
}
