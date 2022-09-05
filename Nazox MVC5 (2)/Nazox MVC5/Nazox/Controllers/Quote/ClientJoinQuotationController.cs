using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nazox.Models.QuotationViewModel;
using Nazox.Models;
using System.Threading.Tasks;
using System.Net;

namespace Nazox.Controllers.Quote
{
    public class ClientJoinQuotationController : Controller
    {
        private EasyMovingSystemEntities db = new EasyMovingSystemEntities();
        // GET: ClientJoinQuotation
        public ActionResult Index(int? id, int? User_ID)
        {
            List<Client> clients = db.Clients.ToList();
            List<Quotation> quotationList = db.Quotations.ToList();
            List<QuotationRequest> QuotationRequestList = db.QuotationRequests.ToList();
            List<QuotationLine> QuotationLineList = db.QuotationLines.ToList();
            List<QuotationRequestLine> quotationRequestLines = db.QuotationRequestLines.ToList();

            //var multipleJoin = from q in quotationList
            //                   join qL in QuotationLineList on q.Quotation_ID equals qL.Quotation_ID into table1
            //                   from qL in table1.DefaultIfEmpty()
            //                   join qR in QuotationRequestList on qL.QuotationRequest_ID equals qR.QuotationRequest_ID into table2
            //                   from qR in table2.DefaultIfEmpty()
            //                   join qRL in quotationRequestLines on qR.QuotationRequest_ID equals qRL.QuotationRequest_ID into table3
            //                   from qRL in table3.DefaultIfEmpty()
            //                   where qR.Client_ID == id
            //                   select new ClientQuotationClass
            //                   {
            //                       quotationList = q,
            //                       QuotationLineList = qL,
            //                       QuotationRequestList = qR,
            //                       QuotationRequestLineList = qRL
            //                   };
            //return View();


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

            //Quotation and QuotationLine
            var multipleJoin = from q in quotationList.AsEnumerable()
                               join qL in QuotationLineList.AsEnumerable() on q.Quotation_ID equals qL.Quotation_ID into table1
                               from qL in table1.DefaultIfEmpty()
                               
                               //QuotationLine and QuotionRequest
                               join qR in QuotationRequestList.AsEnumerable() on qL?.QuotationRequest_ID equals qR.QuotationRequest_ID into table2
                               from qR in table2.DefaultIfEmpty()
                               //QuotionRequest and QuotaionRequestLine
                               join qRL in quotationRequestLines.AsEnumerable() on qR?.QuotationRequest_ID equals qRL.QuotationRequest_ID into table3
                               from qRL in table3.DefaultIfEmpty()
                               
                               //QuotationLine and QuoutaionRequest
                               
                               where qR?.Client_ID == id
                               
                               select new ClientQuotationClass
                               {
                                   quotationList = q,
                                   QuotationLineList = qL,
                                   QuotationRequestList = qR,
                                   QuotationRequestLineList = qRL

                               };
            //return View();
            return View(multipleJoin);
        }

        // GET: QuotationRequestsList/Edit/5
        public async Task<ActionResult> Edit( int? ids,int? id, int? User_ID)
        {
            //var quote = db.Quotations.FirstOrDefault(b => b.Quotation_ID == ids.quotationList.Quotation_ID);
            
            /*FOR NAVIGATION*/
            // var quote = db.QuotationRequests.Where(b => b.Client_ID == id);

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
            //ClientQuotationClass quotation = db.Quotations.FirstOrDefault(x=>x.Quotation_ID=ids);

            var classVM = new ClientQuotationClass();
            {
                Quotation quotation = db.Quotations.SingleOrDefault(c => c.Quotation_ID == ids);

                if (quotation == null)
                {
                    return HttpNotFound();
                }

                //classVM.quotationList.Quotation_ID = (int)(quotation?.Quotation_ID);
                //classVM.quotationList.Amount = (int)quotation?.Amount;
                
            }
            // ViewBag.Client_ID = new SelectList(db.Clients, "Client_ID", "ClientName", quotation.Quotation_ID);



            /*For Booking*/
            List<DriverSlotsObj> startAndEndTimeObjs = new List<DriverSlotsObj>();

            var TimeSlots = db.DateOrTimeSlotOrDrivers.ToList();

            for (int i = 0; i < TimeSlots.Count(); i++)
            {
                var item = TimeSlots.ElementAt(i);
                if (item != null)
                {
                    startAndEndTimeObjs.Add(new DriverSlotsObj()
                    {
                        DateorTimeslotDiriver_ID = item.DateorTimeslotDiriver_ID,
                        Description = item.TimeSlot.Date + "-" + item.TimeSlot.StartTime + " - " + item.TimeSlot.EndTime
                    });
                }
            }

            ViewBag.TimeSlot_ID = new SelectList(startAndEndTimeObjs, "DateorTimeslotDiriver_ID", "Description");
            ViewBag.Service_ID = new SelectList(db.Services, "Service_ID", "ServiceName").ToList();
            ViewBag.Truck_ID = new SelectList(db.Trucks, "Truck_ID", "RegNum");
            ViewBag.Employee_ID = new SelectList(db.Employees, "Employee_ID", "EmployeeName");
            ViewBag.Client_ID = new SelectList(db.Clients.Where(x => x.Client_ID == id), "Client_ID", "ClientName").ToList();
            /*For Booking*/

            return View();
            //return View(quotation);
            // return View("~/Views/QuotationRequest/Create.cshtml");
        }

        // POST: QuotationRequestsList/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Booking_ID,Client_ID,DateMade,BookingDescription")] Booking booking, int? id, int? User_ID, BookingInstance bookingInstance1)
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

            /*create quote*/
            if (ModelState.IsValid)
            {
                //Booking booking = new Booking();
                //booking.Client_ID = (int)id;
                //booking.DateMade = booking.DateMade;
                db.Bookings.Add(booking);
                await db.SaveChangesAsync();

                /*Booking instance*/
                BookingInstance bookingInstance = new BookingInstance();
                bookingInstance.Booking_ID = booking.Booking_ID;
                bookingInstance.Truck_ID = bookingInstance1.Truck_ID;
                bookingInstance.BookingInstanceDate = booking.DateMade;
                bookingInstance.BookingInstanceDescription = bookingInstance1.BookingInstanceDescription;
                bookingInstance.BookingStatus_ID = 1;
                db.BookingInstances.Add(bookingInstance);
                await db.SaveChangesAsync();

               
                TempData["success"] = "Booking created successfully!!";
                return RedirectToAction("Index", new { id = ViewBag.ClientId, User_ID = ViewBag.UserId });
            }

           




            ViewBag.Client_ID = new SelectList(db.Clients, "Client_ID", "ClientName", booking.Client_ID);
            return View(booking);
        }

        public class DriverSlotsObj
        {
            public int? Employee_ID { get; set; }
            public int? DateorTimeslotDiriver_ID { get; set; }
            public string Employee { get; set; }
            public string Description { get; set; }
        }
    }
}