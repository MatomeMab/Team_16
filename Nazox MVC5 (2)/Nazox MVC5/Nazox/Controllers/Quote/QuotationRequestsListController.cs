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

namespace Nazox.Controllers.Quote
{
    public class QuotationRequestsListController : Controller
    {
        private EasyMovingSystemEntities db = new EasyMovingSystemEntities();
        private static int? RoleTypeId = null;
        private static int? User_ID = null;
        // GET: QuotationRequestsList
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

            var quotationRequests = db.QuotationRequests.Include(q => q.Client);
            return View(await quotationRequests.ToListAsync());
        }

        // GET: QuotationRequestsList/Details/5
        public async Task<ActionResult> Details(int? ids)
        {

            var RequestQuote_ID = db.QuotationRequests.Where(x => x.QuotationRequest_ID == ids);
            //ViewBag.Service_ID = new SelectList(db.QuotationRequests, "Service_ID", "ServiceName").Where(x => x.QuotationRequest_ID == ids).
            ViewBag.RequestQuote_ID = new SelectList(RequestQuote_ID, "QuotationRequest_ID", "QuotationRequest_ID").ToList();
            /*NAVIGATION*/
            //User tblUser = await db.Users.FindAsync(id);
            //if (tblUser == null)
            //{
            //    return HttpNotFound();
            //}

            //var userRole = db.UserRoles.FirstOrDefault(a => a.User_ID == tblUser.User_ID);
            //if (userRole == null)
            //{
            //    TempData["error"] = "User is not linked to a role, please conatct your system administrator";
            //    return RedirectToAction("Index", "AuthLogin");
            //}

            //RoleTypeId = userRole.RoleTypeId;
            //ViewBag.RoleTypeId = userRole.RoleTypeId;
            //ViewBag.UserId = id;
            //User_ID = id;

            /*NAVIGATION*/

            if (ids == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuotationRequest quotationRequest = await db.QuotationRequests.FindAsync(ids);
            if (quotationRequest == null)
            {
                return HttpNotFound();
            }
            /*if (ModelState.IsValid)
            {
                Quotation quotation = new Quotation();
                // quotation.QuotationDate = quotation.QuotationDate;
                quotation.Amount = quotation.Amount;
                quotation.QuotationDescription = quotation.QuotationDescription;
                db.Quotations.Add(quotation);
                await db.SaveChangesAsync();
                var reqID = ids;
                QuotationLine quotationLine = new QuotationLine();
                quotationLine.Quotation_ID = quotation.Quotation_ID;
                quotationLine.QuotationRequest_ID = ids;
                quotationLine.Service_ID = quotationLine.Service_ID;
                return RedirectToAction("Index");
            }*/
            return View(quotationRequest);
           


        }

        // GET: QuotationRequestsList/Create
        public ActionResult Create()
        {

            ViewBag.Client_ID = new SelectList(db.Clients, "Client_ID", "ClientName");
            return View();
        }

        // POST: QuotationRequestsList/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "QuotationRequest_ID,Client_ID,QuotationReqDate,QuotationReqDescription,FromAddress,ToAddress")] QuotationRequest quotationRequest)
        {
            if (ModelState.IsValid)
            {
                db.QuotationRequests.Add(quotationRequest);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Client_ID = new SelectList(db.Clients, "Client_ID", "ClientName", quotationRequest.Client_ID);
            return View(quotationRequest);
        }

        // GET: QuotationRequestsList/Edit/5
        public async Task<ActionResult> Edit(int? ids, int? id)
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
            QuotationRequest quotationRequest = await db.QuotationRequests.FindAsync(ids);
            if (quotationRequest == null)
            {
                return HttpNotFound();
            }
            ViewBag.Client_ID = new SelectList(db.Clients, "Client_ID", "ClientName", quotationRequest.Client_ID);
            return View(quotationRequest);
        }

        // POST: QuotationRequestsList/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "QuotationRequest_ID,Client_ID,QuotationReqDate,QuotationReqDescription,FromAddress,ToAddress")] QuotationRequest quotationRequest,Quotation quotation1,QuotationLine quotationLine1,int?id)
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

            /*create quote*/
            if (ModelState.IsValid)
            {
                Quotation quotation = new Quotation();
                quotation.QuotationDate = quotation1.QuotationDate;
                quotation.Amount = quotation1.Amount;
                quotation.QuotationDescription = quotation1.QuotationDescription;
                db.Quotations.Add(quotation);
                await db.SaveChangesAsync();
                //var reqID = quotationRequest.QuotationRequest_ID;
                QuotationLine quotationLine = new QuotationLine();
                quotationLine.Quotation_ID = quotation.Quotation_ID;
                quotationLine.QuotationRequest_ID = quotationRequest.QuotationRequest_ID;
                //quotationLine.Service_ID = quotationLine.Service_ID;
                db.QuotationLines.Add(quotationLine);
                await db.SaveChangesAsync();
                TempData["Success"] = "Quote created succefully!!";
                return RedirectToAction("Index", new { id = ViewBag.UserId });
            }
            /*create quote*/


            //if (ModelState.IsValid)
            //{
            //    db.Entry(quotationRequest).State = EntityState.Modified;
            //    await db.SaveChangesAsync();
            //    return RedirectToAction("Index", new { id = ViewBag.UserId });
            //}
            ViewBag.Client_ID = new SelectList(db.Clients, "Client_ID", "ClientName", quotationRequest.Client_ID);
            return View(quotationRequest);
        }

        // GET: QuotationRequestsList/Delete/5
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
            return View(quotationRequest);
        }

        // POST: QuotationRequestsList/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            QuotationRequest quotationRequest = await db.QuotationRequests.FindAsync(id);
            db.QuotationRequests.Remove(quotationRequest);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
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
