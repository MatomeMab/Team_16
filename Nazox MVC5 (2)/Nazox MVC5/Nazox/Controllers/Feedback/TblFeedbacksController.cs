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

namespace Nazox.Controllers.Feedback
{
    public class TblFeedbacksController : Controller
    {
        private EasyMovingSystemEntities db = new EasyMovingSystemEntities();

        // GET: TblFeedbacks
        public async Task<ActionResult> Index(int? id, int? User_ID)
        {
            var quote = db.TblFeedbacks.Where(b => b.Client_ID == id);


            var userRole = db.UserRoles.FirstOrDefault(a => a.User_ID == User_ID);
            if (userRole == null)
            {
                TempData["error"] = "User is not linked to a role, please conatct your system administrator";
                return RedirectToAction("Index", "AuthLogin");
            }

            ViewBag.RoleTypeId = userRole.RoleTypeId;
            //ViewBag.UserId = id;
            ViewBag.UserId = User_ID;
            var userAccount = db.Clients.FirstOrDefault(a => a.User_ID == User_ID);
            ViewBag.UserName = userAccount.ClientName + " " + userAccount.ClientSurname;
            //ViewBag.ClientId = userAccount.Client_ID;
            ViewBag.ClientId = id;


            var tblFeedbacks = db.TblFeedbacks.Include(t => t.Client).Include(t => t.FeedbackType).Include(t => t.Service);
            return View(tblFeedbacks.Where(x => x.Client_ID == id).ToList());
        }

