namespace CplexConnect.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class e55 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
    "dbo.OverlapGroups",
    c => new
    {
        ID = c.Int(nullable: false, identity: true),
        OverlapGroup = c.String(),
        Sections = c.String(),
    })
    .PrimaryKey(t => t.ID);

        }

        public override void Down()
        {
        }
    }
}
