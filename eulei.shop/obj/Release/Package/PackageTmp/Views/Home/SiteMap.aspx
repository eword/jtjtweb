<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>SiteMap</title>
</head>
<body>
    <div>
        <%:Html.MvcSiteMap().SiteMap() %>
    </div>
</body>
</html>
