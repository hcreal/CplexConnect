namespace CplexConnect.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removefk : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Buildings", "ID", "dbo.Rooms");

        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Buildings", "Room_ID", "dbo.Rooms");

        }
    }
}
