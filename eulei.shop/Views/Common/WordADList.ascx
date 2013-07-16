<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<eulei.shop.Models.TB_Advertisement>>" %>
<ul class="tmargin bmargin">
    <% foreach (var item in Model)
       { %>
    <li class="list-dot tleft"><a href="<%:Url.Content(item.AdvertisementUrl) %>" title="<%:item.AdvertisementUrl %>"
        class="tblue">
        <%:item.AdvertisementTitle %></a> </li>
    <% } %>
</ul>
