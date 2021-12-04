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
    public class SeatTypesController : Controller
    {
        private BusBookingContext db = new BusBookingContext();

        // GET: SeatTypes
        public ActionResult Index()
        {
            return View(db.seatType.ToList());
        }

        // GET: SeatTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SeatType seatType = db.seatType.Find(id);
            if (seatType == null)
            {
                return HttpNotFound();
            }
            return View(seatType);
        }

        // GET: SeatTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SeatTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "typeID,fare,typeName")] SeatType seatType)
        {
            if (ModelState.IsValid)
            {
                db.seatType.Add(seatType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(seatType);
        }

        // GET: SeatTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SeatType seatType = db.seatType.Find(id);
            if (seatType == null)
            {
                return HttpNotFound();
            }
            return View(seatType);
        }

        // POST: SeatTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "typeID,fare,typeName")] SeatType seatType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(seatType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(seatType);
        }

        // GET: SeatTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SeatType seatType = db.seatType.Find(id);
            if (seatType == null)
            {
                return HttpNotFound();
            }
            return View(seatType);
        }

        // POST: SeatTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SeatType seatType = db.seatType.Find(id);
            db.seatType.Remove(seatType);
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
