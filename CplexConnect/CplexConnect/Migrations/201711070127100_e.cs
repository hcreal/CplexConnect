namespace CplexConnect.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class e : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Instructors", "PrimaryProgram", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Instructors", "PrimaryProgram", c => c.String(nullable: false));
        }
    }
}