        // GET: TblFeedbacks/Details/5
        public async Task<ActionResult> Details(int? ids, int? id, int? User_ID)
        {
            if (ids == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblFeedback tblFeedback = await db.TblFeedbacks.FindAsync(ids);
            if (tblFeedback == null)
            {
                return HttpNotFound();
            }
            /*FOR NAVIGATION*/
            var quote = db.TblFeedbacks.Where(b => b.Client_ID == id);

            var userRole = db.UserRoles.FirstOrDefault(a => a.User_ID == User_ID);
            if (userRole == null)
            {
                TempData["error"] = "User is not linked to a role, please conatct your system administrator";
                return RedirectToAction("Index", "AuthLogin");
            }

            ViewBag.RoleTypeId = userRole.RoleTypeId;
            ViewBag.UserId = User_ID;
            var userAccount = db.Clients.FirstOrDefault(a => a.User_ID == User_ID);
            ViewBag.UserName = userAccount.ClientName + " " + userAccount.ClientSurname;
            ViewBag.ClientId = id;
            /*FOR NAVIGATION*/

            return View(tblFeedback);
        }

        // GET: TblFeedbacks/Create
        public ActionResult Create(int? id, int? User_ID)
        {

            var feedbacks = db.TblFeedbacks.Where(b => b.Client_ID == id);

            var userRole = db.UserRoles.FirstOrDefault(a => a.User_ID == User_ID);
            if (userRole == null)
            {
                TempData["error"] = "User is not linked to a role, please conatct your system administrator";
                return RedirectToAction("Index", "AuthLogin");
            }

            ViewBag.RoleTypeId = userRole.RoleTypeId;
            ViewBag.UserId = id;
            ViewBag.UserId = User_ID;
            var userAccount = db.Clients.FirstOrDefault(a => a.User_ID == User_ID);
            ViewBag.UserName = userAccount.ClientName + " " + userAccount.ClientSurname;
            //ViewBag.ClientId = userAccount.Client_ID;
            ViewBag.ClientId = id;


            //ViewBag.Client_ID = new SelectList(db.Clients, "Client_ID", "ClientName");
            ViewBag.Client_ID = new SelectList(db.Clients.Where(x => x.Client_ID == id), "Client_ID", "ClientName").ToList();
            ViewBag.FeedbackType_ID = new SelectList(db.FeedbackTypes, "FeedbackType_ID", "FeedbackType1");
            ViewBag.Service_ID = new SelectList(db.Services, "Service_ID", "ServiceName");
            return View();
        }

        // POST: TblFeedbacks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Feedback_ID,FeedbackType_ID,FeedbackDescription,Service_ID,Client_ID,Booking_ID,Rating")] TblFeedback tblFeedback, int? id, int? User_ID)
        {
            /*FOR NAVIGATION*/
            var quote = db.TblFeedbacks.Where(b => b.Client_ID == id);

            var userRole = db.UserRoles.FirstOrDefault(a => a.User_ID == User_ID);
            if (userRole == null)
            {
                TempData["error"] = "User is not linked to a role, please conatct your system administrator";
                return RedirectToAction("Index", "AuthLogin");
            }

            ViewBag.RoleTypeId = userRole.RoleTypeId;
            ViewBag.UserId = User_ID;
            var userAccount = db.Clients.FirstOrDefault(a => a.User_ID == User_ID);
            ViewBag.UserName = userAccount.ClientName + " " + userAccount.ClientSurname;
            ViewBag.ClientId = id;
            /*FOR NAVIGATION*/

            if (ModelState.IsValid)
            {
                db.TblFeedbacks.Add(tblFeedback);
                await db.SaveChangesAsync();
                TempData["success"] = "Feedback created successfully";
                return RedirectToAction("Index", new { id = ViewBag.ClientId, User_ID = ViewBag.UserId });
            }

            //ViewBag.Client_ID = new SelectList(db.Clients, "Client_ID", "ClientName", tblFeedback.Client_ID);
            ViewBag.Client_ID = new SelectList(db.Clients.Where(x => x.Client_ID == id), "Client_ID", "ClientName").ToList();
            ViewBag.FeedbackType_ID = new SelectList(db.FeedbackTypes, "FeedbackType_ID", "FeedbackType1", tblFeedback.FeedbackType_ID);
            ViewBag.Service_ID = new SelectList(db.Services, "Service_ID", "ServiceName", tblFeedback.Service_ID);
            return View(tblFeedback);
        }

        // GET: TblFeedbacks/Edit/5
        public async Task<ActionResult> Edit(int? ids,int? id, int? User_ID)
        {

            /*FOR NAVIGATION*/
            var quote = db.TblFeedbacks.Where(b => b.Client_ID == id);

            var userRole = db.UserRoles.FirstOrDefault(a => a.User_ID == User_ID);
            if (userRole == null)
            {
                TempData["error"] = "User is not linked to a role, please conatct your system administrator";
                return RedirectToAction("Index", "AuthLogin");
            }

            ViewBag.RoleTypeId = userRole.RoleTypeId;
            ViewBag.UserId = User_ID;
            var userAccount = db.Clients.FirstOrDefault(a => a.User_ID == User_ID);
            ViewBag.UserName = userAccount.ClientName + " " + userAccount.ClientSurname;
            ViewBag.ClientId = id;
            /*FOR NAVIGATION*/

            if (ids == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblFeedback tblFeedback = await db.TblFeedbacks.FindAsync(ids);
            if (tblFeedback == null)
            {
                return HttpNotFound();
            }
            ViewBag.Client_ID = new SelectList(db.Clients, "Client_ID", "ClientName", tblFeedback.Client_ID);
            ViewBag.FeedbackType_ID = new SelectList(db.FeedbackTypes, "FeedbackType_ID", "FeedbackType1", tblFeedback.FeedbackType_ID);
            ViewBag.Service_ID = new SelectList(db.Services, "Service_ID", "ServiceName", tblFeedback.Service_ID);
            return View(tblFeedback);
        }

        // POST: TblFeedbacks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Feedback_ID,FeedbackType_ID,FeedbackDescription,Service_ID,Client_ID,Booking_ID,Rating")] TblFeedback tblFeedback, int? id, int? User_ID)
        {
            /*FOR NAVIGATION*/
            var quote = db.TblFeedbacks.Where(b => b.Client_ID == id);

            var userRole = db.UserRoles.FirstOrDefault(a => a.User_ID == User_ID);
            if (userRole == null)
            {
                TempData["error"] = "User is not linked to a role, please conatct your system administrator";
                return RedirectToAction("Index", "AuthLogin");
            }

            ViewBag.RoleTypeId = userRole.RoleTypeId;
            ViewBag.UserId = User_ID;
            var userAccount = db.Clients.FirstOrDefault(a => a.User_ID == User_ID);
            ViewBag.UserName = userAccount.ClientName + " " + userAccount.ClientSurname;
            ViewBag.ClientId = id;
            /*FOR NAVIGATION*/

            if (ModelState.IsValid)
            {
                db.Entry(tblFeedback).State = EntityState.Modified;
                await db.SaveChangesAsync();
                TempData["success"] = "Saved";
                return RedirectToAction("Index", new { id = ViewBag.ClientId, User_ID = ViewBag.UserId });
            }
            ViewBag.Client_ID = new SelectList(db.Clients, "Client_ID", "ClientName", tblFeedback.Client_ID);
            ViewBag.FeedbackType_ID = new SelectList(db.FeedbackTypes, "FeedbackType_ID", "FeedbackType1", tblFeedback.FeedbackType_ID);
            ViewBag.Service_ID = new SelectList(db.Services, "Service_ID", "ServiceName", tblFeedback.Service_ID);
            return View(tblFeedback);
        }

        // GET: TblFeedbacks/Delete/5
        public async Task<ActionResult> Delete(int? ids, int? id, int? User_ID)
        {

            /*FOR NAVIGATION*/
            var quote = db.QuotationRequests.Where(b => b.Client_ID == id);

            var userRole = db.UserRoles.FirstOrDefault(a => a.User_ID == User_ID);
            if (userRole == null)
            {
                TempData["error"] = "User is not linked to a role, please conatct your system administrator";
                return RedirectToAction("Index", "AuthLogin");
            }

            ViewBag.RoleTypeId = userRole.RoleTypeId;
            ViewBag.UserId = User_ID;
            var userAccount = db.Clients.FirstOrDefault(a => a.User_ID == User_ID);
            ViewBag.UserName = userAccount.ClientName + " " + userAccount.ClientSurname;
            ViewBag.ClientId = id;
            /*FOR NAVIGATION*/

            if (ids == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblFeedback tblFeedback = await db.TblFeedbacks.FindAsync(ids);
            if (tblFeedback == null)
            {
                return HttpNotFound();
            }
            return View(tblFeedback);
        }

        // POST: TblFeedbacks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int ids, int? id, int? User_ID)
        {


            /*FOR NAVIGATION*/
            var quote = db.TblFeedbacks.Where(b => b.Client_ID == id);

            var userRole = db.UserRoles.FirstOrDefault(a => a.User_ID == User_ID);
            if (userRole == null)
            {
                TempData["error"] = "User is not linked to a role, please conatct your system administrator";
                return RedirectToAction("Index", "AuthLogin");
            }

            ViewBag.RoleTypeId = userRole.RoleTypeId;
            ViewBag.UserId = User_ID;
            var userAccount = db.Clients.FirstOrDefault(a => a.User_ID == User_ID);
            ViewBag.UserName = userAccount.ClientName + " " + userAccount.ClientSurname;
            ViewBag.ClientId = id;
            /*FOR NAVIGATION*/

            TblFeedback tblFeedback = await db.TblFeedbacks.FindAsync(ids);
            db.TblFeedbacks.Remove(tblFeedback);
            await db.SaveChangesAsync();
            return RedirectToAction("Index", new { id = ViewBag.ClientId, User_ID = ViewBag.UserId });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public class ServiceObj
        {
            public int? Service_ID { get; set; }
            public string ServiceName { get; set; }
        }


    }
}
