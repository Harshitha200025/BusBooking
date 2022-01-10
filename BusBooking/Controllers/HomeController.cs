using BusBooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BusBooking.Controllers
{
    public class HomeController : Controller
    {
        BusBookingContext bbc = new BusBookingContext();
        public ActionResult Index()
        {
            //if (HttpContext.User.IsInRole("admin"))
            //{
            //    return RedirectToAction("about");
            //}
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection fc)
        {
            DateTime dt = Convert.ToDateTime(fc["bookingdate"].ToString());
            string src = fc["source"].ToString();

            string dest = fc["destination"].ToString();
            Session["bdList"] = bbc.busDetails.Where(x => x.source == src && x.destination == dest).ToList();
            return RedirectToAction("Index", "BusDetails");
        }
        
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}