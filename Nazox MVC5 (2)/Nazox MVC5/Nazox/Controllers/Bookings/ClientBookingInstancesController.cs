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
    public class ClientBookingInstancesController : Controller
    {
        private EasyMovingSystemEntities db = new EasyMovingSystemEntities();

        // GET: ClientBookingInstances
        public async Task<ActionResult> Index(int?id,int?User_ID)
        {

            var quote = db.Bookings.Where(b => b.Client_ID == id);


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

            var bookingInstances = db.BookingInstances.Include(b => b.Booking).Include(b => b.BookingStatu).Include(b => b.Employee).Include(b => b.InspectionItem).Include(b => b.TrackingStatu).Include(b => b.Truck);
           
            return View(bookingInstances.Where(b=>b.Booking.Client_ID==id).ToList());
        }

        // GET: ClientBookingInstances/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookingInstance bookingInstance = await db.BookingInstances.FindAsync(id);
            if (bookingInstance == null)
            {
                return HttpNotFound();
            }
            return View(bookingInstance);
        }

        // GET: ClientBookingInstances/Create
        public ActionResult Create()
        {
            ViewBag.Booking_ID = new SelectList(db.Bookings, "Booking_ID", "Booking_ID");
            ViewBag.BookingStatus_ID = new SelectList(db.BookingStatus, "BookingStatus_ID", "BookingDescription");
            ViewBag.Employee_ID = new SelectList(db.Employees, "Employee_ID", "EmployeeEmail");
            ViewBag.InspectionItem_ID = new SelectList(db.InspectionItems, "InspectionItem_ID", "ItemDescription");
            ViewBag.Rental_ID = new SelectList(db.RentalAgreements, "Rental_ID", "Description");
            ViewBag.TrackingStatus_ID = new SelectList(db.TrackingStatus, "TrackingStatus_ID", "TrackingStatusName");
            ViewBag.Truck_ID = new SelectList(db.Trucks, "Truck_ID", "Model");
            return View();
        }

        // POST: ClientBookingInstances/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "BookingInstance_ID,TrackingStatus_ID,InspectionItem_ID,BookingStatus_ID,Booking_ID,Employee_ID,Truck_ID,Rental_ID,BookingInstanceDate,BookingInstanceDescription,BookingTime")] BookingInstance bookingInstance)
        {
            if (ModelState.IsValid)
            {
                db.BookingInstances.Add(bookingInstance);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Booking_ID = new SelectList(db.Bookings, "Booking_ID", "Booking_ID", bookingInstance.Booking_ID);
            ViewBag.BookingStatus_ID = new SelectList(db.BookingStatus, "BookingStatus_ID", "BookingDescription", bookingInstance.BookingStatus_ID);
            ViewBag.Employee_ID = new SelectList(db.Employees, "Employee_ID", "EmployeeEmail", bookingInstance.Employee_ID);
            ViewBag.InspectionItem_ID = new SelectList(db.InspectionItems, "InspectionItem_ID", "ItemDescription", bookingInstance.InspectionItem_ID);
            //ViewBag.Rental_ID = new SelectList(db.RentalAgreements, "Rental_ID", "Description", bookingInstance.Rental_ID);
            ViewBag.TrackingStatus_ID = new SelectList(db.TrackingStatus, "TrackingStatus_ID", "TrackingStatusName", bookingInstance.TrackingStatus_ID);
            ViewBag.Truck_ID = new SelectList(db.Trucks, "Truck_ID", "Model", bookingInstance.Truck_ID);
            return View(bookingInstance);
        }

        // GET: ClientBookingInstances/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookingInstance bookingInstance = await db.BookingInstances.FindAsync(id);
            if (bookingInstance == null)
            {
                return HttpNotFound();
            }
            ViewBag.Booking_ID = new SelectList(db.Bookings, "Booking_ID", "Booking_ID", bookingInstance.Booking_ID);
            ViewBag.BookingStatus_ID = new SelectList(db.BookingStatus, "BookingStatus_ID", "BookingDescription", bookingInstance.BookingStatus_ID);
            ViewBag.Employee_ID = new SelectList(db.Employees, "Employee_ID", "EmployeeEmail", bookingInstance.Employee_ID);
            ViewBag.InspectionItem_ID = new SelectList(db.InspectionItems, "InspectionItem_ID", "ItemDescription", bookingInstance.InspectionItem_ID);
            //ViewBag.Rental_ID = new SelectList(db.RentalAgreements, "Rental_ID", "Description", bookingInstance.Rental_ID);
            ViewBag.TrackingStatus_ID = new SelectList(db.TrackingStatus, "TrackingStatus_ID", "TrackingStatusName", bookingInstance.TrackingStatus_ID);
            ViewBag.Truck_ID = new SelectList(db.Trucks, "Truck_ID", "Model", bookingInstance.Truck_ID);
            return View(bookingInstance);
        }

        // POST: ClientBookingInstances/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "BookingInstance_ID,TrackingStatus_ID,InspectionItem_ID,BookingStatus_ID,Booking_ID,Employee_ID,Truck_ID,Rental_ID,BookingInstanceDate,BookingInstanceDescription,BookingTime")] BookingInstance bookingInstance)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bookingInstance).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Booking_ID = new SelectList(db.Bookings, "Booking_ID", "Booking_ID", bookingInstance.Booking_ID);
            ViewBag.BookingStatus_ID = new SelectList(db.BookingStatus, "BookingStatus_ID", "BookingDescription", bookingInstance.BookingStatus_ID);
            ViewBag.Employee_ID = new SelectList(db.Employees, "Employee_ID", "EmployeeEmail", bookingInstance.Employee_ID);
            ViewBag.InspectionItem_ID = new SelectList(db.InspectionItems, "InspectionItem_ID", "ItemDescription", bookingInstance.InspectionItem_ID);
            //ViewBag.Rental_ID = new SelectList(db.RentalAgreements, "Rental_ID", "Description", bookingInstance.Rental_ID);
            ViewBag.TrackingStatus_ID = new SelectList(db.TrackingStatus, "TrackingStatus_ID", "TrackingStatusName", bookingInstance.TrackingStatus_ID);
            ViewBag.Truck_ID = new SelectList(db.Trucks, "Truck_ID", "Model", bookingInstance.Truck_ID);
            return View(bookingInstance);
        }

        // GET: ClientBookingInstances/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookingInstance bookingInstance = await db.BookingInstances.FindAsync(id);
            if (bookingInstance == null)
            {
                return HttpNotFound();
            }
            return View(bookingInstance);
        }

        // POST: ClientBookingInstances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            BookingInstance bookingInstance = await db.BookingInstances.FindAsync(id);
            db.BookingInstances.Remove(bookingInstance);
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
