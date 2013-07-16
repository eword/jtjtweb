<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl`1[[MvcSiteMapProvider.Web.Html.Models.MenuHelperModel,MvcSiteMapProvider]]" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="MvcSiteMapProvider.Web.Html.Models" %>
<%@ Import Namespace="eulei.shop.Code" %>
<% if (Model.Nodes.Count > 1)
   { 
       bool _isFirst = true;
       foreach (var node in Model.Nodes)
       { %>
<% if (_isFirst)
   {%>
<h3>
    <%=node.Title%></h3>
<div>
    <ul>
        <%
       _isFirst = false;
   }
   else
   { %>
        <li><a href="<%=node.Url %>" title="<%=node.Title %>">
            <%=node.Title %></a> </li>
        <%}
    } %>
    </ul>
</div>
<%} %>
