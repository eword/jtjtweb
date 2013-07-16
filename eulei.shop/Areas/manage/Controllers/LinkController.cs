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
    public class LinkController : MyController
    {
        //
        // GET: /manage/Article/
        [Paging(PageSize = 15, VaryByParams = new string[] { "LinkTitle" })]
        public ActionResult Index(string LinkTitle)
        {
            try
            {
                this.GetAuthority(SystemMemberShip.FriendLinkManage);
                string _title = string.IsNullOrEmpty(LinkTitle) ? "" : LinkTitle;
                Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
                var _return = _dct.TB_Link
                    .Where(m => !m.LinkState.Equals((int)LinkState.Delete)
                    && m.LinkTitle.Contains(_title))
                    .OrderByDescending(m => m.LinkOrder);
                ViewData.Model = _return;
                return View();

            }
            catch (AuthorityException ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/Link/Index”");
                return RedirectToAction("Index", "Error", new { @area = "", @MyContent = ex.Message });
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/Link/Index”");
                return RedirectToAction("Index", "Error", new { @area = "" });
            }
        }
        [Paging(PageSize = 15, VaryByParams = new string[] { "LinkTitle" })]
        //[HttpPost]
        public ActionResult Search(string LinkTitle)
        {
            System.Web.Routing.RouteValueDictionary _returns = new System.Web.Routing.RouteValueDictionary();
            // _returns.Add("area","manage");
            try
            {
                this.GetAuthority(SystemMemberShip.FriendLinkManage);
                string _title = string.IsNullOrEmpty(LinkTitle) ? "" : LinkTitle;

                _returns.Add("LinkTitle", _title);

            }
            catch (AuthorityException ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/Link/Search【post】”");
                return RedirectToAction("Index", "Error", new { @area = "", @MyContent = ex.Message });
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/Link/Search【post】”");
                return RedirectToAction("Index", "Error", new { @area = "" });
            }
            return RedirectToAction("Index", "Link", _returns);
        }
        //
        // GET: /manage/Article/Create

        public ActionResult Create()
        {
            try
            {
                this.GetAuthority(SystemMemberShip.FriendLinkManage);
                string url = Request.QueryString["_returnUrl"].ToString();
                ViewData["_returnUrl"] = url;
                return View();
            }
            catch (AuthorityException ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/Link/Create”");
                return RedirectToAction("Index", "Error", new { @area = "", @MyContent = ex.Message });
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/Link/Create”");
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
                this.GetAuthority(SystemMemberShip.FriendLinkManage);
                string url = collection["_returnUrl"].ToString();
                Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
                TB_Link _result = new TB_Link();
                _result.LinkTitle = collection["LinkTitle"].ToString();
                _result.LinkName = collection["LinkName"].ToString();
                _result.LinkUrl = collection["LinkUrl"].ToString();
                _result.LinkIsPicLink = collection["LinkIsPicLink"].ToString().Equals("false") ? false : true;
                _result.LinkPicUrl = collection["LinkPicUrl"].ToString();
                _result.LinkState = (int)LinkState.Editing;
                _result.LinkOrder = _dct.TB_Link.Count() + 1;
                /////////////////////////////////////
                _dct.TB_Link.InsertOnSubmit(_result);
                _dct.SubmitChanges();
                return Redirect(url);
            }
            catch (AuthorityException ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/Link/Create【post】”");
                return RedirectToAction("Index", "Error", new { @area = "", @MyContent = ex.Message });
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/Link/Create【post】”");
                return RedirectToAction("Index", "Error", new { @area = "" });
            }
        }

        //
        // GET: /manage/Article/Edit/5

        public ActionResult Edit(int id)
        {
            try
            {
                this.GetAuthority(SystemMemberShip.FriendLinkManage);
                string url = Request.QueryString["_returnUrl"].ToString();
                ViewData["_returnUrl"] = url;
                Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
                ViewData.Model = _dct.TB_Link.Single(m => m.LinkID.Equals(id));
                return View();
            }
            catch (AuthorityException ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/Link/Edit”");
                return RedirectToAction("Index", "Error", new { @area = "", @MyContent = ex.Message });
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/Link/Edit”");
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
                this.GetAuthority(SystemMemberShip.FriendLinkManage);
                string url = collection["_returnUrl"].ToString();
                Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
                var _result = _dct.TB_Link.Single(m => m.LinkID.Equals(id));
                _result.LinkTitle = collection["LinkTitle"].ToString();
                _result.LinkName = collection["LinkName"].ToString();
                _result.LinkUrl = collection["LinkUrl"].ToString();
                _result.LinkIsPicLink = collection["LinkIsPicLink"].ToString().Equals("false") ? false : true;
                _result.LinkPicUrl = collection["LinkPicUrl"].ToString();
                _result.LinkState = (int)LinkState.Editing;
                //////////////////////////////////////////
                _dct.SubmitChanges();
                return Redirect(url);
            }
            catch (AuthorityException ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/Link/Edit【post】”");
                return RedirectToAction("Index", "Error", new { @area = "", @MyContent = ex.Message });
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/Link/Edit【post】”");
                return RedirectToAction("Index", "Error", new { @area = "" });
            }
        }

        //
        // GET: /manage/Article/Delete/5

        //
        // POST: /manage/Article/Delete/5

        public ActionResult Delete(int id)
        {
            try
            {
                this.GetAuthority(SystemMemberShip.FriendLinkManage);
                string url = Request.QueryString["_returnUrl"].ToString();
                ViewData["_returnUrl"] = url;
                Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
                var _result = _dct.TB_Link.Single(m => m.LinkID.Equals(id));
                _result.LinkState = (int)LinkState.Delete;
                _dct.SubmitChanges();
                return Redirect(url);
            }
            catch (AuthorityException ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/Link/Delete【post】”");
                return RedirectToAction("Index", "Error", new { @area = "", @MyContent = ex.Message });
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/Link/Delete【post】”");
                return RedirectToAction("Index", "Error", new { @area = "" });
            }
        }


        public ActionResult Auditing(int id)
        {
            try
            {
                this.GetAuthority(SystemMemberShip.FriendLinkManage);
                string url = Request.QueryString["_returnUrl"].ToString();
                ViewData["_returnUrl"] = url;
                Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
                var _result = _dct.TB_Link.Single(m => m.LinkID.Equals(id));
                _result.LinkState = (int)LinkState.Auditing;
                _dct.SubmitChanges();
                return Redirect(url);
            }
            catch (AuthorityException ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/Link/Auditing【post】”");
                return RedirectToAction("Index", "Error", new { @area = "", @MyContent = ex.Message });
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/Link/Auditing【post】”");
                return RedirectToAction("Index", "Error", new { @area = "" });
            }
        }
        public ActionResult ReAuditing(int id)
        {
            try
            {
                this.GetAuthority(SystemMemberShip.FriendLinkManage);
                string url = Request.QueryString["_returnUrl"].ToString();
                ViewData["_returnUrl"] = url;
                Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
                var _result = _dct.TB_Link.Single(m => m.LinkID.Equals(id));
                _result.LinkState = (int)LinkState.Editing;
                _dct.SubmitChanges();
                return Redirect(url);
            }
            catch (AuthorityException ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/Link/ReAuditing【post】”");
                return RedirectToAction("Index", "Error", new { @area = "", @MyContent = ex.Message });
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/Link/ReAuditing【post】”");
                return RedirectToAction("Index", "Error", new { @area = "" });
            }
        }
    }
}
