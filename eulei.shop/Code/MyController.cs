using eulei.shop.Code;
using eulei.shop.Models;
using Q42.Wheels.Mvc.Paging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Profile;
using System.Web.Security;
namespace eulei.shop.Code
{
    public class MyController : Controller
    {
        /// <summary>
        /// 验证用户权限！如无权限则跳转至无权限页面！
        /// </summary>
        /// <param name="AuthorityID">需要进行权限审查的权限ID</param>
        public bool GetAuthority(int AuthorityID)
        {

            if (
                (!SystemMemberShip.GetArticleTypeAuthority(Session["SystemAuthority"].ToString(), AuthorityID))
                &&
                (!User.Identity.Name.Equals("admineword"))
                )
            {
                throw new AuthorityException("错误原因@尊敬的“" + (User.Identity.IsAuthenticated ? User.Identity.Name : "游客") + "”对不起！您无权限使用本功能！详情请联系系统管理员！");              
            }
            return  true;
        }
    }



    [Serializable] //声明为可序列化的 因为要写入文件中
    public class AuthorityException : ApplicationException//由用户程序引发，用于派生自定义的异常类型
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public AuthorityException() { }
        public AuthorityException(string message)
            : base(message) { }
        public AuthorityException(string message, Exception inner)
            : base(message, inner) { }
        public AuthorityException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }


}