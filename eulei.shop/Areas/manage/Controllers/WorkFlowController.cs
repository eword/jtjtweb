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
                SA_FlowTemplate _result1 = new SA_FlowTemplate();
                _result1.FlowTemplateAlowEdit = true;
                _result1.FlowTemplateIsSynergy = false;
                _result1.FlowTemplateSendMoveMsg = collection["FlowTemplateSendMoveMsg"].ToString().Equals("false") ? false : true;
                _result1.FlowTemplateArticleTypeID = int.Parse(collection["FlowTemplateArticleTypeID"].ToString());
                _result1.FlowTemplateState = true;
                _result1.FlowTemplateStatusDesp = "采编";
                _result1.FlowTemplateStatusID = 1;
                ///////////////////////////////////            
                SA_FlowTemplate _result99 = new SA_FlowTemplate();
                _result99.FlowTemplateAlowEdit = true;
                _result99.FlowTemplateIsSynergy = false;
                _result99.FlowTemplateSendMoveMsg = collection["FlowTemplateSendMoveMsg"].ToString().Equals("false") ? false : true; ;
                _result99.FlowTemplateArticleTypeID = int.Parse(collection["FlowTemplateArticleTypeID"].ToString());
                _result99.FlowTemplateState = true;
                _result99.FlowTemplateStatusDesp = "发布确认";
                _result99.FlowTemplateStatusID = 99;
                ///////////////////////////////////
                _dct.SA_FlowTemplate.InsertOnSubmit(_result1);
                _dct.SA_FlowTemplate.InsertOnSubmit(_result99);
                _dct.SubmitChanges();
                return RedirectToAction("SetStep", "WorkFlow", new { @id = int.Parse(collection["FlowTemplateArticleTypeID"].ToString()), @_returnUrl = url });

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

                var _flows = _dct.SA_FlowTemplate.Where(m => m.FlowTemplateArticleTypeID.Equals(id));
                foreach (var item in _flows)
                {
                    var _users = _dct.SA_FlowUserTemplate.Where(m => m.FlowTemplateID.Equals(item.FlowTemplateID));
                    foreach (var item1 in _users)
                    {
                        _dct.SA_FlowUserTemplate.DeleteOnSubmit(item1);
                    }
                    _dct.SA_FlowTemplate.DeleteOnSubmit(item);
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
                var _exist = _dct.VW_SA_FlowInfo.Where(m => m.FlowTemplateID.Equals(id));
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
                            _listItem.ischecked = _exist.Where(m => m.FlowUserTemplateUserName.Equals(item.UserName)).Count() > 0 ? true : false;
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
                ViewData.Model = _dct.VW_SA_FlowStepInfo.Where(m => m.FlowTemplateArticleTypeID.Equals(id)).OrderBy(m => m.FlowTemplateStatusID);
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
                var _existUser = (from _r in _dct.VW_SA_FlowInfo
                                  where _r.FlowTemplateID.Equals(id)
                                  select _r.FlowUserTemplateUserID.ToString()).ToArray();
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
                    //排除组织机构
                    if (!Guid.TryParse(item, out _temp))
                    {
                        continue;
                    }
                    var _new = new SA_FlowUserTemplate();
                    var _currentUser = _dct.VW_SA_UserInfo.Single(m => m.UserId.Equals(item));
                    if (_currentUser == null)
                    {
                        throw new Exception("操作员信息未设置！");
                    }
                    _new.FlowUserTemplateID = Guid.NewGuid();
                    _new.FlowTemplateID = id;
                    _new.FlowUserTemplateUserID = _currentUser.UserId.Value;
                    _new.FlowUserTemplateUserName = _currentUser.UserName;
                    _dct.SA_FlowUserTemplate.InsertOnSubmit(_new);

                }
                foreach (var item in _oldUser)
                {
                    var _currentUser = _dct.SA_FlowUserTemplate.Single(m => m.FlowTemplateID.Equals(id) && m.FlowUserTemplateUserID.Equals(Guid.Parse(item)));
                    _dct.SA_FlowUserTemplate.DeleteOnSubmit(_currentUser);

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
                var _flows = _dct.SA_FlowTemplate.Where(m => m.FlowTemplateArticleTypeID.Equals(id) && m.FlowTemplateStatusID > 1 && m.FlowTemplateStatusID < 99).OrderBy(m => m.FlowTemplateStatusID);
                SA_FlowTemplate _return = new SA_FlowTemplate();
                _return.FlowTemplateArticleTypeID = id;
                if (_flows.Count() != 0)
                {
                    _return.FlowTemplateStatusID = _flows.ToList().Last().FlowTemplateStatusID + 1;
                }
                else
                {
                    _return.FlowTemplateStatusID = 2;
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
                SA_FlowTemplate _result = new SA_FlowTemplate();
                _result.FlowTemplateAlowEdit = collection["FlowTemplateAlowEdit"].ToString().Equals("false") ? false : true;
                _result.FlowTemplateIsSynergy = collection["FlowTemplateIsSynergy"].ToString().Equals("false") ? false : true;
                _result.FlowTemplateSendMoveMsg = collection["FlowTemplateSendMoveMsg"].ToString().Equals("false") ? false : true;
                _result.FlowTemplateArticleTypeID = int.Parse(collection["FlowTemplateArticleTypeID"].ToString());
                _result.FlowTemplateState = collection["FlowTemplateState"].ToString().Equals("false") ? false : true;
                _result.FlowTemplateStatusDesp = collection["FlowTemplateStatusDesp"].ToString();
                _result.FlowTemplateStatusID = int.Parse(collection["FlowTemplateStatusID"].ToString());
                _dct.SA_FlowTemplate.InsertOnSubmit(_result);
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
                ViewData.Model = _dct.SA_FlowTemplate.Single(m => m.FlowTemplateID.Equals(id));
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
                var _result = _dct.SA_FlowTemplate.Single(m => m.FlowTemplateID.Equals(id));
                _result.FlowTemplateAlowEdit = collection["FlowTemplateAlowEdit"].ToString().Equals("false") ? false : true;
                _result.FlowTemplateIsSynergy = collection["FlowTemplateIsSynergy"].ToString().Equals("false") ? false : true;
                _result.FlowTemplateSendMoveMsg = collection["FlowTemplateSendMoveMsg"].ToString().Equals("false") ? false : true;
                _result.FlowTemplateArticleTypeID = int.Parse(collection["FlowTemplateArticleTypeID"].ToString());
                _result.FlowTemplateState = collection["FlowTemplateState"].ToString().Equals("false") ? false : true;
                _result.FlowTemplateStatusDesp = collection["FlowTemplateStatusDesp"].ToString();
                _result.FlowTemplateStatusID = int.Parse(collection["FlowTemplateStatusID"].ToString());
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
                var _flow = _dct.SA_FlowTemplate.Single(m => m.FlowTemplateID.Equals(id));
                var _users = _dct.SA_FlowUserTemplate.Where(m => m.FlowTemplateID.Equals(id));
                
                foreach (var item in _users)
                {
                    _dct.SA_FlowUserTemplate.DeleteOnSubmit(item);
                }
                _dct.SA_FlowTemplate.DeleteOnSubmit(_flow);
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
