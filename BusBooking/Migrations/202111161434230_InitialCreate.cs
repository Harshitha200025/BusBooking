namespace BusBooking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BoardingPoints",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        buisID = c.Int(),
                        pickUpPlace = c.String(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.BusDetails", t => t.buisID)
                .Index(t => t.buisID);
            
            CreateTable(
                "dbo.BusDetails",
                c => new
                    {
                        busID = c.Int(nullable: false, identity: true),
                        typeID = c.Int(),
                        busName = c.String(),
                        source = c.String(),
                        destination = c.String(),
                        time = c.String(),
                    })
                .PrimaryKey(t => t.busID)
                .ForeignKey("dbo.SeatTypes", t => t.typeID)
                .Index(t => t.typeID);
            
            CreateTable(
                "dbo.SeatTypes",
                c => new
                    {
                        typeID = c.Int(nullable: false, identity: true),
                        fare = c.Single(nullable: false),
                        typeName = c.String(),
                    })
                .PrimaryKey(t => t.typeID);
            
            CreateTable(
                "dbo.Bookings",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        customerID = c.String(),
                        phone = c.String(),
                        email = c.String(),
                        busID = c.Int(),
                        dateStamp = c.DateTime(nullable: false),
                        source = c.String(),
                        destination = c.String(),
                        pickUpPoint = c.Int(nullable: false),
                        fare = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.BusDetails", t => t.busID)
                .ForeignKey("dbo.BoardingPoints", t => t.pickUpPoint, cascadeDelete: true)
                .Index(t => t.busID)
                .Index(t => t.pickUpPoint);
            
            CreateTable(
                "dbo.RouteDetails",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        busID = c.Int(),
                        travelTime = c.String(),
                        source = c.String(),
                        destinations = c.String(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.BusDetails", t => t.busID)
                .Index(t => t.busID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RouteDetails", "busID", "dbo.BusDetails");
            DropForeignKey("dbo.Bookings", "pickUpPoint", "dbo.BoardingPoints");
            DropForeignKey("dbo.Bookings", "busID", "dbo.BusDetails");
            DropForeignKey("dbo.BoardingPoints", "buisID", "dbo.BusDetails");
            DropForeignKey("dbo.BusDetails", "typeID", "dbo.SeatTypes");
            DropIndex("dbo.RouteDetails", new[] { "busID" });
            DropIndex("dbo.Bookings", new[] { "pickUpPoint" });
            DropIndex("dbo.Bookings", new[] { "busID" });
            DropIndex("dbo.BusDetails", new[] { "typeID" });
            DropIndex("dbo.BoardingPoints", new[] { "buisID" });
            DropTable("dbo.RouteDetails");
            DropTable("dbo.Bookings");
            DropTable("dbo.SeatTypes");
            DropTable("dbo.BusDetails");
            DropTable("dbo.BoardingPoints");
        }
    }
}
