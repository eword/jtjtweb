<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Product/Views/Shared/Product_Areas.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<eulei.shop.Models.TV_Merchandise>>" %>
<%@ Import Namespace="Q42.Wheels.Mvc.Paging" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
       <%=ViewBag.Title %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="ui-box">
        <div class="ui-box-title">
            <h3>
                <%=ViewBag.Title %></h3>
        </div>
        <div class="ui-box-content">
            <ul class="ware-list">
                <% foreach (var item in this.GetPage(Model))
                   { %>
                <li><a href="<%:Url.Content("~/Product/Home/Details/"+item.MerchandiseID) %>" title=" <%: item.MerchandiseTitle %>">
                    <img alt="<%: item.MerchandiseTitle %>" src="<%:Url.Content(item.MerchandiseTitlePicUrl) %>" />
                    <strong>
                        <%: item.MerchandiseTitle %></strong> </a></li>
                <% } %>
            </ul>
        </div>
    </div>
    <div class="pager">
        <%= Html.RenderPagination() %>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="headOtherInfo" runat="server">
    <link type="text/css" href="<%:Url.Content("~/App_Themes/"+ConfigurationManager.AppSettings["ProductTheme"].ToString()+"/css/general.css") %>"
        rel="stylesheet" />
    <link type="text/css" href="<%:Url.Content("~/App_Themes/"+ConfigurationManager.AppSettings["ProductTheme"].ToString()+"/css/master.css") %>"
        rel="stylesheet" />
    <link type="text/css" href="<%:Url.Content("~/App_Themes/"+ConfigurationManager.AppSettings["ProductTheme"].ToString()+"/css/product.css") %>"
        rel="stylesheet" />
                    <link type="text/css" href="<%:Url.Content("~/Libs/ligerUI/skins/Aqua/css/ligerui-all.css") %>"
        rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="LeftContent" runat="server">
 <%Html.RenderAction("Index", "ContentNav", new { @id = ViewBag.NavID, @area = "Product" }); %>
</asp:Content>

