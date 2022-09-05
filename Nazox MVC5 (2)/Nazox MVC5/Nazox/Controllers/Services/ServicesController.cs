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

namespace Nazox.Controllers.Services
{
    public class ServicesController : Controller
    {
        private EasyMovingSystemEntities db = new EasyMovingSystemEntities();
        private static int? RoleTypeId = null;
        private static int? User_ID = null;

        // GET: Services
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

            return View(await db.Services.ToListAsync());
        }

        // GET: Services/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = await db.Services.FindAsync(id);
            if (service == null)
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
            ViewBag.UserId = User_ID;

            return View(service);
        }

        // GET: Services/Create
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

        // POST: Services/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Service_ID,ServiceName,ServiceDescription")] Service service,int?id)
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

                db.Services.Add(service);
                await db.SaveChangesAsync();
                TempData["success"] = "Service created succefully!!";
                return RedirectToAction("Index", new { id = ViewBag.UserId });
            }
            TempData["error"] = "Something went wrong please try again";
            return View(service);
        }

        // GET: Services/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = await db.Services.FindAsync(id);
            if (service == null)
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
            ViewBag.UserId = User_ID;

            return View(service);
        }

        // POST: Services/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Service_ID,ServiceName,ServiceDescription")] Service service)
        {
            if (ModelState.IsValid)
            {
                db.Entry(service).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index", new { id = ViewBag.UserId });
            }
            return View(service);
        }

        // GET: Services/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = await db.Services.FindAsync(id);
            if (service == null)
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
            ViewBag.UserId = User_ID;

            return View(service);
        }

        // POST: Services/Delete/5
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

            var fk = db.TblFeedbacks.Any(m => m.Service_ID == ids);
           var qL=db.QuotationRequestLines.Any(m => m.Service_ID==ids);
            if (!fk||qL)
            {
                var serv = db.Services.FirstOrDefault(u => u.Service_ID == ids);
                var quotationLine = db.QuotationRequestLines.FirstOrDefault(u => u.Service_ID == ids);
                if (serv == null)
                {
                    TempData["Error"] = "Error occurred while attempting to delete";
                }
                if (quotationLine==null)
                {
                    TempData["Error"] = "Error occurred while attempting to delete";
                }

                Service service = await db.Services.FindAsync(ids);
                db.Services.Remove(service);
                await db.SaveChangesAsync();
                return RedirectToAction("Index", new { id = ViewBag.UserId });
            }
            else if (fk)
            {
                TempData["Delete"] = "Failed to delete the service is linked to Feedback";
                return RedirectToAction("Index", new { id = ViewBag.UserId });
            }

            else if(qL)
            {
                TempData["Delete"] = "Failed to delete the service is linked to Quotation Request Line";
                return RedirectToAction("Index", new { id = ViewBag.UserId });
            }
            return RedirectToAction("Index", new { id = ViewBag.UserId });
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
