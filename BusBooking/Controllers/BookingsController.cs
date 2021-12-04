using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BusBooking.Models;

namespace BusBooking.Controllers
{
    public class BookingsController : Controller
    {
        private BusBookingContext db = new BusBookingContext();

        // GET: Bookings
        public ActionResult Index()
        {
            var bookings = db.Bookings.Include(b => b.bid).Include(b => b.pup);
            return View(bookings.ToList());
        }

        // GET: Bookings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // GET: Bookings/Create
        public ActionResult Create()
        {
            ViewBag.busID = new SelectList(db.busDetails, "busID", "busName");
            ViewBag.source = new SelectList(db.busDetails, "busID", "source");
            ViewBag.destination = new SelectList(db.busDetails, "busID", "destination");
            ViewBag.pickUpPoint = new SelectList(db.boardingPoint, "id", "pickUpPlace");
            return View();
        }

        public JsonResult PickUpPoints(int busid)
        {
            string pickup = db.boardingPoint.Single(x => x.buisID == busid).pickUpPlace;
            string[] pickups = pickup.Split(',');
            List<string> plist = new List<string>();
            foreach(string pick in pickups)
            {
                plist.Add(pick);
            }
            return Json(plist, JsonRequestBehavior.AllowGet);
        }

        public ActionResult BookingTicket()
        {
            List<BusDetails> bd = db.busDetails.ToList();
            return View(bd);
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,customerID,phone,email,busID,dateStamp,source,destination,pickUpPoint,fare")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Bookings.Add(booking);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.busID = new SelectList(db.busDetails, "busID", "busName", booking.busID);
            ViewBag.pickUpPoint = new SelectList(db.boardingPoint, "id", "pickUpPlace", booking.pickUpPoint);
            return View(booking);
        }

        // GET: Bookings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            ViewBag.busID = new SelectList(db.busDetails, "busID", "busName", booking.busID);
            ViewBag.pickUpPoint = new SelectList(db.boardingPoint, "id", "pickUpPlace", booking.pickUpPoint);
            return View(booking);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,customerID,phone,email,busID,dateStamp,source,destination,pickUpPoint,fare")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(booking).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.busID = new SelectList(db.busDetails, "busID", "busName", booking.busID);
            ViewBag.pickUpPoint = new SelectList(db.boardingPoint, "id", "pickUpPlace", booking.pickUpPoint);
            return View(booking);
        }

        // GET: Bookings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Booking booking = db.Bookings.Find(id);
            db.Bookings.Remove(booking);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
