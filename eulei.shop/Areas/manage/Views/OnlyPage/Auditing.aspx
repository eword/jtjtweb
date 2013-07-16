<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/manage/Views/Shared/Manage_Areas.Master" Inherits="System.Web.Mvc.ViewPage<eulei.shop.Models.TB_OnlyPage>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    单页文章审核
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <h3>您真的确定要审核并发布该文章吗？</h3>
    <% using (Html.BeginForm())
       { %>
    <p>
        <%:Html.Hidden("_returnUrl", ViewData["_returnUrl"].ToString())%>
        <input type="submit" value="审核通过" />
        | <a href="<%= ViewData["_returnUrl"].ToString() %>">返回</a>
    </p>
    <% } %>
    <hr />
    <br />
    <div class="OnlyPageBox">
        <h3 class="fixck OnlyPageTitle">
            <%=Model.OnlyPageTitle %>
        </h3>
        <hr class="titledownline" />
        <div class="OnlyPageContent">
            <%=Model.OnlyPageContent %>
        </div>
    </div>
    <div class="pager">
        <% Html.RenderAction("CustomerPagination", "Common", new { @Area = "", @CustomerUrl = ViewBag.Url, @FirstPage = ViewBag.FirstPage, @CurrentPage = ViewBag.CurrentPage, @LastPage = ViewBag.LastPage });%>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="headOtherInfo" runat="server">
    <link type="text/css" href="<%:Url.Content("~/Libs/ligerUI/skins/Aqua/css/ligerui-all.css") %>"
        rel="stylesheet" />
    <link type="text/css" href="<%:Url.Content("~/App_Themes/"+ConfigurationManager.AppSettings["ManageTheme"].ToString()+"/css/general.css") %>"
        rel="stylesheet" />
    <link type="text/css" href="<%:Url.Content("~/App_Themes/"+ConfigurationManager.AppSettings["ManageTheme"].ToString()+"/css/master.css") %>"
        rel="stylesheet" />
    <link type="text/css" href="<%:Url.Content("~/App_Themes/"+ConfigurationManager.AppSettings["ManageTheme"].ToString()+"/css/OnlyPage.css") %>"
        rel="stylesheet" />
    <link rel="stylesheet" href="<%: Url.Content("~/Content/JqueryUI/css/smoothness/jquery-ui-1.9.1.custom.min.css") %>"
        type="text/css" media="all" />
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="TabTitleContent" runat="server">
    文章审核
</asp:Content>

