namespace CplexConnect.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedatabase2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Buildings",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        BuildingName = c.String(),
                        Room_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Rooms", t => t.Room_ID)
                .Index(t => t.Room_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Buildings", "Room_ID", "dbo.Rooms");
            DropIndex("dbo.Buildings", new[] { "Room_ID" });
            DropTable("dbo.Buildings");
        }
    }
}
