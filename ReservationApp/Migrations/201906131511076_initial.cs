namespace ReservationApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Reservations",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        RoomType_Id = c.Long(nullable: false),
                        Room_Id = c.Long(nullable: false),
                        From = c.DateTime(nullable: false),
                        To = c.DateTime(nullable: false),
                        NoOfDays = c.Int(nullable: false),
                        TotallPrice = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Rooms", t => t.Room_Id, cascadeDelete: true)
                .ForeignKey("dbo.RoomTypes", t => t.RoomType_Id, cascadeDelete: true)
                .Index(t => t.RoomType_Id)
                .Index(t => t.Room_Id);
            
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RoomTypes",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        price = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservations", "RoomType_Id", "dbo.RoomTypes");
            DropForeignKey("dbo.Reservations", "Room_Id", "dbo.Rooms");
            DropIndex("dbo.Reservations", new[] { "Room_Id" });
            DropIndex("dbo.Reservations", new[] { "RoomType_Id" });
            DropTable("dbo.RoomTypes");
            DropTable("dbo.Rooms");
            DropTable("dbo.Reservations");
        }
    }
}
