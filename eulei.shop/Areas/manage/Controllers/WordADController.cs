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
    public class WordADController : MyController
    {

        // GET: /manage/Article/
        [Paging(PageSize = 15, VaryByParams = new string[] { "AdvertisementType", "AdvertisementTitle" })]
        public ActionResult Index(string AdvertisementTitle, int? AdvertisementType)
        {
            try
            {                
                this.GetAuthority(SystemMemberShip.WordADManage);
                Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
                string _title = string.IsNullOrEmpty(AdvertisementTitle) ? "" : AdvertisementTitle;
                int _typeID = AdvertisementType.HasValue ? AdvertisementType.Value : 0;
                if (_typeID == 0)
                {
                    ViewData.Model = _dct.TB_Advertisement
                        .Where(m => !m.AdvertisementState.Equals((int)AdvertisementState.Delete)
                        && m.AdvertisementTitle.Contains(_title))
                        .OrderByDescending(m => m.AdvertisementID);
                }
                else
                {
                    ViewData.Model = _dct.TB_Advertisement
               .Where(m => !m.AdvertisementState.Equals((int)AdvertisementState.Delete)
               && m.AdvertisementTitle.Contains(_title)
               && m.AdvertisementType.Equals(_typeID))
               .OrderByDescending(m => m.AdvertisementID);
                }
                return View();

            }
            catch (AuthorityException ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/WordAD/Index”");
                return RedirectToAction("Index", "Error", new { @area = "", @MyContent = ex.Message });
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/WordAD/Index”");
                return RedirectToAction("Index", "Error", new { @area = "" });
            }
        }
        [Paging(PageSize = 15, VaryByParams = new string[] { "AdvertisementType", "AdvertisementTitle" })]
        //[HttpPost]
        public ActionResult Search(string AdvertisementTitle, int? AdvertisementType)
        {
            System.Web.Routing.RouteValueDictionary _returns = new System.Web.Routing.RouteValueDictionary();
            // _returns.Add("area","manage");
            try
            {
                this.GetAuthority(SystemMemberShip.WordADManage);
                string _title = string.IsNullOrEmpty(AdvertisementTitle) ? "" : AdvertisementTitle;
                int _typeID = AdvertisementType.HasValue ? AdvertisementType.Value : 0;
                if (_typeID == 0)
                {
                    _returns.Add("AdvertisementTitle", _title);
                }
                else
                {
                    _returns.Add("AdvertisementTitle", _title);
                    _returns.Add("AdvertisementType", _typeID);
                }

            }
            catch (AuthorityException ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/WordAD/Search【post】”");
                return RedirectToAction("Index", "Error", new { @area = "", @MyContent = ex.Message });
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/WordAD/Search【post】”");
                return RedirectToAction("Index", "Error", new { @area = "" });
            }
            return RedirectToAction("Index", "WordAD", _returns);
        }
        //
        // GET: /manage/Article/Create

        public ActionResult Create()
        {
            try
            {
                this.GetAuthority(SystemMemberShip.WordADManage);
                string url = Request.QueryString["_returnUrl"].ToString();
                ViewData["_returnUrl"] = url;
                return View();
            }
            catch (AuthorityException ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/WordAD/Create”");
                return RedirectToAction("Index", "Error", new { @area = "", @MyContent = ex.Message });
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/WordAD/Create”");
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
                this.GetAuthority(SystemMemberShip.WordADManage);
                string url = collection["_returnUrl"].ToString();
                Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
                TB_Advertisement _result = new TB_Advertisement();
                _result.AdvertisementTitle = collection["AdvertisementTitle"].ToString();
                _result.AdvertisementUrl = collection["AdvertisementUrl"].ToString();
                _result.AdvertisementRemindDate = DateTime.Parse(collection["AdvertisementRemindDate"].ToString());
                _result.AdvertisementState = (int)AdvertisementState.Editing;
                _result.AdvertisementType = int.Parse(collection["AdvertisementType"].ToString());
                /////////////////////////////////////
                _dct.TB_Advertisement.InsertOnSubmit(_result);
                _dct.SubmitChanges();
                return Redirect(url);
            }
            catch (AuthorityException ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/WordAD/Create【post】”");
                return RedirectToAction("Index", "Error", new { @area = "", @MyContent = ex.Message });
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/WordAD/Create【post】”");
                return RedirectToAction("Index", "Error", new { @area = "" });
            }
        }

        //
        // GET: /manage/Article/Edit/5

        public ActionResult Edit(int id)
        {
            try
            {
                this.GetAuthority(SystemMemberShip.WordADManage);
                string url = Request.QueryString["_returnUrl"].ToString();
                ViewData["_returnUrl"] = url;

                Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
                ViewData.Model = _dct.TB_Advertisement.Single(m => m.AdvertisementID.Equals(id));
                return View();
            }
            catch (AuthorityException ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/WordAD/Edit”");
                return RedirectToAction("Index", "Error", new { @area = "", @MyContent = ex.Message });
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/WordAD/Edit”");
                return RedirectToAction("Index", "Error", new { @area = "" });
            }
        }


        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                this.GetAuthority(SystemMemberShip.WordADManage);
                string url = collection["_returnUrl"].ToString();

                Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
                var _result = _dct.TB_Advertisement.Single(m => m.AdvertisementID.Equals(id));
                _result.AdvertisementTitle = collection["AdvertisementTitle"].ToString();
                _result.AdvertisementUrl = collection["AdvertisementUrl"].ToString();
                _result.AdvertisementRemindDate = DateTime.Parse(collection["AdvertisementRemindDate"].ToString());
                _result.AdvertisementState = (int)AdvertisementState.Editing;
                _result.AdvertisementType = int.Parse(collection["AdvertisementType"].ToString());
                //////////////////////////////////////////
                _dct.SubmitChanges();
                return Redirect(url);
            }
            catch (AuthorityException ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/WordAD/Edit【post】”");
                return RedirectToAction("Index", "Error", new { @area = "", @MyContent = ex.Message });
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/WordAD/Edit【post】”");
                return RedirectToAction("Index", "Error", new { @area = "" });
            }
        }


        public ActionResult Delete(int id)
        {
            try
            {
                this.GetAuthority(SystemMemberShip.WordADManage);
                string url = Request.QueryString["_returnUrl"].ToString();
                ViewData["_returnUrl"] = url;
                Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
                var _result = _dct.TB_Advertisement.Single(m => m.AdvertisementID.Equals(id));
                _result.AdvertisementState = (int)AdvertisementState.Delete;
                _dct.SubmitChanges();
                return Redirect(url);
            }
            catch (AuthorityException ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/WordAD/Delete【post】”");
                return RedirectToAction("Index", "Error", new { @area = "", @MyContent = ex.Message });
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/WordAD/Delete【post】”");
                return RedirectToAction("Index", "Error", new { @area = "" });
            }
        }



        public ActionResult Auditing(int id)
        {
            try
            {
                this.GetAuthority(SystemMemberShip.WordADManage);
                string url = Request.QueryString["_returnUrl"].ToString();
                ViewData["_returnUrl"] = url;
                Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
                var _result = _dct.TB_Advertisement.Single(m => m.AdvertisementID.Equals(id));
                _result.AdvertisementState = (int)AdvertisementState.Auditing;
                _dct.SubmitChanges();
                return Redirect(url);
            }
            catch (AuthorityException ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/WordAD/Auditing【post】”");
                return RedirectToAction("Index", "Error", new { @area = "", @MyContent = ex.Message });
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/WordAD/Auditing【post】”");
                return RedirectToAction("Index", "Error", new { @area = "" });
            }
        }
        public ActionResult ReAuditing(int id)
        {
            try
            {
                this.GetAuthority(SystemMemberShip.WordADManage);
                string url = Request.QueryString["_returnUrl"].ToString();
                ViewData["_returnUrl"] = url;
                Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
                var _result = _dct.TB_Advertisement.Single(m => m.AdvertisementID.Equals(id));
                _result.AdvertisementState = (int)AdvertisementState.Editing;
                _dct.SubmitChanges();
                return Redirect(url);
            }
            catch (AuthorityException ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/WordAD/ReAuditing【post】”");
                return RedirectToAction("Index", "Error", new { @area = "", @MyContent = ex.Message });
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/WordAD/ReAuditing【post】”");
                return RedirectToAction("Index", "Error", new { @area = "" });
            }
        }
    }
}
