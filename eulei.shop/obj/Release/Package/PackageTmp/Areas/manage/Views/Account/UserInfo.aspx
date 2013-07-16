<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/manage/Views/Shared/Manage_Areas.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<eulei.shop.Models.VW_SA_UserInfo>>" %>

<%@ Import Namespace="Q42.Wheels.Mvc.Paging" %>
<%@ Import Namespace="eulei.shop.Code" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    账号管理
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" src="<%: Url.Content("~/Libs/ligerUI/js/core/base.js") %>"></script>
    <script type="text/javascript" src="<%: Url.Content("~/Libs/ligerUI/js/ligerui.min.js") %>"></script>
    <script type="text/javascript" src="<%: Url.Content("~/Libs/ligerUI/js/plugins/ligerComboBox.js") %>"></script>
    <script type="text/javascript" src="<%: Url.Content("~/Libs/ligerUI/js/plugins/ligerTree.js") %>"></script>
    <script type="text/javascript" src="<%: Url.Content("~/Content/js/confirm.js") %>"></script>
    <script type="text/javascript" src="<%: Url.Content("~/Content/themes/ui/js/jquery-ui-1.8.22.custom.min.js") %>"></script>
    <script type="text/javascript" src="<%: Url.Content("~/Scripts/jquery.validate.min.js") %>"></script>
    <script type="text/javascript" src="<%: Url.Content("~/Scripts/jquery.unobtrusive-ajax.min.js") %>"></script>

    <script type="text/javascript">
        $(function () {
            $("#FrameworkIDCombobox").ligerComboBox({
                width: 200,
                height: 20,
                selectBoxWidth: 200,
                selectBoxHeight: 200, lable: '组织结构',
                textField: 'textcontent', valueField: 'id', valueFieldID: 'UserInfoFrameworkID', treeLeafOnly: false,
                tree: {
                    url: '<%: Url.Content("~/Common/GetJsonForFrameworkID") %>',
                    checkbox: false,
                    textFieldName: "textcontent",
                    idFieldName: "id",
                    parentIDFieldName: "fid"
                }
            });

            $('.deletelink').confirm({
                msg: '删除？',
                ok: '是',
                cancel: '否',
                timeout: 10000
            });
        });
    </script>
    <script type="text/javascript">
        $(function () {
            $("#dialog-form-ReSetPassWord").dialog({
                autoOpen: false,
                height: 200,
                width: 350,
                modal: true,
                buttons: {
                    "提交": function () {
                        var _return = "";

                        if ($("#ConfirmPassword").attr("value") != $("#Password").attr("value")) {
                            _return += "<div>两次密码不一致！</div>";
                        }
                        else {
                            if ($("#Password").attr("value") == "") {
                                _return += "<div>新密码不能为空！</div>";
                            }
                            else {
                                if ($("#Password").attr("value").length < 5) {
                                    _return += "<div>新密码长度必须大于等于5！</div>";
                                }
                            }
                        }
                        if (_return == "") {
                            $("#postForm").submit();
                            $("#ReSetPassWordAjaxBox").html("");
                            $(this).dialog({ height: 200 });
                            $(this).dialog("close");
                        }
                        else {
                            $(".error").html("");
                            $(this).dialog({ height: 250 });
                            $(".error").append(_return);
                        }
                    },
                    "取消": function () {
                        $("#ReSetPassWordAjaxBox").html("");
                        $(this).dialog({ height: 200 });
                        $(this).dialog("close");
                    }
                }
            });
        });

    </script>
    <div id="dialog-form-ReSetPassWord" title="密码重置">
        <div id="ReSetPassWordAjaxBox">
        </div>
    </div>
        <script type="text/javascript">
            $(function () {
                $("#dialog-form-UserAuthorityBrowse").dialog({
                    autoOpen: false,
                    height: 200,
                    width: 350,
                    modal: true,
                           beforeClose: function (event, ui) {
                        $("#UserAuthorityBrowseAjaxBox").html("");
                    }
                });
            });
    </script>
    <div id="dialog-form-UserAuthorityBrowse" title="权限预览">
        <div id="UserAuthorityBrowseAjaxBox">
        </div>
    </div>
    <div>
        <div class="searchBox">
            <%using (Html.BeginForm("Search", "Account", new { @area = "manage" }, FormMethod.Post, null))
              { %>
            <div>
                用户名/姓名：<%:Html.TextBox("UserName", "", new { @class = "SearchTextUserInfoTitle" })%>
            </div>
            <div>
                <input id="FrameworkIDCombobox" type="text" />
            </div>
            <div>
                <input type="submit" id="searchSubmit" value="  " />
            </div>
            <%} %>
            <%:Html.ActionLink("添加", "Register", "Account", new { @_returnUrl = Request.Url.PathAndQuery, @area = "" }, new { @class = "create" })%>
        </div>
        <table width="100%" bgcolor="#b5d6e6" border="0" cellpadding="0" cellspacing="0">
            <tr class="tbg">
                <td class="tc">
                    <%= Html.RenderSortLink("账号名", "UserName")%>
                </td>
                <td class="tc ">
                    <%= Html.RenderSortLink("姓名", "UserInfoFriendName")%>
                </td>
                <td class="tc w240">
                    <%= Html.RenderSortLink("组织机构", "FrameworkName")%>
                </td>
                <td class="tc  w280">基本操作
                </td>
            </tr>
            <% 
                int _i = 1;
                foreach (var item in this.GetPage(Model))
                {                  
            %>
            <%if(_i%2==0){ %>          
               
              <tr class="lbgalt"> <%} %>
                <%else %>
                <%{ %>
                      <tr class="lbg">
                <%} %>
                <td class="tc">
                    <%:Html.ActionLink(item.UserName, "UserAuthorityBrowse", "Account", new { @id = item.UserName, @area = "Manage" },new{ @target="_blank"} )%>
                </td>
                <td class="tc">
                    <%if (!string.IsNullOrEmpty(item.UserInfoFriendName))
                      {%>
                    <%: item.UserInfoFriendName %>
                    <%}
                      else
                      { %>
                    <%:Html.ActionLink("绑定账号使用人", "CreateUserInfo", "Account", new { @id = item.UserId, @area = "Manage", @_returnUrl = Request.Url.PathAndQuery  },null)%>
                    <%}%>
                </td>
                <td class="tc">
                    <%if (!string.IsNullOrEmpty(item.FrameworkName))
                      {%>
                    <%: item.FrameworkName %>                      <%}
                      else
                      { %>
                    <%:Html.ActionLink("选择机构", "CreateUserInfo", "Account", new { @id = item.UserId, @area = "Manage", @_returnUrl = Request.Url.PathAndQuery  }, null)%>
                    <%}%>
                </td>
                <td class="tc">
                    <div>
                        <%--<%: Html.ActionLink("密码重置", "ReSetPassWord", new { @id = item.UserName, @_returnUrl = Request.Url.PathAndQuery }, new { @class = "detailslink ReSetPassWord" })%>--%>
                        <%:Ajax.ActionLink("密码重置", "ReSetPassWord", new { @id = item.UserName, @_returnUrl = Request.Url.PathAndQuery }, new AjaxOptions { UpdateTargetId = "ReSetPassWordAjaxBox" }, new { @class = "editlink ReSetPassWord" })%>
                        <%if (!string.IsNullOrEmpty(item.UserInfoFriendName))
                          {%>
                        <%: Html.ActionLink("编辑", "EditUserInfo", new { @id = item.UserId, @_returnUrl = Request.Url.PathAndQuery }, new { @class = "editlink" })%>
                        <%}
                          else
                          { %>
                        <%:Html.ActionLink("编辑", "CreateUserInfo", "Account", new { @id = item.UserId, @area = "Manage", @_returnUrl = Request.Url.PathAndQuery }, new { @class = "editlink" })%>
                        <%}%>
                    </div>
                    <div>
                        <%: Html.ActionLink("设置角色", "SetRole", new { @id = item.UserName,@area = "Manage", @_returnUrl = Request.Url.PathAndQuery }, new { @class = "editlink" })%>
                        <%: Html.ActionLink("删除", "Delete", new { @id = item.UserName, @_returnUrl = Request.Url.PathAndQuery }, new { @class = "deletelink" })%>
                    </div>
                </td>
            </tr>
            <%_i++;
                } %>
        </table>
        <%= Html.RenderPagination() %>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="headOtherInfo" runat="server">
    <link type="text/css" href="<%:Url.Content("~/Libs/ligerUI/skins/Aqua/css/ligerui-all.css") %>" rel="stylesheet" />
    <link type="text/css" href="<%:Url.Content("~/App_Themes/"+ConfigurationManager.AppSettings["ManageTheme"].ToString()+"/css/general.css") %>"
        rel="stylesheet" />
    <link type="text/css" href="<%:Url.Content("~/App_Themes/"+ConfigurationManager.AppSettings["ManageTheme"].ToString()+"/css/master.css") %>"
        rel="stylesheet" />
    <link type="text/css" href="<%:Url.Content("~/App_Themes/"+ConfigurationManager.AppSettings["ManageTheme"].ToString()+"/css/UserInfo.css") %>"
        rel="stylesheet" />
    <link rel="stylesheet" href="<%: Url.Content("~/Content/themes/ui/css/smoothness/jquery-ui-1.8.22.custom.css") %>"
        type="text/css" media="all" />
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="TabTitleContent" runat="server">
    账号管理
</asp:Content>
