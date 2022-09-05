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

namespace Nazox.Controllers.Document
{
    public class DocumentFilesController : Controller
    {
        private EasyMovingSystemEntities db = new EasyMovingSystemEntities();

        // GET: DocumentFiles
        public async Task<ActionResult> Index()
        {
            var documentFiles = db.DocumentFiles.Include(d => d.DocumentType);
            return View(await documentFiles.ToListAsync());
        }

        // GET: DocumentFiles/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DocumentFile documentFile = await db.DocumentFiles.FindAsync(id);
            if (documentFile == null)
            {
                return HttpNotFound();
            }
            return View(documentFile);
        }

        // GET: DocumentFiles/Create
        public ActionResult Create()
        {
            ViewBag.DocumentType_ID = new SelectList(db.DocumentTypes, "DocumentType_ID", "DocumentTypeDescription");
            return View();
        }

        // POST: DocumentFiles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Document_ID,DocumentName,DocumentType_ID,FileData,ContentType")] DocumentFile documentFile)
        {
            if (ModelState.IsValid)
            {
                db.DocumentFiles.Add(documentFile);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.DocumentType_ID = new SelectList(db.DocumentTypes, "DocumentType_ID", "DocumentTypeDescription", documentFile.DocumentType_ID);
            return View(documentFile);
        }

        // GET: DocumentFiles/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DocumentFile documentFile = await db.DocumentFiles.FindAsync(id);
            if (documentFile == null)
            {
                return HttpNotFound();
            }
            ViewBag.DocumentType_ID = new SelectList(db.DocumentTypes, "DocumentType_ID", "DocumentTypeDescription", documentFile.DocumentType_ID);
            return View(documentFile);
        }

        // POST: DocumentFiles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Document_ID,DocumentName,DocumentType_ID,FileData,ContentType")] DocumentFile documentFile)
        {
            if (ModelState.IsValid)
            {
                db.Entry(documentFile).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.DocumentType_ID = new SelectList(db.DocumentTypes, "DocumentType_ID", "DocumentTypeDescription", documentFile.DocumentType_ID);
            return View(documentFile);
        }

        // GET: DocumentFiles/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DocumentFile documentFile = await db.DocumentFiles.FindAsync(id);
            if (documentFile == null)
            {
                return HttpNotFound();
            }
            return View(documentFile);
        }

        // POST: DocumentFiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            DocumentFile documentFile = await db.DocumentFiles.FindAsync(id);
            db.DocumentFiles.Remove(documentFile);
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
                FileData = bytes
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
