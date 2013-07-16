using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eulei.shop.Models;
using Q42.Wheels.Mvc.Paging;
using eulei.shop.Code;
namespace eulei.shop.Areas.manage.Controllers
{
    [Authorize(Roles = "Logins")]
    [HandleError]
    public class OnlyPageController : MyController
    {
        string _splitCode = @"<div\s{1,}style=""\s{0,}page-break-after\s{0,}:\s{0,}always;{0,1}\s{0,}"">\s{0,}<span\s{1,}style=""display\s{0,}:\s{0,}none;{0,1}\s{0,}"">\s{0,}&nbsp;\s{0,}</span>\s{0,}</div>";
        [Paging(PageSize = 15, VaryByParams = new string[] { "OnlyPageTitle" })]
        public ActionResult Index(string OnlyPageTitle)
        {
            try
            {
                this.GetAuthority(SystemMemberShip.SiteOnlyPageBrowse);
                Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
                string _title = string.IsNullOrEmpty(OnlyPageTitle) ? "" : OnlyPageTitle;
                var _return = _dct.TB_OnlyPage
                    .Where(m => !m.OnlyPageState.Equals((int)ArticleState.Delete)
                    && m.OnlyPageTitle.Contains(_title))
                    .OrderByDescending(m => m.OnlyPageID);
                ViewData.Model = _return;
                return View();

            }
            catch (AuthorityException ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/OnlyPage/Index【post】”");
                return RedirectToAction("Index", "Error", new { @area = "", @MyContent = ex.Message });
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/OnlyPage/Index【post】”");
                return RedirectToAction("Index", "Error", new { @area = "" });
            }
        }
        [Paging(PageSize = 15, VaryByParams = new string[] { "OnlyPageTitle" })]
        //[HttpPost]
        public ActionResult Search(string OnlyPageTitle)
        {
            System.Web.Routing.RouteValueDictionary _returns = new System.Web.Routing.RouteValueDictionary();
            try
            {
                this.GetAuthority(SystemMemberShip.SiteOnlyPageBrowse);
                string _title = string.IsNullOrEmpty(OnlyPageTitle) ? "" : OnlyPageTitle;

                _returns.Add("OnlyPageTitle", _title);

            }
            catch (AuthorityException ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/OnlyPage/Search【post】”");
                return RedirectToAction("Index", "Error", new { @area = "", @MyContent = ex.Message });
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/OnlyPage/Search【post】”");
                return RedirectToAction("Index", "Error", new { @area = "" });
            }
            return RedirectToAction("Index", "OnlyPage", _returns);
        }

        //
        // GET: /manage/Article/Create

        public ActionResult Create()
        {
            try
            {
                this.GetAuthority(SystemMemberShip.SiteOnlyPageEdit);
                string url = Request.QueryString["_returnUrl"].ToString();
                ViewData["_returnUrl"] = url;
                return View();
            }
            catch (AuthorityException ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/OnlyPage/Create”");
                return RedirectToAction("Index", "Error", new { @area = "", @MyContent = ex.Message });
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/OnlyPage/Create”");
                return RedirectToAction("Index", "Error", new { @area = "" });
            }
        }

