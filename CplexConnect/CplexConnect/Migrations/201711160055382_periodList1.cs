namespace CplexConnect.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class periodList1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Periods", "Periods_ID", "dbo.Periods");
            DropIndex("dbo.Periods", new[] { "Periods_ID" });
            AddColumn("dbo.Periods", "TimeRange_ID", c => c.Int());
            CreateIndex("dbo.Periods", "TimeRange_ID");
            AddForeignKey("dbo.Periods", "TimeRange_ID", "dbo.TimeRanges", "ID");
            DropColumn("dbo.Periods", "Periods_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Periods", "Periods_ID", c => c.Int());
            DropForeignKey("dbo.Periods", "TimeRange_ID", "dbo.TimeRanges");
            DropIndex("dbo.Periods", new[] { "TimeRange_ID" });
            DropColumn("dbo.Periods", "TimeRange_ID");
            CreateIndex("dbo.Periods", "Periods_ID");
            AddForeignKey("dbo.Periods", "Periods_ID", "dbo.Periods", "ID");
        }
    }
}
