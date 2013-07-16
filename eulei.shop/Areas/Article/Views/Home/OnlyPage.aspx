<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Article/Views/Shared/Article_Areas.Master"
    Inherits="System.Web.Mvc.ViewPage<eulei.shop.Models.TB_OnlyPage>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%=Model.OnlyPageTitle %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="listpager_arrow_box">
        <div class="listpager_arrow">
            当前位置：<%:Html.MvcSiteMap().SiteMapPath() %>&gt;&gt;内容
        </div>
    </div>
    <div class="OnlyPageBox">
        <h3 class="OnlyPageTitle">
            <%=Model.OnlyPageTitle %>
        </h3>
        <hr class="titledownline" />
        <div class="fixck OnlyPageContent">
            <%=Model.OnlyPageContent %>
        </div>
    </div>
    <div class="pager">
        <% Html.RenderAction("CustomerPagination", "Common", new { @Area = "", @CustomerUrl = ViewBag.Url, @FirstPage = ViewBag.FirstPage, @CurrentPage = ViewBag.CurrentPage, @LastPage = ViewBag.LastPage });%>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="headOtherInfo" runat="server">
    <link type="text/css" href="<%:Url.Content("~/App_Themes/"+ConfigurationManager.AppSettings["ArticleTheme"].ToString()+"/css/general.css") %>"
        rel="stylesheet" />
    <link type="text/css" href="<%:Url.Content("~/App_Themes/"+ConfigurationManager.AppSettings["ArticleTheme"].ToString()+"/css/master.css") %>"
        rel="stylesheet" />
    <link type="text/css" href="<%:Url.Content("~/App_Themes/"+ConfigurationManager.AppSettings["ArticleTheme"].ToString()+"/css/article.css") %>"
        rel="stylesheet" />
    <link type="text/css" href="<%:Url.Content("~/App_Themes/"+ConfigurationManager.AppSettings["ArticleTheme"].ToString()+"/css/OnlyPage.css") %>"
        rel="stylesheet" />
    <meta name="keywords" content="<%:Model.OnlyPageLabel %>" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="RightContent" runat="server">
    <%Html.RenderAction("Details", "ContentNav", new { @id = ViewBag.NavID, @area = "Article" }); %>
</asp:Content>
