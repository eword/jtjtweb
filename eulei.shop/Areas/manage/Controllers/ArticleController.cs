﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eulei.shop.Models;
using System.Web.Profile;
using Q42.Wheels.Mvc.Paging;
using eulei.shop.Code;
using eulei.shop.ServiceShareArticle;
namespace eulei.shop.Areas.manage.Controllers
{
    [Authorize(Roles = "Logins")]
    [HandleError]
    public class ArticleController : MyController
    {
        string _splitCode = @"<div\s{1,}style=""\s{0,}page-break-after\s{0,}:\s{0,}always;{0,1}\s{0,}"">\s{0,}<span\s{1,}style=""display\s{0,}:\s{0,}none;{0,1}\s{0,}"">\s{0,}&nbsp;\s{0,}</span>\s{0,}</div>";

        //
        // GET: /manage/Article/
        [Paging(PageSize = 15, VaryByParams = new string[] { "ArticleTypeID", "ArticleTitle" })]
        public ActionResult Index(int id, string ArticleTitle, int? ArticleTypeID)
        {
            try
            {
                #region
                Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
                string _title = string.IsNullOrEmpty(ArticleTitle) ? "" : ArticleTitle;
                int _typeID = ArticleTypeID.HasValue ? ArticleTypeID.Value : 0;
                switch (id)
                {
                    case (int)ArticleSearchType.MySend://我的发文
                        this.GetAuthority(SystemMemberShip.ArticleBrowse);
                        if (_typeID == 0)
                        {
                            ViewData.Model = _dct.VW_SA_ArticleMySend
                                .Where(m => !m.ArticleState.Equals((int)ArticleState.Delete)
                                    && m.ArticleTitle.Contains(_title)
                                    && m.ArticleAuthor.Equals(User.Identity.Name))
                                .OrderByDescending(m => m.ArticleID);
                        }
                        else
                        {
                            ViewData.Model = _dct.VW_SA_ArticleMySend
                                .Where(m => !m.ArticleState.Equals((int)ArticleState.Delete)
                                        && m.ArticleAuthor.Equals(User.Identity.Name)
                                    && m.ArticleTitle.Contains(_title)
                                    && m.ArticleTypeID.Equals(_typeID))
                                .OrderByDescending(m => m.ArticleID);
                        }
                        return View("ArticleMySend");
                        break;
                    case (int)ArticleSearchType.NeedHandle://代办文章
                        this.GetAuthority(SystemMemberShip.ArticleBrowse);
                        if (_typeID == 0)
                        {
                            ViewData.Model = _dct.VW_SA_ArticleNeedHandle
                                .Where(m => m.ArticleState.Equals((int)ArticleState.Editing)
                                    &&
                                    (
                                    m.FlowUserUserName.Equals(User.Identity.Name)
                                    ||
                                    (m.ArticleStatusID.Equals(1) && m.ArticleAuthor.Equals(User.Identity.Name))
                                    )
                                    && m.ArticleTitle.Contains(_title)
                                    )
                                .OrderByDescending(m => m.ArticleID);
                        }
                        else
                        {
                            ViewData.Model = _dct.VW_SA_ArticleNeedHandle
                                .Where(m => !m.ArticleState.Equals((int)ArticleState.Editing)
                                &&
                                (
                                    m.FlowUserUserName.Equals(User.Identity.Name)
                                ||
                                (m.ArticleStatusID.Equals(1) && m.ArticleAuthor.Equals(User.Identity.Name))
                                )
                                && m.ArticleTitle.Contains(_title)
                                && m.ArticleTypeID.Equals(_typeID))
                                .OrderByDescending(m => m.ArticleID);
                        }
                        return View("ArticleNeedHandle");
                        break;
                    case (int)ArticleSearchType.Handled:
                        this.GetAuthority(SystemMemberShip.ArticleBrowse);
                        if (_typeID == 0)
                        {
                            ViewData.Model = _dct.VW_SA_ArticleHandled
                                .Where(m => !m.ArticleState.Equals((int)ArticleState.Delete)
                                     &&
                                 
                                  m.FlowUserUserName.Equals(User.Identity.Name)
                                 
                                    && m.ArticleTitle.Contains(_title))
                                .OrderByDescending(m => m.ArticleID);
                        }
                        else
                        {
                            ViewData.Model = _dct.VW_SA_ArticleHandled
                                .Where(m => !m.ArticleState.Equals((int)ArticleState.Delete)
                                     &&
                               m.FlowUserUserName.Equals(User.Identity.Name)
                                    && m.ArticleTitle.Contains(_title)
                                    && m.ArticleTypeID.Equals(_typeID))
                                .OrderByDescending(m => m.ArticleID);
                        }
                        return View("ArticleHandled");
                        break;
                    case (int)ArticleSearchType.All:
                        this.GetAuthority(SystemMemberShip.ArticleBrowseAll);
                        if (_typeID == 0)
                        {
                            ViewData.Model = _dct.TV_Article
                                .Where(m => !m.ArticleState.Equals((int)ArticleState.Delete)
                                    && m.ArticleTitle.Contains(_title))
                                .OrderByDescending(m => m.ArticleID);
                        }
                        else
                        {
                            ViewData.Model = _dct.TV_Article
                                .Where(m => !m.ArticleState.Equals((int)ArticleState.Delete)
                                    && m.ArticleTitle.Contains(_title)
                                    && m.ArticleTypeID.Equals(_typeID))
                                .OrderByDescending(m => m.ArticleID);
                        }
                        return View();
                        break;
                }
                return View();
                #endregion
            }
            catch (AuthorityException ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/Article/Index”");
                return RedirectToAction("Index", "Error", new { @area = "", @MyContent = ex.Message });
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/Article/Index”");
                return RedirectToAction("Index", "Error", new { @area = "" });
            }

        }

        [Paging(PageSize = 15, VaryByParams = new string[] { "ArticleTypeID", "ArticleTitle" })]
        public ActionResult Search(int id, string ArticleTitle, int? ArticleTypeID)
        {
            System.Web.Routing.RouteValueDictionary _returns = new System.Web.Routing.RouteValueDictionary();
            // _returns.Add("area","manage");
            try
            {
                this.GetAuthority(SystemMemberShip.ArticleBrowse);
                _returns.Add("id", id);
                string _title = string.IsNullOrEmpty(ArticleTitle) ? "" : ArticleTitle;
                int _typeID = ArticleTypeID.HasValue ? ArticleTypeID.Value : 0;
                if (_typeID == 0)
                {
                    _returns.Add("ArticleTitle", _title);
                }
                else
                {
                    _returns.Add("ArticleTitle", _title);
                    _returns.Add("ArticleTypeID", _typeID);
                }
            }
            catch (AuthorityException ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/Article/Search”");
                return RedirectToAction("Index", "Error", new { @area = "", @MyContent = ex.Message });
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/Article/Search”");
                return RedirectToAction("Index", "Error", new { @area = "" });
            }
            return RedirectToAction("Index", "Article", _returns);
        }

