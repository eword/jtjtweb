<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<% using (
       Ajax.BeginForm("ReSetPassWord", "Account", new { @area = "manage" }, new AjaxOptions { UpdateTargetId = "ReSetPassWordAjaxBox" }, new { @id = "postForm" }))
       //Html.BeginForm("ReSetPassWord", "Account", new { @area = "manage" }, FormMethod.Post, new { @id = "postForm" }))
   { %>
<table>
    <tr class="lbg">
        <td class="editor-label">账号：
            </td>
        <td class="editor-field" colspan="3">
            <%:ViewBag.UserName %>
            <%:Html.Hidden("UserName",(string)ViewBag.UserName) %>
            </td>
    </tr>
    <tr class="lbg">
        <td class="editor-label">姓名：
            </td>
        <td class="editor-field" colspan="3">
            <%:ViewBag.UserInfoFriendName %>
            </td>
    </tr>
    <tr class="lbg">
        <td class="editor-label">新密码：
            </td>
        <td class="editor-field" colspan="3">
            <%: Html.Password("Password") %>            
            </td>
    </tr>
    <tr class="lbg">
        <td class="editor-label">确认密码：
            </td>
        <td class="editor-field" colspan="3">
            <%: Html.Password("ConfirmPassword") %>
            </td>
    </tr>
    <tr class="lbg">
        <td colspan="4">
            <div class="error">
            </div>
    <%--        <input type="submit" class="btnsubmit fr" value="提交" />--%>
        </td>
    </tr>
    <% } %>
</table>
<script type="text/javascript">

    $(function () {
        $("#dialog-form-ReSetPassWord").dialog("open");
    });
</script>
