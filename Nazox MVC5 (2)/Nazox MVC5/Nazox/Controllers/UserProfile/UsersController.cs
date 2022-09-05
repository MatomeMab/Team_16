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
using System.Text;
using System.Dynamic;

namespace Nazox.Controllers.UserProfile
{
    public class UsersController : Controller
    {
        private EasyMovingSystemEntities db = new EasyMovingSystemEntities();
        private static int? RoleTypeId = null;
        private static int? User_ID = null;
        // GET: Users
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
                TempData["error"] = "User is not linked to a role, please contact your system administrator";
                return RedirectToAction("Index", "AuthLogin");
            }

            ViewBag.RoleTypeId = userRole.RoleTypeId;
            ViewBag.UserId = id;

            var userAccount = db.Users.FirstOrDefault(a => a.User_ID == tblUser.User_ID);
            ViewBag.UserName = userAccount.User_ID + " " + userAccount.Email;
            ViewBag.Bookings = db.Bookings.Where(a => a.Client_ID == userAccount.User_ID).ToList();
            ViewBag.ClientId = userAccount.User_ID;

            return View(userRole);
        }

        // GET: Users/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "User_ID,Role_ID,SessionID,OTP,Active,Email,Session_Expiry,Password")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = await db.Users.FindAsync(id);
            if (user == null)
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

            var userAccount = db.Users.FirstOrDefault(a => a.User_ID == tblUser.User_ID);
            ViewBag.UserName = userAccount.Email + " " + userAccount.Password;
            ViewBag.ClientId = userAccount.User_ID;

            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "User_ID,Role_ID,SessionID,OTP,Active,Email,Session_Expiry,Password")] User user)
        {
            if (ModelState.IsValid)
            {
                user.Password = Hash(user.Password);

                db.Entry(user).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index", new { id = User_ID });
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            User user = await db.Users.FindAsync(id);
            db.Users.Remove(user);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public static string Hash(string value)
        {
            return Convert.ToBase64String(
                System.Security.Cryptography.SHA256.Create()
                .ComputeHash(Encoding.UTF8.GetBytes(value))
                );
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public Task<ActionResult> deactivateUser(int id)
        {
            using (DbContextTransaction transaction = db.Database.BeginTransaction())
            {
                try
                {
                    int userID = Convert.ToInt16(id);
                    db.Configuration.ProxyCreationEnabled = false;
                    User user = db.Users.Where(z => z.User_ID == userID).FirstOrDefault();
                    dynamic obj = new ExpandoObject();

                    if (user != null)
                    {
                        user.Active = false;

                        db.SaveChanges();

                        transaction.Commit();

                        obj.Success = "You have been deregistred Successfully";
                        return obj;
                    }
                    else
                    {
                        transaction.Rollback();
                        obj.Error = "Error, Please try again";
                        return obj;
                    }
                }
                catch (Exception e)
                {
                    transaction.Rollback();

                    dynamic obj = new ExpandoObject();
                    obj.Error = (e.Message);
                    return obj;
                }
            }
        }

        [HttpPost]
        public async Task<ActionResult> UpdatePassword([Bind(Include = "User_ID,Password")] User usr,int?id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User users = await db.Users.FindAsync(id);
            if (users == null)
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

            var userAccount = db.Users.FirstOrDefault(a => a.User_ID == tblUser.User_ID);
            ViewBag.UserName = userAccount.Email + " " + userAccount.Password;
            ViewBag.ClientId = userAccount.User_ID;
            using (DbContextTransaction transaction = db.Database.BeginTransaction())
            {
                dynamic obj = new ExpandoObject();

                try
                {
                    using (EasyMovingSystemEntities dc = new EasyMovingSystemEntities())
                    {
                        db.Configuration.ProxyCreationEnabled = false;

                        var user = dc.Users.Where(a => a.User_ID == usr.User_ID).FirstOrDefault();
                        if (User != null)
                        {
                            user.Password = Hash(usr.Password);
                            //user.ResetPasswordCode = null;
                            dc.Configuration.ValidateOnSaveEnabled = false;
                            dc.SaveChanges();

                            transaction.Commit();
                            obj.message = "Password updated successfully.";

                            dynamic u = new ExpandoObject();
                            u.ID = user.User_ID;
                            u.UserPassword = user.Password;
                            u.UserEmail = user.Email;

                            return u;
                        }
                        else
                        {
                            transaction.Rollback();

                            obj.Error = ("You do not have premission to change your password.");

                            return obj;
                        }
                    }
                }
                catch (Exception e)
                {
                    transaction.Rollback();

                    obj.Error = (e.Message);
                    return obj.Error;
                }
            }
            return View(users);
        }
    }
}
