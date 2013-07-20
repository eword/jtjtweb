using eulei.shop.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace eulei.shop
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{*allsvc}", new { allsvc = @".*\.svc(/.*)?" });
            routes.IgnoreRoute("{*allaspx}", new { allaspx = @".*\.aspx(/.*)?" });
            routes.IgnoreRoute("{*allrpt}", new { allaspx = @".*\.rpt(/.*)?" });
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },// Parameter defaults
                new[] { "eulei.shop.Controllers" }
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
           
        }
        protected void Session_Start(Object sender, EventArgs e)
        {
            try
            {
                Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
                if (User.Identity.IsAuthenticated)
                {
                    #region 读取账号权限
                    byte[] _userInfoArticleTypeAuthorityArray = new byte[1000];
                    byte[] _userInfoAuthorityArray = new byte[1000];                    
                    if (_dct.VW_SA_UserInfo.Where(m => m.UserName.Equals(User.Identity.Name)).Count() > 0)
                    {
                        var _user = _dct.VW_SA_UserInfo.Single(m => m.UserName.Equals(User.Identity.Name));
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
                }                
                Application["CustomerCount"] = _dct.TB_SiteInfo.First().CustomerCount++;
                _dct.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void Session_End(Object sender, EventArgs e)
        {
        }
    }
}