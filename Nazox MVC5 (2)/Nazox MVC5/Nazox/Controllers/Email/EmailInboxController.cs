using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nazox.Controllers.Email
{
    public class EmailInboxController : Controller
    {
        // GET: EmailInbox
        public ActionResult Index()
        {
            return View();
        }
    }
}