using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nazox.Controllers.Ecommerce
{
    public class EcommerceOrdersController : Controller
    {
        // GET: EcommerceOrders
        public ActionResult Index()
        {
            return View();
        }
    }
}