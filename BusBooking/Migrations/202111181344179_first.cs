namespace BusBooking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RouteDetails", "distance", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RouteDetails", "distance");
        }
    }
}
