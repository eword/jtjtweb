<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl`1[[MvcSiteMapProvider.Web.Html.Models.MenuHelperModel,MvcSiteMapProvider]]" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="MvcSiteMapProvider.Web.Html.Models" %>
<%@ Import Namespace="eulei.shop.Code" %>
<ul>
    <% 
        bool _isFirst = true;
        foreach (var node in Model.Nodes)
        { %>
    <%if (!node.Area.Equals("CopyRight"))
      {
          continue;
      }
    %>
    <% if (_isFirst)
       {%>
    <li>
        <%
           _isFirst = false;
       }
       else
       { %>
        <li class="divide">
            <% } %>
            <%=Html.DisplayFor(m => node)%>
            <% if (node.Children.Any())
               { %>
            <%=Html.DisplayFor(m => node.Children)%>
            <% } %>
        </li>
        <%} %>
</ul>