        public ActionResult Create()
        {
            try
            {
                this.GetAuthority(SystemMemberShip.ArticleCreate);
                string url = Request.QueryString["_returnUrl"].ToString();
                ViewData["_returnUrl"] = url;
                return View();
            }
            catch (AuthorityException ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/Article/Create”");
                return RedirectToAction("Index", "Error", new { @area = "", @MyContent = ex.Message });
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/Article/Create”");
                return RedirectToAction("Index", "Error", new { @area = "" });
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                this.GetAuthority(SystemMemberShip.ArticleCreate);
                string url = collection["_returnUrl"].ToString();
                Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
                TB_Article _result = new TB_Article();
                _result.ArticleContent = collection["ArticleContent"].ToString();

                _result.ArticleClickCount = int.Parse(collection["ArticleClickCount"].ToString());
                _result.ArticleSendDate = DateTime.Parse(collection["ArticleSendDate"].ToString());
                _result.ArticleDescription = collection["ArticleDescription"].ToString();
                _result.ArticleTitle = collection["ArticleTitle"].ToString();
                _result.ArticleTypeID = int.Parse(collection["ArticleTypeID"].ToString());
                _result.ArticleLabel = collection["ArticleLabel"].ToString();
                _result.ArticleLastOperatDate = DateTime.Now;
                _result.ArticleLastOperator = User.Identity.Name;
                _result.ArticleAuthor = User.Identity.Name;
                _result.ArticleState = (int)ArticleState.Editing;
                _result.ArticleStatusID = 1;
                _result.ArtilcleOperatingRecord += "\r\n" + DateTime.Now.ToString() + ":" + User.Identity.Name + " 操作类型：添加";

                _dct.TB_Article.InsertOnSubmit(_result);
                _dct.SubmitChanges();

                string _content = "";
                var _currentUser = new CurrentLoginUser().GetUserInfo(User.Identity.Name);
                ArticleOperationLogHelper.WriteSendLog(_result.ArticleID, "添加/创建", _content, _currentUser.UserName, _currentUser.FriendlyName);

                return RedirectToAction("Index", "Article", new { @area = "manage", @id = (int)ArticleSearchType.NeedHandle });
                //return Redirect(url);
            }
            catch (AuthorityException ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/Article/Create【post】”");
                return RedirectToAction("Index", "Error", new { @area = "", @MyContent = ex.Message });
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/Article/Create【post】”");
                return RedirectToAction("Index", "Error", new { @area = "" });
            }
        }

        public ActionResult Edit(int id)
        {
            try
            {
                this.GetAuthority(SystemMemberShip.ArticleBrowse);
                string url = Request.QueryString["_returnUrl"].ToString();
                ViewData["_returnUrl"] = url;

                Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
                ViewData.Model = _dct.TB_Article.Single(m => m.ArticleID.Equals(id));
                return View();
            }
            catch (AuthorityException ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/Article/Edit”");
                return RedirectToAction("Index", "Error", new { @area = "", @MyContent = ex.Message });
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/Article/Edit”");
                return RedirectToAction("Index", "Error", new { @area = "" });
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                this.GetAuthority(SystemMemberShip.ArticleBrowse);
                string url = collection["_returnUrl"].ToString();
                Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
                var _result = _dct.TB_Article.Single(m => m.ArticleID.Equals(id));
                _result.ArticleContent = collection["ArticleContent"].ToString();
                _result.ArticleClickCount = int.Parse(collection["ArticleClickCount"].ToString());
                _result.ArticleSendDate = DateTime.Parse(collection["ArticleSendDate"].ToString());
                _result.ArticleDescription = collection["ArticleDescription"].ToString();
                _result.ArticleTitle = collection["ArticleTitle"].ToString();
                _result.ArticleTypeID = int.Parse(collection["ArticleTypeID"].ToString());
                _result.ArticleLabel = collection["ArticleLabel"].ToString();
                _result.ArticleLastOperatDate = DateTime.Now;
                _result.ArticleLastOperator = User.Identity.Name;
                _result.ArtilcleOperatingRecord += "\r\n" + DateTime.Now.ToString() + ":" + User.Identity.Name + " 操作类型：编辑";
                _dct.SubmitChanges();
                string _content = "";
                var _currentUser = new CurrentLoginUser().GetUserInfo(User.Identity.Name);
                ArticleOperationLogHelper.WriteSendLog(_result.ArticleID, "编辑", _content, _currentUser.UserName, _currentUser.FriendlyName);
                return Redirect(url);
            }
            catch (AuthorityException ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/Article/Edit【post】”");
                return RedirectToAction("Index", "Error", new { @area = "", @MyContent = ex.Message });
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/Article/Edit【post】”");
                return RedirectToAction("Index", "Error", new { @area = "" });
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                this.GetAuthority(SystemMemberShip.ArticleDelete);
                string url = Request.QueryString["_returnUrl"].ToString();
                ViewData["_returnUrl"] = url;
                Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
                var _result = _dct.TB_Article.Single(m => m.ArticleID.Equals(id));
                _result.ArticleState = (int)ArticleState.Delete;
                _dct.SubmitChanges();
                if (_dct.VW_SA_UserInfo.Where(m => m.UserName.Equals(User.Identity.Name)).Count() > 0)
                {
                    string _content = "";
                    var _currentUser = new CurrentLoginUser().GetUserInfo(User.Identity.Name);
                    ArticleOperationLogHelper.WriteSendLog(_result.ArticleID, "删除", _content, _currentUser.UserName, _currentUser.FriendlyName);
                }
                return Redirect(url);
            }
            catch (AuthorityException ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/Article/Delete【post】”");
                return RedirectToAction("Index", "Error", new { @area = "", @MyContent = ex.Message });
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/Article/Delete【post】”");
                return RedirectToAction("Index", "Error", new { @area = "" });
            }
        }

