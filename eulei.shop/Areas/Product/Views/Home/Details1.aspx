<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Product/Views/Shared/Product_Areas.Master"
    Inherits="System.Web.Mvc.ViewPage<eulei.shop.Models.TV_Merchandise>" %>

<%@ Import Namespace="eulei.shop.Content" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%:ConfigurationManager.AppSettings["webTitle"].ToString()%>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="headOtherInfo" runat="server">
    <link type="text/css" href="<%:Url.Content("~/App_Themes/"+ConfigurationManager.AppSettings["ProductTheme"].ToString()+"/css/general.css") %>"
        rel="stylesheet" />
    <link type="text/css" href="<%:Url.Content("~/App_Themes/"+ConfigurationManager.AppSettings["ProductTheme"].ToString()+"/css/master.css") %>"
        rel="stylesheet" />
    <link type="text/css" href="<%:Url.Content("~/App_Themes/"+ConfigurationManager.AppSettings["ProductTheme"].ToString()+"/css/product.css") %>"
        rel="stylesheet" />
    <link href="<%:Url.Content("~/Content/JqueryUI/css/smoothness/jquery-ui-1.9.1.custom.min.css") %>"
        rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        $(function () { $("#tabs").tabs(); }); 

    </script>
    <div class="merchandise-box">
        <h3>
            <%:Model.MerchandiseTitle %></h3>
        <hr />
        <div class="merchandise-box-top">
            <img src="<%:Url.Content(Model.MerchandiseTitlePicUrl) %>" alt="<%:Model.MerchandiseName %>" />
            <div class="merchandise-box-top-right">
                <h6>
                    <%:Model.MerchandiseName %></h6>
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
<asp:Content ID="Content3" ContentPlaceHolderID="LeftContent" runat="server">
    <%Html.RenderAction("Details", "ContentNav", new { @id = ViewBag.NavID, @area = "Product" }); %>
</asp:Content>
