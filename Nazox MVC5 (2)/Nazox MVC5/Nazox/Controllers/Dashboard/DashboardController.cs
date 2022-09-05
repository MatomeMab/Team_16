using Nazox.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Nazox.Controllers.Dashboard
{
    public class DashboardController : Controller
    {
        private EasyMovingSystemEntities db = new EasyMovingSystemEntities();

        // GET: Dashboard
        public async Task<ActionResult> Index(int? id)
        {

            //var tblUser = db.Users.FirstOrDefault(a => a.User_ID == User_ID);
           

            ViewBag.UsersCount = db.Users.Count();

            User tblUser = await db.Users.FindAsync(id);
            if (tblUser == null)
            {
                return HttpNotFound();
            }
            /*Sign OUT*/
            if (tblUser.SessionID == null)
            {
                return RedirectToAction("Index", "AuthLogin");
            }
            /*Sign out*/
            var userRole = db.UserRoles.FirstOrDefault(a =>a.User_ID == tblUser.User_ID);
            if (userRole == null)
            {
                TempData["error"] = "User is not linked to a role, please conatct your system administrator";
                return RedirectToAction("Index", "AuthLogin");
            }

            ViewBag.RoleTypeId = userRole.RoleTypeId;
            ViewBag.UserId = id;
            var client = db.Clients.FirstOrDefault(a => a.User_ID == tblUser.User_ID);
            if (client !=null)
            {
                var userAccount = db.Clients.FirstOrDefault(a => a.User_ID == tblUser.User_ID);
                ViewBag.UserName = userAccount.ClientName + " " + userAccount.ClientSurname;
                ViewBag.ClientId = userAccount.Client_ID;
            }
            

            var employee = db.Employees.FirstOrDefault(a => a.User_ID == tblUser.User_ID);

            if (employee != null)
            {
                var userAccount = db.Employees.FirstOrDefault(a => a.User_ID == tblUser.User_ID);
                ViewBag.UserName = userAccount.EmployeeName + " " + userAccount.EmployeeSurname;
                ViewBag.ClientId = userAccount.Employee_ID;
                ViewBag.EmployeeId = userAccount.Employee_ID;
            }

            return View(tblUser);
        }

        //public async Task<ActionResult> MyProfile(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }

        //    TblUser tblUser = await db.TblUsers.FindAsync(id);
        //    if (tblUser == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    ViewBag.LoggedIn = id;
        //    ViewBag.UserId = id;

        //    ViewBag.UserName = tblUser.FirstName + " " + tblUser.Surname;

        //    return View(tblUser);
        //}
    }
}