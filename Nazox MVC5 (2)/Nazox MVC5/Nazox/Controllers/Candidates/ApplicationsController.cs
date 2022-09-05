using Nazox.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Nazox.Controllers.Candidates
{
    public class ApplicationsController : Controller
    {
        private EasyMovingSystemEntities db = new EasyMovingSystemEntities();
        // GET: Applications
        public ActionResult Index()
        {
            return View();
        }

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
                documentFile.DocumentName = documentFile.DocumentName;
                documentFile.FileData = documentFile.FileData;
                documentFile.DocumentType_ID = 1;
                documentFile.ContentType = documentFile.ContentType;
                db.DocumentFiles.Add(documentFile);
                await db.SaveChangesAsync();

                Application application = new Application();
                application.Candidate_ID = candidate1.Candidate_ID;
                application.Document_ID = documentFile.Document_ID;
                application.ApplicationDate = application.ApplicationDate;
                application.ApplicationStatus_ID = 1;
                _ = application.Job_ID;

                db.Applications.Add(application);
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return View();

        }
    }
}