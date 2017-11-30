namespace CplexConnect.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class secNums : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sections", "SectionNumbers", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sections", "SectionNumbers");
        }
    }
}
