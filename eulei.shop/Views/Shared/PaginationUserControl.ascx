<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Q42.Wheels.Mvc.Paging.Paging>" %>
<%@ Import Namespace="Q42.Wheels.Mvc.Paging" %>
<div>
    <div style=" float:left; width:auto;">
        共有<%=Model.TotalNumber %>条记录，当前第<%=Model.CurrentPage+1 %>/<%=Model.NumberPages %>页</div>
  <div style=" float:right; width:auto;">
        <% if (!Model.IsFirstPage)
           { %><%= Html.RenderPageLink("上一页", Model.CurrentPage - 1, new { @class = "custompaginationlink" }, false) %><% } %>
        <% for (int i = 0; i < Model.NumberPages; i++)
           { %>
        <% if (i == 0 && Model.CurrentPage > 2)
           { %>
        <%= Html.RenderPageLink("首页", i, new { @class = "custompaginationlink" })%>
        <% } %>
        <% if (Math.Abs(Model.CurrentPage - i) < 5)
           { %>
        <%= Html.RenderPageLink((i + 1).ToString(), i, new { @class = "custompaginationlink" })%>
        <% } %>
        <% if (i == Model.NumberPages - 1 && Model.CurrentPage < Model.NumberPages - 5)
           { %>
        <%= Html.RenderPageLink("尾页", i, new { @class = "custompaginationlink" })%>
        <% } %>
        <% } %>
        <% if ((!Model.IsLastPage)&&(Model.NumberPages>0))
           { %><%= Html.RenderPageLink("下一页", Model.CurrentPage + 1, new { @class = "custompaginationlink" })%><% } %>
    </div>
</div>
