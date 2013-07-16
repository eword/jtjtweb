<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<eulei.shop.Models.TB_Merchandise>>" %>
<ul class="ware-list">
    <% foreach (var item in Model)
       { %>
    <li><a href="<%:Url.Content("~/Product/Home/Details/"+item.MerchandiseID) %>" title=" <%: item.MerchandiseTitle %>">
        <img alt="<%: item.MerchandiseTitle %>" src="<%:Url.Content(item.MerchandiseTitlePicUrl) %>" />
        <strong>
            <%: item.MerchandiseTitle %></strong> </a></li>
    <% } %>
</ul>
