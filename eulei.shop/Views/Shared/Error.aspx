<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>Error.aspx</title>
        <link type="text/css" href="<%:Url.Content("~/App_Themes/"+ConfigurationManager.AppSettings["IndexTheme"].ToString()+"/css/default.css") %>"
        rel="stylesheet" />
</head>
<body>
<center>
<br />
<br />
<br />
<br />
<br />
    <div>
        啊！出错了，休息一下！喝杯茶缓解一下情绪。
    </div>   
    <div>
       <strong class="tred">原因：</strong><%Response.Write(ViewBag.MyContent); %>
    </div>
</center>
</body>
</html>
