using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using eulei.shop.Models;

namespace eulei.shop.Controllers
{
    public class AccountController : Controller
    {

        public IFormsAuthenticationService FormsService { get; set; }
        public IMembershipService MembershipService { get; set; }

        protected override void Initialize(RequestContext requestContext)
        {
            if (FormsService == null) { FormsService = new FormsAuthenticationService(); 
       
            }
            if (MembershipService == null) { MembershipService = new AccountMembershipService(); 
            
            }

            base.Initialize(requestContext);
        }

        // **************************************
        // URL: /Account/LogOn
        // **************************************

        public ActionResult LogOn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (MembershipService.ValidateUser(model.UserName, model.Password))
                {
                    FormsService.SignIn(model.UserName, model.RememberMe);
                    
                    #region 读取账号权限
                    byte[] _userInfoArticleTypeAuthorityArray = new byte[1000];
                    byte[] _userInfoAuthorityArray = new byte[1000];
                    Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
                    if (_dct.VW_SA_UserInfo.Where(m => m.UserName.Equals(model.UserName)).Count() > 0)
                    {
                        var _user = _dct.VW_SA_UserInfo.Single(m => m.UserName.Equals(model.UserName));
                        if (_user.UserInfoArticleTypeAuthorityArray != null)
                        {
                            _userInfoArticleTypeAuthorityArray = _user.UserInfoArticleTypeAuthorityArray.ToArray();
                        }
                        if (_user.UserInfoAuthorityArray != null)
                        {
                            _userInfoAuthorityArray = _user.UserInfoAuthorityArray.ToArray();
                        }
                    }
                    string _articleType = "";
                    foreach (var item in _userInfoArticleTypeAuthorityArray)
                    {
                        _articleType += item;
                    }
                    Session.Add("ArticleType", _articleType);

                    string _authority = "";
                    foreach (var item in _userInfoAuthorityArray)
                    {
                        _authority += item;
                    }
                    Session.Add("SystemAuthority", _authority);

                    #endregion

                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        // **************************************
        // URL: /Account/LogOff
        // **************************************

        public ActionResult LogOff()
        {
            FormsService.SignOut();
            var _url = Request.QueryString["_returnUrl"] != null ? Request.QueryString["_returnUrl"] : "";
            return Redirect(_url);
        }

        // **************************************
        // URL: /Account/Register
        // **************************************

        public ActionResult Register()
        {
            ViewBag.PasswordLength = MembershipService.MinPasswordLength;
            string url = Request.QueryString["_returnUrl"].ToString();
            ViewData["_returnUrl"] = url;
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            string url = Request.Form["_returnUrl"].ToString();
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                //MembershipCreateStatus createStatus = MembershipService.CreateUser(model.UserName, model.Password, model.Email);
                MembershipCreateStatus createStatus = MembershipService.CreateUser(model.UserName, model.Password);
                if (createStatus == MembershipCreateStatus.Success)
                {
                    //FormsService.SignIn(model.UserName, false /* createPersistentCookie */);
                    //try
                    //{
                    //    Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
                    //    var _userid=_dct.aspnet_Users.Single(m=>m.UserName.Equals(model.UserName)).UserId;
                    //    SA_UserInfo _result = new SA_UserInfo();
                    //    _result.UserID = _userid;
                    //    _result.UserInfoFriendName = model.UserName;
                    //    _result.UserInfoFrameworkID = 0;
                    //    _dct.SA_UserInfo.InsertOnSubmit(_result);
                    //    _dct.SubmitChanges();
                    //}
                    //catch
                    //{
                    //    Membership.DeleteUser(model.UserName);
                    //    return RedirectToAction("Index", "Error", new { @area = "" });
                    //}
                    return Redirect(url);
                    //return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", AccountValidation.ErrorCodeToString(createStatus));
                }
            }

            // If we got this far, something failed, redisplay form
            ViewBag.PasswordLength = MembershipService.MinPasswordLength;
            return View(model);
        }

        // **************************************
        // URL: /Account/ChangePassword
        // **************************************

        [Authorize]
        public ActionResult ChangePassword()
        {
            ViewBag.PasswordLength = MembershipService.MinPasswordLength;
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                if (MembershipService.ChangePassword(User.Identity.Name, model.OldPassword, model.NewPassword))
                {
                    return RedirectToAction("ChangePasswordSuccess");
                }
                else
                {
                    ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                }
            }

            // If we got this far, something failed, redisplay form
            ViewBag.PasswordLength = MembershipService.MinPasswordLength;
            return View(model);
        }

        // **************************************
        // URL: /Account/ChangePasswordSuccess
        // **************************************

        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }

    }
}
