using Nazox.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Nazox.Controllers.Schedule
{
    public class ScheduleController : Controller
    {
        private EasyMovingSystemEntities db = new EasyMovingSystemEntities();
        private static int? RoleTypeId = null;
        private static int? User_ID = null;
        // GET: Schedule
        public async Task<ActionResult> Index(int? id)
        {
            /*Navigation*/
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

            /*Navigation*/





            ViewBag.TimeSlots = db.TimeSlots.ToList();

            var DateTimeSlotDrivers = db.DateOrTimeSlotOrDrivers.Include(d => d.TimeSlot).Include(d => d.Employee).ToList();
            List<DriverSlotsObj> driverSlotsObjs = new List<DriverSlotsObj>();
            var TimeSlots = db.TimeSlots.ToList();
            var NameSurname = db.Employees.ToList();

            /*emp*/
            //string cNameSurname = "";
            List<EmployeeObj> empObjs = new List<EmployeeObj>();
            var nameSurnames = db.Employees.ToList();
            var Emp = db.Employees.ToList();

            for (int i = 0; i < Emp.Count(); i++)
            {
                var item = Emp.ElementAt(i);
                if (item != null)
                {
                    empObjs.Add(new EmployeeObj()
                    {
                        Employee_ID = item.Employee_ID,
                        NameSurname = item.EmployeeName+"-"+item.EmergencySurname
                    });
                   // cNameSurname = item.EmployeeName +"-"+ item.EmployeeSurname;
                }
            }

            ViewBag.Date_ID = new SelectList(empObjs, "Employee_ID", "NameSurname");
            

            for (int i = 0; i < DateTimeSlotDrivers.Count(); i++)
            {
                var item = DateTimeSlotDrivers.ElementAt(i);
                if (item != null)
                {


                    string cnameSurname = "";
                    string nSurname = "";
                    string timeSlot = "";
                    string Combined = "";
                    var dateTimeSlot = TimeSlots.FirstOrDefault(a => a.TimeSlot_ID == item.Timeslot_ID);
                    var cNameSurname = NameSurname.FirstOrDefault(a => a.Employee_ID == item.Employee_ID);
                    if (dateTimeSlot != null)
                    {
                        var slot = TimeSlots.FirstOrDefault(a => a.TimeSlot_ID == dateTimeSlot.TimeSlot_ID);
                        var nameSurname = NameSurname.FirstOrDefault(a => a.Employee_ID == cNameSurname.Employee_ID);
                        if (slot != null)
                        {
                            timeSlot = slot.Date + "-" + slot.StartTime + " - " + slot.EndTime;
                            nSurname = nameSurname.EmployeeName + "-" + nameSurname.EmergencySurname;
                        }


                        Combined = timeSlot;
                        cnameSurname = nSurname;
                    }




                    driverSlotsObjs.Add(new DriverSlotsObj()
                    {
                        Employee_ID = item.Employee_ID,
                        //Employee = item.Employee.EmployeeName,
                         Employee=cnameSurname,
                        DateorTimeslotDiriver_ID = item.DateorTimeslotDiriver_ID,
                        Description = Combined
                        


                    }) ;
                }
            }


            ViewBag.DriverSlotsObj = driverSlotsObjs;



            return View();
        }


        // GET: 
        //public async Task<ActionResult> Index()
        //{
        //    ViewBag.TimeSlots = db.TimeSlots.ToList();
        //    var DateTimeSlotDrivers = db.DateOrTimeSlotOrDrivers.Include(d => d.TimeSlot).Include(d => d.Employee).ToList();

        //    List<DriverSlotsObj> driverSlotsObjs = new List<DriverSlotsObj>();

        //    var TimeSlots = db.TimeSlots.ToList();
            
 

        //    for (int i = 0; i < DateTimeSlotDrivers.Count(); i++)
        //    {
        //        var item = DateTimeSlotDrivers.ElementAt(i);
        //        if (item != null)
        //        {
        //            string timeSlot = "";
        //            string Combined = "";
        //            var dateTimeSlot = TimeSlots.FirstOrDefault(a => a.TimeSlot_ID == item.Timeslot_ID);
        //            if (dateTimeSlot != null)
        //            {
        //                var slot = TimeSlots.FirstOrDefault(a => a.TimeSlot_ID == dateTimeSlot.TimeSlot_ID);

        //                if (slot != null)
        //                {
        //                    timeSlot = slot.Date+"-"+slot.StartTime + " - " + slot.EndTime;
        //                }


        //                Combined =  timeSlot;
        //            }


        //            driverSlotsObjs.Add(new DriverSlotsObj()
        //            {
        //                Employee_ID = item.Employee_ID,
        //                Employee = item.Employee.EmployeeSurname,
        //                DateorTimeslotDiriver_ID = item.DateorTimeslotDiriver_ID,
                      
        //            });
        //        }
        //    }


        //    ViewBag.DriverSlotsObj = driverSlotsObjs;

        //    return View(await db.TimeSlots.ToListAsync());
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }




    }

    public class DriverSlotsObj
    {
        public int? Employee_ID { get; set; }
        public int? DateorTimeslotDiriver_ID { get; set; }
        public string Employee { get; set; }
        public string Description { get; set; }
    }

    public class EmployeeObj
    {
        public int? Employee_ID { get; set; }
        public string NameSurname { get; set; }
    }
}

