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
    public class RouteDetailsController : Controller
    {
        private BusBookingContext db = new BusBookingContext();

        // GET: RouteDetails
        public ActionResult Index()
        {
            var routeDetails = db.routeDetails.Include(r => r.busDetails);
            return View(routeDetails.ToList());
        }

        // GET: RouteDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RouteDetails routeDetails = db.routeDetails.Find(id);
            if (routeDetails == null)
            {
                return HttpNotFound();
            }
            return View(routeDetails);
        }

        // GET: RouteDetails/Create
        public ActionResult Create()
        {
            ViewBag.busID = new SelectList(db.busDetails, "busID", "busName");
            return View();
        }

        // POST: RouteDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,busID,travelTime,source,distance,destinations")] RouteDetails routeDetails)
        {
            if (ModelState.IsValid)
            {
                db.routeDetails.Add(routeDetails);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.busID = new SelectList(db.busDetails, "busID", "busName", routeDetails.busID);
            return View(routeDetails);
        }

        // GET: RouteDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RouteDetails routeDetails = db.routeDetails.Find(id);
            if (routeDetails == null)
            {
                return HttpNotFound();
            }
            ViewBag.busID = new SelectList(db.busDetails, "busID", "busName", routeDetails.busID);
            return View(routeDetails);
        }

        // POST: RouteDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,busID,travelTime,source,distance,destinations")] RouteDetails routeDetails)
        {
            if (ModelState.IsValid)
            {
                db.Entry(routeDetails).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.busID = new SelectList(db.busDetails, "busID", "busName", routeDetails.busID);
            return View(routeDetails);
        }

        // GET: RouteDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RouteDetails routeDetails = db.routeDetails.Find(id);
            if (routeDetails == null)
            {
                return HttpNotFound();
            }
            return View(routeDetails);
        }

        // POST: RouteDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RouteDetails routeDetails = db.routeDetails.Find(id);
            db.routeDetails.Remove(routeDetails);
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
