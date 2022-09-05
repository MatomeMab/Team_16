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

namespace Nazox.Controllers.Quaote
{
    public class QuotationRequestsController : Controller
    {
        private EasyMovingSystemEntities db = new EasyMovingSystemEntities();
        private static int? RoleTypeId = null;
        private static int? User_ID = null;

        // GET: QuotationRequests
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

            var quotationRequests = db.QuotationRequests.Include(q => q.Client).Include(q => q.Client);
            
            return View(await quotationRequests.ToListAsync());
        }

        // GET: QuotationRequests/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuotationRequest quotationRequest = await db.QuotationRequests.FindAsync(id);
            if (quotationRequest == null)
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

            return View(quotationRequest);
        }

        // GET: QuotationRequests/Create
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
            ViewBag.UserId = User_ID;

            ViewBag.Client_ID = new SelectList(db.Clients, "Client_ID", "ClientName");
            ViewBag.Quotation_ID = new SelectList(db.Quotations, "Quotation_ID", "QuotationDescription");
            ViewBag.Service_ID = new SelectList(db.Services, "Service_ID", "ServiceName");
            return View();
        }

        // POST: QuotationRequests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "QuotationRequest_ID,Client_ID,Quotation_ID,Service_ID,QuotationReqDate,QuotationReqDescription,FromAddress,ToAddress")] QuotationRequest quotationRequest)
        {
            if (ModelState.IsValid)
            {
                db.QuotationRequests.Add(quotationRequest);
                await db.SaveChangesAsync();
                return RedirectToAction("Index", new { id = User_ID });
            }

            ViewBag.Client_ID = new SelectList(db.Clients, "Client_ID", "ClientName", quotationRequest.Client_ID);
           // ViewBag.Quotation_ID = new SelectList(db.Quotations, "Quotation_ID", "QuotationDescription", quotationRequest.Quotation_ID);
           // ViewBag.Service_ID = new SelectList(db.Services, "Service_ID", "ServiceName", quotationRequest.Service_ID);
            return View(quotationRequest);
        }

        // GET: QuotationRequests/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuotationRequest quotationRequest = await db.QuotationRequests.FindAsync(id);
            if (quotationRequest == null)
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


            ViewBag.Client_ID = new SelectList(db.Clients, "Client_ID", "ClientName", quotationRequest.Client_ID);
           // ViewBag.Quotation_ID = new SelectList(db.Quotations, "Quotation_ID", "QuotationDescription", quotationRequest.Quotation_ID);
          //  ViewBag.Service_ID = new SelectList(db.Services, "Service_ID", "ServiceName", quotationRequest.Service_ID);
            return View(quotationRequest);
        }

        // POST: QuotationRequests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "QuotationRequest_ID,Client_ID,Quotation_ID,Service_ID,QuotationReqDate,QuotationReqDescription,FromAddress,ToAddress")] QuotationRequest quotationRequest)
        {
            if (ModelState.IsValid)
            {
                db.Entry(quotationRequest).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index", new { id = User_ID });
            }
            ViewBag.Client_ID = new SelectList(db.Clients, "Client_ID", "ClientName", quotationRequest.Client_ID);
            //ViewBag.Quotation_ID = new SelectList(db.Quotations, "Quotation_ID", "QuotationDescription", quotationRequest.Quotation_ID);
          //  ViewBag.Service_ID = new SelectList(db.Services, "Service_ID", "ServiceName", quotationRequest.Service_ID);
            return View(quotationRequest);
        }

        // GET: QuotationRequests/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuotationRequest quotationRequest = await db.QuotationRequests.FindAsync(id);
            if (quotationRequest == null)
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

            ViewBag.Client_ID = new SelectList(db.Clients, "Client_ID", "ClientName", quotationRequest.Client_ID);
            //ViewBag.Quotation_ID = new SelectList(db.Quotations, "Quotation_ID", "QuotationDescription", quotationRequest.Quotation_ID);
            //ViewBag.Service_ID = new SelectList(db.Services, "Service_ID", "ServiceName", quotationRequest.Service_ID);

            return View(quotationRequest);
        }

        // POST: QuotationRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            QuotationRequest quotationRequest = await db.QuotationRequests.FindAsync(id);
            db.QuotationRequests.Remove(quotationRequest);
            await db.SaveChangesAsync();
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
