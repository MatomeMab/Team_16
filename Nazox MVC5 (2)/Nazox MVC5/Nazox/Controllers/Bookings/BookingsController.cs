using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Nazox.Models;

namespace Nazox.Controllers.Bookings
{
    public class BookingsController : Controller
    {
        private EasyMovingSystemEntities db = new EasyMovingSystemEntities();

        // GET: Bookings
        public async Task<ActionResult> Index(int? id,int? User_ID)
        {
            var bookings = db.Bookings.Where(b => b.Client_ID == id);

            var userRole = db.UserRoles.FirstOrDefault(a => a.User_ID == User_ID);
            if (userRole == null)
            {
                TempData["error"] = "User is not linked to a role, please conatct your system administrator";
                return RedirectToAction("Index", "AuthLogin");
            }

            ViewBag.RoleTypeId = userRole.RoleTypeId;
            ViewBag.UserId = id;

            var userAccount = db.Clients.FirstOrDefault(a => a.User_ID == User_ID);
            ViewBag.UserName = userAccount.ClientName + " " + userAccount.ClientSurname;
            ViewBag.ClientId = userAccount.Client_ID;

            return View(await bookings.ToListAsync());
        }

        // GET: Bookings/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = await db.Bookings.FindAsync(id);
            if (booking == null)
            {
                return HttpNotFound();
            }

            Client client = await db.Clients.FindAsync(booking.Client_ID);

            var userRole = db.UserRoles.FirstOrDefault(a => a.User_ID == client.User_ID);
            if (userRole == null)
            {
                TempData["error"] = "User is not linked to a role, please conatct your system administrator";
                return RedirectToAction("Index", "AuthLogin");
            }

            ViewBag.RoleTypeId = userRole.RoleTypeId;
            ViewBag.UserId = id;

            var userAccount = db.Clients.FirstOrDefault(a => a.User_ID == client.User_ID);
            ViewBag.UserName = userAccount.ClientName + " " + userAccount.ClientSurname;
            ViewBag.ClientId = userAccount.Client_ID;

            return View(booking);
        }

        // GET: Bookings/Create
        public ActionResult Create(int? id)
        {
            ViewBag.Client_ID = new SelectList(db.Clients, "Client_ID", "ClientName",id);
            return View();
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Booking_ID,Client_ID,DateMade")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Bookings.Add(booking);
                await db.SaveChangesAsync();

                return RedirectToAction("Create", "BookingInstances", new { id = booking.Booking_ID });
            }

            ViewBag.Client_ID = new SelectList(db.Clients, "Client_ID", "ClientName", booking.Client_ID);
            
            return View(booking);
        }

        // GET: Bookings/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = await db.Bookings.FindAsync(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            ViewBag.Client_ID = new SelectList(db.Clients, "Client_ID", "ClientName", booking.Client_ID);
            return View(booking);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Booking_ID,Client_ID,DateMade")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(booking).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Client_ID = new SelectList(db.Clients, "Client_ID", "ClientName", booking.Client_ID);
            return View(booking);
        }

        // GET: Bookings/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = await db.Bookings.FindAsync(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Booking booking = await db.Bookings.FindAsync(id);
            db.Bookings.Remove(booking);
            await db.SaveChangesAsync();
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
