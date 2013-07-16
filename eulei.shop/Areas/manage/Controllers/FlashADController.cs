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
    public class FlashADController : MyController
    {

        [Paging(PageSize = 15, VaryByParams = new string[] { "FlashADXMLType", "FlashADTitle" })]
        public ActionResult Index(string FlashADTitle, int? FlashADXMLType)
        {
            try
            {
                this.GetAuthority(SystemMemberShip.FlashADManage);
                Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
                string _title = string.IsNullOrEmpty(FlashADTitle) ? "" : FlashADTitle;
                int _typeID = FlashADXMLType.HasValue ? FlashADXMLType.Value : 0;
                if (_typeID == 0)
                {
                    ViewData.Model = _dct.TB_FlashADXML
                        .Where(m => !m.FlashADXMLState.Equals((int)AdvertisementState.Delete)
                        && m.FlashADXMLTitle.Contains(_title))
                        .OrderByDescending(m => m.FlashADXMLID);
                }
                else
                {
                    ViewData.Model = _dct.TB_FlashADXML
        .Where(m => !m.FlashADXMLState.Equals((int)AdvertisementState.Delete)
        && m.FlashADXMLTitle.Contains(_title)
        && m.FlashADXMLType.Equals(_typeID))
        .OrderByDescending(m => m.FlashADXMLID);
                }
                return View();
            }
            catch (AuthorityException ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/FlashAD/Index”");
                return RedirectToAction("Index", "Error", new { @area = "", @MyContent = ex.Message });
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/FlashAD/Index”");
                return RedirectToAction("Index", "Error", new { @area = "" });
            }
        }
        [Paging(PageSize = 15, VaryByParams = new string[] { "FlashADXMLType", "FlashADTitle" })]
        public ActionResult Search(string FlashADTitle, int? FlashADXMLType)
        {
            System.Web.Routing.RouteValueDictionary _returns = new System.Web.Routing.RouteValueDictionary();
            // _returns.Add("area","manage");
            try
            {
                this.GetAuthority(SystemMemberShip.FlashADManage);
                string _title = string.IsNullOrEmpty(FlashADTitle) ? "" : FlashADTitle;
                int _typeID = FlashADXMLType.HasValue ? FlashADXMLType.Value : 0;
                if (_typeID == 0)
                {
                    _returns.Add("FlashADTitle", _title);
                }
                else
                {
                    _returns.Add("FlashADTitle", _title);
                    _returns.Add("FlashADXMLType", _typeID);
                }

            }
            catch (AuthorityException ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/FlashAD/Search”");
                return RedirectToAction("Index", "Error", new { @area = "", @MyContent = ex.Message });
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/FlashAD/Search”");
                return RedirectToAction("Index", "Error", new { @area = "" });
            }
            return RedirectToAction("Index", "FlashAD", _returns);
        }
        //
        // GET: /manage/Article/Create

        public ActionResult Create()
        {
            try
            {
                this.GetAuthority(SystemMemberShip.FlashADManage);
                string url = Request.QueryString["_returnUrl"].ToString();
                ViewData["_returnUrl"] = url;
                return View();
            }
            catch (AuthorityException ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/FlashAD/Create”");
                return RedirectToAction("Index", "Error", new { @area = "", @MyContent = ex.Message });
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/FlashAD/Create”");
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
                this.GetAuthority(SystemMemberShip.FlashADManage);
                string url = collection["_returnUrl"].ToString();
                Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
                TB_FlashADXML _result = new TB_FlashADXML();
                _result.FlashADXMLTitle = collection["FlashADXMLTitle"].ToString();
                _result.FlashADXMLPicUrl = collection["FlashADXMLPicUrl"].ToString();
                _result.FlashADXMLRedirectUrl = collection["FlashADXMLRedirectUrl"].ToString();
                _result.FlashADXMLRemindDate = DateTime.Parse(collection["FlashADXMLRemindDate"].ToString());
                _result.FlashADXMLState = (int)AdvertisementState.Editing;
                _result.FlashADXMLType = int.Parse(collection["FlashADXMLType"].ToString());
                /////////////////////////////////////
                _dct.TB_FlashADXML.InsertOnSubmit(_result);
                _dct.SubmitChanges();
                return Redirect(url);
            }
            catch (AuthorityException ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/FlashAD/Create【post】”");
                return RedirectToAction("Index", "Error", new { @area = "", @MyContent = ex.Message });
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/FlashAD/Create【post】”");
                return RedirectToAction("Index", "Error", new { @area = "" });
            }
        }

        //
        // GET: /manage/Article/Edit/5

        public ActionResult Edit(int id)
        {
            try
            {
                this.GetAuthority(SystemMemberShip.FlashADManage);
                string url = Request.QueryString["_returnUrl"].ToString();
                ViewData["_returnUrl"] = url;

                Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
                ViewData.Model = _dct.TB_FlashADXML.Single(m => m.FlashADXMLID.Equals(id));
                return View();
            }
            catch (AuthorityException ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/FlashAD/Edit”");
                return RedirectToAction("Index", "Error", new { @area = "", @MyContent = ex.Message });
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/FlashAD/Edit”");
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
                this.GetAuthority(SystemMemberShip.FlashADManage);
                string url = collection["_returnUrl"].ToString();

                Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
                var _result = _dct.TB_FlashADXML.Single(m => m.FlashADXMLID.Equals(id));
                _result.FlashADXMLTitle = collection["FlashADXMLTitle"].ToString();
                _result.FlashADXMLPicUrl = collection["FlashADXMLPicUrl"].ToString();
                _result.FlashADXMLRedirectUrl = collection["FlashADXMLRedirectUrl"].ToString();
                _result.FlashADXMLRemindDate = DateTime.Parse(collection["FlashADXMLRemindDate"].ToString());
                _result.FlashADXMLState = (int)AdvertisementState.Editing;
                _result.FlashADXMLType = int.Parse(collection["FlashADXMLType"].ToString());
                //////////////////////////////////////////
                _dct.SubmitChanges();
                return Redirect(url);
            }
            catch (AuthorityException ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/FlashAD/Edit【post】”");
                return RedirectToAction("Index", "Error", new { @area = "", @MyContent = ex.Message });
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/FlashAD/Edit【post】”");
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
                this.GetAuthority(SystemMemberShip.FlashADManage);
                string url = Request.QueryString["_returnUrl"].ToString();
                ViewData["_returnUrl"] = url;
                Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
                var _result = _dct.TB_FlashADXML.Single(m => m.FlashADXMLID.Equals(id));
                _result.FlashADXMLState = (int)AdvertisementState.Delete;
                _dct.SubmitChanges();
                return Redirect(url);
            }
            catch (AuthorityException ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/FlashAD/Delete【post】”");
                return RedirectToAction("Index", "Error", new { @area = "", @MyContent = ex.Message });
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/FlashAD/Delete【post】”");
                return RedirectToAction("Index", "Error", new { @area = "" });
            }
        }


        public ActionResult Auditing(int id)
        {
            try
            {
                this.GetAuthority(SystemMemberShip.FlashADManage);
                string url = Request.QueryString["_returnUrl"].ToString();
                ViewData["_returnUrl"] = url;
                Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
                var _result = _dct.TB_FlashADXML.Single(m => m.FlashADXMLID.Equals(id));
                _result.FlashADXMLState = (int)AdvertisementState.Auditing;
                _dct.SubmitChanges();
                return Redirect(url);
            }
            catch (AuthorityException ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/FlashAD/Auditing【post】”");
                return RedirectToAction("Index", "Error", new { @area = "", @MyContent = ex.Message });
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/FlashAD/Auditing【post】”");
                return RedirectToAction("Index", "Error", new { @area = "" });
            }
        }
        public ActionResult ReAuditing(int id)
        {
            try
            {
                this.GetAuthority(SystemMemberShip.FlashADManage);
                string url = Request.QueryString["_returnUrl"].ToString();
                ViewData["_returnUrl"] = url;
                Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
                var _result = _dct.TB_FlashADXML.Single(m => m.FlashADXMLID.Equals(id));
                _result.FlashADXMLState = (int)AdvertisementState.Editing;
                _dct.SubmitChanges();
                return Redirect(url);
            }
            catch (AuthorityException ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/FlashAD/ReAuditing【post】”");
                return RedirectToAction("Index", "Error", new { @area = "", @MyContent = ex.Message });
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/FlashAD/ReAuditing【post】”");
                return RedirectToAction("Index", "Error", new { @area = "" });
            }
        }
    }
}
