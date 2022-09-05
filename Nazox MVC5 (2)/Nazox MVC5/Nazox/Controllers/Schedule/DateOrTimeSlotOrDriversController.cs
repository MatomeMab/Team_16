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
    public class DateOrTimeSlotOrDriversController : Controller
    {
        private EasyMovingSystemEntities db = new EasyMovingSystemEntities();
        private static int? RoleTypeId = null;
        private static int? User_ID = null;
        // GET: DateOrTimeSlotOrDrivers
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


            var dateOrTimeSlotOrDrivers = db.DateOrTimeSlotOrDrivers.Include(d => d.Employee).Include(d => d.TimeSlot);
            return View(await dateOrTimeSlotOrDrivers.ToListAsync());
        }

        // GET: DateOrTimeSlotOrDrivers/Details/5
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
            DateOrTimeSlotOrDriver dateOrTimeSlotOrDriver = await db.DateOrTimeSlotOrDrivers.FindAsync(ids);
            if (dateOrTimeSlotOrDriver == null)
            {
                return HttpNotFound();
            }
            return View(dateOrTimeSlotOrDriver);
        }

        // GET: DateOrTimeSlotOrDrivers/Create
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

            List<StartAndEndTimeObj> startAndEndTimeObjs = new List<StartAndEndTimeObj>();

            var TimeSlots = db.TimeSlots.ToList();

            for (int i = 0; i < TimeSlots.Count(); i++)
            {
                var item = TimeSlots.ElementAt(i);
                if (item != null)
                {
                    startAndEndTimeObjs.Add(new StartAndEndTimeObj()
                    {
                        TimeSlot_ID = item.TimeSlot_ID,
                        StartAndEndTime = item.Date + "-" + item.StartTime + " - " + item.EndTime
                    });
                }
            }
           
            ViewBag.TimeSlot_ID = new SelectList(startAndEndTimeObjs, "TimeSlot_ID", "StartAndEndTime");
            ViewBag.Employee_ID = new SelectList(db.Employees, "Employee_ID", "EmployeeName");
            //ViewBag.Timeslot_ID = new SelectList(db.TimeSlots, "TimeSlot_ID", "TimeSlot_ID");
            var time = db.TimeSlots.Include(t => t.DateOrTimeSlotOrDrivers);
            
            return View();
        }

        // POST: DateOrTimeSlotOrDrivers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Employee_ID,DateorTimeslotDiriver_ID,Timeslot_ID")] DateOrTimeSlotOrDriver dateOrTimeSlotOrDriver,int?id)
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
                db.DateOrTimeSlotOrDrivers.Add(dateOrTimeSlotOrDriver);
                await db.SaveChangesAsync();
                return RedirectToAction("Index", "Schedule",new { id = ViewBag.UserId });
            }
           

            ViewBag.Employee_ID = new SelectList(db.Employees, "Employee_ID", "EmployeeName", dateOrTimeSlotOrDriver.Employee_ID);
            //ViewBag.TimeSlot_ID = new SelectList(db.TimeSlots, "TimeSlot_ID", "TimeSlot_ID", dateOrTimeSlot.TimeSlot_ID);
            ViewBag.Timeslot_ID = new SelectList(db.TimeSlots, "TimeSlot_ID", "TimeSlot_ID", dateOrTimeSlotOrDriver.Timeslot_ID);
            return View(dateOrTimeSlotOrDriver);
        }

        // GET: DateOrTimeSlotOrDrivers/Edit/5
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
            DateOrTimeSlotOrDriver dateOrTimeSlotOrDriver = await db.DateOrTimeSlotOrDrivers.FindAsync(ids);
            if (dateOrTimeSlotOrDriver == null)
            {
                return HttpNotFound();
            }
            List<StartAndEndTimeObj> startAndEndTimeObjs = new List<StartAndEndTimeObj>();

            var TimeSlots = db.TimeSlots.ToList();

            for (int i = 0; i < TimeSlots.Count(); i++)
            {
                var item = TimeSlots.ElementAt(i);
                if (item != null)
                {
                    startAndEndTimeObjs.Add(new StartAndEndTimeObj()
                    {
                        TimeSlot_ID = item.TimeSlot_ID,
                        StartAndEndTime = item.Date + "-" + item.StartTime + " - " + item.EndTime
                    });
                }
            }

            ViewBag.TimeSlot_ID = new SelectList(startAndEndTimeObjs, "TimeSlot_ID", "StartAndEndTime");

            ViewBag.Employee_ID = new SelectList(db.Employees, "Employee_ID", "EmployeeName", dateOrTimeSlotOrDriver.Employee_ID);
           // ViewBag.Timeslot_ID = new SelectList(db.TimeSlots, "TimeSlot_ID", "TimeSlot_ID", dateOrTimeSlotOrDriver.Timeslot_ID);
            return View(dateOrTimeSlotOrDriver);
        }

        // POST: DateOrTimeSlotOrDrivers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Employee_ID,DateorTimeslotDiriver_ID,Timeslot_ID")] DateOrTimeSlotOrDriver dateOrTimeSlotOrDriver,int?id)
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
                db.Entry(dateOrTimeSlotOrDriver).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index", new { id = ViewBag.UserId });
            }
            ViewBag.Employee_ID = new SelectList(db.Employees, "Employee_ID", "EmployeeName", dateOrTimeSlotOrDriver.Employee_ID);
            ViewBag.Timeslot_ID = new SelectList(db.TimeSlots, "TimeSlot_ID", "TimeSlot_ID", dateOrTimeSlotOrDriver.Timeslot_ID);
            return View(dateOrTimeSlotOrDriver);
        }

        // GET: DateOrTimeSlotOrDrivers/Delete/5
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
            DateOrTimeSlotOrDriver dateOrTimeSlotOrDriver = await db.DateOrTimeSlotOrDrivers.FindAsync(ids);
            if (dateOrTimeSlotOrDriver == null)
            {
                return HttpNotFound();
            }


            return View(dateOrTimeSlotOrDriver);
        }

        // POST: DateOrTimeSlotOrDrivers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int ids,int?id)
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


            DateOrTimeSlotOrDriver dateOrTimeSlotOrDriver = await db.DateOrTimeSlotOrDrivers.FindAsync(ids);
            db.DateOrTimeSlotOrDrivers.Remove(dateOrTimeSlotOrDriver);
            await db.SaveChangesAsync();
            return RedirectToAction("Index", "Schedule", new { id = ViewBag.UserId });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public class StartAndEndTimeObj
        {
            public int? TimeSlot_ID { get; set; }
            public string StartAndEndTime { get; set; }
        }

    }
}
