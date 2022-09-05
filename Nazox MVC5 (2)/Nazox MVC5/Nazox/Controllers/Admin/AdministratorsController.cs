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

namespace Nazox.Controllers.Admin
{
    public class AdministratorsController : Controller
    {
        private EasyMovingSystemEntities db = new EasyMovingSystemEntities();
        private static int? RoleTypeId = null;
        private static int? User_ID = null;

        // GET: Administrators
        public async Task<ActionResult> Index(int? id)
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

            ViewBag.RoleTypeId = userRole.RoleTypeId;
            ViewBag.UserId = id;
            User_ID = id;

            var userAccount = db.Clients.FirstOrDefault(a => a.User_ID == tblUser.User_ID);
            ViewBag.UserName = userAccount.ClientName + " " + userAccount.ClientSurname;
            ViewBag.ClientId = userAccount.Client_ID;

            return View(await db.Administrators.ToListAsync());
        }

        // GET: Administrators/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Administrator administrator = await db.Administrators.FindAsync(id);
            if (administrator == null)
            {
                return HttpNotFound();
            }

            User tblUser = await db.Users.FindAsync(User_ID);
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

            var userAccount = db.Clients.FirstOrDefault(a => a.User_ID == tblUser.User_ID);
            ViewBag.UserName = userAccount.ClientName + " " + userAccount.ClientSurname;
            ViewBag.ClientId = userAccount.Client_ID;
            ViewBag.User_ID = userAccount.User_ID;

            return View(administrator);
        }

        // GET: Administrators/Create
        public async Task<ActionResult> Create()
        {
            User tblUser = await db.Users.FindAsync(User_ID);
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

            var userAccount = db.Clients.FirstOrDefault(a => a.User_ID == tblUser.User_ID);
            ViewBag.UserName = userAccount.ClientName + " " + userAccount.ClientSurname;
            ViewBag.ClientId = userAccount.Client_ID;
            ViewBag.User_ID = userAccount.User_ID;

            ViewBag.RoleType = new SelectList(db.RoleTypes.Where(a =>a.RoleTypeId != 2 && a.RoleTypeId != 3), "RoleTypeId", "RoleTpe");

            return View();
        }

        // POST: Administrators/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Admin_ID,EmailAddress,AdminName,SurName,PhoneNum")] Administrator administrator, FormCollection form)
        {
            //Check if user dpoes not exist 
            var checkExist = db.Administrators.FirstOrDefault(a => a.EmailAddress == administrator.EmailAddress);

            if(checkExist != null)
            {
                TempData["error"] = "The user with email " + administrator.EmailAddress + " already exists, please edit the user information";
                return RedirectToAction("Create");
            }

            if (ModelState.IsValid)
            {
                int? RoleTypeId = int.Parse(form["RoleType"]);
                int? User_ID = int.Parse(form["User_ID"]);

                db.Administrators.Add(administrator);
                await db.SaveChangesAsync();

                //Add user account
                User user = new User();
                user.Active = false;
                user.Email = administrator.EmailAddress;
                
                db.Users.Add(user);
                await db.SaveChangesAsync();

                //User Role
                UserRole userRole = new UserRole();
                userRole.User_ID = user.User_ID;
                userRole.Admin_ID = administrator.Admin_ID;
               // userRole.RoleTypeId = administrator.RoleTypeId;

                db.UserRoles.Add(userRole);
                await db.SaveChangesAsync();

                string Subject = "WENDA Logistics Admin Account";

                var verifyUrl = "/AuthLogin/NewPassword";
                var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);

                string Body = "Hi New User</br> welcome to WENDA Logistics Group. User new account has been created create your passowrd. Follow the link below to reset your password " + verifyUrl;
               
                var smpt = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("felicityfanelo@zohomail.com", "Nghonyama.1")
                };

                using (var message = new MailMessage("ffelicityfanelo@zohomail.com", administrator.EmailAddress.Trim())
                {
                    Subject = Subject,
                    Body = Body,
                    IsBodyHtml = true
                })
                    smpt.Send(message);

                TempData["success"] = "Account created successfully";

                return RedirectToAction("Index", new { id = User_ID });
            }

            return View(administrator);
        }

        // GET: Administrators/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Administrator administrator = await db.Administrators.FindAsync(id);
            if (administrator == null)
            {
                return HttpNotFound();
            }
            User tblUser = await db.Users.FindAsync(User_ID);
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

            var userAccount = db.Clients.FirstOrDefault(a => a.User_ID == tblUser.User_ID);
            ViewBag.UserName = userAccount.ClientName + " " + userAccount.ClientSurname;
            ViewBag.ClientId = userAccount.Client_ID;
            ViewBag.RoleType = new SelectList(db.RoleTypes.Where(a => a.RoleTypeId != 2 && a.RoleTypeId != 3), "RoleTypeId", "RoleTpe" /*,administrator.RoleTypeId*/);
            ViewBag.User_ID = userAccount.User_ID;

            return View(administrator);
        }

        // POST: Administrators/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Admin_ID,EmailAddress,RoleTypeId,AdminName,SurName,PhoneNum")] Administrator administrator, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                int? RoleTypeId = int.Parse(form["RoleType"]);

                db.Entry(administrator).State = EntityState.Modified;
                await db.SaveChangesAsync();

                TempData["success"] = "Edit successful";

                return RedirectToAction("Index", new { id = User_ID });
            }

            return View(administrator);
        }

        // GET: Administrators/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Administrator administrator = await db.Administrators.FindAsync(id);
            if (administrator == null)
            {
                return HttpNotFound();
            }
            User tblUser = await db.Users.FindAsync(User_ID);
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

            var userAccount = db.Clients.FirstOrDefault(a => a.User_ID == tblUser.User_ID);
            ViewBag.UserName = userAccount.ClientName + " " + userAccount.ClientSurname;
            ViewBag.ClientId = userAccount.Client_ID;

            ViewBag.RoleType = new SelectList(db.RoleTypes.Where(a => a.RoleTypeId != 2 && a.RoleTypeId != 3), "RoleTypeId", "RoleTpe" /*, administrator.RoleTypeId*/);

            return View(administrator);
        }

        // POST: Administrators/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Administrator administrator = await db.Administrators.FindAsync(id);

            var userRole = db.UserRoles.Where(a => a.Admin_ID == id).ToList();

            db.UserRoles.RemoveRange(userRole);
            await db.SaveChangesAsync();

            db.Administrators.Remove(administrator);
            await db.SaveChangesAsync();

            TempData["success"] = "Delete successful";

            return RedirectToAction("Index", new { id = User_ID });
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
