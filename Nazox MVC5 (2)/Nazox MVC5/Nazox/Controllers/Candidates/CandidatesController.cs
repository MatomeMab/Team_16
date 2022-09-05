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
using System.IO;

namespace Nazox.Controllers.Candidates
{
    public class CandidatesController : Controller
    {
        private EasyMovingSystemEntities db = new EasyMovingSystemEntities();
        private static int? RoleTypeId = null;
        private static int? User_ID = null;
        // GET: Candidates
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
            return View(await db.Candidates.ToListAsync());
        }

        // GET: Candidates/Details/5
        public async Task<ActionResult> Details(int? ids,int?id)
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
            Candidate candidate = await db.Candidates.FindAsync(ids);
            if (candidate == null)
            {
                return HttpNotFound();
            }
            return View(candidate);
        }

        // GET: Candidates/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Candidates/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Candidate_ID,CandidateSurname,CandidateName,CandidateEmailAddress,CandidatePhonNum")] Candidate candidate)
        {

            if (ModelState.IsValid)
            {
                Candidate candidate1 = new Candidate();
                candidate1.CandidateName = candidate.CandidateName;
                candidate1.CandidateSurname = candidate.CandidateSurname;
                candidate1.CandidateEmailAddress = candidate.CandidateEmailAddress;
                db.Candidates.Add(candidate1);
                //db.Candidates.Add(candidate);
                await db.SaveChangesAsync();

                DocumentFile documentFile = new DocumentFile();
               
                db.DocumentFiles.Add(documentFile);
                await db.SaveChangesAsync();

                Application application = new Application();
                application.Candidate_ID = candidate1.Candidate_ID;
                application.Document_ID = documentFile.Document_ID;
                application.ApplicationDate = application.ApplicationDate;
                application.ApplicationStatus_ID = 1;
                _=application.Job_ID;

                db.Applications.Add(application);
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(candidate);
        }

        // GET: Candidates/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Candidate candidate = await db.Candidates.FindAsync(id);
            if (candidate == null)
            {
                return HttpNotFound();
            }
            return View(candidate);
        }

        // POST: Candidates/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Candidate_ID,CandidateSurname,CandidateName,CandidateEmailAddress,CandidatePhonNum")] Candidate candidate)
        {
            if (ModelState.IsValid)
            {
                db.Entry(candidate).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(candidate);
        }

        // GET: Candidates/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Candidate candidate = await db.Candidates.FindAsync(id);
            if (candidate == null)
            {
                return HttpNotFound();
            }
            return View(candidate);
        }

        // POST: Candidates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Candidate candidate = await db.Candidates.FindAsync(id);
            db.Candidates.Remove(candidate);
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


        [HttpPost]
        public ActionResult Index(HttpPostedFileBase postedFile)
        {
            byte[] bytes;
            using (BinaryReader br = new BinaryReader(postedFile.InputStream))
            {
                bytes = br.ReadBytes(postedFile.ContentLength);
            }



            db.DocumentFiles.Add(new DocumentFile
            {
                DocumentName = Path.GetFileName(postedFile.FileName),
                ContentType = postedFile.ContentType,
                FileData = bytes,
               
            });
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public FileResult DownloadFile(int? fileId)
        {

            DocumentFile file = db.DocumentFiles.ToList().Find(p => p.Document_ID == fileId.Value);
            return File(file.FileData, file.ContentType, file.DocumentName);
        }
    }
}
