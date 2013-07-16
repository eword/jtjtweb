using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Q42.Wheels.Mvc.Paging;
using eulei.shop.Models;
using eulei.shop.Code;
namespace eulei.shop.Areas.Product.Controllers
{
    public class ClassController : Controller
    {
        [Paging(PageSize = 12)]
        public ActionResult Index(int id)
        {
        
            ViewBag.NavID = id;
            Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
            var _result = _dct.TV_Merchandise.Where(m => m.MerchandiseType.Equals(id)).OrderByDescending(m => m.MerchandiseID);
            ViewBag.Title = _dct.TB_ProductType.Single(m=>m.ProductTypeID.Equals(id)).ProductTypeName;
            ViewData.Model = _result;
            if (Request.IsAjaxRequest())
                return PartialView();
            else
                return View();
        }

    }
}
