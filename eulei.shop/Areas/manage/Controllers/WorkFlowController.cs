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
    public class WorkFlowController : MyController
    {
        [Paging(PageSize = 15, VaryByParams = new string[] { "ArticleTypeID" })]
        public ActionResult Index(int? ArticleTypeID)
        {
            try
            {
                this.GetAuthority(SystemMemberShip.WorkFlowManage);
                Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
                int _typeID = ArticleTypeID.HasValue ? ArticleTypeID.Value : 0;
                if (_typeID == 0)
                {
                    ViewData.Model = _dct.VW_SA_WorkFlow.OrderBy(m => m.ParentArticleTypeName).OrderBy(m => m.ArticleTypeName);
                }
                else
                {
                    ViewData.Model = _dct.VW_SA_WorkFlow
                        .Where(m => m.ArticleTypeID.Equals(_typeID))
                        .OrderBy(m => m.ParentArticleTypeName).OrderBy(m => m.ArticleTypeName);
                }

                return View();
            }
            catch (AuthorityException ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/WorkFlow/Index”");
                return RedirectToAction("Index", "Error", new { @area = "", @MyContent = ex.Message });
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/WorkFlow/Index”");
                return RedirectToAction("Index", "Error", new { @area = "" });
            }
        }
        [Paging(PageSize = 15, VaryByParams = new string[] { "ArticleTypeID" })]
        public ActionResult Search(int? ArticleTypeID)
        {
            System.Web.Routing.RouteValueDictionary _returns = new System.Web.Routing.RouteValueDictionary();
            _returns.Add("area", "manage");
            try
            {
                this.GetAuthority(SystemMemberShip.WorkFlowManage);
                int _typeID = ArticleTypeID.HasValue ? ArticleTypeID.Value : 0;
                if (_typeID != 0)
                {
                    _returns.Add("ArticleTypeID", _typeID);
                }

            }
            catch (AuthorityException ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/WorkFlow/Search”");
                return RedirectToAction("Index", "Error", new { @area = "", @MyContent = ex.Message });
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/WorkFlow/Search”");
                return RedirectToAction("Index", "Error", new { @area = "" });
            }
            return RedirectToAction("Index", "WorkFlow", _returns);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                this.GetAuthority(SystemMemberShip.WorkFlowManage);
                string url = Request.QueryString["_returnUrl"].ToString();
                ViewBag.returnUrl = url;

                Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
                SA_Flow _result1 = new SA_Flow();
                _result1.FlowAlowEdit = true;
                _result1.FlowIsSynergy = false;
                _result1.FlowSendMoveMsg = collection["FlowSendMoveMsg"].ToString().Equals("false") ? false : true;
                _result1.FlowSourceID = int.Parse(collection["FlowSourceID"].ToString());
                _result1.FlowState = true;
                _result1.FlowStatusDesp = "采编";
                _result1.FlowStatusID = 1;
                _result1.FlowSynergy = "";
                ///////////////////////////////////            
                SA_Flow _result99 = new SA_Flow();
                _result99.FlowAlowEdit = true;
                _result99.FlowIsSynergy = false;
                _result99.FlowSendMoveMsg = collection["FlowSendMoveMsg"].ToString().Equals("false") ? false : true; ;
                _result99.FlowSourceID = int.Parse(collection["FlowSourceID"].ToString());
                _result99.FlowState = true;
                _result99.FlowStatusDesp = "发布确认";
                _result99.FlowStatusID = 99;
                _result99.FlowSynergy = "";
                ///////////////////////////////////
                _dct.SA_Flow.InsertOnSubmit(_result1);
                _dct.SA_Flow.InsertOnSubmit(_result99);
                _dct.SubmitChanges();
                return RedirectToAction("SetStep", "WorkFlow", new { @id = int.Parse(collection["FlowSourceID"].ToString()), @_returnUrl = url });

            }
            catch (AuthorityException ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/WorkFlow/Create【post】”");
                return RedirectToAction("Index", "Error", new { @area = "", @MyContent = ex.Message });
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/WorkFlow/Create【post】”");
                return RedirectToAction("Index", "Error", new { @area = "" });
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                this.GetAuthority(SystemMemberShip.WorkFlowManage);
                string url = Request.QueryString["_returnUrl"].ToString();
                ViewData["_returnUrl"] = url;
                Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();

                var _flows = _dct.SA_Flow.Where(m => m.FlowSourceID.Equals(id));
                foreach (var item in _flows)
                {
                    var _users = _dct.SA_FlowUser.Where(m => m.FlowUserFlowID.Equals(item.FlowID));
                    foreach (var item1 in _users)
                    {
                        _dct.SA_FlowUser.DeleteOnSubmit(item1);
                    }
                    _dct.SA_Flow.DeleteOnSubmit(item);
                }
                _dct.SubmitChanges();
                return Redirect(url);

            }
            catch (AuthorityException ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/WorkFlow/Delete【post】”");
                return RedirectToAction("Index", "Error", new { @area = "", @MyContent = ex.Message });
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/WorkFlow/Delete【post】”");
                return RedirectToAction("Index", "Error", new { @area = "" });
            }
        }


        /// <summary>
        /// 文章分类
        /// </summary>
        /// <returns></returns>
        public JsonResult GetJsonForArticleTypeIDWithNoCreate()
        {
            var _res = new JsonResult();
            try
            {
                this.GetAuthority(SystemMemberShip.WorkFlowManage);
                Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
                var _return = _dct.TB_ArticleType.OrderBy(m => m.ArticleTypeParentID).OrderBy(m => m.ArticleTypeOrder);
                var _exist = _dct.VW_SA_WorkFlow;
                List<LigeruiTreeItem> _lists = new List<LigeruiTreeItem>();
                foreach (var item in _return)
                {
                    LigeruiTreeItem _list = new LigeruiTreeItem();
                    if (_exist != null)
                    {
                        if (_exist.Where(m => m.ArticleTypeID.Equals(item.ArticleTypeID)).Count() < 1)
                        {

                            _list.id = item.ArticleTypeID.ToString();
                            _list.fid = item.ArticleTypeParentID.ToString();
                            _list.textcontent = item.ArticleTypeName;
                            _list.isexpand = true;
                            _lists.Add(_list);

                        }
                        continue;
                    }

                    _list.id = item.ArticleTypeID.ToString();
                    _list.fid = item.ArticleTypeParentID.ToString();
                    _list.textcontent = item.ArticleTypeName;
                    _list.isexpand = true;
                    _lists.Add(_list);

                }
                _res.Data = _lists;
                _res.JsonRequestBehavior = JsonRequestBehavior.AllowGet;//允许使用GET方式获取，否则用GET获取是会报错。      
            }
            catch (AuthorityException ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/WorkFlow/GetJsonForArticleTypeIDWithNoCreate”");
               
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/WorkFlow/GetJsonForArticleTypeIDWithNoCreate”");
           
            }
            return _res;
        }

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <returns></returns>
        public JsonResult GetJsonForUserList(int id)
        {
            var _res = new JsonResult();
            try
            {
                this.GetAuthority(SystemMemberShip.WorkFlowManage);
                Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
                var _frameworks = _dct.SA_Framework;
                var _users = _dct.VW_SA_UserInfo;
                var _exist = _dct.VW_SA_FlowInfo.Where(m => m.FlowID.Equals(id));
                List<LigeruiTreeItem> _list = new List<LigeruiTreeItem>();
                if (_frameworks != null)
                {

                    foreach (var item in _frameworks)
                    {
                        LigeruiTreeItem _listItem = new LigeruiTreeItem();
                        _listItem.id = (item.FrameworkID + 100000).ToString();
                        _listItem.fid = (item.FrameworkParentID + 100000).ToString();
                        _listItem.queueOrder = item.FrameworkOrder;
                        _listItem.textcontent = item.FrameworkName;
                        _listItem.isexpand = true;
                        _list.Add(_listItem);
                    }

                }
                if (_users != null)
                {

                    foreach (var item in _users)
                    {
                        LigeruiTreeItem _listItem = new LigeruiTreeItem();
                        _listItem.id = item.UserId.ToString();
                        _listItem.fid = (item.UserInfoFrameworkID + 100000).ToString();
                        _listItem.textcontent = item.UserInfoFriendName + "(" + item.UserName + ")";
                        if (_exist != null)
                        {
                            _listItem.ischecked = _exist.Where(m => m.FlowUserOperaterName.Equals(item.UserName)).Count() > 0 ? true : false;
                        }
                        _listItem.isexpand = true;
                        _list.Add(_listItem);
                    }

                }
                _res.Data = _list;
                _res.JsonRequestBehavior = JsonRequestBehavior.AllowGet;//允许使用GET方式获取，否则用GET获取是会报错。      

            }
            catch (AuthorityException ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/WorkFlow/GetJsonForUserList”");
               
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/WorkFlow/GetJsonForUserList”");

            }
            return _res;
        }

        public ActionResult SetStep(int id)
        {
            try
            {
                this.GetAuthority(SystemMemberShip.WorkFlowManage);
                string url = Request.QueryString["_returnUrl"].ToString();
                ViewData["_returnUrl"] = url;
                ViewBag.id = id;
                Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
                ViewData.Model = _dct.VW_SA_FlowHelper.Where(m => m.ArticleTypeID.Equals(id)).OrderBy(m => m.FlowStatusID);
                var _title = _dct.VW_SA_WorkFlow.Single(m => m.ArticleTypeID.Equals(id));
                ViewBag.Title = (string.IsNullOrEmpty(_title.ParentArticleTypeName) ? "站点跟目录" : _title.ParentArticleTypeName) + "-->" + _title.ArticleTypeName;
                return View();
            }
            catch (AuthorityException ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/WorkFlow/SetStep【post】”");
                return RedirectToAction("Index", "Error", new { @area = "", @MyContent = ex.Message });
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/WorkFlow/SetStep【post】”");
                return RedirectToAction("Index", "Error", new { @area = "" });
            }
        }


        public ActionResult SetUsers(int id, string userList)
        {
            try
            {
                this.GetAuthority(SystemMemberShip.WorkFlowManage);
                userList = HttpUtility.UrlDecode(userList, Encoding.UTF8);
                userList = userList.ToUpper();
                Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
                var _userList = string.IsNullOrEmpty(userList) ? null : userList.Split(',');
                var _users = from _r in _dct.VW_SA_UserInfo select _r.UserId;
                var _existUser = (from _r in _dct.SA_FlowUser
                                  where _r.FlowUserFlowID.Equals(id)
                                  select _r.FlowUserOperaterID.ToString()).ToArray();
                string[] _newUser;
                string[] _oldUser;

                if (_userList != null)
                {
                    _newUser = _userList.Except(_existUser).ToArray();
                    _oldUser = _existUser.Except(_userList).ToArray();
                }
                else
                {
                    _newUser = new string[0];
                    _oldUser = _existUser.ToArray();
                }

                foreach (var item in _newUser)
                {
                    Guid _temp;
                    if (!Guid.TryParse(item, out _temp))
                    {
                        continue;
                    }
                    var _new = new SA_FlowUser();
                    var _currentUser = _dct.VW_SA_UserInfo.Single(m => m.UserId.Equals(item));
                    if (_currentUser == null)
                    {
                        throw new Exception("操作员信息为设置！");
                    }
                    _new.FlowUserID = Guid.NewGuid();
                    _new.FlowUserFlowID = id;
                    _new.FlowUserOperaterID = _currentUser.UserId.Value;
                    _new.FlowUserOperaterName = _currentUser.UserName;
                    _dct.SA_FlowUser.InsertOnSubmit(_new);

                }
                foreach (var item in _oldUser)
                {
                    var _currentUser = _dct.SA_FlowUser.Single(m => m.FlowUserFlowID.Equals(id) && m.FlowUserOperaterID.Equals(Guid.Parse(item)));
                    _dct.SA_FlowUser.DeleteOnSubmit(_currentUser);

                }
                _dct.SubmitChanges();
                return Content("设置成功！");
            }
            catch (AuthorityException ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/WorkFlow/SetUsers【post】”");
                return RedirectToAction("Index", "Error", new { @area = "", @MyContent = ex.Message });
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/WorkFlow/SetUsers【post】”");
                return RedirectToAction("Index", "Error", new { @area = "" });
            }
        }


        public ActionResult CreateStep(int id)
        {
            try
            {
                this.GetAuthority(SystemMemberShip.WorkFlowManage);
                string url = Request.QueryString["_returnUrl"].ToString();
                ViewData["_returnUrl"] = url;
                Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
                var _flows = _dct.SA_Flow.Where(m => m.FlowSourceID.Equals(id) && m.FlowStatusID > 1 && m.FlowStatusID < 99).OrderBy(m => m.FlowStatusID);
                SA_Flow _return = new SA_Flow();
                _return.FlowSourceID = id;
                if (_flows.Count() != 0)
                {
                    _return.FlowStatusID = _flows.ToList().Last().FlowStatusID + 1;
                }
                else
                {
                    _return.FlowStatusID = 2;
                }
                ViewData.Model = _return;
                if (Request.IsAjaxRequest())
                    return PartialView();
                else
                    return View();
            }
            catch (AuthorityException ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/WorkFlow/CreateStep”");
                return RedirectToAction("Index", "Error", new { @area = "", @MyContent = ex.Message });
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/WorkFlow/CreateStep”");
                return RedirectToAction("Index", "Error", new { @area = "" });
            }
        }


        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CreateStep(FormCollection collection)
        {
            try
            {
                this.GetAuthority(SystemMemberShip.WorkFlowManage);
                string url = collection["_returnUrl"].ToString();
                Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
                SA_Flow _result = new SA_Flow();
                _result.FlowAlowEdit = collection["FlowAlowEdit"].ToString().Equals("false") ? false : true;
                _result.FlowIsSynergy = collection["FlowIsSynergy"].ToString().Equals("false") ? false : true;
                _result.FlowSendMoveMsg = collection["FlowSendMoveMsg"].ToString().Equals("false") ? false : true;
                _result.FlowSourceID = int.Parse(collection["FlowSourceID"].ToString());
                _result.FlowState = collection["FlowState"].ToString().Equals("false") ? false : true;
                _result.FlowStatusDesp = collection["FlowStatusDesp"].ToString();
                _result.FlowStatusID = int.Parse(collection["FlowStatusID"].ToString());
                _dct.SA_Flow.InsertOnSubmit(_result);
                _dct.SubmitChanges();
                return Redirect(url);
            }
            catch (AuthorityException ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/WorkFlow/CreateStep【post】”");
                return RedirectToAction("Index", "Error", new { @area = "", @MyContent = ex.Message });
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/WorkFlow/CreateStep【post】”");
                return RedirectToAction("Index", "Error", new { @area = "" });
            }
        }


        public ActionResult EditStep(int id)
        {
            try
            {
                this.GetAuthority(SystemMemberShip.WorkFlowManage);
                string url = Request.QueryString["_returnUrl"].ToString();
                ViewData["_returnUrl"] = url;
                Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
                ViewData.Model = _dct.SA_Flow.Single(m => m.FlowID.Equals(id));
                if (Request.IsAjaxRequest())
                    return PartialView();
                else
                    return View();
            }
            catch (AuthorityException ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/WorkFlow/EditStep【post】”");
                return RedirectToAction("Index", "Error", new { @area = "", @MyContent = ex.Message });
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/WorkFlow/EditStep【post】”");
                return RedirectToAction("Index", "Error", new { @area = "" });
            }
        }


        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditStep(int id, FormCollection collection)
        {
            try
            {
                this.GetAuthority(SystemMemberShip.WorkFlowManage);
                string url = collection["_returnUrl"].ToString();
                Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
                var _result = _dct.SA_Flow.Single(m => m.FlowID.Equals(id));
                _result.FlowAlowEdit = collection["FlowAlowEdit"].ToString().Equals("false") ? false : true;

                _result.FlowIsSynergy = collection["FlowIsSynergy"].ToString().Equals("false") ? false : true;
                _result.FlowSendMoveMsg = collection["FlowSendMoveMsg"].ToString().Equals("false") ? false : true;
                _result.FlowSourceID = int.Parse(collection["FlowSourceID"].ToString());
                _result.FlowState = collection["FlowState"].ToString().Equals("false") ? false : true;
                _result.FlowStatusDesp = collection["FlowStatusDesp"].ToString();
                _result.FlowStatusID = int.Parse(collection["FlowStatusID"].ToString());
                _dct.SubmitChanges();
                return Redirect(url);
            }
            catch (AuthorityException ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/WorkFlow/EditStep【post】”");
                return RedirectToAction("Index", "Error", new { @area = "", @MyContent = ex.Message });
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/WorkFlow/EditStep【post】”");
                return RedirectToAction("Index", "Error", new { @area = "" });
            }
        }


        public ActionResult DeleteStep(int id)
        {
            try
            {
                this.GetAuthority(SystemMemberShip.WorkFlowManage);
                string url = Request.QueryString["_returnUrl"].ToString();
                ViewData["_returnUrl"] = url;
                Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
                var _flow = _dct.SA_Flow.Single(m => m.FlowID.Equals(id));
                var _users = _dct.SA_FlowUser.Where(m => m.FlowUserFlowID.Equals(id));
                foreach (var item in _users)
                {
                    _dct.SA_FlowUser.DeleteOnSubmit(item);
                }
                _dct.SA_Flow.DeleteOnSubmit(_flow);
                _dct.SubmitChanges();
                return Redirect(url);
            }
            catch (AuthorityException ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/WorkFlow/DeleteStep【post】”");
                return RedirectToAction("Index", "Error", new { @area = "", @MyContent = ex.Message });
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“manage/WorkFlow/DeleteStep【post】”");
                return RedirectToAction("Index", "Error", new { @area = "" });
            }
        }
    }
}
