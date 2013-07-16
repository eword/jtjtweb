using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eulei.shop.Models;
using eulei.shop.Code;
using Eword.Linq;
namespace eulei.shop.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";
            return View();
        }
        public ActionResult Bulletin(int id)
        {
            try
            {
                Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
                var _return = _dct.TV_Article.Where(m => (m.ArticleState.Equals((int)ArticleState.Auditing)||m.ArticleState.Equals((int)ArticleState.IsShare)) && m.ArticleTypeID.Equals(id))
                    .OrderByDescending(m => m.ArticleSendDate).Take(10);
                ViewData.Model = _return;
                if (Request.IsAjaxRequest())
                    return PartialView();
                else
                    return View();
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“Home/Bulletin”");
                return RedirectToAction("Index", "Error", new { @area = "" });
            }
        }

        public ActionResult ArticleList(string id, int count, string type)
        {
            try
            {
                Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
               // string _sql = "(ArticleState=" + ((int)ArticleState.Auditing).ToString() + ")";
                string _sql = "(ArticleState=" + ((int)ArticleState.Auditing).ToString() + " or ArticleState=" + ((int)ArticleState.IsShare).ToString() + ")";
                _sql += " and (";
                string[] _ids = id.Split(',');
                for (int _i = 0; _i < _ids.Count(); _i++)
                {
                    if (_i == 0)
                    {
                        _sql += " (ArticleTypeID=" + _ids[_i] + ")";
                    }
                    else
                    {
                        _sql += " or (ArticleTypeID=" + _ids[_i] + ")";
                    }
                    FillArticleTypeID(int.Parse(_ids[_i]));
                }
                foreach (var item in _ArticleTypeIDs)
                {
                    _sql = _sql + " or (ArticleTypeID=" + item.ToString() + ")";
                }
                if (!string.IsNullOrEmpty(type))
                {
                    if (type.Equals("New"))
                    {
                        _sql = _sql + " or (ArticleIsNew = true)";
                    }
                }
                _sql += ")";
                ViewData.Model = _dct.TV_Article.Where(_sql, "")
                    .OrderByDescending(m => m.ArticleSendDate)
                    .Take(count);
                if (Request.IsAjaxRequest())
                    return PartialView();
                else
                    return View();
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“Home/ArticleList”");
                return RedirectToAction("Index", "Error", new { @area = "" });
            }
        }

        List<int> _ArticleTypeIDs = new List<int>();
        private void FillArticleTypeID(int id)
        {
            try
            {
                Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
                var _temp = _dct.TB_ArticleType.Where(m => m.ArticleTypeParentID.Equals(id));
                foreach (var item in _temp)
                {
                    _ArticleTypeIDs.Add(item.ArticleTypeID);
                    FillArticleTypeID(item.ArticleTypeID);
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“Home/ArticleList”");
            }

        }
        public ActionResult ShortArticleList(string id, int count)
        {
            try
            {
                Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
               // string _sql = "(ArticleState=" + ((int)ArticleState.Auditing).ToString() + ")";
                string _sql = "(ArticleState=" + ((int)ArticleState.Auditing).ToString() + " or ArticleState=" + ((int)ArticleState.IsShare).ToString() + ")";
                _sql += " and (";
                string[] _ids = id.Split(',');
                for (int _i = 0; _i < _ids.Count(); _i++)
                {
                    if (_i == 0)
                    {
                        _sql += " (ArticleTypeID=" + _ids[_i] + ")";
                    }
                    else
                    {
                        _sql += " or (ArticleTypeID=" + _ids[_i] + ")";
                    }
                    FillArticleTypeID(int.Parse(_ids[_i]));
                }
                foreach (var item in _ArticleTypeIDs)
                {
                    _sql = _sql + " or (ArticleTypeID=" + item.ToString() + ")";
                }
                _sql += ")";
                ViewData.Model = _dct.TV_Article.Where(_sql, "")
                    .OrderByDescending(m => m.ArticleSendDate)
                    .Take(count);
                if (Request.IsAjaxRequest())
                    return PartialView();
                else
                    return View();
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“Home/ShortArticleList”");
                return RedirectToAction("Index", "Error", new { @area = "" });
            }
        }
        public ActionResult ProductList(int id, int _count)
        {
            try
            {
                Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();


                switch (id)
                {
                    case (int)ProductFilter.Hot:
                        ViewData.Model = _dct.TV_Merchandise
                            .Where(m => m.MerchandiseState.Equals(id))
                            .OrderByDescending(m => m.MerchandiseClickCount).Take(_count);
                        break;
                    case (int)ProductFilter.New:
                        ViewData.Model = _dct.TV_Merchandise
        .Where(m => m.MerchandiseState.Equals((int)ProductState.Auditing))
        .OrderByDescending(m => m.MerchandiseBeginDate).Take(_count);
                        break;
                    case (int)ProductFilter.commendatory:
                        ViewData.Model = _dct.TV_Merchandise
        .Where(m => m.MerchandiseState.Equals((int)ProductState.Auditing) && m.MerchandiseIsemp.Equals(true))
        .OrderByDescending(m => m.MerchandiseClickCount).Take(_count);
                        break;
                    case (int)ProductFilter.NotSet:
                        ViewData.Model = _dct.TV_Merchandise
                            .Where(m => m.MerchandiseState.Equals((int)ProductState.Auditing)).Take(_count);
                        break;
                    default:
                        ViewData.Model = _dct.TV_Merchandise
         .Where(m => m.MerchandiseState.Equals((int)ProductState.Auditing)).Take(_count);
                        break;

                }

                if (Request.IsAjaxRequest())
                    return PartialView();
                else
                    return View();
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“Home/ProductList”");
                return RedirectToAction("Index", "Error", new { @area = "" });
            }
        }

        public ActionResult LinkList()
        {
            try
            {
                Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
                var _return = _dct.TB_Link.Where(m => m.LinkState.Equals((int)LinkState.Auditing) && m.LinkIsPicLink.Equals(true))
                    .OrderBy(m => m.LinkOrder).Take(6);
                ViewData.Model = _return;
                if (Request.IsAjaxRequest())
                    return PartialView();
                else
                    return View();
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“Home/LinkList”");
                return RedirectToAction("Index", "Error", new { @area = "" });
            }
        }
        public ActionResult WordLinkList()
        {
            try
            {
                Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
                var _return = _dct.TB_Link.Where(m => m.LinkState.Equals((int)LinkState.Auditing) && m.LinkIsPicLink.Equals(false))
                    .OrderBy(m => m.LinkOrder);
                ViewData.Model = _return;
                if (Request.IsAjaxRequest())
                    return PartialView();
                else
                    return View();
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“Home/WordLinkList”");
                return RedirectToAction("Index", "Error", new { @area = "" });
            }
        }
        public ActionResult Class()
        {
            return View();
        }

        public ActionResult Product()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult SiteMap()
        {
            return View();
        }
    }
}
