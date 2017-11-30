namespace CplexConnect.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addsemname : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InputDatas", "SemesterName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.InputDatas", "SemesterName");
        }
    }
}
