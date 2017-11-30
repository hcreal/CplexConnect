namespace CplexConnect.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class periodList : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Periods", "Periods_ID", c => c.Int());
            CreateIndex("dbo.Periods", "Periods_ID");
            AddForeignKey("dbo.Periods", "Periods_ID", "dbo.Periods", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Periods", "Periods_ID", "dbo.Periods");
            DropIndex("dbo.Periods", new[] { "Periods_ID" });
            DropColumn("dbo.Periods", "Periods_ID");
        }
    }
}
