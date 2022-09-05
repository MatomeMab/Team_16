using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Microsoft.Reporting.WebForms;
using Nazox.Models;
using Nazox.Models.QuotationViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

using System.Drawing;
using Microsoft.ReportingServices.Interfaces;
using DocumentFormat.OpenXml.Wordprocessing;
using NPOI.SS.Formula.Functions;
using AjaxControlToolkit;

namespace Nazox.Controllers.Reports
{

    public class ReportsController : Controller
    {
        private EasyMovingSystemEntities db = new EasyMovingSystemEntities();
        private static int? RoleTypeId = null;
        private static int? User_ID = null;

        // GET: Reports
        public async Task<ActionResult> TblFeedback(int? id)
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

            var tblFeedbacks = db.TblFeedbacks.Include(t => t.Client);
            return View(await tblFeedbacks.ToListAsync());
        }

        public async Task<ActionResult> ComplaintFeedback(int? id)
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

            var tblFeedbacks = db.TblFeedbacks.Include(t => t.Client).Where(t=>t.Feedback_ID==1);
            return View(await tblFeedbacks.ToListAsync());
            //return View();
        }

        public async Task<ActionResult> Outstanding(int? id)
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
            return View();
        }

        public async Task<ActionResult> Employee(int?id)
        {
            /*NAVIGATION*/
            User tblUser = await db.Users.FindAsync(id);
            if (tblUser == null)
            {

                return HttpNotFound();
            }
            if (tblUser.SessionID == null)
            {
                return RedirectToAction("Index", "AuthLogin");
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

            //for data tables
            db.Configuration.LazyLoadingEnabled = false; // if your table is relational, contain foreign key
            //
            var employees = db.Employees.Include(e => e.EmployeeStatu).Include(e => e.Title).Include(e => e.EmployeeType);

            return View(await employees.ToListAsync());
        }


        public ActionResult BookingIndex()
        {
            ViewBag.bookings = db.Bookings.ToList();

            List<Client> clients = db.Clients.ToList();
            List<Booking> bookings = db.Bookings.ToList();
            ViewBag.booking = from b in bookings
                              join c in clients on b.Client_ID equals
                              c.Client_ID into table1
                              from c in table1.ToList()
                              select new
                              {
                                  bookings = b,
                                  clients = c

                              };



            return View();

        }

        public ActionResult employeeIndex()
        {
            ViewBag.employees = db.Employees.ToList();

            return View();
        }
        public ActionResult exportEmployee()
        {
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Models/Reports/EmployeeReport.rpt")));
            rd.SetDataSource(db.Employees.Select(p => new
            {
                p.EmployeeName,
                p.EmployeeSurname,
                p.EmployeeStatu.EmployeeStatusName,
                p.EmployeeType.EmployeeTypeName,
                p.DateEmployed

            }).ToList());
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            try
            {
                Stream stream = rd.ExportToStream
                    (ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/pdf", "employee.pdf");


            }

            catch (Exception ex)
            {
                throw;
            }

        }
        public ActionResult exportComplaintsFeedback()
        {
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Models/Reports/CompaintFeedback.rpt")));
            rd.SetDataSource(db.TblFeedbacks.Select(p => new
            {
             
                p.Service.ServiceName,
                p.Client.ClientName,
                p.Client.PhoneNum,
                p.FeedbackDescription
                

            }).ToList());
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            try
            {
                Stream stream = rd.ExportToStream
                    (ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/pdf", "complaints.pdf");


            }

            catch (Exception ex)
            {
                throw;
            }

        }
        public ActionResult exportFeedback()
        {
            var sum = db.TblFeedbacks.Select(c => c.Rating).Sum();
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Models/Reports/ServiceRatings.rpt")));
            rd.SetDataSource(db.TblFeedbacks.Select(p => new
            {
                p.Service.ServiceName,
                sum
            }).ToList());
            //var sum = db.TblFeedbacks.Select(c => c.Rating).Sum();
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            try
            {
                Stream stream = rd.ExportToStream
                    (ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/pdf", "feedback.pdf");


            }

            catch (Exception ex)
            {
                throw;
            }

        }
        public ActionResult exportClients()
        {
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Models/Reports/ClientReport.rpt")));
            rd.SetDataSource(db.Clients.Select(p => new
            {

                p.ClientName,
                p.ClientSurname,
                p.User.Active
               


            }).ToList());
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            try
            {
                Stream stream = rd.ExportToStream
                    (ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/pdf", "client.pdf");


            }

            catch (Exception ex)
            {
                throw;
            }

        }

        /* public FileResult DownloadPDFReport()
         {
              string ReportPDFFilename = "employee" + "-" + DateTime.Now +".pdf";
              List<Employee> lstData = new List<Employee>();
             employeeVM clientQuotationClass = new employeeVM();

             lstData = clientQuotationClass.employee();
              LocalReport localReport = new LocalReport();
              localReport.DataSources.Clear();
              localReport.DataSources.Add(new ReportDataSource("dsData", lstData));
              localReport.ReportPath = Server.MapPath("~/Models/Reports/Report1.rdlc");

              byte[] bytes = localReport.Render("PDF");
              return File(bytes, "application/pdf", ReportPDFFilename);


         }
         */



        public async Task<ActionResult> clientIndex(int? id)
        {
            /*NAVIGATION*/
            User tblUser = await db.Users.FindAsync(id);
            if (tblUser == null)
            {

                return HttpNotFound();
            }
            if (tblUser.SessionID == null)
            {
                return RedirectToAction("Index", "AuthLogin");
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

            var clients = db.Clients.Include(t => t.User);

            return View(await clients.ToListAsync());
        }
        [HttpGet]
        public ActionResult Advanced()
        {
            ReportsVM vm = new ReportsVM();
            //retrieve list of employees,client,feedback
            vm.Employees = GetEmployees(0);
            //set default date
            vm.DateFrom = new DateTime(2020, 08, 1);
            vm.DateTo = new DateTime(2020, 08, 31);
            return View(vm);
        }
        private SelectList GetEmployees(int selected)
        {
            using (db)
            {
                db.Configuration.AutoDetectChangesEnabled = false;
                //create selectedlistItem 
                var employees = db.Employees.Select(x => new SelectListItem
                {
                    Value = x.Employee_ID.ToString(),
                    Text = x.EmployeeName

                }).ToList();
                //if selected parameter has a value,config the selected item
                if (selected == 0)
                    return new SelectList(employees, "Value", "Text");
                else
                    return new SelectList(employees, "Value", "Text", selected);
            }
        }

        [HttpPost]
        public ActionResult Advanced(ReportsVM vm)
        {
            using (db)
            {
                db.Configuration.ProxyCreationEnabled = false;
                //retrieve list of employee to populate the dropdown
                //the ID of currently selected item
                vm.Employees = GetEmployees(vm.SelectedEmployeeID);

                //get full details of selected vendor to be displayed on the view
                vm.employee = db.Employees.Where(x => x.Employee_ID == vm.SelectedEmployeeID).FirstOrDefault();

                //get all employee to adheres to the entered criteria
                //for each result load data into a new report
                var list = db.Employees.Include("DateOrTimeSlotOrDriver").Where(e => e.Employee_ID == vm.employee.Employee_ID);
                return View(vm);

            }
        }
    }
}