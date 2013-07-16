<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/manage/Views/Shared/Manage_Areas.Master"
    Inherits="System.Web.Mvc.ViewPage<eulei.shop.Models.TV_Merchandise>" %>
<%@ Import Namespace="eulei.shop.Content" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    商品审核
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script src="<%: Url.Content("~/Content/JqueryUI/js/jquery-ui-1.9.1.custom.min.js") %>"
        type="text/javascript"></script>
    <script type="text/javascript">
        $(function () { $("#tabs").tabs(); }); 

    </script>
    <br />
    <h3>
        您真的确定要审核并发布该商品吗？</h3>
    <% using (Html.BeginForm())
       { %>
    <p>
        <%:Html.Hidden("_returnUrl", ViewData["_returnUrl"].ToString())%>
        <input type="submit" value="审核通过" />
        | <a href="<%= ViewData["_returnUrl"].ToString() %>">返回</a>
    </p>
    <% } %>
    <hr />
    <div class="merchandise-box">
        <h3>
            <%:Model.MerchandiseTitle %></h3>
        <hr />
        <div class="merchandise-box-top">
            <img src="<%:Url.Content(Model.MerchandiseTitlePicUrl) %>" alt="<%:Model.MerchandiseName %>" />
            <div class="merchandise-box-top-right">
                <h3>
                    <%:Model.MerchandiseName %></h3>
                <div class="merchandise-box-top-synopsis">
                    <%=Model.MerchandiseSynopsis %>
                </div>
            </div>
        </div>
        <div id="tabs">
            <ul>
                <li><a href="#tabs-1">商品信息</a></li>
                <%if (!string.IsNullOrEmpty(Model.MerchandiseAfterSaleService))
                  { %>
                <li><a href="#tabs-2">
                    <%=Resource.Article_AfterSaveService%></a></li>
                <%} %>
            </ul>
            <div id="tabs-1">
                <%= Model.MerchandiseDescription %>
            </div>
            <%if (!string.IsNullOrEmpty(Model.MerchandiseAfterSaleService))
              { %>
            <div id="tabs-2">
                <%= Model.MerchandiseAfterSaleService %>
            </div>
            <%} %>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="headOtherInfo" runat="server">
    <link type="text/css" href="<%:Url.Content("~/Libs/ligerUI/skins/Aqua/css/ligerui-all.css") %>"
        rel="stylesheet" />
    <link type="text/css" href="<%:Url.Content("~/App_Themes/"+ConfigurationManager.AppSettings["ManageTheme"].ToString()+"/css/general.css") %>"
        rel="stylesheet" />
    <link type="text/css" href="<%:Url.Content("~/App_Themes/"+ConfigurationManager.AppSettings["ManageTheme"].ToString()+"/css/master.css") %>"
        rel="stylesheet" />
    <link type="text/css" href="<%:Url.Content("~/App_Themes/"+ConfigurationManager.AppSettings["ManageTheme"].ToString()+"/css/ProductAuditing.css") %>"
        rel="stylesheet" />
    <link href="<%:Url.Content("~/Content/JqueryUI/css/smoothness/jquery-ui-1.9.1.custom.min.css") %>"
        rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="TabTitleContent" runat="server">
    商品审核
</asp:Content>
