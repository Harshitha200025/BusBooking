using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BusBooking.Models
{
    public class SeatType
    {
        [Key]
        public int typeID { get; set; }
        public float fare { get; set; }
        public string typeName { get; set; }

    }

    public class BusDetails {
        [Key]
        public int busID { get; set; }
        [ForeignKey("seatType")]
        public int? typeID { get; set; }
        public SeatType seatType { get; set; }
        public string busName { get; set; }
        public string source { get; set; }
        public string  destination { get; set; }
        public string time { get; set; }

    }

    public class RouteDetails
    {
        public int id { get; set; }
        [ForeignKey("busDetails")]
        public int? busID { get; set; }
        public BusDetails busDetails { get; set; }
        public string travelTime { get; set; }
        public string source { get; set; }
        public float distance { get; set; }
        public string destinations { get; set; } //string[] placenames = places.split(',')
    }

    public class BoardingPoint
    {
        public int id { get; set; }
        [ForeignKey("busDetails")]
        public int? buisID { get; set; }
        public BusDetails busDetails { get; set; }
        public string pickUpPlace { get; set; }
    }

    public class Booking
    {
        public int id { get; set; }
        public string customerID { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        [ForeignKey("bid")]
        public int? busID { get; set; }
        public BusDetails bid { get; set; }
        public DateTime dateStamp { get; set; }
        public string source { get; set; }
        public string destination { get; set; }
        [ForeignKey("pup")]
        public int pickUpPoint { get; set; }
        public BoardingPoint pup { get; set; }
        public float fare { get; set; }
    }

    public class BusBookingContext: DbContext
    {
        public DbSet<SeatType> seatType { get; set; }
        public DbSet<BusDetails> busDetails { get; set; }
        public DbSet<RouteDetails> routeDetails { get; set; }
        public DbSet<BoardingPoint> boardingPoint { get; set; }

        public System.Data.Entity.DbSet<BusBooking.Models.Booking> Bookings { get; set; }
    }
}