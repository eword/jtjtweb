using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Q42.Wheels.Mvc.Paging;
using eulei.shop.Models;
using eulei.shop.Code;

namespace eulei.shop.Areas.Article.Controllers
{
    public class ContentNavController : Controller
    {

        public ActionResult Index(string id)
        {
            try
            {
                int _id = int.Parse(id.Split(',').First());
                Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
                var _currentNode = _dct.TB_ArticleType.Single(m => m.ArticleTypeID.Equals(_id));
                var _childNodes = _dct.TB_ArticleType.Where(m => m.ArticleTypeParentID.Equals(_currentNode.ArticleTypeID));
                var _brotherNodes = _dct.TB_ArticleType.Where(m => m.ArticleTypeParentID.Equals(_currentNode.ArticleTypeParentID));
                ViewBag.ChindNodesHasValue = false;
                ViewBag.BrotherNodesHasValue = false;
                if (_childNodes != null && _childNodes.Count() > 0)
                {
                    ViewBag.ChindNodesHasValue = true;
                    ViewBag.ChindNodesTitle = _currentNode.ArticleTypeName;
                    ViewBag.ChindNodesValue = _childNodes.ToList<TB_ArticleType>();
                }
                if (_brotherNodes != null && _brotherNodes.Count() > 0)
                {
                    ViewBag.BrotherNodesHasValue = true;
                    ViewBag.BrotherNodesValue = _brotherNodes.ToList<TB_ArticleType>();
                }
                if (Request.IsAjaxRequest())
                    return PartialView();
                else
                    return View();
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“Article/ContentNav/Index”");
                return RedirectToAction("Index", "Error", new { @area = "" });
            }
        }
        public ActionResult Details(string id)
        {
            try
            {
                int _id = int.Parse(id.Split(',').First());
                Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
                var _currentNode = _dct.TB_ArticleType.Single(m => m.ArticleTypeID.Equals(_id));
                var _childNodes = _dct.TB_ArticleType.Where(m => m.ArticleTypeParentID.Equals(_currentNode.ArticleTypeID));
                var _brotherNodes = _dct.TB_ArticleType.Where(m => m.ArticleTypeParentID.Equals(_currentNode.ArticleTypeParentID));
                ViewBag.ChindNodesHasValue = false;
                ViewBag.BrotherNodesHasValue = false;
                if (_childNodes != null && _childNodes.Count() > 0)
                {
                    ViewBag.ChindNodesHasValue = true;
                    ViewBag.ChindNodesTitle = _currentNode.ArticleTypeName;
                    ViewBag.ChindNodesValue = _childNodes.ToList<TB_ArticleType>();
                }
                if (_brotherNodes != null && _brotherNodes.Count() > 0)
                {
                    ViewBag.BrotherNodesHasValue = true;
                    ViewBag.BrotherNodesValue = _brotherNodes.ToList<TB_ArticleType>();
                }
                if (Request.IsAjaxRequest())
                    return PartialView();
                else
                    return View();
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“Article/ContentNav/Details”");
                return RedirectToAction("Index", "Error", new { @area = "" });
            }
        }

    }
}