        public ActionResult SendToNextAuditing(int id)
        {
            try
            {
                this.GetAuthority(SystemMemberShip.ArticleBrowse);
                string url = Request.QueryString["_returnUrl"].ToString();
                ViewData["_returnUrl"] = url;
                if (User.Identity.IsAuthenticated)
                {
                    Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
                    var _query = _dct.TV_Article.Single(m => m.ArticleID.Equals(id));
                    var _currentVW = _dct.VW_SA_ArticleNeedHandle.Where(m => m.ArticleID.Equals(id) && m.FlowUserOperaterName.Equals(User.Identity.Name) && m.FlowStatusID.Equals(m.ArticleStatusID));
                    if (_query.ArticleAuthor.Equals(User.Identity.Name) || _currentVW != null)
                    {
                        if (_query.ArticleStatusID.Equals(1))
                        {
                            ViewBag.Title = "送审";
                        }
                        else
                        {
                            ViewBag.Title = "审核通过";
                        }

                        string[] _pagecontens = System.Text.RegularExpressions.Regex.Split(_query.ArticleContent, _splitCode);

                        int _pageNum = Request.QueryString["PageNum"] == null ? 0 :
                            (
                            int.Parse(Request.QueryString["PageNum"].ToString()) < _pagecontens.Count() ? int.Parse(Request.QueryString["PageNum"].ToString()) : _pagecontens.Count() - 1
                            );
                        ViewBag.Url = id.ToString() + "?_returnUrl=" + ViewData["_returnUrl"].ToString();
                        ViewBag.CurrentPage = _pageNum;
                        ViewBag.FirstPage = 0;
                        ViewBag.LastPage = _pagecontens.Count() - 1;
                        _query.ArticleContent = _pagecontens[_pageNum];
                        ViewData.Model = _query;
                    }
                    else
                    {
                        throw new Exception("无权限！");
                    }
                }
                else
                {
                    throw new Exception("未登入！");
                }
                return View();

            }
            catch (AuthorityException ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/Article/SendToNextAuditing”");
                return RedirectToAction("Index", "Error", new { @area = "", @MyContent = ex.Message });
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/Article/SendToNextAuditing”");
                return RedirectToAction("Index", "Error", new { @area = "" });
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SendToNextAuditing(int id, FormCollection collection)
        {
            //try
            //{
                this.GetAuthority(SystemMemberShip.ArticleBrowse);
                string url = collection["_returnUrl"].ToString();
                if (User.Identity.IsAuthenticated)
                {
                    Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
                    var _current = _dct.TB_Article.Single(m => m.ArticleID.Equals(id));
                    var _nexts = _dct.VW_SA_FlowInfo.Where(m => m.ArticleTypeID.Equals(_current.ArticleTypeID) && m.FlowStatusID > _current.ArticleStatusID).OrderBy(m => m.FlowStatusID);
                    var _currentVW = _dct.TV_Article.Where(m => m.ArticleID.Equals(id));
                    if (_nexts != null)
                    {
                        var _next = _nexts.First();
                        //string _strtemp = "";
                        //foreach (var item in _nexts)
                        //{ 
                        //_strtemp+=item.FlowStatusID+"||";
                        //}
                        //return Content(_strtemp);
                        if (_current.ArticleAuthor.Equals(User.Identity.Name) || _currentVW != null)
                        {
                            _current.ArticleStatusID = _next.FlowStatusID;
                            if (_current.ArticleIsApplyReturn)
                            {
                                _current.ArticleIsApplyReturn = false;
                            }
                            string _content = collection["ReturnInfo"] != null ? collection["ReturnInfo"].ToString() : "";
                            var _FlowStatusDesp = string.IsNullOrEmpty(_currentVW.First().ArticleCurrentFlowName) || _currentVW.First().ArticleStatusID.Equals(1) ? "送审" : _currentVW.First().ArticleCurrentFlowName;
                            var _currentUser = new CurrentLoginUser().GetUserInfo(User.Identity.Name);
                            ArticleOperationLogHelper.WriteSendLog(_current.ArticleID, _FlowStatusDesp, _content, _currentUser.UserName, _currentUser.FriendlyName);
                            _dct.SubmitChanges();

                        }
                    }
                }

                return Redirect(url);
            //}
            //catch (AuthorityException ex)
            //{
            //    LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/Article/SendToNextAuditing【post】”");
            //    return RedirectToAction("Index", "Error", new { @area = "", @MyContent = ex.Message });
            //}
            //catch (Exception ex)
            //{
            //    LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/Article/SendToNextAuditing【post】”");
            //    return RedirectToAction("Index", "Error", new { @area = "" });
            //}
        }

        public ActionResult ReturnToAuthor(int id, FormCollection collection)
        {
            try
            {
                this.GetAuthority(SystemMemberShip.ArticleBrowse);
                string url = collection["_returnUrl"] != null ? collection["_returnUrl"].ToString() :
                    Request.QueryString["_returnUrl"] != null ? Request.QueryString["_returnUrl"].ToString() : "/";
                if (User.Identity.IsAuthenticated)
                {
                    Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
                    var _current = _dct.TB_Article.Single(m => m.ArticleID.Equals(id));

                    _current.ArticleStatusID = 1;
                    _current.ArticleIsApplyReturn = false;
                    _dct.SubmitChanges();
                    if (_dct.VW_SA_UserInfo.Where(m => m.UserName.Equals(User.Identity.Name)).Count() > 0)
                    {
                        string _content = collection["ReturnInfo"] != null ? collection["ReturnInfo"].ToString() : "！";
                        var _currentUser = new CurrentLoginUser().GetUserInfo(User.Identity.Name);
                        ArticleOperationLogHelper.WriteSendLog(_current.ArticleID, "退回", _content, _currentUser.UserName, _currentUser.FriendlyName);
                    }


                }
                return Redirect(url);

            }
            catch (AuthorityException ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/Article/ReturnToAuthor【post】”");
                return RedirectToAction("Index", "Error", new { @area = "", @MyContent = ex.Message });
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/Article/ReturnToAuthor【post】”");
                return RedirectToAction("Index", "Error", new { @area = "" });
            }
        }

        public ActionResult Withdraw(int id)
        {
            try
            {
                this.GetAuthority(SystemMemberShip.ArticleBrowse);
                Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
                if (Request.QueryString["_returnUrl"] != null)
                {
                    ViewData["_returnUrl"] = Request.QueryString["_returnUrl"].ToString();
                }
                else
                {
                    ViewData["_returnUrl"] = "";
                }
                var _query = _dct.TV_Article.Single(m => m.ArticleID == id);
                string[] _pagecontens = System.Text.RegularExpressions.Regex.Split(_query.ArticleContent, _splitCode);

                int _pageNum = Request.QueryString["PageNum"] == null ? 0 :
                    (
                    int.Parse(Request.QueryString["PageNum"].ToString()) < _pagecontens.Count() ? int.Parse(Request.QueryString["PageNum"].ToString()) : _pagecontens.Count() - 1
                    );
                ViewBag.Url = id.ToString() + "?_returnUrl=" + ViewData["_returnUrl"].ToString();
                ViewBag.CurrentPage = _pageNum;
                ViewBag.FirstPage = 0;
                ViewBag.LastPage = _pagecontens.Count() - 1;
                _query.ArticleContent = _pagecontens[_pageNum];
                ViewData.Model = _query;
                return View();
            }
            catch (AuthorityException ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/Article/Withdraw”");
                return RedirectToAction("Index", "Error", new { @area = "", @MyContent = ex.Message });
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/Article/Withdraw”");
                return RedirectToAction("Index", "Error", new { @area = "" });
            }
        }
        /// <summary>
        /// 退回申请
        /// </summary>
        /// <param name="id"></param>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Withdraw(int id, FormCollection collection)
        {
            try
            {
                this.GetAuthority(SystemMemberShip.ArticleBrowse);
                string url = Request.QueryString["_returnUrl"].ToString();
                if (User.Identity.IsAuthenticated)
                {
                    Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
                    var _current = _dct.TB_Article.Single(m => m.ArticleID.Equals(id));
                    var _flowInfo = _dct.VW_SA_FlowInfo.Where(m => m.FlowState.Equals(true) && m.ArticleTypeID.Equals(_current.ArticleTypeID) && m.FlowStatusID > 1).OrderBy(m => m.FlowStatusID);
                    if (_current.ArticleAuthor.Equals(User.Identity.Name))
                    {
                        if (_flowInfo != null)
                        {
                            if (_current.ArticleStatusID == _flowInfo.First().FlowStatusID && _current.ArticleState.Equals((int)ArticleState.Editing))
                            {
                                _current.ArticleStatusID = 1;
                                string _content = collection["ReturnInfo"] != null ? collection["ReturnInfo"].ToString() : "";
                                var _currentUser = new CurrentLoginUser().GetUserInfo(User.Identity.Name);
                                ArticleOperationLogHelper.WriteSendLog(_current.ArticleID, "撤回", _content, _currentUser.UserName, _currentUser.FriendlyName);
                                _dct.SubmitChanges();
                            }
                            else
                            {
                                _current.ArticleIsApplyReturn = true;
                                if (_current.ArticleState.Equals((int)ArticleState.Auditing))
                                {
                                    _current.ArticleState = (int)ArticleState.Editing;
                                }
                                string _content = collection["ReturnInfo"] != null ? collection["ReturnInfo"].ToString() : "";
                                var _currentUser = new CurrentLoginUser().GetUserInfo(User.Identity.Name);
                                ArticleOperationLogHelper.WriteSendLog(_current.ArticleID, "退回申请", _content, _currentUser.UserName, _currentUser.FriendlyName);
                                _dct.SubmitChanges();
                            }
                        }
                        else
                        {
                            _current.ArticleIsApplyReturn = true;
                            if (_current.ArticleState.Equals((int)ArticleState.Auditing))
                            {
                                _current.ArticleState = (int)ArticleState.Editing;
                            }
                            string _content = collection["ReturnInfo"] != null ? collection["ReturnInfo"].ToString() : "";
                            var _currentUser = new CurrentLoginUser().GetUserInfo(User.Identity.Name);
                            ArticleOperationLogHelper.WriteSendLog(_current.ArticleID, "退回申请", _content, _currentUser.UserName, _currentUser.FriendlyName);
                            _dct.SubmitChanges();
                        }

                    }
                }
                return Redirect(url);
            }
            catch (AuthorityException ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/Article/Withdraw【post】”");
                return RedirectToAction("Index", "Error", new { @area = "", @MyContent = ex.Message });
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/Article/Withdraw【post】”");
                return RedirectToAction("Index", "Error", new { @area = "" });
            }
        }

        public ActionResult Auditing(int id)
        {
            try
            {
                this.GetAuthority(SystemMemberShip.ArticleBrowse);
                Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
                if (Request.QueryString["_returnUrl"] != null)
                {
                    ViewData["_returnUrl"] = Request.QueryString["_returnUrl"].ToString();
                }
                else
                {
                    ViewData["_returnUrl"] = "";
                }
                var _query = _dct.TV_Article.Single(m => m.ArticleID == id);
                string[] _pagecontens = System.Text.RegularExpressions.Regex.Split(_query.ArticleContent, _splitCode);

                int _pageNum = Request.QueryString["PageNum"] == null ? 0 :
                    (
                    int.Parse(Request.QueryString["PageNum"].ToString()) < _pagecontens.Count() ? int.Parse(Request.QueryString["PageNum"].ToString()) : _pagecontens.Count() - 1
                    );
                ViewBag.Url = id.ToString() + "?_returnUrl=" + ViewData["_returnUrl"].ToString();
                ViewBag.CurrentPage = _pageNum;
                ViewBag.FirstPage = 0;
                ViewBag.LastPage = _pagecontens.Count() - 1;
                _query.ArticleContent = _pagecontens[_pageNum];
                ViewData.Model = _query;
                return View();
            }
            catch (AuthorityException ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/Article/Auditing”");
                return RedirectToAction("Index", "Error", new { @area = "", @MyContent = ex.Message });
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/Article/Auditing”");
                return RedirectToAction("Index", "Error", new { @area = "" });
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Auditing(int id, FormCollection collection)
        {
            try
            {
                this.GetAuthority(SystemMemberShip.ArticleBrowse);
                string url = collection["_returnUrl"].ToString();
                Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
                var _result = _dct.TB_Article.Single(m => m.ArticleID.Equals(id));
                _result.ArticleState = (int)ArticleState.Auditing;
                if (_result.ArticleIsApplyReturn)
                {
                    _result.ArticleIsApplyReturn = false;
                }
                _dct.SubmitChanges();

                string _content = collection["ReturnInfo"] != null ? collection["ReturnInfo"].ToString() : "";
                var _currentUser = new CurrentLoginUser().GetUserInfo(User.Identity.Name);
                ArticleOperationLogHelper.WriteSendLog(_result.ArticleID, "发布", _content, _currentUser.UserName, _currentUser.FriendlyName);


                return Redirect(url);
            }
            catch (AuthorityException ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/Article/Auditing【post】”");
                return RedirectToAction("Index", "Error", new { @area = "", @MyContent = ex.Message });
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/Article/Auditing【post】”");
                return RedirectToAction("Index", "Error", new { @area = "" });
            }
        }

        public ActionResult ReAuditing(int id)
        {
            try
            {
                this.GetAuthority(SystemMemberShip.ArticleBrowse);
                string url = Request.QueryString["_returnUrl"].ToString();
                ViewData["_returnUrl"] = url;
                Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
                var _result = _dct.TB_Article.Single(m => m.ArticleID.Equals(id));
                _result.ArticleState = (int)ArticleState.Editing;
                _dct.SubmitChanges();
                if (_dct.VW_SA_UserInfo.Where(m => m.UserName.Equals(User.Identity.Name)).Count() > 0)
                {
                    string _content = "";
                    var _currentUser = new CurrentLoginUser().GetUserInfo(User.Identity.Name);
                    ArticleOperationLogHelper.WriteSendLog(_result.ArticleID, "反审核", _content, _currentUser.UserName, _currentUser.FriendlyName);
                }
                return Redirect(url);
            }
            catch (AuthorityException ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/Article/ReAuditing【post】”");
                return RedirectToAction("Index", "Error", new { @area = "", @MyContent = ex.Message });
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/Article/ReAuditing【post】”");
                return RedirectToAction("Index", "Error", new { @area = "" });
            }
        }

        public ActionResult SetIsemp(int id)
        {
            try
            {
                this.GetAuthority(SystemMemberShip.ArticleBrowse);
                string url = Request.QueryString["_returnUrl"].ToString();
                ViewData["_returnUrl"] = url;
                Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
                var _result = _dct.TB_Article.Single(m => m.ArticleID.Equals(id));
                _result.ArticleIsemp = !_result.ArticleIsemp;
                _dct.SubmitChanges();
                var _currentUser = new CurrentLoginUser().GetUserInfo(User.Identity.Name);
                ArticleOperationLogHelper.WriteSendLog(_result.ArticleID, "点灯设置", "点灯设置", _currentUser.UserName, _currentUser.FriendlyName);
                return Redirect(url);
            }
            catch (AuthorityException ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/Article/SetIsemp【post】”");
                return RedirectToAction("Index", "Error", new { @area = "", @MyContent = ex.Message });
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/Article/SetIsemp【post】”");
                return RedirectToAction("Index", "Error", new { @area = "" });
            }
        }

        public ActionResult SetIsNew(int id)
        {
            try
            {
                this.GetAuthority(SystemMemberShip.ArticleBrowse);
                string url = Request.QueryString["_returnUrl"].ToString();
                ViewData["_returnUrl"] = url;
                Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
                var _result = _dct.TB_Article.Single(m => m.ArticleID.Equals(id));
                _result.ArticleIsNew = !_result.ArticleIsNew;
                _dct.SubmitChanges();
                var _currentUser = new CurrentLoginUser().GetUserInfo(User.Identity.Name);
                ArticleOperationLogHelper.WriteSendLog(_result.ArticleID, "新闻设置", "新闻设置", _currentUser.UserName, _currentUser.FriendlyName);
                return Redirect(url);
            }
            catch (AuthorityException ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/Article/SetIsNew【post】”");
                return RedirectToAction("Index", "Error", new { @area = "", @MyContent = ex.Message });
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/Article/SetIsNew【post】”");
                return RedirectToAction("Index", "Error", new { @area = "" });
            }
        }

        public ActionResult Push(int id)
        {
            try
            {
                this.GetAuthority(SystemMemberShip.ArticleShare);
                string url = Request.QueryString["_returnUrl"].ToString();
                ViewData["_returnUrl"] = url;
                Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
                var _result = _dct.TV_Article.Single(m => m.ArticleID.Equals(id));
                var _service = new ServiceShareArticle.ShareArticleClient("ShareArticle");
                var _shareArtilceEntity = new ServiceShareArticle.ShareArticleEntity();
                var _user = _dct.VW_SA_UserInfo.Single(m => m.UserName.Equals(User.Identity.Name));
                var _author = _dct.VW_SA_UserInfo.Single(m => m.UserName.Equals(_result.ArticleAuthor));
                _shareArtilceEntity.ShareArticleArticleID = _result.ArticleID;
                _shareArtilceEntity.ShareArticleAuthor = _author.UserInfoFriendName + "\r\n(" + _author.UserName + ")";
                _shareArtilceEntity.ShareArticleCitedTypeID = _result.ArticleTypeID;
                _shareArtilceEntity.ShareArticleContent = _result.ArticleContent;
                _shareArtilceEntity.ShareArticleDescription = _result.ArticleDescription;
                _shareArtilceEntity.ShareArticleFrameworkName = _result.FrameworkName;
                _shareArtilceEntity.ShareArticleID = Guid.NewGuid();
                _shareArtilceEntity.ShareArticleIsApplyReturn = _result.ArticleIsApplyReturn;
                _shareArtilceEntity.ShareArticleIsemp = _result.ArticleIsemp;
                _shareArtilceEntity.ShareArticleIsNew = _result.ArticleIsNew;
                _shareArtilceEntity.ShareArticleIsPicTheme = _result.ArticleIsPicTheme;
                _shareArtilceEntity.ShareArticleIsShare = _result.ArticleIsShare; ;
                _shareArtilceEntity.ShareArticleLabel = _result.ArticleLabel;
                _shareArtilceEntity.ShareArticleLastOperatDate = _result.ArticleLastOperatDate;
                _shareArtilceEntity.ShareArticleLastOperstor = _result.ArticleLastOperator;
                _shareArtilceEntity.ShareArticleOperationgRecord = _result.ArtilcleOperatingRecord;
                _shareArtilceEntity.ShareArticlePictureTitle = _result.ArticlePictureTitle;
                _shareArtilceEntity.ShareArticleSendDate = _result.ArticleSendDate;
                _shareArtilceEntity.ShareArticleShareDate = DateTime.Now;
                _shareArtilceEntity.ShareArticleState = (int)ShareArticleState.Waiting;
                _shareArtilceEntity.ShareArticleStatusID = _result.ArticleStatusID;
                _shareArtilceEntity.ShareArticleTitle = _result.ArticleTitle;
                _shareArtilceEntity.ShareArticleType = _result.ArticleTypeName;
                _shareArtilceEntity.ShareArticleUrl = _result.ArticlePictureUrl;
                _shareArtilceEntity.ShareArticlePusher = _user.UserName;
                _shareArtilceEntity.ShareArticlePusherFriendName = _user.UserInfoFriendName;
                _shareArtilceEntity.ShareArticlePusherID = _user.UserId.Value;
                if (_service.Push(_shareArtilceEntity))
                {
                    var _update = _dct.TB_Article.Single(m => m.ArticleID.Equals(id));
                    _update.ArticleIsShare = true;
                    _dct.SubmitChanges();
                    var _currentUser = new CurrentLoginUser().GetUserInfo(User.Identity.Name);
                    ArticleOperationLogHelper.WriteSendLog(_result.ArticleID, "推送", "推送", _currentUser.UserName, _currentUser.FriendlyName);
                }
                else
                {
                    return RedirectToAction("Index", "Error", new { @area = "", @MyContent = "<strong class=\"tred\">上级站点之前推送的文件撤回申请还未处理，请联系上级站点管理员！<strong>" });
                }
                return Redirect(url);
            }
            catch (AuthorityException ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/Article/Push【post】”");
                return RedirectToAction("Index", "Error", new { @area = "", @MyContent = ex.Message });
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/Article/Push【post】”");
                return RedirectToAction("Index", "Error", new { @area = "" });
            }
        }

        public ActionResult Repush(int id)
        {
            try
            {
                this.GetAuthority(SystemMemberShip.ArticleShare);
                string url = Request.QueryString["_returnUrl"].ToString();
                ViewData["_returnUrl"] = url;
                Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
                var _result = _dct.TV_Article.Single(m => m.ArticleID.Equals(id));
                var _service = new ServiceShareArticle.ShareArticleClient("ShareArticle");
                var _shareArtilceEntity = new ServiceShareArticle.ShareArticleEntity();
                var _user = _dct.VW_SA_UserInfo.Single(m => m.UserName.Equals(User.Identity.Name));
                var _author = _dct.VW_SA_UserInfo.Single(m => m.UserName.Equals(_result.ArticleAuthor));
                _shareArtilceEntity.ShareArticleArticleID = _result.ArticleID;
                _shareArtilceEntity.ShareArticleAuthor = _author.UserInfoFriendName + "\r\n(" + _author.UserName + ")";
                _shareArtilceEntity.ShareArticleCitedTypeID = _result.ArticleTypeID;
                _shareArtilceEntity.ShareArticleContent = _result.ArticleContent;
                _shareArtilceEntity.ShareArticleDescription = _result.ArticleDescription;
                _shareArtilceEntity.ShareArticleFrameworkName = _result.FrameworkName;
                _shareArtilceEntity.ShareArticleID = Guid.NewGuid();
                _shareArtilceEntity.ShareArticleIsApplyReturn = _result.ArticleIsApplyReturn;
                _shareArtilceEntity.ShareArticleIsemp = _result.ArticleIsemp;
                _shareArtilceEntity.ShareArticleIsNew = _result.ArticleIsNew;
                _shareArtilceEntity.ShareArticleIsPicTheme = _result.ArticleIsPicTheme;
                _shareArtilceEntity.ShareArticleIsShare = _result.ArticleIsShare; ;
                _shareArtilceEntity.ShareArticleLabel = _result.ArticleLabel;
                _shareArtilceEntity.ShareArticleLastOperatDate = _result.ArticleLastOperatDate;
                _shareArtilceEntity.ShareArticleLastOperstor = _result.ArticleLastOperator;
                _shareArtilceEntity.ShareArticleOperationgRecord = _result.ArtilcleOperatingRecord;
                _shareArtilceEntity.ShareArticlePictureTitle = _result.ArticlePictureTitle;
                _shareArtilceEntity.ShareArticleSendDate = _result.ArticleSendDate;
                _shareArtilceEntity.ShareArticleShareDate = DateTime.Now;
                _shareArtilceEntity.ShareArticleState = (int)ShareArticleState.Waiting;
                _shareArtilceEntity.ShareArticleStatusID = _result.ArticleStatusID;
                _shareArtilceEntity.ShareArticleTitle = _result.ArticleTitle;
                _shareArtilceEntity.ShareArticleType = _result.ArticleTypeName;
                _shareArtilceEntity.ShareArticleUrl = _result.ArticlePictureUrl;
                _shareArtilceEntity.ShareArticlePusher = _user.UserName;
                _shareArtilceEntity.ShareArticlePusherFriendName = _user.UserInfoFriendName;
                _shareArtilceEntity.ShareArticlePusherID = _user.UserId.Value;
                if (_service.RePush(_shareArtilceEntity))
                {
                    var _update = _dct.TB_Article.Single(m => m.ArticleID.Equals(id));
                    _update.ArticleIsShare = false;
                    _dct.SubmitChanges();
                    var _currentUser = new CurrentLoginUser().GetUserInfo(User.Identity.Name);
                    ArticleOperationLogHelper.WriteSendLog(_result.ArticleID, "推送撤回申请", "推送撤回申请", _currentUser.UserName, _currentUser.FriendlyName);
                }
                else
                {
                    return RedirectToAction("Index", "Error", new { @area = "", @MyContent = "<strong class=\"tred\">上级站点的相关文章不存在或已删除！<strong>" });
                }
                return Redirect(url);
            }
            catch (AuthorityException ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/Article/Repush【post】”");
                return RedirectToAction("Index", "Error", new { @area = "", @MyContent = ex.Message });
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/Article/Repush【post】”");
                return RedirectToAction("Index", "Error", new { @area = "" });
            }
        }

        [Paging(PageSize = 15, VaryByParams = new string[] { "ShareArticleCitedTypeID", "ShareArticleTitle" })]
        public ActionResult ShareArticleIndex(int id, string ShareArticleTitle, int? ShareArticleCitedTypeID)
        {
            try
            {
                this.GetAuthority(SystemMemberShip.ArticleShareManage);
                #region
                Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
                string _title = string.IsNullOrEmpty(ShareArticleTitle) ? "" : ShareArticleTitle;
                int _typeID = ShareArticleCitedTypeID.HasValue ? ShareArticleCitedTypeID.Value : 0;
                switch (id)
                {
                    case (int)ShareArticleState.Waiting://待处理
                        if (_typeID == 0)
                        {
                            ViewData.Model = _dct.VW_SA_ShareArticle
                                .Where(m => 
                                    (
                                    m.ShareArticleState.Equals((int)ShareArticleState.Waiting)||
                                    (m.ShareArticleIsApplyReturn.Equals(true) && !m.ShareArticleState.Equals((int)ShareArticleState.Delete))
                                    )
                                    && m.ShareArticleTitle.Contains(_title))
                                .OrderByDescending(m => m.ShareArticleArticleID);
                        }
                        else
                        {
                            ViewData.Model = _dct.VW_SA_ShareArticle
                                .Where(m => 
                                    (
                                    m.ShareArticleState.Equals((int)ShareArticleState.Waiting)||
                                    (m.ShareArticleIsApplyReturn.Equals(true) && !m.ShareArticleState.Equals((int)ShareArticleState.Delete))
                                    )
                                    && m.ShareArticleTitle.Contains(_title)
                                    && m.ShareArticleCitedTypeID.Equals(_typeID))
                                .OrderByDescending(m => m.ShareArticleArticleID);
                        }
                        return View();
                        break;
                    case (int)ShareArticleState.Reprint://重载

                        if (_typeID == 0)
                        {
                            ViewData.Model = _dct.VW_SA_ShareArticle
                                .Where(m => m.ShareArticleState.Equals((int)ShareArticleState.Reprint)
                                    && m.ShareArticleTitle.Contains(_title))
                                .OrderByDescending(m => m.ShareArticleArticleID);
                        }
                        else
                        {
                            ViewData.Model = _dct.VW_SA_ShareArticle
                             .Where(m => m.ShareArticleState.Equals((int)ShareArticleState.Reprint)
                                 && m.ShareArticleTitle.Contains(_title)
                                 && m.ShareArticleCitedTypeID.Equals(_typeID))
                             .OrderByDescending(m => m.ShareArticleArticleID);
                        }
                        return View();
                        break;
                    case (int)ShareArticleState.Cited://引用

                        if (_typeID == 0)
                        {
                            ViewData.Model = _dct.VW_SA_ShareArticle
                                .Where(m => m.ShareArticleState.Equals((int)ShareArticleState.Cited)
                                && m.ShareArticleTitle.Contains(_title))
                                .OrderByDescending(m => m.ShareArticleArticleID);
                        }
                        else
                        {
                            ViewData.Model = _dct.VW_SA_ShareArticle
                        .Where(m => m.ShareArticleState.Equals((int)ShareArticleState.Cited)
                        && m.ShareArticleTitle.Contains(_title)
                        && m.ShareArticleCitedTypeID.Equals(_typeID))
                        .OrderByDescending(m => m.ShareArticleArticleID);
                        }
                        return View();
                        break;
                    case (int)ShareArticleState.Delete://删除
                        this.GetAuthority(SystemMemberShip.ArticleBrowseAll);
                        if (_typeID == 0)
                        {
                            ViewData.Model = _dct.VW_SA_ShareArticle
                             .Where(m => m.ShareArticleState.Equals((int)ShareArticleState.Delete)
                                 && m.ShareArticleTitle.Contains(_title))
                             .OrderByDescending(m => m.ShareArticleArticleID);
                        }
                        else
                        {
                            ViewData.Model = _dct.VW_SA_ShareArticle
                              .Where(m => m.ShareArticleState.Equals((int)ShareArticleState.Delete)
                                  && m.ShareArticleTitle.Contains(_title)
                                  && m.ShareArticleCitedTypeID.Equals(_typeID))
                              .OrderByDescending(m => m.ShareArticleArticleID);
                        }
                        return View();
                        break;
                }
                return View();
                #endregion
            }
            catch (AuthorityException ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/Article/ShareArticleIndex”");
                return RedirectToAction("Index", "Error", new { @area = "", @MyContent = ex.Message });
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/Article/ShareArticleIndex”");
                return RedirectToAction("Index", "Error", new { @area = "" });
            }

        }

        [Paging(PageSize = 15, VaryByParams = new string[] { "ShareArticleCitedTypeID", "ShareArticleTitle" })]
        public ActionResult ShareArticleSearch(int? id, string ShareArticleTitle, int? ShareArticleCitedTypeID)
        {
            System.Web.Routing.RouteValueDictionary _returns = new System.Web.Routing.RouteValueDictionary();
            // _returns.Add("area","manage");
            try
            {
                this.GetAuthority(SystemMemberShip.ArticleShareManage);
                int _id = id.HasValue ? id.Value : (int)ShareArticleState.Waiting;
                string _title = string.IsNullOrEmpty(ShareArticleTitle) ? "" : ShareArticleTitle;
                int _typeID = ShareArticleCitedTypeID.HasValue ? ShareArticleCitedTypeID.Value : 0;
                _returns.Add("id", _id);
                _returns.Add("ShareArticleTitle", _title);
                if (_typeID != 0)
                {
                    _returns.Add("ShareArticleCitedTypeID", _typeID);
                }
            }
            catch (AuthorityException ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/Article/ShareArticleSearch”");
                return RedirectToAction("Index", "Error", new { @area = "", @MyContent = ex.Message });
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/Article/ShareArticleSearch”");
                return RedirectToAction("Index", "Error", new { @area = "" });
            }
            return RedirectToAction("ShareArticleIndex", "Article", _returns);
        }


        public ActionResult ShareArticleCited(Guid id)
        {
            try
            {
                this.GetAuthority(SystemMemberShip.ArticleShareManage);
                string url = Request.QueryString["_returnUrl"].ToString();
                ViewData["_returnUrl"] = url;

                Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
                var _result = new TB_Article();
                var _shareArticle = _dct.VW_SA_ShareArticle.Single(m => m.ShareArticleID.Equals(id));

                _result.ArticleContent = _shareArticle.ShareArticleContent;
                _result.ArticleClickCount = 0;
                _result.ArticleSendDate = _shareArticle.ShareArticleSendDate;
                _result.ArticleDescription = _shareArticle.ShareArticleDescription;
                _result.ArticleTitle = _shareArticle.ShareArticleTitle;
                _result.ArticleLabel = _shareArticle.ShareArticleLabel;
                ViewBag.Guid = id;
                ViewData.Model = _result;
                return View();
            }
            catch (AuthorityException ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/Article/ShareArticleCited”");
                return RedirectToAction("Index", "Error", new { @area = "", @MyContent = ex.Message });
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/Article/ShareArticleCited”");
                return RedirectToAction("Index", "Error", new { @area = "" });
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ShareArticleCited(Guid id, FormCollection collection)
        {
            Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
            try
            {
                this.GetAuthority(SystemMemberShip.ArticleShareManage);
                string url = collection["_returnUrl"].ToString();
                TB_Article _result = new TB_Article();
                _result.ArticleContent = collection["ArticleContent"].ToString();
                _result.ArticleClickCount = int.Parse(collection["ArticleClickCount"].ToString());
                _result.ArticleSendDate = DateTime.Parse(collection["ArticleSendDate"].ToString());
                _result.ArticleDescription = collection["ArticleDescription"].ToString();
                _result.ArticleTitle = collection["ArticleTitle"].ToString();
                _result.ArticleTypeID = int.Parse(collection["ArticleTypeID"].ToString());
                _result.ArticleLabel = collection["ArticleLabel"].ToString();
                _result.ArticleLastOperatDate = DateTime.Now;
                _result.ArticleLastOperator = User.Identity.Name;
                _result.ArticleAuthor = User.Identity.Name;
                _result.ArticleState = (int)ArticleState.Editing;
                _result.ArticleStatusID = 1;
                _result.ArtilcleOperatingRecord += "\r\n" + DateTime.Now.ToString() + ":" + User.Identity.Name + " 操作类型：引用共享/创建";
                _dct.Connection.Open();
                _dct.Transaction = _dct.Connection.BeginTransaction();
                _dct.TB_Article.InsertOnSubmit(_result);
                _dct.SubmitChanges();
                //状态修改
                var _shareArticle = _dct.SA_ShareArticle.Single(m => m.ShareArticleID.Equals(id));
                var _user = _dct.VW_SA_UserInfo.Single(m => m.UserName.Equals(User.Identity.Name));
                _shareArticle.ShareArticleState = (int)ShareArticleState.Cited;
                _shareArticle.ShareArticleCitedArticleID = _result.ArticleID;
                _shareArticle.ShareArticleCitedTypeID = _result.ArticleTypeID;
                _shareArticle.ShareArticleCitedUserID = _user.UserId.Value;
                _shareArticle.ShareArticleCitedDate = DateTime.Now;
                _dct.SubmitChanges();

                string _content = "";
                var _currentUser = new CurrentLoginUser().GetUserInfo(User.Identity.Name);
                ArticleOperationLogHelper.WriteSendLog(_result.ArticleID, "引用共享/创建", _content, _currentUser.UserName, _currentUser.FriendlyName);
                _dct.Transaction.Commit();
                return RedirectToAction("Index", "Article", new { @area = "manage", @id = (int)ArticleSearchType.NeedHandle });
                //return Redirect(url);
            }
            catch (AuthorityException ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/Article/ShareArticleCited【post】”");
                _dct.Transaction.Rollback();
                return RedirectToAction("Index", "Error", new { @area = "", @MyContent = ex.Message });
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/Article/ShareArticleCited【post】”");
                return RedirectToAction("Index", "Error", new { @area = "" });
            }
            finally
            {
                _dct.Connection.Close();
                _dct.Transaction = null;
            }
        }

        public ActionResult ShareArticleReprint(Guid id)
        {
            try
            {
                this.GetAuthority(SystemMemberShip.ArticleShareManage);
                string url = Request.QueryString["_returnUrl"].ToString();
                ViewData["_returnUrl"] = url;
                Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();

                var _query = _dct.VW_SA_ShareArticle.Single(m => m.ShareArticleID.Equals(id));
                string[] _pagecontens = System.Text.RegularExpressions.Regex.Split(_query.ShareArticleContent, _splitCode);

                int _pageNum = Request.QueryString["PageNum"] == null ? 0 :
                    (
                    int.Parse(Request.QueryString["PageNum"].ToString()) < _pagecontens.Count() ? int.Parse(Request.QueryString["PageNum"].ToString()) : _pagecontens.Count() - 1
                    );
                ViewBag.Url = id.ToString() + "?_returnUrl=" + ViewData["_returnUrl"].ToString();
                ViewBag.CurrentPage = _pageNum;
                ViewBag.FirstPage = 0;
                ViewBag.LastPage = _pagecontens.Count() - 1;
                _query.ShareArticleContent = _pagecontens[_pageNum];
                ViewData.Model = _query;
                return View();
            }
            catch (AuthorityException ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/Article/ShareArticleReprint”");
                return RedirectToAction("Index", "Error", new { @area = "", @MyContent = ex.Message });
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/Article/ShareArticleReprint”");
                return RedirectToAction("Index", "Error", new { @area = "" });
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ShareArticleReprint(FormCollection collection)
        {
            Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
            try
            {
                this.GetAuthority(SystemMemberShip.ArticleShareManage);
                string url = collection["_returnUrl"].ToString();
                _dct.Connection.Open();
                _dct.Transaction = _dct.Connection.BeginTransaction();
                var _shareArticle = _dct.VW_SA_ShareArticle.Single(m => m.ShareArticleID.Equals(Guid.Parse(collection["ShareArticleID"].ToString())));
                TB_Article _result = new TB_Article();
                _result.ArticleContent = _shareArticle.ShareArticleContent;
                _result.ArticleClickCount = 0;
                _result.ArticleSendDate = DateTime.Now;
                _result.ArticleDescription = _shareArticle.ShareArticleDescription;
                _result.ArticleTitle = _shareArticle.ShareArticleTitle;
                _result.ArticleTypeID = int.Parse(collection["ShareArticleCitedTypeID"].ToString());
                _result.ArticleLabel = _shareArticle.ShareArticleLabel;
                _result.ArticleLastOperatDate = DateTime.Now;
                _result.ArticleLastOperator = User.Identity.Name;
                _result.ArticleAuthor = User.Identity.Name;
                _result.ArticleState = (int)ArticleState.IsShare;
                _result.ArticleStatusID = 99;
                _result.ArtilcleOperatingRecord += "\r\n" + DateTime.Now.ToString() + ":" + User.Identity.Name + " 操作类型：引用共享/创建";
                _dct.TB_Article.InsertOnSubmit(_result);
                _dct.SubmitChanges();
                //状态修改
                var _user = _dct.VW_SA_UserInfo.Single(m => m.UserName.Equals(User.Identity.Name));
                var _update = _dct.SA_ShareArticle.Single(m => m.ShareArticleID.Equals(Guid.Parse(collection["ShareArticleID"].ToString())));
                _update.ShareArticleState = (int)ShareArticleState.Reprint;
                _update.ShareArticleCitedArticleID = _result.ArticleID;
                _update.ShareArticleCitedTypeID = _result.ArticleTypeID;
                _update.ShareArticleCitedUserID = _user.UserId.Value;
                _update.ShareArticleCitedDate = DateTime.Now;
                _dct.SubmitChanges();
                string _content = "";
                var _currentUser = new CurrentLoginUser().GetUserInfo(User.Identity.Name);
                ArticleOperationLogHelper.WriteSendLog(_result.ArticleID, "转载/创建", _content, _currentUser.UserName, _currentUser.FriendlyName);
                _dct.Transaction.Commit();
                return RedirectToAction("Index", "Article", new { @area = "manage", @id = (int)ArticleSearchType.Handled });
                //return Redirect(url);
            }
            catch (AuthorityException ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/Article/ShareArticleReprint【post】”");
                _dct.Transaction.Rollback();
                return RedirectToAction("Index", "Error", new { @area = "", @MyContent = ex.Message });
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/Article/ShareArticleReprint【post】”");
                return RedirectToAction("Index", "Error", new { @area = "" });
            }
            finally
            {
                _dct.Connection.Close();
                _dct.Transaction = null;
            }
        }

        public ActionResult ShareArticleDelete(Guid id)
        {
            try
            {
                this.GetAuthority(SystemMemberShip.ArticleShareManage);
                string url = Request.QueryString["_returnUrl"].ToString();
                ViewData["_returnUrl"] = url;
                Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
                var _result = _dct.SA_ShareArticle.Single(m => m.ShareArticleID.Equals(id));
                if (_result.ShareArticleCitedArticleID.HasValue)
                {
                    if (!_result.ShareArticleCitedArticleID.Value.Equals(0))
                    {
                        var _query = _dct.TV_Article.Where(m => m.ArticleID.Equals(_result.ShareArticleCitedArticleID.Value));
                        if (_query.Count() > 0)
                        {
                            throw new Exception("最后引用文章未删除！");
                        }
                    }
                }
                _result.ShareArticleState = (int)ShareArticleState.Delete;
                var _currentUser = new CurrentLoginUser().GetUserInfo(User.Identity.Name);
                _result.ShareArticleLastOperatDate = DateTime.Now;
                _result.ShareArticleLastOperstor = User.Identity.Name;
                _result.ShareArticleOperationgRecord += "\r\n" + DateTime.Now.ToString() + ":" + User.Identity.Name + " 操作类型：删除";
                _dct.SubmitChanges();
                return Redirect(url);
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/Article/ShareArticleDelete【post】”");
                return RedirectToAction("Index", "Error", new { @area = "", @MyContent = ex.Message });
            }
        }

        public ActionResult GetArticleOperationgRecord(int id)
        {
            try
            {
                this.GetAuthority(SystemMemberShip.ArticleBrowse);
                string url = Request.QueryString["_returnUrl"].ToString();
                ViewData["_returnUrl"] = url;
                Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
                var _result = _dct.SA_ArticleOperationLog.Where(m => m.OperationLogArticleID.Equals(id));
                ViewData.Model = _result;
                return View();
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/Article/GetArticleOperationgRecord”");
                return RedirectToAction("Index", "Error", new { @area = "", @MyContent = ex.Message });
            }
        }



    }

}