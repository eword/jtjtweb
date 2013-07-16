<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<div>
    <div style="float: left; width: auto;">
        共有<%=ViewBag.LastPage+1%>页，当前第<%=ViewBag.CurrentPage + 1%>/<%=ViewBag.LastPage+1%>页</div>
    <div style="float: right; width: auto;">
        <% if (ViewBag.CurrentPage != ViewBag.FirstPage)
           { %>
        <a href="<%=ViewBag.Url %>&PageNum=<%= ViewBag.CurrentPage - 1 %>" title="上一页" class="custompaginationlink">
            上一页</a>
        <% } %>
        <% for (int i = 0; i <=ViewBag.LastPage; i++)
           { %>
        <% if (i == 0 && ViewBag.CurrentPage > 4)
           { %>
        <a class="<%=ViewBag.CurrentPage==i?"custompaginationlink selected custompaginationlink_selected":"custompaginationlink"%>"
            href="<%=ViewBag.Url %>&PageNum=<%= i %>" title="首页">首页</a>
        <% } %>
        <% if (Math.Abs(ViewBag.CurrentPage - i) < 5)
           { %>
        <a class="<%=ViewBag.CurrentPage==i?"custompaginationlink selected custompaginationlink_selected":"custompaginationlink"%>"
            href="<%=ViewBag.Url %>&PageNum=<%= i %>" title="<%= i+1 %>">
            <%= i+1 %></a>
        <% } %>
        <% if (i == ViewBag.LastPage&& ViewBag.CurrentPage < ViewBag.LastPage - 4)
           { %>
        <a class="<%=ViewBag.CurrentPage==i?"custompaginationlink selected custompaginationlink_selected":"custompaginationlink"%>"
            href="<%=ViewBag.Url %>&PageNum=<%= i %>" title="尾页">
            尾页</a>
        <% } %>
        <% } %>
        <% if (ViewBag.CurrentPage != ViewBag.LastPage)
           { %>
        <a class="custompaginationlink" href="<%=ViewBag.Url %>&PageNum=<%= ViewBag.CurrentPage + 1 %>"
            title="下一页">下一页</a>
        <% } %>
    </div>
</div>
