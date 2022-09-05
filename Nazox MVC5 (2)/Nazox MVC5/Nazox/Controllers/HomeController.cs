using Nazox.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nazox.Controllers
{
    public class HomeController : Controller
    {
        private EasyMovingSystemEntities db = new EasyMovingSystemEntities();
        // GET: Home
        public ActionResult Index()
        {
            //ViewBag.jobs = db.JobListings.ToList();

            return View();
        }
    }
}