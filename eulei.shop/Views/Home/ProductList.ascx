<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<eulei.shop.Models.TV_Merchandise>>" %>
<% foreach (var item in Model)
   { %>
<div class="merchandise">

    <img src="<%:Url.Content(item.MerchandiseTitlePicUrl) %>" alt="<%:item.MerchandiseTitle %>" />
    <div class="merchandise-info">
        <h6 class="overflowHidden">
            <%:Html.ActionLink(item.MerchandiseTitle, "Details", "Home", new { @Area = "Product", @id = item.MerchandiseID }, new { @target = "_blank", @class = "tblack" })%></h6>
        <div>
            <%=item.MerchandiseTitleDescription %>
        </div>
    </div>
</div>
<% } %>
