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

namespace Nazox.Controllers.Trucks
{
    public class TrucksController : Controller
    {
        private EasyMovingSystemEntities db = new EasyMovingSystemEntities();
        private static int? RoleTypeId = null;
        private static int? User_ID = null;
        // GET: Trucks
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


            var trucks = db.Trucks.Include(t => t.TruckStatu).Include(t => t.TruckType);
            return View(await trucks.ToListAsync());
        }

        // GET: Trucks/Details/5
        public async Task<ActionResult> Details(int? ids,int?id)
        {
            if (ids == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Truck truck = await db.Trucks.FindAsync(ids);
            if (truck == null)
            {
                return HttpNotFound();
            }
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



            return View(truck);
        }

        // GET: Trucks/Create
        public async Task<ActionResult> Create(int?id)
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

            ViewBag.TruckStatus_ID = new SelectList(db.TruckStatus, "TruckStatus_ID", "TruckStatusName");
            ViewBag.TruckType_ID = new SelectList(db.TruckTypes, "TruckType_ID", "TruckTypeName");
            return View();
        }

        // POST: Trucks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Truck_ID,TruckType_ID,TruckStatus_ID,Model,Year,Colour,RegNum,Make")] Truck truck,int id)
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
                db.Trucks.Add(truck);
                await db.SaveChangesAsync();
                return RedirectToAction("Index", new { id = User_ID });
            }

            ViewBag.TruckStatus_ID = new SelectList(db.TruckStatus, "TruckStatus_ID", "TruckStatusName", truck.TruckStatus_ID);
            ViewBag.TruckType_ID = new SelectList(db.TruckTypes, "TruckType_ID", "TruckTypeName", truck.TruckType_ID);
            return View(truck);
        }

        // GET: Trucks/Edit/5
        public async Task<ActionResult> Edit(int? ids,int?id)
        {
            if (ids == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Truck truck = await db.Trucks.FindAsync(ids);
            if (truck == null)
            {
                return HttpNotFound();
            }

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


          
            ViewBag.TruckStatus_ID = new SelectList(db.TruckStatus, "TruckStatus_ID", "TruckStatusName", truck.TruckStatus_ID);
            ViewBag.TruckType_ID = new SelectList(db.TruckTypes, "TruckType_ID", "TruckTypeName", truck.TruckType_ID);
           
            return View(truck);
        }

        // POST: Trucks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Truck_ID,TruckType_ID,TruckStatus_ID,Model,Year,Colour,RegNum,Make")] Truck truck,int?id)
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
                db.Entry(truck).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index", new { id = ViewBag.UserId });
            }
            ViewBag.TruckStatus_ID = new SelectList(db.TruckStatus, "TruckStatus_ID", "TruckStatusName", truck.TruckStatus_ID);
            ViewBag.TruckType_ID = new SelectList(db.TruckTypes, "TruckType_ID", "TruckTypeName", truck.TruckType_ID);
            return View(truck);
        }

        // GET: Trucks/Delete/5
        public async Task<ActionResult> Delete(int? ids,int?id)
        {
            if (ids == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Truck truck = await db.Trucks.FindAsync(ids);
            if (truck == null)
            {
                return HttpNotFound();
            }

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


            ViewBag.TruckStatus_ID = new SelectList(db.TruckStatus, "TruckStatus_ID", "TruckStatusName", truck.TruckStatus_ID);
            ViewBag.TruckType_ID = new SelectList(db.TruckTypes, "TruckType_ID", "TruckTypeName", truck.TruckType_ID);

            return View(truck);
        }

        // POST: Trucks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int ids,int?id)
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


            Truck truck = await db.Trucks.FindAsync(ids);
            db.Trucks.Remove(truck);
            await db.SaveChangesAsync();
            return RedirectToAction("Index", new { id = ViewBag.UserId });
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
