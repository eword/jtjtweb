using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eulei.shop.Models;
namespace eulei.shop.Areas.Product.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Product/Home/

        public ActionResult Index()
        {
            ViewBag.NavID = 1;
            return View();
        }
        public ActionResult ProductList(int id)
        {
            Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();

            switch (id)
            {
                case 1://热销排行

                    ViewData.Model = _dct.TB_Merchandise

                                  .OrderBy(m => m.MerchandiseClickCount)
                                  .OrderBy(m => m.MerchandiseIsNew)
                                  .OrderBy(m => m.MerchandiseIsemp)
                                  .Take(4);
                    break;
                case 2://新品上市

                    ViewData.Model = _dct.TB_Merchandise

                                  .OrderBy(m => m.MerchandiseIsNew)
                                  .OrderBy(m => m.MerchandiseIsemp)
                                  .OrderBy(m => m.MerchandiseClickCount)
                                  .Take(4);
                    break;
                case 3://站点推荐

                    ViewData.Model = _dct.TB_Merchandise

                                  .OrderBy(m => m.MerchandiseIsemp)
                                  .OrderBy(m => m.MerchandiseIsNew)
                                  .OrderBy(m => m.MerchandiseClickCount)
                                  .Take(4);
                    break;
                default:
                    ViewData.Model = _dct.TB_Merchandise

                                  .OrderBy(m => m.MerchandiseIsNew)
                                  .OrderBy(m => m.MerchandiseIsemp)
                                  .OrderBy(m => m.MerchandiseClickCount)
                                  .Take(4);
                    break;
            }

            if (Request.IsAjaxRequest())
                return PartialView();
            else
                return View();
        }
        //
        // GET: /Product/Home/Details/5

        public ActionResult Details(int id)
        {
            
            Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
            var _retunt = _dct.TV_Merchandise.Single(m => m.MerchandiseID== id);
            _retunt.MerchandiseClickCount += 1;
            _dct.SubmitChanges();
            ViewBag.NavID = _retunt.MerchandiseType;
            ViewData.Model = _retunt;
            return View();
        }
        public ActionResult OnlyPage(int id)
        {
            return RedirectToAction("Details", new { @id=id});
        }
       
    }
}
