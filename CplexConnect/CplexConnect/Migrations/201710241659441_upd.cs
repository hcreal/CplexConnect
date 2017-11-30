namespace CplexConnect.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class upd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InputDatas", "CourseName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.InputDatas", "CourseName");
        }
    }
}
