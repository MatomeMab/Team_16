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
    public class BookingController : Controller
    {
        private EasyMovingSystemEntities db = new EasyMovingSystemEntities();

        // GET: Booking
        public async Task<ActionResult> Index(int? id, int? User_ID)
        {
            var quote = db.Bookings.Where(b => b.Client_ID == id);
            /*Nav*/
            var userRole = db.UserRoles.FirstOrDefault(a => a.User_ID == User_ID);
            if (userRole == null)
            {
                TempData["error"] = "User is not linked to a role, please conatct your system administrator";
                return RedirectToAction("Index", "AuthLogin");
            }

            ViewBag.RoleTypeId = userRole.RoleTypeId;
            //ViewBag.UserId = id;
            ViewBag.UserId = User_ID;
            var userAccount = db.Clients.FirstOrDefault(a => a.User_ID == User_ID);
            ViewBag.UserName = userAccount.ClientName + " " + userAccount.ClientSurname;
            //ViewBag.ClientId = userAccount.Client_ID;
            ViewBag.ClientId = id;
            /*End Nav*/
            var bookings = db.Bookings.Include(b => b.Client);
            return View(bookings.Where(x => x.Client_ID == id).ToList());
        }

        // GET: Booking/Details/5
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
            return View(booking);
        }

        // GET: Booking/Create
        public ActionResult Create(int? id, int? User_ID)
        {
            var quote = db.Bookings.Where(b => b.Client_ID == id);
            /**/
            var userRole = db.UserRoles.FirstOrDefault(a => a.User_ID == User_ID);
            if (userRole == null)
            {
                TempData["error"] = "User is not linked to a role, please conatct your system administrator";
                return RedirectToAction("Index", "AuthLogin");
            }

            ViewBag.RoleTypeId = userRole.RoleTypeId;
            ViewBag.UserId = id;
            ViewBag.UserId = User_ID;
            var userAccount = db.Clients.FirstOrDefault(a => a.User_ID == User_ID);
            ViewBag.UserName = userAccount.ClientName + " " + userAccount.ClientSurname;
            
            ViewBag.ClientId = id;
            /**/


            List<DriverSlotsObj> startAndEndTimeObjs = new List<DriverSlotsObj>();

            var TimeSlots = db.DateOrTimeSlotOrDrivers.ToList();

            for (int i = 0; i < TimeSlots.Count(); i++)
            {
                var item = TimeSlots.ElementAt(i);
                if (item != null)
                {
                    startAndEndTimeObjs.Add(new DriverSlotsObj()
                    {
                        DateorTimeslotDiriver_ID = item.DateorTimeslotDiriver_ID,
                        Description = item.TimeSlot.Date + "-" + item.TimeSlot.StartTime + " - " + item.TimeSlot.EndTime
                    });
                }
            }

            ViewBag.TimeSlot_ID = new SelectList(startAndEndTimeObjs, "DateorTimeslotDiriver_ID", "Description");
            ViewBag.Service_ID = new SelectList(db.Services, "Service_ID", "ServiceName").ToList();
            ViewBag.Truck_ID = new SelectList(db.Trucks, "Truck_ID", "RegNum");
            ViewBag.Employee_ID = new SelectList(db.Employees, "Employee_ID", "EmployeeName");
            ViewBag.Client_ID = new SelectList(db.Clients.Where(x => x.Client_ID == id), "Client_ID", "ClientName").ToList();
            return View();
        }

        // POST: Booking/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Booking_ID,Client_ID,DateMade")] Booking booking,int?id,int?User_ID,BookingInstance bookingInstance1)
        {
            /*FOR NAVIGATION*/
            var quote = db.Bookings.Where(b => b.Client_ID == id);

            var userRole = db.UserRoles.FirstOrDefault(a => a.User_ID == User_ID);
            if (userRole == null)
            {
                TempData["error"] = "User is not linked to a role, please conatct your system administrator";
                return RedirectToAction("Index", "AuthLogin");
            }

            ViewBag.RoleTypeId = userRole.RoleTypeId;
            ViewBag.UserId = User_ID;
            var userAccount = db.Clients.FirstOrDefault(a => a.User_ID == User_ID);
            ViewBag.UserName = userAccount.ClientName + " " + userAccount.ClientSurname;
            ViewBag.ClientId = id;
            /*FOR NAVIGATION*/

            if (ModelState.IsValid)
            {
                db.Bookings.Add(booking);
                await db.SaveChangesAsync();

                /*Booking instance*/
                BookingInstance bookingInstance = new BookingInstance();
                bookingInstance.Booking_ID = booking.Booking_ID;
                bookingInstance.Truck_ID = bookingInstance1.Truck_ID;
                bookingInstance.BookingInstanceDate = booking.DateMade;
                bookingInstance.BookingInstanceDescription = booking.BookingDescription;
                bookingInstance.DateOrTimeSlotOrDriver_ID = bookingInstance1.DateOrTimeSlotOrDriver_ID;
                bookingInstance.BookingStatus_ID = 1;
                db.BookingInstances.Add(bookingInstance);
                await db.SaveChangesAsync();

                return RedirectToAction("Index", new { id = ViewBag.ClientId, User_ID = ViewBag.UserId });
            }

            ViewBag.Client_ID = new SelectList(db.Clients, "Client_ID", "ClientName", booking.Client_ID);
            return View(booking);
        }

        // GET: Booking/Edit/5
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

        // POST: Booking/Edit/5
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

        // GET: Booking/Delete/5
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

        // POST: Booking/Delete/5
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


        public class DriverSlotsObj
        {
            public int? Employee_ID { get; set; }
            public int? DateorTimeslotDiriver_ID { get; set; }
            public string Employee { get; set; }
            public string Description { get; set; }
        }
    }
}
