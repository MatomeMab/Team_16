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

namespace Nazox.Controllers.Schedule
{
    public class TimeSlotsController : Controller
    {
        private EasyMovingSystemEntities db = new EasyMovingSystemEntities();
        private static int? RoleTypeId = null;
        private static int? User_ID = null;
        // GET: TimeSlots
        /*public async Task<ActionResult> Index()
        {
            return View(await db.TimeSlots.ToListAsync());
        }
        */
        public async Task<ActionResult> Index(int?id)
        {

            /*NAVIGATION*/
            User tblUser = await db.Users.FindAsync(id);
            if (tblUser == null)
            {
                return HttpNotFound();
            }

            var userRole = db.UserRoles.FirstOrDefault(a => a.User_ID == tblUser.User_ID);
            if (userRole == null)
            {
                TempData["error"] = "User is not linked to a role, please conatct your system administrator";
                return RedirectToAction("Index", "AuthLogin");
            }

            RoleTypeId = userRole.RoleTypeId;
            ViewBag.RoleTypeId = userRole.RoleTypeId;
            ViewBag.UserId = id;
            User_ID = id;

            /*NAVIGATION*/

            ViewBag.TimeSlots = db.TimeSlots.ToList();
            var DateTimeSlotDrivers = db.DateOrTimeSlotOrDrivers.Include(d => d.TimeSlot).Include(d => d.Employee).ToList();

            List<DriverSlotsObj> driverSlotsObjs = new List<DriverSlotsObj>();

            var TimeSlots = db.TimeSlots.ToList();



            for (int i = 0; i < DateTimeSlotDrivers.Count(); i++)
            {
                var item = DateTimeSlotDrivers.ElementAt(i);
                if (item != null)
                {
                    string timeSlot = "";
                    string Combined = "";
                    var dateTimeSlot = TimeSlots.FirstOrDefault(a => a.TimeSlot_ID == item.Timeslot_ID);
                    if (dateTimeSlot != null)
                    {
                        var slot = TimeSlots.FirstOrDefault(a => a.TimeSlot_ID == dateTimeSlot.TimeSlot_ID);

                        if (slot != null)
                        {
                            timeSlot = slot.Date + "-" + slot.StartTime + " - " + slot.EndTime;
                        }


                        Combined = timeSlot;
                    }


                    driverSlotsObjs.Add(new DriverSlotsObj()
                    {
                        Employee_ID = item.Employee_ID,
                        Employee = item.Employee.EmployeeSurname,
                        DateorTimeslotDiriver_ID = item.DateorTimeslotDiriver_ID,

                    });
                }
            }


            ViewBag.DriverSlotsObj = driverSlotsObjs;

            return View(await db.TimeSlots.ToListAsync());
        }

       
        // GET: TimeSlots/Details/5
        public async Task<ActionResult> Details(int? ids,int?id)
        {
            /*NAVIGATION*/
            User tblUser = await db.Users.FindAsync(id);
            if (tblUser == null)
            {
                return HttpNotFound();
            }

            var userRole = db.UserRoles.FirstOrDefault(a => a.User_ID == tblUser.User_ID);
            if (userRole == null)
            {
                TempData["error"] = "User is not linked to a role, please conatct your system administrator";
                return RedirectToAction("Index", "AuthLogin");
            }

            RoleTypeId = userRole.RoleTypeId;
            ViewBag.RoleTypeId = userRole.RoleTypeId;
            ViewBag.UserId = id;
            User_ID = id;

            /*NAVIGATION*/

            if (ids == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TimeSlot timeSlot = await db.TimeSlots.FindAsync(ids);
            if (timeSlot == null)
            {
                return HttpNotFound();
            }
            return View(timeSlot);
        }

        // GET: TimeSlots/Create
        public async Task<ActionResult> Create(int?id)
        {
            /*NAVIGATION*/
            User tblUser = await db.Users.FindAsync(id);
            if (tblUser == null)
            {
                return HttpNotFound();
            }

            var userRole = db.UserRoles.FirstOrDefault(a => a.User_ID == tblUser.User_ID);
            if (userRole == null)
            {
                TempData["error"] = "User is not linked to a role, please conatct your system administrator";
                return RedirectToAction("Index", "AuthLogin");
            }

            RoleTypeId = userRole.RoleTypeId;
            ViewBag.RoleTypeId = userRole.RoleTypeId;
            ViewBag.UserId = id;
            User_ID = id;

            /*NAVIGATION*/

            return View();
        }

        // POST: TimeSlots/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "TimeSlot_ID,StartTime,EndTime,Date")] TimeSlot timeSlot,int?id)
        {

            /*NAVIGATION*/
            User tblUser = await db.Users.FindAsync(id);
            if (tblUser == null)
            {
                return HttpNotFound();
            }

            var userRole = db.UserRoles.FirstOrDefault(a => a.User_ID == tblUser.User_ID);
            if (userRole == null)
            {
                TempData["error"] = "User is not linked to a role, please conatct your system administrator";
                return RedirectToAction("Index", "AuthLogin");
            }

            RoleTypeId = userRole.RoleTypeId;
            ViewBag.RoleTypeId = userRole.RoleTypeId;
            ViewBag.UserId = id;
            User_ID = id;

            /*NAVIGATION*/
            
            if (ModelState.IsValid)
            {
                db.TimeSlots.Add(timeSlot);
                await db.SaveChangesAsync();
                TempData["Success"] = "Timeslot created succefully!!";
                return RedirectToAction("Index", new { id = ViewBag.UserId });
              
            }
            TempData["errorMsg"] = "Something went wrong please try again";
            return View(timeSlot);
        }

        // GET: TimeSlots/Edit/5
        public async Task<ActionResult> Edit(int? ids,int?id)
        {

            /*NAVIGATION*/
            User tblUser = await db.Users.FindAsync(id);
            if (tblUser == null)
            {
                return HttpNotFound();
            }

            var userRole = db.UserRoles.FirstOrDefault(a => a.User_ID == tblUser.User_ID);
            if (userRole == null)
            {
                TempData["error"] = "User is not linked to a role, please conatct your system administrator";
                return RedirectToAction("Index", "AuthLogin");
            }

            RoleTypeId = userRole.RoleTypeId;
            ViewBag.RoleTypeId = userRole.RoleTypeId;
            ViewBag.UserId = id;
            User_ID = id;

            /*NAVIGATION*/


            if (ids == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TimeSlot timeSlot = await db.TimeSlots.FindAsync(ids);
            if (timeSlot == null)
            {
                return HttpNotFound();
            }
            return View(timeSlot);
        }

        // POST: TimeSlots/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "TimeSlot_ID,StartTime,EndTime,Date")] TimeSlot timeSlot,int?id)
        {

            /*NAVIGATION*/
            User tblUser = await db.Users.FindAsync(id);
            if (tblUser == null)
            {
                return HttpNotFound();
            }

            var userRole = db.UserRoles.FirstOrDefault(a => a.User_ID == tblUser.User_ID);
            if (userRole == null)
            {
                TempData["error"] = "User is not linked to a role, please conatct your system administrator";
                return RedirectToAction("Index", "AuthLogin");
            }

            RoleTypeId = userRole.RoleTypeId;
            ViewBag.RoleTypeId = userRole.RoleTypeId;
            ViewBag.UserId = id;
            User_ID = id;

            /*NAVIGATION*/

            if (ModelState.IsValid)
            {
                db.Entry(timeSlot).State = EntityState.Modified;
                await db.SaveChangesAsync();
                TempData["success"] = "Saved!!";
                return RedirectToAction("Index", new { id = ViewBag.UserId });
            }
            return View(timeSlot);
        }

        // GET: TimeSlots/Delete/5
        public async Task<ActionResult> Delete(int? ids,int?id)
        {
            
            /*NAVIGATION*/
            User tblUser = await db.Users.FindAsync(id);
            if (tblUser == null)
            {
                return HttpNotFound();
            }

            var userRole = db.UserRoles.FirstOrDefault(a => a.User_ID == tblUser.User_ID);
            if (userRole == null)
            {
                TempData["error"] = "User is not linked to a role, please conatct your system administrator";
                return RedirectToAction("Index", "AuthLogin");
            }

            RoleTypeId = userRole.RoleTypeId;
            ViewBag.RoleTypeId = userRole.RoleTypeId;
            ViewBag.UserId = id;
            User_ID = id;

            /*NAVIGATION*/

            if (ids == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TimeSlot timeSlot = await db.TimeSlots.FindAsync(ids);
            if (timeSlot == null)
            {
                return HttpNotFound();
            }
            return View(timeSlot);
        }

        // POST: TimeSlots/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int ids,int id)
        {
            ViewBag.Dialogue = "Delete Confirmation Dialogue";
            /*NAVIGATION*/
            User tblUser = await db.Users.FindAsync(id);
            if (tblUser == null)
            {
                return HttpNotFound();
            }

            var userRole = db.UserRoles.FirstOrDefault(a => a.User_ID == tblUser.User_ID);
            if (userRole == null)
            {
                TempData["error"] = "User is not linked to a role, please conatct your system administrator";
                return RedirectToAction("Index", "AuthLogin");
            }

            RoleTypeId = userRole.RoleTypeId;
            ViewBag.RoleTypeId = userRole.RoleTypeId;
            ViewBag.UserId = id;
            User_ID = id;

            /*NAVIGATION*/
            var fk = db.DateOrTimeSlotOrDrivers.Any(m => m.Timeslot_ID == ids);
            if (!fk)
            {
                var timeslot = db.TimeSlots.FirstOrDefault(u => u.TimeSlot_ID == ids);
                if (timeslot == null)
                {
                    TempData["Error"] = "Error occurred while atempting to delete";
                }

                TimeSlot timeSlot = await db.TimeSlots.FindAsync(ids);
                db.TimeSlots.Remove(timeSlot);
                await db.SaveChangesAsync();
                TempData["success"] = "Timeslot deleted successfuly";
                return RedirectToAction("Index", "Schedule", new { id = ViewBag.UserId }); 
            }
            else
            {
                TempData["Delete"] = "The slot cannot be deleted since is asigned to the employee";
                return RedirectToAction("Index", "Schedule", new { id = ViewBag.UserId });
            }
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
