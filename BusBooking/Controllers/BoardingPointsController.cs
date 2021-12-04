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
    public class BoardingPointsController : Controller
    {
        private BusBookingContext db = new BusBookingContext();

        // GET: BoardingPoints
        public ActionResult Index()
        {
            var boardingPoint = db.boardingPoint.Include(b => b.busDetails);
            return View(boardingPoint.ToList());
        }

        // GET: BoardingPoints/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BoardingPoint boardingPoint = db.boardingPoint.Find(id);
            if (boardingPoint == null)
            {
                return HttpNotFound();
            }
            return View(boardingPoint);
        }

        // GET: BoardingPoints/Create
        public ActionResult Create()
        {
            ViewBag.buisID = new SelectList(db.busDetails, "busID", "busName");
            return View();
        }

        // POST: BoardingPoints/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,buisID,pickUpPlace")] BoardingPoint boardingPoint)
        {
            if (ModelState.IsValid)
            {
                db.boardingPoint.Add(boardingPoint);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.buisID = new SelectList(db.busDetails, "busID", "busName", boardingPoint.buisID);
            return View(boardingPoint);
        }

        // GET: BoardingPoints/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BoardingPoint boardingPoint = db.boardingPoint.Find(id);
            if (boardingPoint == null)
            {
                return HttpNotFound();
            }
            ViewBag.buisID = new SelectList(db.busDetails, "busID", "busName", boardingPoint.buisID);
            return View(boardingPoint);
        }

        // POST: BoardingPoints/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,buisID,pickUpPlace")] BoardingPoint boardingPoint)
        {
            if (ModelState.IsValid)
            {
                db.Entry(boardingPoint).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.buisID = new SelectList(db.busDetails, "busID", "busName", boardingPoint.buisID);
            return View(boardingPoint);
        }

        // GET: BoardingPoints/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BoardingPoint boardingPoint = db.boardingPoint.Find(id);
            if (boardingPoint == null)
            {
                return HttpNotFound();
            }
            return View(boardingPoint);
        }

        // POST: BoardingPoints/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BoardingPoint boardingPoint = db.boardingPoint.Find(id);
            db.boardingPoint.Remove(boardingPoint);
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
