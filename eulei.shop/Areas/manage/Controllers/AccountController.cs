using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using eulei.shop.Models;
using Q42.Wheels.Mvc.Paging;
using System.Web.Profile;
using eulei.shop.Code;
using System.Web.Security;
namespace eulei.shop.Areas.manage.Controllers
{
    [Authorize(Roles = "Logins")]
    [HandleError]
    public class AccountController : MyController
    {
        //
        // GET: /manage/Account/        
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 用户信息
        /// </summary>
        /// <returns></returns>
        [Paging(PageSize = 15)]
        public ActionResult UserInfo()
        {
            try
            {
                this.GetAuthority(SystemMemberShip.SystemUserManage);
                Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
                ViewData.Model = _dct.VW_SA_UserInfo;
                return View();
            }
            catch (AuthorityException ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/Account/UserInfo”");
                return RedirectToAction("Index", "Error", new { @area = "", @MyContent=ex.Message });
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/Account/UserInfo”");
                return RedirectToAction("Index", "Error", new { @area = "" });
            }

        }
        /// <summary>
        /// 添加用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult CreateUserInfo(Guid id)
        {
            try
            {
                this.GetAuthority(SystemMemberShip.SystemUserManage);
                string url = Request.QueryString["_returnUrl"].ToString();
                ViewData["_returnUrl"] = url;
                Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
                ViewData.Model = _dct.VW_SA_UserInfo.Single(m => m.UserId.Equals(id));
                return View();
            }
            catch (AuthorityException ex)
            {              
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/Account/CreateUserInfo”");
                return RedirectToAction("Index", "Error", new { @area = "", @MyContent = ex.Message });
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/Account/CreateUserInfo”");
                return RedirectToAction("Index", "Error", new { @area = "" });
            }
        }
        /// <summary>
        /// 添加用户信息
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateUserInfo(FormCollection collection)
        {
            try
            {
                this.GetAuthority(SystemMemberShip.SystemUserManage);
                string url = collection["_returnUrl"].ToString();
                Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
                SA_UserInfo _result = new SA_UserInfo();
                _result.UserInfoFriendName = collection["UserInfoFriendName"].ToString();
                _result.UserID = Guid.Parse(collection["UserId"].ToString());
                _result.UserInfoFrameworkID = int.Parse(collection["UserInfoFrameworkID"].ToString());
                _result.UserInfoTEL = collection["UserInfoTEL"].ToString();
                MembershipUser _user = Membership.GetUser(collection["UserName"].ToString());
                ProfileBase profile = ProfileBase.Create(_user.UserName, true);
                profile.SetPropertyValue("FriendlyName", _result.UserInfoFriendName);
                profile.SetPropertyValue("TEL", _result.UserInfoTEL);
                profile.Save();
                _dct.SA_UserInfo.InsertOnSubmit(_result);
                _dct.SubmitChanges();
                return Redirect(url);
            }
            catch (AuthorityException ex)
            {               
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/Account/CreateUserInfo【Post】”");
                return RedirectToAction("Index", "Error", new { @area = "", @MyContent = ex.Message });
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/Account/CreateUserInfo【Post】”");
                return RedirectToAction("Index", "Error", new { @area = "" });
            }
        }
        /// <summary>
        /// 编辑用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult EditUserInfo(Guid id)
        {
            try
            {
                this.GetAuthority(SystemMemberShip.SystemUserManage);
                string url = Request.QueryString["_returnUrl"].ToString();
                ViewData["_returnUrl"] = url;
                Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
                ViewData.Model = _dct.VW_SA_UserInfo.Single(m => m.UserId.Equals(id));
                return View();
            }
            catch (AuthorityException ex)
            {
          
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/Account/EditUserInfo”");
                return RedirectToAction("Index", "Error", new { @area = "", @MyContent = ex.Message });
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/Account/EditUserInfo”");
                return RedirectToAction("Index", "Error", new { @area = "" });
            }
        }
        /// <summary>
        ///编辑用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditUserInfo(Guid id, FormCollection collection)
        {
            try
            {
                this.GetAuthority(SystemMemberShip.SystemUserManage);
                string url = collection["_returnUrl"].ToString();
                Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
                var _result = _dct.SA_UserInfo.Single(m => m.UserID.Equals(id));
                var _user = _dct.VW_SA_UserInfo.Single(m => m.UserId.Equals(id));
                _result.UserInfoFriendName = collection["UserInfoFriendName"].ToString();
                _result.UserInfoFrameworkID = int.Parse(collection["UserInfoFrameworkID"].ToString());
                _result.UserInfoTEL = collection["UserInfoTEL"].ToString();
                ProfileBase profile = ProfileBase.Create(_user.UserName, true);
                profile.SetPropertyValue("FriendlyName", _result.UserInfoFriendName);
                profile.SetPropertyValue("TEL", _result.UserInfoTEL);
                profile.Save();
                _dct.SubmitChanges();
                return Redirect(url);
            }
            catch (AuthorityException ex)
            {
               
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/Account/EditUserInfo【Post】”");
                return RedirectToAction("Index", "Error", new { @area = "", @MyContent = ex.Message });
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/Account/EditUserInfo【Post】”");
                return RedirectToAction("Index", "Error", new { @area = "" });
            }
        }
        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult ReSetPassWord(string id)
        {
            try
            {
                this.GetAuthority(SystemMemberShip.SystemUserManage);
                Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
                var _return = _dct.VW_SA_UserInfo.Single(m => m.UserName.Equals(id));
                ViewBag.UserName = _return.UserName;
                ViewBag.UserInfoFriendName = _return.UserInfoFriendName;
                if (Request.IsAjaxRequest())
                    return PartialView();
                else
                    return View();
            }
            catch (AuthorityException ex)
            {
        
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/Account/ReSetPassWord”");
                return RedirectToAction("Index", "Error", new { @area = "", @MyContent = ex.Message });
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/Account/ReSetPassWord”");
                return RedirectToAction("Index", "Error", new { @area = "" });
            }
        }

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="id"></param>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ReSetPassWord(string id, FormCollection collection)
        {
            try
            {
                this.GetAuthority(SystemMemberShip.SystemUserManage);
                MembershipUser _user = Membership.GetUser(id);
                if (collection["PassWord"] != null)
                {
                    if (_user.IsLockedOut)
                    {
                        _user.UnlockUser();
                    }
                    if (_user.ChangePassword(_user.ResetPassword(), collection["PassWord"].ToString().Trim()))
                    {
                        return Content("<script >alert('重置“" + id + "”密码成功！新密码为：" + collection["PassWord"].ToString().Trim() + "');</script >", "text/html");
                    }
                    else
                    {
                        return Content("<script >alert('重置“" + id + "”密码失败！');</script >", "text/html");
                    }

                }
                return Content("<script >alert('重置“" + id + "”密码失败！@新密码不能为空！');</script >", "text/html");
            }
            catch (AuthorityException ex)
            {
         
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/Account/ReSetPassWord【Post】”");
                return RedirectToAction("Index", "Error", new { @area = "", @MyContent = ex.Message });
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/Account/ReSetPassWord【Post】”");
                return Content("<script >alert('失败@：" + ex.Message + "');</script >", "text/html");
            }
        }






