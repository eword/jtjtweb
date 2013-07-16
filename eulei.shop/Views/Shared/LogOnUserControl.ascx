<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="eulei.shop.Code" %>
<%
    if (Request.IsAuthenticated) {
%>
        欢迎您： <b><%=new CurrentLoginUser().GetUserInfo(Page.User.Identity.Name).FriendlyName %>(<%: Page.User.Identity.Name %>)</b>!
        [ <%: Html.ActionLink("退出", "LogOff", "Account", new { @area="",@_returnUrl=Request.Url.PathAndQuery},null)%> ]
<%
    }
    else {
%> 
        [ <%: Html.ActionLink("登入", "LogOn", "Account", new { @area = "" }, null)%> ]
<%
    }
%>
