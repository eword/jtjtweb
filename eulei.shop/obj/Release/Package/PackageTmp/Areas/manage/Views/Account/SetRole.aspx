<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/manage/Views/Shared/Manage_Areas.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    设置角色
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <% var _roleList = ViewBag.RoleList as List<string>;
       var _userInList = ViewBag.UserInRoleList as List<string>;
    %>
    <% using (Html.BeginForm("SetRole", "Account", new { @area = "manage" }, FormMethod.Post, new { @id = "postForm" }))
       { %>
    <%:Html.Hidden("_returnUrl",ViewData["_returnUrl"]) %>
    <table>
        <tr class="lbg">
            <td>
                <h2>账号：<%:ViewBag._UserName %></h2>
            </td>
        </tr>
        <% 
           int _i = 1;
           foreach (var item in _roleList)%>
        <%{ %>

        <%if (_i % 2 == 0)%>
        <%{ %>
        <tr class="lbgalt">
            <%} %>
            <%else %>
            <%{ %>
            <tr class="lbg">
                <%} %>
                <td>
                    <%:Html.CheckBox("Role", _userInList.Where(m => m.Equals(item)).Count() > 0 ? true : false, new { @value=item })%><%=item %>
          
                </td>
            </tr>
            <%
          _i++;
          } %>
            <tr>
                <td colspan="4">
                    <div class="error">
                    </div>
                    <input type="submit" class="btnsubmit fr" value="提交" />
                </td>
            </tr>
    </table>
    <% } %>
    <div>
        <a href="<%:ViewData["_returnUrl"]%>">返回列表</a>
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
    设置角色
</asp:Content>
