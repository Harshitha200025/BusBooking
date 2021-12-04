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
    public class BusDetailsController : Controller
    {
        private BusBookingContext db = new BusBookingContext();

        // GET: BusDetails
        public ActionResult Index()
        {
            List<BusDetails> bd = (List<BusDetails>)Session["bdList"];
            //var busDetails = db.busDetails.Include(b => b.seatType);
            return View(bd);
        }

        // GET: BusDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BusDetails busDetails = db.busDetails.Find(id);
            if (busDetails == null)
            {
                return HttpNotFound();
            }
            return View(busDetails);
        }

        // GET: BusDetails/Create
        public ActionResult Create()
        {
            ViewBag.typeID = new SelectList(db.seatType, "typeID", "typeName");
            return View();
        }

        // POST: BusDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "busID,typeID,busName,source,destination,time")] BusDetails busDetails)
        {
            if (ModelState.IsValid)
            {
                db.busDetails.Add(busDetails);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.typeID = new SelectList(db.seatType, "typeID", "typeName", busDetails.typeID);
            return View(busDetails);
        }

        // GET: BusDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BusDetails busDetails = db.busDetails.Find(id);
            if (busDetails == null)
            {
                return HttpNotFound();
            }
            ViewBag.typeID = new SelectList(db.seatType, "typeID", "typeName", busDetails.typeID);
            return View(busDetails);
        }

        // POST: BusDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "busID,typeID,busName,source,destination,time")] BusDetails busDetails)
        {
            if (ModelState.IsValid)
            {
                db.Entry(busDetails).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.typeID = new SelectList(db.seatType, "typeID", "typeName", busDetails.typeID);
            return View(busDetails);
        }

        // GET: BusDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BusDetails busDetails = db.busDetails.Find(id);
            if (busDetails == null)
            {
                return HttpNotFound();
            }
            return View(busDetails);
        }

        // POST: BusDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BusDetails busDetails = db.busDetails.Find(id);
            db.busDetails.Remove(busDetails);
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
