using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Q42.Wheels.Mvc.Paging;
using eulei.shop.Models;
using eulei.shop.Code;
using Eword.Linq;
namespace eulei.shop.Areas.Article.Controllers
{
    public class HomeController : Controller
    {
        string _splitCode = @"<div\s{1,}style=""\s{0,}page-break-after\s{0,}:\s{0,}always;{0,1}\s{0,}"">\s{0,}<span\s{1,}style=""display\s{0,}:\s{0,}none;{0,1}\s{0,}"">\s{0,}&nbsp;\s{0,}</span>\s{0,}</div>";
        [Paging(PageSize = 7)]
        public ActionResult Index(string id, int? aid)
        {
            try
            {
                ViewBag.NavID = id;
                Linq_DefaultDataContext _dc = new Linq_DefaultDataContext();
                if (aid.HasValue)
                {

                    if (Request.QueryString["PageNum"] == null)
                    {
                        _dc.TB_Article.Single(m => m.ArticleID.Equals(aid.Value)).ArticleClickCount += 1;
                        _dc.SubmitChanges();
                    }
                    var _query = _dc.TV_Article.Single(m => m.ArticleID == aid.Value && (m.ArticleState.Equals((int)ArticleState.Auditing) || m.ArticleState.Equals((int)ArticleState.IsShare)));
                    string[] _pagecontens = System.Text.RegularExpressions.Regex.Split(_query.ArticleContent, _splitCode);

                    int _pageNum = Request.QueryString["PageNum"] == null ? 0 :
                        (
                        int.Parse(Request.QueryString["PageNum"].ToString()) < _pagecontens.Count() ? int.Parse(Request.QueryString["PageNum"].ToString()) : _pagecontens.Count() - 1
                        );
                    ViewBag.Url = id.ToString() + "?aid=" + aid.Value.ToString();
                    ViewBag.CurrentPage = _pageNum;
                    ViewBag.FirstPage = 0;
                    ViewBag.LastPage = _pagecontens.Count() - 1;
                    _query.ArticleContent = _pagecontens[_pageNum];
                    ViewData.Model = _query;
                    return View("Details");
                }
                else
                {
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
                    ViewData.Model = _dc.TV_Article.Where(_sql, "")
                        .OrderByDescending(m => m.ArticleSendDate)
                        .OrderByDescending(m => m.ArticleID);
                    return View();
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“Article/Home/Index”");
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
                LogHelper.WriteErrorLog(ex.Message + "@" + "“Article/Home/FillArticleTypeID”");

            }
        }
        [Paging(PageSize = 7)]
        public ActionResult Tag(int id, int? aid)
        {
            try
            {
                ViewBag.NavID = id;
                Linq_DefaultDataContext _dc = new Linq_DefaultDataContext();
                if (aid.HasValue)
                {
                    _dc.TB_Article.Single(m => m.ArticleID.Equals(aid.Value)).ArticleClickCount += 1;
                    _dc.SubmitChanges();
                    ViewData.Model = _dc.TV_Article.Single(m => m.ArticleID == aid.Value && m.ArticleState.Equals((int)ArticleState.Auditing));
                    return View("TagDetails");
                }
                ViewData.Model = _dc.TV_Article.Where(m => m.ArticleTypeID.Equals(id) && m.ArticleState.Equals((int)ArticleState.Auditing));
                return View();
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“Article/Home/Tag”");
                return RedirectToAction("Index", "Error", new { @area = "" });
            }
        }

        public ActionResult OnlyPage(int id)
        {
            try
            {
                ViewBag.NavID = 1;
                Linq_DefaultDataContext _dc = new Linq_DefaultDataContext();
                var _query = _dc.TB_OnlyPage.Single(m => m.OnlyPageID.Equals(id) && m.OnlyPageState.Equals((int)ArticleState.Auditing));
                if (_query == null)
                    throw new Exception("内容缺失，请与网站管理员联系！给您带来的不便，我们表示非常的抱歉！");
                _query.OnlyPageClickCount += 1;
                _dc.SubmitChanges();

                string[] _pagecontens = System.Text.RegularExpressions.Regex.Split(_query.OnlyPageContent, _splitCode);

                int _pageNum = Request.QueryString["PageNum"] == null ? 0 :
                    (
                    int.Parse(Request.QueryString["PageNum"].ToString()) < _pagecontens.Count() ? int.Parse(Request.QueryString["PageNum"].ToString()) : _pagecontens.Count() - 1
                    );
                ViewBag.Url = id.ToString() + "?time=" + new Random().Next(10).ToString();
                ViewBag.CurrentPage = _pageNum;
                ViewBag.FirstPage = 0;
                ViewBag.LastPage = _pagecontens.Count() - 1;
                _query.OnlyPageContent = _pagecontens[_pageNum];
                ViewData.Model = _query;

                if (Request.IsAjaxRequest())
                    return PartialView();
                else
                    return View();
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“Article/Home/OnlyPage”");
                return RedirectToAction("Index", "Error", new { @area = "", @MyContent=ex.Message });
            }
        }
        [Authorize(Roles = "Logins")]
        public ActionResult Preview(int id)
        {
            try
            {

                Linq_DefaultDataContext _dc = new Linq_DefaultDataContext();
                string _splitCode = @"<div\s{1,}style=""\s{0,}page-break-after\s{0,}:\s{0,}always;{0,1}\s{0,}"">\s{0,}<span\s{1,}style=""display\s{0,}:\s{0,}none;{0,1}\s{0,}"">\s{0,}&nbsp;\s{0,}</span>\s{0,}</div>";
                var _query = _dc.TV_Article.Single(m => m.ArticleID == id);
                string[] _pagecontens = System.Text.RegularExpressions.Regex.Split(_query.ArticleContent, _splitCode);

                int _pageNum = Request.QueryString["PageNum"] == null ? 0 :
                    (
                    int.Parse(Request.QueryString["PageNum"].ToString()) < _pagecontens.Count() ? int.Parse(Request.QueryString["PageNum"].ToString()) : _pagecontens.Count() - 1
                    );
                ViewBag.Url = id.ToString() + "?time=" + new Random().Next(10).ToString();
                ViewBag.CurrentPage = _pageNum;
                ViewBag.FirstPage = 0;
                ViewBag.LastPage = _pagecontens.Count() - 1;
                _query.ArticleContent = _pagecontens[_pageNum];
                ViewBag.NavID = _query.ArticleTypeID;
                ViewData.Model = _query;
                return View("Details");
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“Article/Home/Preview”");
                return RedirectToAction("Index", "Error", new { @area = "", @MyContent = ex.Message });
            }
        }
        [Authorize(Roles = "Logins")]
        public ActionResult OnlyPagePreview(int id)
        {
            try
            {
                ViewBag.NavID = 1;
                Linq_DefaultDataContext _dc = new Linq_DefaultDataContext();
                var _query = _dc.TB_OnlyPage.Single(m => m.OnlyPageID.Equals(id));
                _query.OnlyPageClickCount += 1;
                _dc.SubmitChanges();

                string[] _pagecontens = System.Text.RegularExpressions.Regex.Split(_query.OnlyPageContent, _splitCode);

                int _pageNum = Request.QueryString["PageNum"] == null ? 0 :
                    (
                    int.Parse(Request.QueryString["PageNum"].ToString()) < _pagecontens.Count() ? int.Parse(Request.QueryString["PageNum"].ToString()) : _pagecontens.Count() - 1
                    );
                ViewBag.Url = id.ToString() + "?time=" + new Random().Next(10).ToString();
                ViewBag.CurrentPage = _pageNum;
                ViewBag.FirstPage = 0;
                ViewBag.LastPage = _pagecontens.Count() - 1;
                _query.OnlyPageContent = _pagecontens[_pageNum];
                ViewData.Model = _query;

                if (Request.IsAjaxRequest())
                    return PartialView("OnlyPage");
                else
                    return View("OnlyPage");
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“Article/Home/OnlyPagePreview”");
                return RedirectToAction("Index", "Error", new { @area = "" });
            }
        }
    }
}
