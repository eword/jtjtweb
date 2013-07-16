<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<eulei.shop.Models.TB_Link>>" %>
<% foreach (var item in Model)
   { %>
<div class="bottom-left-link">
    <h6 class="fl">
        <a href="<%:item.LinkUrl %>" title="<%:item.LinkTitle %>" target="_blank" class = "tblack" >
            <img class="fl" src="<%:Url.Content(item.LinkPicUrl) %>" alt="<%:item.LinkTitle %>" />
            <%:item.LinkTitle %>
        </a>
    </h6>
</div>
<% } %>
