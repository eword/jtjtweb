<%@ Page Language="C#"  Inherits="System.Web.Mvc.ViewPage<eulei.shop.Models.LogOnModel>" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01//EN" "http://www.w3.org/TR/html4/strict.dtd">
<html>
<head runat="server">
    <title>登入-<%=ConfigurationManager.AppSettings["webTitle"].ToString()%></title>
    <link rel="icon" href="~/Content/images/favicon.ico" type="image/x-icon" />
    <link rel="shortcut icon" href="~/Content/images/favicon.ico" type="image/x-icon" />    
    <link type="text/css" href="../../App_Themes/manage/css/login.css"
        rel="stylesheet" />
</head>
<body class="lgdbg">
    <center>
        <script type="text/javascript" src="<%: Url.Content("~/Scripts/jquery-1.4.4.min.js") %>"></script>
        <script src="<%: Url.Content("~/Scripts/jquery.validate.min.js") %>" type="text/javascript"></script>
        <script src="<%: Url.Content("~/Scripts/jquery.validate.unobtrusive.min.js") %>"
            type="text/javascript"></script>
        <div class="loginbg">
            <% using (Html.BeginForm())
               { %>
            <div class="loginValidationMessage" >
                <div>
                    <%: Html.ValidationSummary(true, "登入失败. 请重试！") %></div>
                <div>
                    <%: Html.ValidationMessageFor(m => m.UserName) %></div>
                <div>
                    <%: Html.ValidationMessageFor(m => m.Password) %></div>
            </div>
            <div class="loginUserName">
                <%: Html.TextBoxFor(m => m.UserName) %>
            </div>
            <div class="loginPassword">
                <%: Html.PasswordFor(m => m.Password) %>
            </div>
            <div class="loginbutton" style="">
                <%: Html.CheckBoxFor(m => m.RememberMe) %>
                <input type="submit" value="    " />
            </div>
            <% } %>
        </div>
    </center>
</body>
</html>
