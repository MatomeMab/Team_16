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

namespace Nazox.Controllers.Document
{
    public class DocumentTypesController : Controller
    {
        private EasyMovingSystemEntities db = new EasyMovingSystemEntities();
        private static int? RoleTypeId = null;
        private static int? User_ID = null;
        // GET: DocumentTypes
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

            RoleTypeId = userRole.RoleTypeId;
            ViewBag.RoleTypeId = userRole.RoleTypeId;
            ViewBag.UserId = id;
            User_ID = id;

            var userAccount = db.Clients.FirstOrDefault(a => a.User_ID == tblUser.User_ID);
            ViewBag.UserName = userAccount.ClientName + " " + userAccount.ClientSurname;
            ViewBag.ClientId = userAccount.Client_ID;

            //for data tables
            //db.Configuration.LazyLoadingEnabled = false; // if your table is relational, contain foreign key
            var data = db.DocumentTypes.OrderBy(a => a.DocumentTypeDescription).ToList();

             return View(await db.DocumentTypes.ToListAsync());
          
        }

        // GET: DocumentTypes/Details/5
        public async Task<ActionResult> Details(int? ids,int?id)
        {
            if (ids == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DocumentType documentType = await db.DocumentTypes.FindAsync(ids);
            if (documentType == null)
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


            return View(documentType);
        }

        // GET: DocumentTypes/Create
        public async Task<ActionResult> Create(int? id)
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

        // POST: DocumentTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "DocumentType_ID,DocumentTypeDescription")] DocumentType documentType, int? id)
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
                db.DocumentTypes.Add(documentType);
                await db.SaveChangesAsync();
                return RedirectToAction("Index", new { id = ViewBag.UserId });
            }

            return View(documentType);
        }

        // GET: DocumentTypes/Edit/5
        public async Task<ActionResult> Edit(int? ids,int?id)
        {
            if (ids == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DocumentType documentType = await db.DocumentTypes.FindAsync(ids);
            if (documentType == null)
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


            return View(documentType);
        }

        // POST: DocumentTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "DocumentType_ID,DocumentTypeDescription")] DocumentType documentType,int id)
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
                db.Entry(documentType).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index", new { id = ViewBag.UserId });
            }
            return View(documentType);
        }

        // GET: DocumentTypes/Delete/5
        public async Task<ActionResult> Delete(int? ids,int id)
        {
            if (ids == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DocumentType documentType = await db.DocumentTypes.FindAsync(ids);
            if (documentType == null)
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

            return View(documentType);
        }

        // POST: DocumentTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int ids,int id)
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
            /*DELETION CONSTRAINT*/
            var fk = db.Applications.Any(m => m.Document_ID == ids);
            if (!fk)
            {
                var doc = db.DocumentTypes.FirstOrDefault(u => u.DocumentType_ID == ids);
                if (doc == null)
                {

                    TempData["Error"] = "Error occurred while attempting to delete";
                }


                DocumentType documentType = await db.DocumentTypes.FindAsync(ids);
                db.DocumentTypes.Remove(documentType);
                await db.SaveChangesAsync();
                return RedirectToAction("Index", new { id = ViewBag.UserId });
            }
            else
            {
                TempData["Delete"] = "Failed to delete the Document type is linked to Application ";
                return RedirectToAction("Index", new { id = ViewBag.UserId });
            }

            /*END-OF DELETION CONSTRAINT*/

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
