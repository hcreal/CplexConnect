namespace CplexConnect.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sqlll : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Sections", "ID", "dbo.OverlapGroups");
            AddColumn("dbo.Sections", "OverlapGroups_ID", c => c.Int());
            CreateIndex("dbo.Sections", "OverlapGroups_ID");
            AddForeignKey("dbo.Sections", "OverlapGroups_ID", "dbo.OverlapGroups", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sections", "OverlapGroups_ID", "dbo.OverlapGroups");
            DropIndex("dbo.Sections", new[] { "OverlapGroups_ID" });
            DropColumn("dbo.Sections", "OverlapGroups_ID");
            AddForeignKey("dbo.Sections", "ID", "dbo.OverlapGroups", "ID");
        }
    }
}
