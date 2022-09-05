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
using System.Net.Mail;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;
using MailKit.Security;

namespace Nazox.Controllers.Employees
{
    public class EmployeesController : Controller
    {
        private EasyMovingSystemEntities db = new EasyMovingSystemEntities();
        private static int? RoleTypeId = null;
        private static int? User_ID = null;

        // GET: Employees
        public async Task<ActionResult> Index(int? id)
        {

            /*NAVIGATION*/
            User tblUser = await db.Users.FindAsync(id);
            if (tblUser == null)
            {
                
                return HttpNotFound();
            }
            if (tblUser.SessionID == null)
            {
                return RedirectToAction("Index", "AuthLogin");
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

            //for data tables
            db.Configuration.LazyLoadingEnabled = false; // if your table is relational, contain foreign key
            //
            var employees = db.Employees.Include(e => e.EmployeeStatu).Include(e => e.Title).Include(e=>e.EmployeeType);

            return View(await employees.ToListAsync());
            //return Json(new { data = employees }, JsonRequestBehavior.AllowGet);
           
        }

        // GET: Employees DataTables



        // GET: Employees/Details/5
        public async Task<ActionResult> Details(int? ids, int? id)
        {
            if (ids == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = await db.Employees.FindAsync(ids);
            if (employee == null)
            {
                return HttpNotFound();
            }

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


            return View(employee);
        }

        // GET: Employees/Create
        public async Task<ActionResult> Create(int? id)
        {
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
            

            ViewBag.EmployeeType_ID = new SelectList(db.EmployeeTypes, "EmployeeType_ID", "EmployeeTypeName");
            ViewBag.EmployeeStatus_ID = new SelectList(db.EmployeeStatus, "EmployeeStatus_ID", "EmployeeStatusName");
            ViewBag.Title_ID = new SelectList(db.Titles, "Title_ID", "TitleName");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Employee_ID,User_ID,EmployeeEmail,EmployeeType_ID,EmployeeStatus_ID,Title_ID,EmployeeName,EmployeeSurname,DateOfBirth,DateEmployed,PhoneNum,EmergencyName,EmergencySurname,EmergencyPhoneNum")] Employee employee,int?id)
        {

            /*FOR NAVIGATION*/
            User tblUsers = await db.Users.FindAsync(id);
            if (tblUsers == null)
            {
                return HttpNotFound();
            }

            var userRoles = db.UserRoles.FirstOrDefault(a => a.User_ID == tblUsers.User_ID);
            if (userRoles == null)
            {
                TempData["error"] = "User is not linked to a role, please conatct your system administrator";
                return RedirectToAction("Index", "AuthLogin");
            }

            RoleTypeId = userRoles.RoleTypeId;
            ViewBag.RoleTypeId = userRoles.RoleTypeId;
            ViewBag.UserId = id;
            User_ID = id;

            /*FOR NAVIGATION*/

            if (ModelState.IsValid)
            {
				
                db.Employees.Add(employee);
                await db.SaveChangesAsync();

                //Create user account
                User tblUser = new User();
                tblUser.Role_ID=2;
                tblUser.Active = false;
                tblUser.Email = employee.EmployeeEmail;
                
                db.Users.Add(tblUser);
                await db.SaveChangesAsync();

                //User Role
                UserRole userRole = new UserRole();
                userRole.User_ID = tblUser.User_ID;
                userRole.RoleTypeId = 2;

                db.UserRoles.Add(userRole);
                await db.SaveChangesAsync();

                employee.User_ID = tblUser.User_ID;
                db.Entry(employee).State = EntityState.Modified;
                await db.SaveChangesAsync();


                Employee emp = new Employee();

                emp.User_ID = tblUser.User_ID;
                emp.EmployeeName = employee.EmployeeName;
                emp.EmployeeSurname = employee.EmployeeSurname;
                emp.PhoneNum = employee.PhoneNum;
                emp.EmergencySurname = employee.EmergencySurname;
                emp.EmergencyName = employee.EmergencyName;
                emp.EmergencyPhoneNum = employee.EmergencyPhoneNum;
                emp.DateOfBirth = employee.DateOfBirth;
                emp.DateEmployed = employee.DateEmployed;
                emp.EmployeeStatus_ID = employee.EmployeeStatus_ID;
                emp.Title_ID = employee.Title_ID;
                emp.EmployeeType_ID = employee.EmployeeType_ID;

                db.Employees.Add(emp);
                await db.SaveChangesAsync();

                string Subject = "WENDA Logistics New Group Account";

                var verifyUrl = "/AuthLogin/NewPassword";

                var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);
                string Body = "Hi New Employee</br> welcome to WENDA Logistics Group. Please go and create your password using this link " + link;

                string from = "wendalogisticsgroup@zohomail.com";
                string passwod = "Wendalogistics_123";
                SmtpClient smpt = new SmtpClient();
                smpt.Connect("smtp.zoho.com", 587, SecureSocketOptions.StartTls);
                smpt.Authenticate(from, passwod);
                smpt.AuthenticationMechanisms.Remove("XOAUTH2");


                using (var message = new MailMessage("wendalogisticsgroup@zohomail.com", employee.EmployeeEmail.Trim())
                {
                    Subject = Subject,
                    Body = Body,
                    IsBodyHtml = true
                })
                    smpt.Send((MimeKit.MimeMessage)message);

                TempData["Success"] = "Employee addedd successfully, details have been sent to the employee";
                return RedirectToAction("Index", new { id = ViewBag.UserId });
            }

            ViewBag.EmployeeType_ID = new SelectList(db.EmployeeTypes, "EmployeeType_ID", "EmployeeTypeName", employee.EmployeeType_ID);
            ViewBag.EmployeeStatus_ID = new SelectList(db.EmployeeStatus, "EmployeeStatus_ID", "EmployeeStatusName", employee.EmployeeStatus_ID);
            ViewBag.Title_ID = new SelectList(db.Titles, "Title_ID", "TitleName", employee.Title_ID);
            return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = await db.Employees.FindAsync(id);
            if (employee == null)
            {
                return HttpNotFound();
            }

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

            ViewBag.EmployeeType_ID = new SelectList(db.EmployeeTypes, "EmployeeType_ID", "EmployeeTypeName", employee.EmployeeType_ID);
            ViewBag.EmployeeStatus_ID = new SelectList(db.EmployeeStatus, "EmployeeStatus_ID", "EmployeeStatusName", employee.EmployeeStatus_ID);
            ViewBag.Title_ID = new SelectList(db.Titles, "Title_ID", "TitleName", employee.Title_ID);
            
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Employee_ID,EmployeeEmail,User_ID,EmployeeType_ID,EmployeeStatus_ID,Title_ID,EmployeeName,EmployeeSurname,DateOfBirth,DateEmployed,PhoneNum,EmergencyName,EmergencySurname,EmergencyPhoneNum")] Employee employee, int? id)
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
                db.Entry(employee).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index", new { id = ViewBag.UserId });
            }
            ViewBag.EmployeeType_ID = new SelectList(db.EmployeeTypes, "EmployeeType_ID", "EmployeeTypeName", employee.EmployeeType_ID);
            ViewBag.EmployeeStatus_ID = new SelectList(db.EmployeeStatus, "EmployeeStatus_ID", "EmployeeStatusName", employee.EmployeeStatus_ID);
            ViewBag.Title_ID = new SelectList(db.Titles, "Title_ID", "TitleName", employee.Title_ID);
            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<ActionResult> Delete(int? ids,int?id)
        {
            if (ids == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = await db.Employees.FindAsync(ids);
            if (employee == null)
            {
                return HttpNotFound();
            }


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
          

            ViewBag.EmployeeType_ID = new SelectList(db.EmployeeTypes, "EmployeeType_ID", "EmployeeTypeName", employee.EmployeeType_ID);
            ViewBag.EmployeeStatus_ID = new SelectList(db.EmployeeStatus, "EmployeeStatus_ID", "EmployeeStatusName", employee.EmployeeStatus_ID);
            ViewBag.Title_ID = new SelectList(db.Titles, "Title_ID", "TitleName", employee.Title_ID);

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int ids,int?id)
        {
            /*navi*/
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
            /*navi*/

            var fk = db.DateOrTimeSlotOrDrivers.Any(m => m.Employee_ID == ids);
            if (!fk)
            {
                var emp = db.Employees.FirstOrDefault(u => u.Employee_ID == ids);
                if (emp == null)
                {
                    TempData["Error"] = "Error occurred while attempting to delete";
                }



                Employee employee = await db.Employees.FindAsync(ids);
                db.Employees.Remove(employee);
                await db.SaveChangesAsync();
                TempData["Success"] = "Employee Deleted Succefully";
                return RedirectToAction("Index", new { id = ViewBag.UserId });
            }
            else
            {
                TempData["Delete"] = "Failed to delete the employee is linked to TimeSlot";
                return RedirectToAction("Index", new { id = ViewBag.UserId });
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
    }
}
