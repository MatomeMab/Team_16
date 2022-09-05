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
using Quotation = Nazox.Models.Quotation;

namespace Nazox.Controllers.Quote
{
    public class ClientQuotationsController : Controller
    {
        private EasyMovingSystemEntities db = new EasyMovingSystemEntities();

        // GET: ClientQuotations
        public async Task<ActionResult> Index(int? id, int? User_ID)
        {
            
            /*NAV*/
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
            /*END NAV*/



            List<Client> clients = db.Clients.ToList();
            List<Quotation> quotations = db.Quotations.ToList();
            List<QuotationRequest> quotationRequests = db.QuotationRequests.ToList();
            List<QuotationLine> quotationLines = db.QuotationLines.ToList();


            var quotes = from q in quotations
                             join qL in quotationLines on q.Quotation_ID equals qL.Quotation_ID
                             join qR in quotationRequests on qL.QuotationRequest_ID equals qR.QuotationRequest_ID
                             where qR.Client_ID == id
                             select new 
                             {
                                 q.QuotationDescription,
                                 q.Amount,
                             };
            //ViewData["Quotes"] = quotes;

            ViewBag.quotes = quotes.ToList();
            //Console.WriteLine(ViewBag.quotes);

            return View(await db.Quotations.ToListAsync());
            //return View();
            
            
        }

        // GET: ClientQuotations/Details/5
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

        // GET: ClientQuotations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ClientQuotations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Quotation_ID,QuotationDescription,QuotationDate,Amount")] Quotation quotation)
        {
            if (ModelState.IsValid)
            {
                db.Quotations.Add(quotation);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(quotation);
        }

        // GET: ClientQuotations/Edit/5
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

        // POST: ClientQuotations/Edit/5
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

        // GET: ClientQuotations/Delete/5
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

        // POST: ClientQuotations/Delete/5
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
