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
using System.Data.SqlClient;

namespace Nazox.Controllers.Quote
{
    public class QuotationController : Controller
    {
        private EasyMovingSystemEntities db = new EasyMovingSystemEntities();
        private static int? RoleTypeId = null;
        private static int? User_ID = null;
        // GET: Quotation
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

           /* string sql = @"select QuotationRequestLine.Service_ID,QuotationRequest.Client_ID,
                FromAddress,QuotationRequest.ToAddress,QuotationRequest.QuotationReqDescription 
                from Client,QuotationRequest,QuotationRequestLine where 
                QuotationRequest.QuotationRequest_ID=QuotationRequestLine.QuotationRequest_ID";
            */
            ViewBag.rQuote = db.QuotationRequests.ToList();

            return View(await db.Quotations.ToListAsync());
            //return View(await db.Quotations.ToListAsync());
        }

        // GET: Quotation/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quotation quotation = await db.Quotations.FindAsync(id);
            if (quotation == null)
            {
                return HttpNotFound();
            }
            return View(quotation);
        }

        // GET: Quotation/Create
        public async Task<ActionResult> CreateAsync(int? id)
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

        // POST: Quotation/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Quotation_ID,QuotationDescription,QuotationDate,Amount")] Quotation quotation,QuotationLine quotationLine1,int?id,int?ids)
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
                db.Quotations.Add(quotation);
                await db.SaveChangesAsync();

                QuotationLine quotationLine = new QuotationLine();
                quotationLine.Quotation_ID = quotation.Quotation_ID;
                quotationLine.QuotationRequest_ID = quotationLine.QuotationRequest_ID;
                //quotationLine.Service_ID = quotationLine.Service_ID;
                return RedirectToAction("Index");
            }

            return View(quotation);
        }

        // GET: Quotation/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quotation quotation = await db.Quotations.FindAsync(id);
            if (quotation == null)
            {
                return HttpNotFound();
            }
            return View(quotation);
        }

        // POST: Quotation/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Quotation_ID,QuotationDescription,QuotationDate,Amount")] Quotation quotation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(quotation).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(quotation);
        }

        // GET: Quotation/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quotation quotation = await db.Quotations.FindAsync(id);
            if (quotation == null)
            {
                return HttpNotFound();
            }
            return View(quotation);
        }

        // POST: Quotation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Quotation quotation = await db.Quotations.FindAsync(id);
            db.Quotations.Remove(quotation);
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