        //
        // POST: /manage/Article/Create

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                this.GetAuthority(SystemMemberShip.SiteOnlyPageEdit);
                string url = collection["_returnUrl"].ToString();
                Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
                TB_OnlyPage _result = new TB_OnlyPage();
                _result.OnlyPageTitle = collection["OnlyPageTitle"].ToString();
                _result.OnlyPageLastOperatDate = DateTime.Now;
                _result.OnlyPageContent = collection["OnlyPageContent"].ToString();
                _result.OnlyPageLabel = collection["OnlyPageLabel"].ToString();
                _result.OnlyPageLastOperator = User.Identity.Name;
                _result.OnlyPageOperatingRecord += "\r\n" + DateTime.Now.ToString() + ":" + User.Identity.Name + " 操作类型：添加";
                _result.OnlyPageAuthor = User.Identity.Name;
                _result.OnlyPageState = (int)ArticleState.Editing;
                _dct.TB_OnlyPage.InsertOnSubmit(_result);
                _dct.SubmitChanges();
                return Redirect(url);
            }
            catch (AuthorityException ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/OnlyPage/Create【post】”");
                return RedirectToAction("Index", "Error", new { @area = "", @MyContent = ex.Message });
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/OnlyPage/Create【post】”");
                return RedirectToAction("Index", "Error", new { @area = "" });
            }
        }

        //
        // GET: /manage/Article/Edit/5

        public ActionResult Edit(int id)
        {
            try
            {
                this.GetAuthority(SystemMemberShip.SiteOnlyPageEdit);
                string url = Request.QueryString["_returnUrl"].ToString();
                ViewData["_returnUrl"] = url;
                Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
                ViewData.Model = _dct.TB_OnlyPage.Single(m => m.OnlyPageID.Equals(id));
                return View();
            }
            catch (AuthorityException ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/OnlyPage/Edit”");
                return RedirectToAction("Index", "Error", new { @area = "", @MyContent = ex.Message });
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/OnlyPage/Edit”");
                return RedirectToAction("Index", "Error", new { @area = "" });
            }
        }

        //
        // POST: /manage/Article/Edit/5

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                this.GetAuthority(SystemMemberShip.SiteOnlyPageEdit);
                string url = collection["_returnUrl"].ToString();
                Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
                var _result = _dct.TB_OnlyPage.Single(m => m.OnlyPageID.Equals(id));
                _result.OnlyPageTitle = collection["OnlyPageTitle"].ToString();
                _result.OnlyPageLastOperatDate = DateTime.Now;
                _result.OnlyPageContent = collection["OnlyPageContent"].ToString();
                _result.OnlyPageLabel = collection["OnlyPageLabel"].ToString();
                _result.OnlyPageLastOperator = User.Identity.Name;
                _result.OnlyPageOperatingRecord += "\r\n" + DateTime.Now.ToString() + ":" + User.Identity.Name + " 操作类型：添加";
                _result.OnlyPageState = (int)ArticleState.Editing;
                _dct.SubmitChanges();
                return Redirect(url);
            }
            catch (AuthorityException ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/OnlyPage/Edit【post】”");
                return RedirectToAction("Index", "Error", new { @area = "", @MyContent = ex.Message });
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/OnlyPage/Edit【post】”");
                return RedirectToAction("Index", "Error", new { @area = "" });
            }
        }


        public ActionResult Delete(int id)
        {
            try
            {
                this.GetAuthority(SystemMemberShip.SiteOnlyPageEdit);
                string url = Request.QueryString["_returnUrl"].ToString();
                ViewData["_returnUrl"] = url;
                Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
                var _result = _dct.TB_OnlyPage.Single(m => m.OnlyPageID.Equals(id));
                _result.OnlyPageState = (int)ArticleState.Delete;
                _dct.SubmitChanges();
                return Redirect(url);
            }
            catch (AuthorityException ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/OnlyPage/Delete【post】”");
                return RedirectToAction("Index", "Error", new { @area = "", @MyContent = ex.Message });
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/OnlyPage/Delete【post】”");
                return RedirectToAction("Index", "Error", new { @area = "" });
            }
        }

        public ActionResult Auditing(int id)
        {
            try
            {
                this.GetAuthority(SystemMemberShip.SiteOnlyPageEdit);
                if (Request.QueryString["_returnUrl"] != null)
                {
                    ViewData["_returnUrl"] = Request.QueryString["_returnUrl"].ToString();
                }
                else
                {
                    ViewData["_returnUrl"] = "";
                }

                Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
                var _query = _dct.TB_OnlyPage.Single(m => m.OnlyPageID.Equals(id));

                string[] _pagecontens = System.Text.RegularExpressions.Regex.Split(_query.OnlyPageContent, _splitCode);

                int _pageNum = Request.QueryString["PageNum"] == null ? 0 :
                    (
                    int.Parse(Request.QueryString["PageNum"].ToString()) < _pagecontens.Count() ? int.Parse(Request.QueryString["PageNum"].ToString()) : _pagecontens.Count() - 1
                    );
                ViewBag.Url = id.ToString() + "?_returnUrl=" + ViewData["_returnUrl"].ToString();
                ViewBag.CurrentPage = _pageNum;
                ViewBag.FirstPage = 0;
                ViewBag.LastPage = _pagecontens.Count() - 1;
                _query.OnlyPageContent = _pagecontens[_pageNum];



                ViewData.Model = _query;
                return View();






            }
            catch (AuthorityException ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/OnlyPage/Auditing”");
                return RedirectToAction("Index", "Error", new { @area = "", @MyContent = ex.Message });
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/OnlyPage/Auditing”");
                return RedirectToAction("Index", "Error", new { @area = "" });
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Auditing(int id, FormCollection collection)
        {
            try
            {
                this.GetAuthority(SystemMemberShip.SiteOnlyPageEdit);
                string url = Request.QueryString["_returnUrl"].ToString();
                ViewData["_returnUrl"] = url;
                Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
                var _result = _dct.TB_OnlyPage.Single(m => m.OnlyPageID.Equals(id));
                _result.OnlyPageState = (int)ArticleState.Auditing;
                _dct.SubmitChanges();
                return Redirect(url);
            }
            catch (AuthorityException ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/OnlyPage/Auditing【post】”");
                return RedirectToAction("Index", "Error", new { @area = "", @MyContent = ex.Message });
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/OnlyPage/Auditing【post】”");
                return RedirectToAction("Index", "Error", new { @area = "" });
            }
        }
        public ActionResult ReAuditing(int id)
        {
            try
            {
                this.GetAuthority(SystemMemberShip.SiteOnlyPageEdit);
                string url = Request.QueryString["_returnUrl"].ToString();
                ViewData["_returnUrl"] = url;
                Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
                var _result = _dct.TB_OnlyPage.Single(m => m.OnlyPageID.Equals(id));
                _result.OnlyPageState = (int)ArticleState.Editing;
                _dct.SubmitChanges();
                return Redirect(url);
            }
            catch (AuthorityException ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/OnlyPage/ReAuditing【post】”");
                return RedirectToAction("Index", "Error", new { @area = "", @MyContent = ex.Message });
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/OnlyPage/ReAuditing【post】”");
                return RedirectToAction("Index", "Error", new { @area = "" });
            }
        }
    }
}
