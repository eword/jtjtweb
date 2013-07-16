<%@ Page Title="" Theme="manage" StylesheetTheme="manage" EnableTheming="false" Language="C#" MasterPageFile="~/Areas/manage/Views/Shared/Manage_Areas.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<%@ Import Namespace="eulei.shop.Code" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    欢迎界面
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">   
    <br />
    <fieldset>
        <legend><%=new CurrentLoginUser().GetUserInfo(Page.User.Identity.Name).FriendlyName %>(<%: Page.User.Identity.Name %>):欢迎您登入到“<%:ConfigurationManager.AppSettings["webTitle"].ToString()%>”后台管理界面</legend>
        <%if (SystemMemberShip.GetSystemAuthority(Session["SystemAuthority"].ToString(), SystemMemberShip.ArticleBrowse))
          { %>
        <div class="icolist">
            <a href="/Manage/Article/Index/<%:(int)ArticleSearchType.NeedHandle %>" title="待办文章：共计<%=ViewBag.NeedHandled %>篇">
                <img src="../../../../Content/images/01.gif" alt="待办文章：共计<%=ViewBag.NeedHandled %>篇" />
                <br />
                待办文章<br />
                共计<%=ViewBag.NeedHandled %>篇</a>
        </div>
        <div class="icolist">
            <a href="/Manage/Article/Index/<%:(int)ArticleSearchType.MySend %>" title="我的文章：共计<%:ViewBag.MySend %>篇">
                <img src="../../../../Content/images/04.gif" alt="我的文章：共计<%:ViewBag.MySend %>篇" /><br />
                我的文章<br />
                共计<%:ViewBag.MySend %>篇</a>
        </div>
        <div class="icolist">
            <a href="/Manage/Article/Index/<%:(int)ArticleSearchType.Handled %>" title="已办文章：共计<%:ViewBag.Handled %>篇">
                <img src="../../../../Content/images/09.gif" alt="已办文章：共计<%:ViewBag.Handled %>篇" /><br />
                已办文章<br />
                共计<%:ViewBag.Handled %>篇</a>
        </div>
        <%} %>
        <%if (SystemMemberShip.GetSystemAuthority(Session["SystemAuthority"].ToString(), SystemMemberShip.ArticleShareManage))
          { %>
        <div class="icolist">
            <a href="/Manage/Article/ShareArticleIndex/<%:(int)ShareArticleState.Waiting %>" title="代办共享文章：共计<%:ViewBag.ShardArticle %>篇">
                <img src="../../../../Content/images/12.gif" alt="代办共享文章：共计<%:ViewBag.ShardArticle %>篇" /><br />
                代办共享文章<br />
                共计<%:ViewBag.ShardArticle %>篇</a>
        </div>
        <%} %>
        <%if (SystemMemberShip.GetSystemAuthority(Session["SystemAuthority"].ToString(), SystemMemberShip.FlashADManage))
          { %>
        <div class="icolist">
            <a href="/Manage/WordAD" title="过期文字广告：共计<%:ViewBag.WordAD %>篇">
                <img src="../../../../Content/images/05.gif" alt="过期文字广告：共计<%:ViewBag.WordAD %>篇" /><br />
                过期文字广告<br />
                共计<%:ViewBag.WordAD %>篇</a>
        </div>
        <%} %>
        <%if (SystemMemberShip.GetSystemAuthority(Session["SystemAuthority"].ToString(), SystemMemberShip.WordADManage))
          { %>
        <div class="icolist">
            <a href="/Manage/FlashAD" title="过期Flash广告：共计<%:ViewBag.FlashAD %>篇">
                <img src="../../../../Content/images/08.gif" alt="过期Flash广告：共计<%:ViewBag.FlashAD %>篇" /><br />
                过期Flash广告<br />
                共计<%:ViewBag.FlashAD %>篇</a>
        </div>
        <%} %>
    </fieldset>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="headOtherInfo" runat="server">
    <link type="text/css" href="<%:Url.Content("~/Libs/ligerUI/skins/Aqua/css/ligerui-all.css") %>"
        rel="stylesheet" />
    <link type="text/css" href="<%:Url.Content("~/App_Themes/"+ConfigurationManager.AppSettings["ManageTheme"].ToString()+"/css/general.css") %>"
        rel="stylesheet" />
    <link type="text/css" href="<%:Url.Content("~/App_Themes/"+ConfigurationManager.AppSettings["ManageTheme"].ToString()+"/css/master.css") %>"
        rel="stylesheet" />
    <link type="text/css" href="<%:Url.Content("~/App_Themes/"+ConfigurationManager.AppSettings["ManageTheme"].ToString()+"/css/home.css") %>"
        rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="TabTitleContent" runat="server">
    欢迎界面
</asp:Content>


