namespace CplexConnect.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class f : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Instructors", "InstructorID", c => c.String(nullable: false));
            AlterColumn("dbo.Instructors", "FirstName", c => c.String(nullable: false));
            AlterColumn("dbo.Instructors", "LastName", c => c.String(nullable: false));
            AlterColumn("dbo.Instructors", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.Instructors", "PrimaryProgram", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Instructors", "PrimaryProgram", c => c.String());
            AlterColumn("dbo.Instructors", "Email", c => c.String());
            AlterColumn("dbo.Instructors", "LastName", c => c.String());
            AlterColumn("dbo.Instructors", "FirstName", c => c.String());
            AlterColumn("dbo.Instructors", "InstructorID", c => c.String());
        }
    }
}
