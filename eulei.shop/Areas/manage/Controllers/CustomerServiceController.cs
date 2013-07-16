using eulei.shop.Code;
using eulei.shop.Models;
using Q42.Wheels.Mvc.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;

namespace eulei.shop.Areas.manage.Controllers
{
    [Authorize(Roles = "Logins")]
    [HandleError]
    public class CustomerServiceController : MyController
    {

        [Paging(PageSize = 10, VaryByParams = new string[] { "LetterTitle", "LetterSender", "SearchTextTEL", "LetterTypeID", "LetterAddressee", "LetterIsWriteBack" })]
        public ActionResult LetterIndex(string LetterTitle, string LetterSender, string LetterTEL, int? LetterTypeID, string LetterAddressee, string LetterIsWriteBack)
        {
            try
            {
                this.GetAuthority(SystemMemberShip.LetterManage);
                LetterTypeID = LetterTypeID.HasValue ? LetterTypeID.Value : 0;
                LetterIsWriteBack = string.IsNullOrEmpty(LetterIsWriteBack) ? "0" : LetterIsWriteBack;
                bool _isWriteBack = LetterIsWriteBack.Equals("1") ? true : false;
                LetterTitle = string.IsNullOrEmpty(LetterTitle) ? "" : LetterTitle;
                LetterSender = string.IsNullOrEmpty(LetterSender) ? "" : LetterSender;
                LetterTEL = string.IsNullOrEmpty(LetterTEL) ? "" : LetterTEL;
                LetterAddressee = string.IsNullOrEmpty(LetterAddressee) ? "" : LetterAddressee;
                Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
                if (LetterTypeID.Value == 0)
                {
                    ViewData.Model = _dct.VW_Letter
                       .Where(m => m.LetterSender.Contains(LetterSender)
                            && m.LetterTitle.Contains(LetterTitle)
                           && m.LetterTEL.Contains(LetterTEL)
                           && m.LetterIsWriteBack.Equals(_isWriteBack)
                            && m.LetterAddressee.Contains(LetterAddressee)
                           )
                        .OrderByDescending(m => m.LetterTimeSend);
                }
                else
                {
                    ViewData.Model = _dct.VW_Letter
                       .Where(m => m.LetterSender.Contains(LetterSender)
                            && m.LetterTitle.Contains(LetterTitle)
                           && m.LetterTEL.Contains(LetterTEL)
                                && m.LetterIsWriteBack.Equals(_isWriteBack)
                            && m.LetterAddressee.Contains(LetterAddressee)
                            && m.LetterTypeID.Equals(LetterTypeID.Value)
                           )
                        .OrderByDescending(m => m.LetterTimeSend);
                }
                ViewBag.LetterTypeID = new SelectList((from _r in _dct.TB_Dictionary where _r.DictionaryParentID.Equals(3) orderby _r.DictionaryOrder select _r), "DictionaryID", "DictionaryDescription");
                ViewBag.LetterAddressee = new SelectList((from _r in _dct.SA_Framework orderby _r.FrameworkLevel, _r.FrameworkOrder select _r), "FrameworkName", "FrameworkName");
                return View();
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/CustomerService/LetterIndex”");
                return RedirectToAction("Index", "Error", new { @area = "", @MyContent = ex.Message });
            }
        }

        [Paging(PageSize = 10, VaryByParams = new string[] { "LetterTitle", "LetterSender", "LetterTEL", "LetterTypeID", "LetterAddressee", "LetterIsWriteBack" })]
        public ActionResult SearchLetterIndex(string LetterTitle, string LetterSender, string LetterTEL, int? LetterTypeID, string LetterAddressee, string LetterIsWriteBack)
        {
            System.Web.Routing.RouteValueDictionary _returns = new System.Web.Routing.RouteValueDictionary();
            _returns.Add("area", "manage");
            try
            {
                this.GetAuthority(SystemMemberShip.LetterManage);
                LetterTypeID = LetterTypeID.HasValue ? LetterTypeID.Value : 0;
                LetterIsWriteBack = string.IsNullOrEmpty(LetterIsWriteBack) ? "0" : LetterIsWriteBack;
                LetterTitle = string.IsNullOrEmpty(LetterTitle) ? "" : LetterTitle;
                LetterSender = string.IsNullOrEmpty(LetterSender) ? "" : LetterSender;
                LetterTEL = string.IsNullOrEmpty(LetterTEL) ? "" : LetterTEL;
                LetterAddressee = string.IsNullOrEmpty(LetterAddressee) ? "" : LetterAddressee;
                _returns.Add("LetterSender", LetterSender);
                _returns.Add("LetterTEL", LetterTEL);
                _returns.Add("LetterTypeID", LetterTypeID);
                _returns.Add("LetterAddressee", LetterAddressee);
                _returns.Add("LetterTitle", LetterTitle);
                _returns.Add("LetterIsWriteBack", LetterIsWriteBack);
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/CustomerService/SearchLetterIndex”");
                return RedirectToAction("Index", "Error", new { @area = "", @MyContent = ex.Message });
            }
            return RedirectToAction("LetterIndex", "CustomerService", _returns);
        }


