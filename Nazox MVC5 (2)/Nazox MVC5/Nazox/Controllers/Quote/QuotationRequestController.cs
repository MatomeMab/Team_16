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
    public class QuotationRequestController : Controller
    {
        private EasyMovingSystemEntities db = new EasyMovingSystemEntities();
        // GET: QuotationRequest
        public async Task<ActionResult> Index(int? id, int? User_ID)
        {

            var quote = db.QuotationRequests.Where(b => b.Client_ID == id);

            var userRole = db.UserRoles.FirstOrDefault(a => a.User_ID == User_ID);
            if (userRole == null)
            {
                TempData["error"] = "User is not linked to a role, please conatct your system administrator";
                return RedirectToAction("Index", "AuthLogin");
            }
            //var tblUser = db.Users.FirstOrDefault(a => a.User_ID == User_ID);
            //if (tblUser.SessionID == null)
            //{
            //    return RedirectToAction("Index", "AuthLogin");
            //}
            ViewBag.RoleTypeId = userRole.RoleTypeId;
            //ViewBag.UserId = id;
            ViewBag.UserId = User_ID;
            var userAccount = db.Clients.FirstOrDefault(a => a.User_ID == User_ID);
            ViewBag.UserName = userAccount.ClientName + " " + userAccount.ClientSurname;
            //ViewBag.ClientId = userAccount.Client_ID;
            ViewBag.ClientId = id;
            var quotationRequests = db.QuotationRequests.Include(q => q.Client);
            return View(quotationRequests.Where(x=>x.Client_ID==id).ToList());
        }

        // GET: QuotationRequest/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuotationRequest qoute = await db.QuotationRequests.FindAsync(id);
            if (qoute == null)
            {
                return HttpNotFound();
            }

            Client client = await db.Clients.FindAsync(qoute.Client_ID);

            var userRole = db.UserRoles.FirstOrDefault(a => a.User_ID == client.User_ID);
            if (userRole == null)
            {
                TempData["error"] = "User is not linked to a role, please conatct your system administrator";
                return RedirectToAction("Index", "AuthLogin");
            }

            ViewBag.RoleTypeId = userRole.RoleTypeId;
            ViewBag.UserId = id;

            var userAccount = db.Clients.FirstOrDefault(a => a.User_ID == client.User_ID);
            ViewBag.UserName = userAccount.ClientName + " " + userAccount.ClientSurname;
            ViewBag.ClientId = userAccount.Client_ID;




            return View(qoute);
        }

        // GET: QuotationRequest/Create
        public ActionResult Create( int? id,int? User_ID)
        {
            List<ServiceObj> serviceObjs = new List<ServiceObj>();

            var serv = db.Services.ToList();

            for (int i = 0; i < serv.Count(); i++)
            {
                var item = serv.ElementAt(i);
                if (item != null)
                {
                    serviceObjs.Add(new ServiceObj()
                    {
                        Service_ID = item.Service_ID,
                        ServiceName = item.ServiceName
                    });
                }
            }

            var quote = db.QuotationRequests.Where(b => b.Client_ID == id);

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

            //{
            //ViewBag.data = new SelectList(db.Services, "Service_ID", "ServiceName");
            //ViewBag.data = new SelectList(serviceObjs, "Service_ID", "ServiceName");


            // ViewBag.Client_ID = new SelectList(db.Clients, "Client_ID", "ClientName",id);
            ViewBag.Client_ID = new SelectList(db.Clients.Where(x => x.Client_ID == id),"Client_ID","ClientName").ToList();
            ViewBag.Service_ID = new SelectList(db.Services,"Service_ID","ServiceName").ToList();
            ViewBag.Truck_ID = new SelectList(db.Trucks, "Truck_ID", "RegNum");
            return View("~/Views/QuotationRequest/Create.cshtml");
            //return View();
        }

        // POST: QuotationRequest/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "QuotationRequest_ID,Client_ID,QuotationReqDate,QuotationReqDescription,FromAddress,ToAddress")] QuotationRequest quotationRequest, QuotationRequestLine quotationRequestLine1, int? id, int? User_ID)
        {

            List<ServiceObj> serviceObjs = new List<ServiceObj>();

            var serv = db.Services.ToList();

            for (int i = 0; i < serv.Count(); i++)
            {
                var item = serv.ElementAt(i);
                if (item != null)
                {
                    serviceObjs.Add(new ServiceObj()
                    {
                        Service_ID = item.Service_ID,
                        ServiceName = item.ServiceName
                    });
                }
            }


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

            if (ModelState.IsValid)
            {
                db.QuotationRequests.Add(quotationRequest);
                await db.SaveChangesAsync();
                //new qouteline
               
                //quotationRequestLine.QuotationRequest_ID = quotationRequest.QuotationRequest_ID;
                //_ = quotationRequestLine.Service_ID;
                //await db.SaveChangesAsync();

                //new qouteline
                QuotationRequestLine quotationRequestLine = new QuotationRequestLine();
                quotationRequestLine.QuotationRequest_ID = quotationRequest.QuotationRequest_ID;
                quotationRequestLine.Service_ID= quotationRequestLine1.Service_ID;
                quotationRequestLine.Truck_ID = quotationRequestLine1.Truck_ID;
                db.QuotationRequestLines.Add(quotationRequestLine);
                await db.SaveChangesAsync();
                TempData["success"] = "You have successfully requested a quotation.";
                if (quotationRequestLine.Service_ID == 4)
                {
                    RentalAgreement rentalAgreement = new RentalAgreement();
                    rentalAgreement.Client_ID = quotationRequest.Client_ID;
                    rentalAgreement.Description = quotationRequest.QuotationReqDescription;
                    rentalAgreement.Truck_ID = (int)quotationRequestLine.Truck_ID;
                    rentalAgreement.RentalStatus_ID = 1;
                    rentalAgreement.Inspection_ID = 1;
                    db.RentalAgreements.Add(rentalAgreement);
                    await db.SaveChangesAsync();
                }
                return RedirectToAction("Index", new { id = ViewBag.ClientId, User_ID = ViewBag.UserId });
            }
            var quotationRequestLines = db.QuotationRequestLines.Include(q => q.QuotationRequest).Include(q=>q.Service);
            ViewBag.Service_ID = new SelectList(db.QuotationRequestLines, "Service_ID", "ServiceName");
            ViewBag.Truck_ID = new SelectList(db.Trucks, "Truck_ID", "RegNum");
            ViewBag.data = new SelectList(serviceObjs, "Service_ID", "ServiceName",quotationRequestLine1.Service_ID);
            ViewBag.Client_ID = new SelectList(db.Clients, "Client_ID", "ClientName", quotationRequest.Client_ID);
            return View(quotationRequest);
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
