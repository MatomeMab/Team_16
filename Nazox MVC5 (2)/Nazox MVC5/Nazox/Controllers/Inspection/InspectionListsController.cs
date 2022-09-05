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
    public class InspectionListsController : Controller
    {
        private EasyMovingSystemEntities db = new EasyMovingSystemEntities();
        private static int? RoleTypeId = null;
        private static int? User_ID = null;
        // GET: InspectionLists
        public async Task<ActionResult> Index(int? id)
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

            var inspections = db.Inspections.Include(i => i.Client).Include(i => i.InspectionType).Include(i => i.Truck);
            return View(await inspections.ToListAsync());
        }

        // GET: InspectionLists/Details/5
        public async Task<ActionResult> Details(int? ids,int?id)
        {
            /*Nav*/
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
            /*Nav*/
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

        // GET: InspectionLists/Create
        public ActionResult Create()
        {
            ViewBag.Client_ID = new SelectList(db.Clients, "Client_ID", "ClientName");
            ViewBag.InspectionType_ID = new SelectList(db.InspectionTypes, "InspectionType_ID", "TypeName");
            ViewBag.Truck_ID = new SelectList(db.Trucks, "Truck_ID", "Model");
            return View();
        }

        // POST: InspectionLists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Inspection_ID,InspectionNotetextarea,InspectionTime,InspectionType_ID,Truck_ID,Client_ID")] Inspection inspection)
        {
            if (ModelState.IsValid)
            {
                db.Inspections.Add(inspection);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Client_ID = new SelectList(db.Clients, "Client_ID", "ClientName", inspection.Client_ID);
            ViewBag.InspectionType_ID = new SelectList(db.InspectionTypes, "InspectionType_ID", "TypeName", inspection.InspectionType_ID);
            ViewBag.Truck_ID = new SelectList(db.Trucks, "Truck_ID", "Model", inspection.Truck_ID);
            return View(inspection);
        }

        // GET: InspectionLists/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inspection inspection = await db.Inspections.FindAsync(id);
            if (inspection == null)
            {
                return HttpNotFound();
            }
            ViewBag.Client_ID = new SelectList(db.Clients, "Client_ID", "ClientName", inspection.Client_ID);
            ViewBag.InspectionType_ID = new SelectList(db.InspectionTypes, "InspectionType_ID", "TypeName", inspection.InspectionType_ID);
            ViewBag.Truck_ID = new SelectList(db.Trucks, "Truck_ID", "Model", inspection.Truck_ID);
            return View(inspection);
        }

        // POST: InspectionLists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Inspection_ID,InspectionNotetextarea,InspectionTime,InspectionType_ID,Truck_ID,Client_ID")] Inspection inspection)
        {
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

        // GET: InspectionLists/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inspection inspection = await db.Inspections.FindAsync(id);
            if (inspection == null)
            {
                return HttpNotFound();
            }
            return View(inspection);
        }

        // POST: InspectionLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Inspection inspection = await db.Inspections.FindAsync(id);
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
