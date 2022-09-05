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

namespace Nazox.Controllers.Inspections
{
    public class InspectionsController : Controller
    {
        private EasyMovingSystemEntities db = new EasyMovingSystemEntities();

        // GET: Inspections
        public async Task<ActionResult> Index(int? id, int? User_ID)
        {

            var quote = db.Inspections.Where(b => b.Client_ID == id);


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

            var inspections = db.Inspections.Include(i => i.Client).Include(i => i.InspectionType).Include(i => i.Truck);
            return View( inspections.Where(x => x.Client_ID == id).ToList());
        }




        // GET: Inspections/Details/5
        public async Task<ActionResult> Details(int? ids, int? id, int? User_ID)
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
            Inspection inspection = await db.Inspections.FindAsync(ids);
            if (inspection == null)
            {
                return HttpNotFound();
            }
            return View(inspection);
        }

        // GET: Inspections/Create
        public ActionResult Create(int? id, int? User_ID)
        {
            var inspections = db.Inspections.Where(b => b.Client_ID == id);

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
            ViewBag.InspectionType_ID = new SelectList(db.InspectionTypes, "InspectionType_ID", "TypeName");
            ViewBag.Truck_ID = new SelectList(db.Trucks, "Truck_ID", "RegNum");
            return View();
        }

        // POST: Inspections/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Inspection_ID,InspectionNotetextarea,InspectionTime,InspectionType_ID,Truck_ID,Client_ID")] Inspection inspection, int? id, int? User_ID)
        {
            /*FOR NAVIGATION*/
            var quote = db.Inspections.Where(b => b.Client_ID == id);

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
                db.Inspections.Add(inspection);
                TempData["success"] = "Successfully created an inspection";
                await db.SaveChangesAsync();
                return RedirectToAction("Index", new { id = ViewBag.ClientId, User_ID = ViewBag.UserId });
            }

            ViewBag.Client_ID = new SelectList(db.Clients, "Client_ID", "ClientName", inspection.Client_ID);
            ViewBag.InspectionType_ID = new SelectList(db.InspectionTypes, "InspectionType_ID", "TypeName", inspection.InspectionType_ID);
            ViewBag.Truck_ID = new SelectList(db.Trucks, "Truck_ID", "Model", inspection.Truck_ID);
            return View(inspection);
        }

        // GET: Inspections/Edit/5
        public async Task<ActionResult> Edit(int? ids,int?id,int?User_ID)
        {


            /*FOR NAVIGATION*/
            var quote = db.Inspections.Where(b => b.Client_ID == id);

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
            Inspection inspection = await db.Inspections.FindAsync(ids);
            if (inspection == null)
            {
                return HttpNotFound();
            }
            ViewBag.Client_ID = new SelectList(db.Clients, "Client_ID", "ClientName", inspection.Client_ID);
            ViewBag.InspectionType_ID = new SelectList(db.InspectionTypes, "InspectionType_ID", "TypeName", inspection.InspectionType_ID);
            ViewBag.Truck_ID = new SelectList(db.Trucks, "Truck_ID", "Model", inspection.Truck_ID);
            return View(inspection);
        }

        // POST: Inspections/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Inspection_ID,InspectionNotetextarea,InspectionTime,InspectionType_ID,Truck_ID,Client_ID")] Inspection inspection,int?id,int?User_ID)
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
                db.Entry(inspection).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Client_ID = new SelectList(db.Clients, "Client_ID", "ClientName", inspection.Client_ID);
            ViewBag.InspectionType_ID = new SelectList(db.InspectionTypes, "InspectionType_ID", "TypeName", inspection.InspectionType_ID);
            ViewBag.Truck_ID = new SelectList(db.Trucks, "Truck_ID", "Model", inspection.Truck_ID);
            return View(inspection);
        }

        // GET: Inspections/Delete/5
        public async Task<ActionResult> Delete(int? ids,int?id,int?User_ID)
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
            Inspection inspection = await db.Inspections.FindAsync(ids);
            if (inspection == null)
            {
                return HttpNotFound();
            }
            return View(inspection);
        }

        // POST: Inspections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int ids,int?id,int?User_ID)
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

            Inspection inspection = await db.Inspections.FindAsync(ids);
            db.Inspections.Remove(inspection);
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