        /// <summary>
        /// 删除用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="collection"></param>
        /// <returns></returns>
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                this.GetAuthority(SystemMemberShip.SystemUserManage);
                string url = Request.QueryString["_returnUrl"].ToString();

                Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();

                var _currentUser = _dct.VW_SA_UserInfo.Single(m => m.UserName.Equals(id));

                if (_dct.SA_UserInfo.Where(m => m.UserID.Equals(_currentUser.UserId)).Count() > 0)
                {
                    var _result = _dct.SA_UserInfo.Single(m => m.UserID.Equals(_currentUser.UserId));
                    _dct.SA_UserInfo.DeleteOnSubmit(_result);
                    _dct.SubmitChanges();
                }
                Membership.DeleteUser(id);

                return Redirect(url);
            }
            catch (AuthorityException ex)
            {
           
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/Account/Delete”");
                return RedirectToAction("Index", "Error", new { @area = "", @MyContent = ex.Message });
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/Account/Delete”");
                return RedirectToAction("Index", "Error", new { @area = "" });
            }
        }

        /// <summary>
        /// 设置角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult SetRole(string id)
        {
            try
            {
                this.GetAuthority(SystemMemberShip.SystemRoleManage);
                string url = Request.QueryString["_returnUrl"].ToString();
                ViewData["_returnUrl"] = url;
                ViewBag._UserName = id;
                List<string> _roleList = new List<string>();
                foreach (var item in Roles.GetAllRoles())
                {
                    _roleList.Add(item);
                }
                ViewBag.RoleList = _roleList;
                List<string> _userList = new List<string>();

                foreach (var item in Roles.GetRolesForUser(id))
                {
                    _userList.Add(item);
                }
                ViewBag.UserInRoleList = _userList;
            }
            catch (AuthorityException ex)
            {
    
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/Account/SetRole”");
                return RedirectToAction("Index", "Error", new { @area = "", @MyContent = ex.Message });
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/Account/SetRole”");
                return RedirectToAction("Index", "Error", new { @area = "" });
            }

            return View();
        }
        /// <summary>
        /// 设置角色
        /// </summary>
        /// <param name="id"></param>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SetRole(string id, FormCollection collection)
        {
            try
            {
                this.GetAuthority(SystemMemberShip.SystemRoleManage);
                string url = collection["_returnUrl"].ToString();
                string[] _roles = Request.Form.GetValues("Role");
                string[] _currentRoles = Roles.GetRolesForUser(id);
                string[] _clearRoles = _roles.Where(m => !m.Equals("false")).ToArray();
                var _newRoles = _clearRoles.Except(_currentRoles).ToArray();
                var _oldRoles = _currentRoles.Except(_clearRoles).ToArray();
                if (_oldRoles.Length > 0)
                {
                    Roles.RemoveUserFromRoles(id, _oldRoles);
                }
                if (_newRoles.Length > 0)
                {
                    Roles.AddUserToRoles(id, _newRoles);
                }
                var _result = this.SetUserInfoAuthority(id);

                return Redirect(url);
            }
            catch (AuthorityException ex)
            {
    
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/Account/SetRole【Post】”");
                return RedirectToAction("Index", "Error", new { @area = "", @MyContent = ex.Message });
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/Account/SetRole【Post】”");
                return RedirectToAction("Index", "Error", new { @area = "" });
            }
        }
        /// <summary>
        /// 创建角色
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateRole(FormCollection collection)
        {
            try
            {
                this.GetAuthority(SystemMemberShip.SystemRoleManage);
                if (collection["roleName"] != null)
                {
                    var _roleName = collection["roleName"].ToString();
                    if (!Roles.RoleExists(_roleName))
                    {
                        Roles.CreateRole(_roleName);
                    }
                }
                return RedirectToAction("RoleInfo", "Account", new { @area = "manage" });
            }
            catch (AuthorityException ex)
            {
        
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/Account/CreateRole”");
                return RedirectToAction("Index", "Error", new { @area = "", @MyContent = ex.Message });
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/Account/CreateRole”");
                return RedirectToAction("Index", "Error", new { @area = "" });
            }
        }
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="id"></param>
        /// <param name="collection"></param>
        /// <returns></returns>
        public ActionResult DeleteRole(string id, FormCollection collection)
        {
            try
            {
                this.GetAuthority(SystemMemberShip.SystemRoleManage);
                string[] _roles = Request.Form.GetValues("Roles");
                foreach (var item in _roles.Where(m => !m.Equals("false")))
                {
                    if (Roles.RoleExists(item))
                        Roles.DeleteRole(item);
                }
                return RedirectToAction("RoleInfo", "Account", new { @area = "manage" });
            }
            catch (AuthorityException ex)
            {
       
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/Account/DeleteRole”");
                return RedirectToAction("Index", "Error", new { @area = "", @MyContent = ex.Message });
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/Account/DeleteRole”");
                return Content("<script >alert('失败@：" + ex.Message + "');</script >", "text/html");

            }
        }
        /// <summary>
        /// 角色信息
        /// </summary>
        /// <returns></returns>
        public ActionResult RoleInfo()
        {
            try
            {
                this.GetAuthority(SystemMemberShip.SystemRoleManage);
                List<string> _roleList = new List<string>();
                foreach (var item in Roles.GetAllRoles())
                {
                    _roleList.Add(item);
                }
                ViewBag.RoleList = _roleList;
                return View();
            }
            catch (AuthorityException ex)
            {
 
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/Account/RoleInfo”");
                return RedirectToAction("Index", "Error", new { @area = "", @MyContent = ex.Message });
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/Account/RoleInfo”");
                return RedirectToAction("Index", "Error", new { @area = "" });
            }
        }

        /// <summary>
        /// 获取权限列表
        /// </summary>
        /// <returns></returns>
        public JsonResult GetJsonForAuthorityList(string id)
        {
            var _res = new JsonResult();
            try
            {
                this.GetAuthority(SystemMemberShip.SystemRoleManage);
                id = HttpUtility.UrlDecode(id, Encoding.UTF8);
                Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
                var _return = _dct.SA_AuthorityInfo.OrderBy(m => m.AuthorityParentID).OrderBy(m => m.AuthorityQueueOrder);
                var _role = _dct.VW_SA_RoleInfo.Single(m => m.RoleName.Equals(id));

                if (_dct.SA_RoleAuthority.Where(m => m.RoleID.Equals(_role.RoleId)).Count() > 0)
                {
                    byte[] _roleList = _dct.VW_SA_RoleInfo.Single(m => m.RoleName.Equals(id)).RoleAuthorityArray.ToArray();
                    List<LigeruiTreeItem> _list = new List<LigeruiTreeItem>();
                    foreach (var item in _return)
                    {
                        LigeruiTreeItem _listItem = new LigeruiTreeItem();
                        _listItem.id = item.AuthorityID.ToString();
                        _listItem.fid = item.AuthorityParentID.ToString();
                        _listItem.queueOrder = item.AuthorityQueueOrder;
                        _listItem.textcontent = item.AuthorityContent;
                        _listItem.ischecked = _roleList[item.AuthorityQueueOrder].Equals(1) ? true : false;
                        _listItem.isexpand = false;
                        _list.Add(_listItem);
                    }

                    _res.Data = _list;
                }
                else
                {
                    var _list = from _r in _return
                                select new
                                {
                                    id = _r.AuthorityID.ToString(),
                                    fid = _r.AuthorityParentID.ToString(),
                                    queueOrder = _r.AuthorityQueueOrder.ToString(),
                                    textcontent = _r.AuthorityContent,
                                    isexpand = false
                                };
                    _res.Data = _list;
                }
                _res.JsonRequestBehavior = JsonRequestBehavior.AllowGet;//允许使用GET方式获取，否则用GET获取是会报错。      

            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/Account/GetJsonForAuthorityList”");               
            }
            return _res;
        }
        /// <summary>
        /// 获取文章分类权限列表
        /// </summary>
        /// <returns></returns>
        public JsonResult GetJsonForArticleTypeList(string id)
        {
            
            var _res = new JsonResult();
            try
            {
                this.GetAuthority(SystemMemberShip.SystemRoleManage);           
                id = HttpUtility.UrlDecode(id, Encoding.UTF8);
                Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
                var _return = _dct.TB_ArticleType.Where(m=>m.ArticleTypeIsUp.Equals(true)).OrderBy(m => m.ArticleTypeParentID).OrderBy(m => m.ArticleTypeID);
                var _role = _dct.VW_SA_RoleInfo.Single(m => m.RoleName.Equals(id));

                if (_dct.SA_RoleAuthority.Where(m => m.RoleID.Equals(_role.RoleId)).Count() > 0)
                {
                    byte[] _roleArticleTypeList = _dct.VW_SA_RoleInfo.Single(m => m.RoleName.Equals(id)).RoleArticleTypeAuthorityArray.ToArray();
                    List<LigeruiTreeItem> _list = new List<LigeruiTreeItem>();
                    foreach (var item in _return)
                    {
                        LigeruiTreeItem _listItem = new LigeruiTreeItem();
                        _listItem.id = item.ArticleTypeID.ToString();
                        _listItem.fid = item.ArticleTypeParentID.ToString();
                        _listItem.textcontent = item.ArticleTypeName;
                        _listItem.ischecked = _roleArticleTypeList[item.ArticleTypeID].Equals(1) ? true : false;
                        _listItem.isexpand = false;
                        _list.Add(_listItem);
                    }
                    _res.Data = _list;
                }
                else
                {
                    var _list = from _r in _return
                                select new
                                {
                                    id = _r.ArticleTypeID.ToString(),
                                    fid = _r.ArticleTypeParentID.ToString(),
                                    textcontent = _r.ArticleTypeName,
                                    isexpand = false
                                };
                    _res.Data = _list;
                }
                _res.JsonRequestBehavior = JsonRequestBehavior.AllowGet;//允许使用GET方式获取，否则用GET获取是会报错。   
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/Account/GetJsonForArticleTypeList”");
            }
            return _res;
        }

        /// <summary>
        /// 更新角色信息权限串
        /// </summary>
        /// <param name="id"></param>
        /// <param name="authorityList"></param>
        /// <param name="articleTypeList"></param>
        /// <returns></returns>
        public ActionResult UpdateRoleAuthority(string id, string authorityList, string articleTypeList)
        {
            try
            {
                this.GetAuthority(SystemMemberShip.SystemRoleManage);
                id = HttpUtility.UrlDecode(id, Encoding.UTF8);
                Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
                var _authorityListArray = string.IsNullOrEmpty(authorityList) ? null : authorityList.Split(',');
                var _articleTypeListArray = string.IsNullOrEmpty(articleTypeList) ? null : articleTypeList.Split(',');
                byte[] _authorityList = new byte[1000];
                byte[] _articleTypeList = new byte[1000];
                if (_authorityListArray != null)
                    foreach (var item in _authorityListArray)
                    {

                        _authorityList[int.Parse(item)] = 1;
                    }
                if (_articleTypeListArray != null)
                    foreach (var item in _articleTypeListArray)
                    {
                        _articleTypeList[int.Parse(item)] = 1;
                    }

                var _role = _dct.VW_SA_RoleInfo.Single(m => m.RoleName.Equals(id));
                if (_dct.SA_RoleAuthority.Where(m => m.RoleID.Equals(_role.RoleId)).Count() > 0)
                {
                    var _currentRole = _dct.SA_RoleAuthority.Single(m => m.RoleID.Equals(_role.RoleId));
                    _currentRole.RoleAuthorityArray = _authorityList;
                    _currentRole.RoleArticleTypeAuthorityArray = _articleTypeList;
                    _dct.SubmitChanges();
                }
                else
                {
                    SA_RoleAuthority _SA_RoleAuthority = new SA_RoleAuthority();
                    _SA_RoleAuthority.RoleID = _role.RoleId;
                    _SA_RoleAuthority.RoleAuthorityArray = _authorityList;
                    _SA_RoleAuthority.RoleArticleTypeAuthorityArray = _articleTypeList;
                    _dct.SA_RoleAuthority.InsertOnSubmit(_SA_RoleAuthority);
                    _dct.SubmitChanges();
                }
                this.SetUserInfoAuthorityForRole(id);
                return Content("设置成功！");
            }
            catch (AuthorityException ex)
            {
              
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/Account/UpdateRoleAuthority”");
                return RedirectToAction("Index", "Error", new { @area = "", @MyContent = ex.Message });
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/Account/UpdateRoleAuthority”");
                return RedirectToAction("Index", "Error", new { @area = "" });
            }

        }
        /// <summary>
        /// 资格验证状态
        /// </summary>
        private enum MemberShipHandlerStatus
        {
            /// <summary>
            /// 成功
            /// </summary>
            successfull,//成功。
            /// <summary>
            /// 失败，未配置角色。
            /// </summary>
            noInRoles,//失败，未配置角色。
            /// <summary>
            /// 失败，未设置角色权限。
            /// </summary>
            noSetRoleAuthority,//失败，未设置角色权限。
            /// <summary>
            /// 失败，未设置角色文章权限。
            /// </summary>
            noSetRoleArticleTypeAuthority,//失败，未设置角色文章权限。
            /// <summary>
            /// 失败。
            /// </summary>
            failed//失败。
        }
        /// <summary>
        /// 设置用户权限数据
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        private MemberShipHandlerStatus SetUserInfoAuthority(string userName)
        {            
            MemberShipHandlerStatus _rutent = MemberShipHandlerStatus.successfull;
            try
            {              
                Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
                byte[] userAuthoritys = new byte[1000];
                byte[] userArticleTypeAuthoritys = new byte[1000];
                var _vw_userInfo = _dct.VW_SA_UserInfo.Single(m => m.UserName.Equals(userName));
                var _userInfo = _dct.SA_UserInfo.Where(m => m.UserID.Equals(_vw_userInfo.UserId));
                var _roles = Roles.GetRolesForUser(userName);
                if (_roles == null)
                {
                    _rutent = MemberShipHandlerStatus.noInRoles;
                }
                else
                {
                    foreach (var itme in _roles)
                    {
                        var _roleAuthority = _dct.VW_SA_RoleInfo.Single(m => m.RoleName.Equals(itme));
                        byte[] _authorithArrays = new byte[1000];
                        byte[] _ArticleTypeauthorithArrays = new byte[1000];
                        if (_roleAuthority.RoleAuthorityArray != null)
                        {
                            _authorithArrays = _roleAuthority.RoleAuthorityArray.ToArray();
                            for (int _i = 0; _i < userAuthoritys.Count(); _i++)
                            {
                                userAuthoritys[_i] = (userAuthoritys[_i].Equals(1) | _authorithArrays[_i].Equals(1)) ? byte.Parse("1") : byte.Parse("0");
                            }
                        }
                        if (_roleAuthority.RoleArticleTypeAuthorityArray != null)
                        {
                            _ArticleTypeauthorithArrays = _roleAuthority.RoleArticleTypeAuthorityArray.ToArray();
                            for (int _i = 0; _i < userArticleTypeAuthoritys.Count(); _i++)
                            {
                                userArticleTypeAuthoritys[_i] = (userArticleTypeAuthoritys[_i].Equals(1) | _ArticleTypeauthorithArrays[_i].Equals(1)) ? byte.Parse("1") : byte.Parse("0");
                            }
                        }

                    }
                }

                if (_userInfo != null)
                {
                    var _currentUserInfo = _dct.SA_UserInfo.Single(m => m.UserID.Equals(_vw_userInfo.UserId));
                    _currentUserInfo.UserInfoAuthorityArray = userAuthoritys;
                    _currentUserInfo.UserInfoArticleTypeAuthorityArray = userArticleTypeAuthoritys;
                    _dct.SubmitChanges();
                }
                else
                {
                    SA_UserInfo _currentUserInfo = new SA_UserInfo();
                    _currentUserInfo.UserID = _vw_userInfo.UserId.Value;
                    _currentUserInfo.UserInfoFriendName = _vw_userInfo.UserName;
                    _currentUserInfo.UserInfoAuthorityArray = userAuthoritys;
                    _currentUserInfo.UserInfoArticleTypeAuthorityArray = userArticleTypeAuthoritys;
                    _currentUserInfo.UserInfoFrameworkID = 0;
                    _dct.SA_UserInfo.InsertOnSubmit(_currentUserInfo);
                    _dct.SubmitChanges();
                }
                string _str = "";
                foreach (var item in userArticleTypeAuthoritys)
                {
                    _str += item.ToString();
                }
                return _rutent;
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/Account/SetUserInfoAuthority”");
                return _rutent;
            }
        }
        /// <summary>
        /// 根据角色名设置用户权限数据
        /// </summary>
        /// <param name="RoleName"></param>
        /// <returns></returns>
        private MemberShipHandlerStatus SetUserInfoAuthorityForRole(string RoleName)
        {
            
            try
            {
              
                var _users = Roles.GetUsersInRole(RoleName);
                if (_users != null)
                {
                    foreach (var item in _users)
                    {
                        var _result = this.SetUserInfoAuthority(item);
                        if (_result != MemberShipHandlerStatus.successfull)
                        {
                            return _result;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/Account/SetUserInfoAuthorityForRole”");
                return MemberShipHandlerStatus.failed;
            }
            return MemberShipHandlerStatus.successfull;
        }
        /// <summary>
        /// 更新用户信息权限串
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult UserAuthorityBrowse(string id)
        {
     
            try
            {
                this.GetAuthority(SystemMemberShip.SystemUserManage);
                ViewBag._UserName = id;
                if (Request.IsAjaxRequest())
                    return PartialView();
                else
                    return View();
            }
            catch (AuthorityException ex)
            {          
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/Account/UserAuthorityBrowse”");
                return RedirectToAction("Index", "Error", new { @area = "", @MyContent = ex.Message });
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/Account/UserAuthorityBrowse”");
                return RedirectToAction("Index", "Error", new { @area = "" });
            }
        }

        /// <summary>
        /// 获取用户最终权限列表
        /// </summary>
        /// <returns></returns>
        public JsonResult GetJsonForUserAuthority(string id)
        {
            var _res = new JsonResult();
            try
            {
                id = HttpUtility.UrlDecode(id, Encoding.UTF8);
                Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
                var _return = _dct.SA_AuthorityInfo.OrderBy(m => m.AuthorityParentID).OrderBy(m => m.AuthorityQueueOrder);
                var _user = _dct.VW_SA_UserInfo.Single(m => m.UserName.Equals(id));

                if (_user.UserInfoAuthorityArray != null)
                {
                    byte[] _userInfoAuthorityArray = _user.UserInfoAuthorityArray.ToArray();
                    List<LigeruiTreeItem> _list = new List<LigeruiTreeItem>();
                    foreach (var item in _return)
                    {
                        LigeruiTreeItem _listItem = new LigeruiTreeItem();
                        _listItem.id = item.AuthorityID.ToString();
                        _listItem.fid = item.AuthorityParentID.ToString();
                        _listItem.queueOrder = item.AuthorityQueueOrder;
                        _listItem.textcontent = item.AuthorityContent;
                        _listItem.ischecked = _userInfoAuthorityArray[item.AuthorityQueueOrder].Equals(1) ? true : false;
                        _listItem.isexpand = false;
                        _list.Add(_listItem);
                    }
                    _res.Data = _list;
                }
                else
                {
                    var _list = from _r in _return
                                select new
                                {
                                    id = _r.AuthorityID.ToString(),
                                    fid = _r.AuthorityParentID.ToString(),
                                    queueOrder = _r.AuthorityQueueOrder.ToString(),
                                    textcontent = _r.AuthorityContent,
                                    isexpand = false
                                };
                    _res.Data = _list;
                }
                _res.JsonRequestBehavior = JsonRequestBehavior.AllowGet;//允许使用GET方式获取，否则用GET获取是会报错。      

            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/Account/GetJsonForUserAuthority”");
            }
            return _res;
        }
        /// <summary>
        /// 获取用户最终文章分类权限列表
        /// </summary>
        /// <returns></returns>
        public JsonResult GetJsonForUserArticleType(string id)
        {
            var _res = new JsonResult();
            try
            {
                id = HttpUtility.UrlDecode(id, Encoding.UTF8);
                Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
                var _return = _dct.TB_ArticleType.Where(m => m.ArticleTypeIsUp.Equals(true)).OrderBy(m => m.ArticleTypeParentID).OrderBy(m => m.ArticleTypeID);
                var _user = _dct.VW_SA_UserInfo.Single(m => m.UserName.Equals(id));
                if (_user.UserInfoArticleTypeAuthorityArray != null)
                {
                    byte[] _userArticleTypeList = _user.UserInfoArticleTypeAuthorityArray.ToArray();
                    List<LigeruiTreeItem> _list = new List<LigeruiTreeItem>();
                    foreach (var item in _return)
                    {
                        LigeruiTreeItem _listItem = new LigeruiTreeItem();
                        _listItem.id = item.ArticleTypeID.ToString();
                        _listItem.fid = item.ArticleTypeParentID.ToString();
                        _listItem.textcontent = item.ArticleTypeName;
                        _listItem.ischecked = _userArticleTypeList[item.ArticleTypeID].Equals(1) ? true : false;
                        _listItem.isexpand = false;
                        _list.Add(_listItem);
                    }
                    _res.Data = _list;
                }
                else
                {
                    var _list = from _r in _return
                                select new
                                {
                                    id = _r.ArticleTypeID.ToString(),
                                    fid = _r.ArticleTypeParentID.ToString(),
                                    textcontent = _r.ArticleTypeName,
                                    isexpand = false
                                };
                    _res.Data = _list;
                }
                _res.JsonRequestBehavior = JsonRequestBehavior.AllowGet;//允许使用GET方式获取，否则用GET获取是会报错。      
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/Account/GetJsonForUserArticleType”");
            }
            return _res;
        }
    }
}
