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
    public class ContentNavController : Controller
    {

        public ActionResult Index(int id)
        {
            Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
            var _currentNode = _dct.TB_ProductType.Single(m => m.ProductTypeID.Equals(id));
            var _childNodes = _dct.TB_ProductType.Where(m => m.ProductTypeParentID.Equals(_currentNode.ProductTypeID));
            var _brotherNodes = _dct.TB_ProductType.Where(m => m.ProductTypeParentID.Equals(_currentNode.ProductTypeParentID));
            ViewBag.ChindNodesHasValue = false;
            ViewBag.BrotherNodesHasValue =false;
            if (_childNodes != null && _childNodes.Count() > 0)
            {
                ViewBag.ChindNodesHasValue = true;
                ViewBag.ChindNodesTitle = _currentNode.ProductTypeName;
                ViewBag.ChindNodesValue = _childNodes.ToList<TB_ProductType>();
            }
            if (_brotherNodes != null && _brotherNodes.Count() > 0)
            {
                ViewBag.BrotherNodesHasValue = true;                
                ViewBag.BrotherNodesValue = _brotherNodes.ToList<TB_ProductType>();
            }
            if (Request.IsAjaxRequest())
                return PartialView();
            else
                return View();
        }
        public ActionResult Details(int id)
        {
            Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
            var _currentNode = _dct.TB_ProductType.Single(m => m.ProductTypeID.Equals(id));
            var _childNodes = _dct.TB_ProductType.Where(m => m.ProductTypeParentID.Equals(_currentNode.ProductTypeID));
            var _brotherNodes = _dct.TB_ProductType.Where(m => m.ProductTypeParentID.Equals(_currentNode.ProductTypeParentID));
            ViewBag.ChindNodesHasValue = false;
            ViewBag.BrotherNodesHasValue = false;
            if (_childNodes != null && _childNodes.Count() > 0)
            {
                ViewBag.ChindNodesHasValue = true;
                ViewBag.ChindNodesTitle = _currentNode.ProductTypeName;
                ViewBag.ChindNodesValue = _childNodes.ToList<TB_ProductType>();
            }
            if (_brotherNodes != null && _brotherNodes.Count() > 0)
            {
                ViewBag.BrotherNodesHasValue = true;
                ViewBag.BrotherNodesValue = _brotherNodes.ToList<TB_ProductType>();
            }
            if (Request.IsAjaxRequest())
                return PartialView();
            else
                return View();
        }

    }
}
