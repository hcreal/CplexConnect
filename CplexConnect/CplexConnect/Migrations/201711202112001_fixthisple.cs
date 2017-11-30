namespace CplexConnect.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixthisple : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Sections", "Groups_ID", "dbo.Groups");
            DropIndex("dbo.Sections", new[] { "Groups_ID" });

        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sections", "Groups_ID1", "dbo.Groups");
            DropIndex("dbo.Sections", new[] { "Groups_ID1" });

        }
    }
}
