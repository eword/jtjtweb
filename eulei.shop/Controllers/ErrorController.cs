using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eulei.shop.Controllers
{
    public class ErrorController : Controller
    {
        //
        // GET: /Error/
        [ValidateInput(false)]
        public ActionResult Index(string MyContent)
        {
            ViewBag.MyContent = MyContent;
            return View("Error");
        }

    }
}
