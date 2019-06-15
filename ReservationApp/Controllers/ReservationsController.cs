using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ReservationApp.Models;
using ReservationApp.ViewModel;

namespace ReservationApp.Controllers
{
    public class ReservationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Reservations
        public ActionResult Index()
        {
            var reservations = db.Reservations.Include(r => r.Room).Include(r => r.RoomType);
            return View(reservations.ToList());
        }

        // GET: Reservations/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        // GET: Reservations/Create
        public ActionResult Create()
        {
            ViewBag.Room_Id = new SelectList(db.Rooms, "Id", "Name");
            ViewBag.RoomType_Id = new SelectList(db.RoomTypes, "Id", "Name");
            ReservationVM reservationVM = new ReservationVM{
                Reservations = db.Reservations.ToList(),
            };
            return View(reservationVM);
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                Calulations(reservation);
                db.Reservations.Add(reservation);
                db.SaveChanges();
                
            }
            ReservationVM reservationVM = new ReservationVM
            {
                Reservations = db.Reservations.ToList(),
                Reservation=new Reservation()
            };
            ViewBag.Room_Id = new SelectList(db.Rooms, "Id", "Name", reservation.Room_Id);
            ViewBag.RoomType_Id = new SelectList(db.RoomTypes, "Id", "Name", reservation.RoomType_Id);
            return View(reservationVM);
        }
        
        // GET: Reservations/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            ViewBag.Room_Id = new SelectList(db.Rooms, "Id", "Name", reservation.Room_Id);
            ViewBag.RoomType_Id = new SelectList(db.RoomTypes, "Id", "Name", reservation.RoomType_Id);
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,RoomType_Id,Room_Id,From,To,NoOfDays,TotallPrice")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                Calulations(reservation);
                db.Entry(reservation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Room_Id = new SelectList(db.Rooms, "Id", "Name", reservation.Room_Id);
            ViewBag.RoomType_Id = new SelectList(db.RoomTypes, "Id", "Name", reservation.RoomType_Id);
            return View(reservation);
        }

        // GET: Reservations/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Reservation reservation = db.Reservations.Find(id);
            db.Reservations.Remove(reservation);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult GetPrice(long id)
        {   
          //  long newId = long.Parse( id);   
            return Json(new { data = db.RoomTypes.Find(id).price }, JsonRequestBehavior.AllowGet);

        }
        void Calulations(Reservation reservation)
        {
            reservation.NoOfDays = (int)reservation.To.Subtract(reservation.From).TotalDays + 1;
            var price = db.RoomTypes.Find(reservation.RoomType_Id).price;
            reservation.TotallPrice = price * reservation.NoOfDays;
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
