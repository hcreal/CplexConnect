namespace CplexConnect.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class periodonTr : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TimeRanges", "Periods", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TimeRanges", "Periods");
        }
    }
}
