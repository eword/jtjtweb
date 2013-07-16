using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eulei.shop.Models;
using eulei.shop.Code;
namespace eulei.shop.Controllers
{
    public class MenuController : Controller
    {
        //
        // GET: /Menu/
        public ActionResult ManiMenu()
        {
            //Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
            //var _return = _dct.TB_Menu.Where(m => m.MenuType.Equals((int)MenuType.HomePage) || m.MenuType.Equals((int)MenuType.HomeOnlyPage));
            return View();
        }

    }
}
