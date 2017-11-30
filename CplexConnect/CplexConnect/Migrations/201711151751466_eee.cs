namespace CplexConnect.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class eee : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sections", "Groups_ID", c => c.Int());
            CreateIndex("dbo.Sections", "Groups_ID");
            AddForeignKey("dbo.Sections", "Groups_ID", "dbo.Groups", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sections", "Groups_ID", "dbo.Groups");
            DropIndex("dbo.Sections", new[] { "Groups_ID" });
            DropColumn("dbo.Sections", "Groups_ID");
        }
    }
}