        public ActionResult EditLetter(Guid id)
        {
            try
            {
                this.GetAuthority(SystemMemberShip.LetterManage);
                string url = Request.QueryString["_returnUrl"].ToString();
                ViewData["_returnUrl"] = url;

                Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
                ViewData.Model = _dct.TB_Letter.Single(m => m.LetterID.Equals(id));
                if (Request.IsAjaxRequest())
                    return PartialView();
                else
                    return View();
            }
            catch (AuthorityException ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/CustomerService/EditLetter”");
                return RedirectToAction("Index", "Error", new { @area = "", @MyContent = ex.Message });
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/CustomerService/EditLetter”");
                return RedirectToAction("Index", "Error", new { @area = "" });
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditLetter(Guid id, FormCollection collection)
        {
            try
            {
                this.GetAuthority(SystemMemberShip.LetterManage);
                string url = collection["_returnUrl"].ToString();
                Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
                var _user = _dct.VW_SA_UserInfo.Single(m => m.UserName.Equals(User.Identity.Name));
                var _result = _dct.TB_Letter.Single(m => m.LetterID.Equals(id));
                _result.LetterWriteBackContent = collection["LetterWriteBackContent"].ToString();
                _result.LetterWriteBackUserID = _user.UserId;
                _result.LetterIsWriteBack = true;
                _result.LetterTimeWriteBack = DateTime.Now;
                _dct.SubmitChanges();
                return Redirect(url);
            }
            catch (AuthorityException ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/CustomerService/EditLetter【post】”");
                return RedirectToAction("Index", "Error", new { @area = "", @MyContent = ex.Message });
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/CustomerService/EditLetter【post】”");
                return RedirectToAction("Index", "Error", new { @area = "" });
            }
        }

        public ActionResult PublicLetter(Guid id)
        {
            try
            {
                this.GetAuthority(SystemMemberShip.LetterManage);
                string url = Request.QueryString["_returnUrl"].ToString();
                ViewData["_returnUrl"] = url;
                Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
                var _result = _dct.TB_Letter.Single(m => m.LetterID.Equals(id));
                _result.LetterIsPublic = !_result.LetterIsPublic;
                _dct.SubmitChanges();
                return Redirect(url);
            }
            catch (AuthorityException ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/CustomerService/PublicLetter【post】”");
                return RedirectToAction("Index", "Error", new { @area = "", @MyContent = ex.Message });
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/CustomerService/PublicLetter【post】”");
                return RedirectToAction("Index", "Error", new { @area = "" });
            }
        }

        public ActionResult DeleteLetter(Guid id)
        {
            try
            {
                this.GetAuthority(SystemMemberShip.LetterManage);
                string url = Request.QueryString["_returnUrl"].ToString();
                ViewData["_returnUrl"] = url;
                Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
                var _result = _dct.TB_Letter.Single(m => m.LetterID.Equals(id));
                _dct.TB_Letter.DeleteOnSubmit(_result);
                _dct.SubmitChanges();
                return Redirect(url);
            }
            catch (AuthorityException ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/CustomerService/DeleteLetter【post】”");
                return RedirectToAction("Index", "Error", new { @area = "", @MyContent = ex.Message });
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/CustomerService/DeleteLetter【post】”");
                return RedirectToAction("Index", "Error", new { @area = "" });
            }
        }


    }
}
