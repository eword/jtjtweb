<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl`1[[MvcSiteMapProvider.Web.Html.Models.MenuHelperModel,MvcSiteMapProvider]]" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="MvcSiteMapProvider.Web.Html.Models" %>
<%@ Import Namespace="eulei.shop.Code" %>
<ul id="menu">
    <% foreach (var node in Model.Nodes)
       { %> 
    <li>
        <% if (node.IsInCurrentPath)
           {%>
        <a href="<%=node.Url %>" class="click">
            <%=node.Title%></a>
        <%}
           else
           { %>
        <%=Html.DisplayFor(m => node)%>
        <%} %>
        <% if (node.Children.Any())
           { %>
        <%=Html.DisplayFor(m => node.Children)%>
        <% } %>
    </li>
    <% } %>
</ul>
