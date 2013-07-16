<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/manage/Views/Shared/Manage_Areas.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
   用户权限预览
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <script type="text/javascript" src="<%: Url.Content("~/Libs/ligerUI/js/core/base.js") %>"></script>
    <script type="text/javascript" src="<%: Url.Content("~/Libs/ligerUI/js/ligerui.min.js") %>"></script>
    <script type="text/javascript" src="<%: Url.Content("~/Libs/ligerUI/js/plugins/ligerComboBox.js") %>"></script>
    <script type="text/javascript" src="<%: Url.Content("~/Libs/ligerUI/js/plugins/ligerTree.js") %>"></script>
    <script type="text/javascript" src="<%: Url.Content("~/Content/js/confirm.js") %>"></script>
    <script type="text/javascript" src="<%: Url.Content("~/Content/themes/ui/js/jquery-ui-1.8.22.custom.min.js") %>"></script>
    <script type="text/javascript" src="<%: Url.Content("~/Scripts/jquery.validate.min.js") %>"></script>
    <script type="text/javascript" src="<%: Url.Content("~/Scripts/jquery.unobtrusive-ajax.min.js") %>"></script>
<script type="text/javascript">
    $(function () {
        var userName = '';
        userName = "<%:ViewBag._UserName%>";
        userName = encodeURI(userName);
        userName = encodeURI(userName);
        $("#authorityList").ligerTree({
            url: '<%: Url.Content("~/manage/account/GetJsonForUserAuthority?id=") %>' + userName + '&time=' + Math.random(),
            checkbox: true,
            ischecked: true,
            single: false,
            textFieldName: "textcontent",
            idFieldName: "id",
            parentIDFieldName: "fid"
        });
        $("#ArticleTypeList").ligerTree({
            url: '<%: Url.Content("~/manage/account/GetJsonForUserArticleType?id=") %>' + userName + '&time=' + Math.random(),
            checkbox: true,
            ischecked: true,
            single: false,
            textFieldName: "textcontent",
            idFieldName: "id",
            parentIDFieldName: "fid"
        });
    });
</script>
    <h2>账号：<%:ViewBag._UserName%></h2>
<table>
    <tr>
        <td class="td-middle" >功能权限</td>
       <td class="td-right">文章分类权限</td>
    </tr>
    <tr>
        <td>
            <ul id="authorityList" style="float:left;vertical-align:text-top; text-align:left;height:350px;"></ul>
        </td>
        <td>
            <ul id="ArticleTypeList"  style="float:left;vertical-align:text-top; text-align:left;height:350px;"></ul>
        </td>
    </tr>
</table>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="headOtherInfo" runat="server">
        <link type="text/css" href="<%:Url.Content("~/Libs/ligerUI/skins/Aqua/css/ligerui-all.css") %>" rel="stylesheet" />
    <link type="text/css" href="<%:Url.Content("~/App_Themes/"+ConfigurationManager.AppSettings["ManageTheme"].ToString()+"/css/general.css") %>"
        rel="stylesheet" />
    <link type="text/css" href="<%:Url.Content("~/App_Themes/"+ConfigurationManager.AppSettings["ManageTheme"].ToString()+"/css/master.css") %>"
        rel="stylesheet" />
    <link type="text/css" href="<%:Url.Content("~/App_Themes/"+ConfigurationManager.AppSettings["ManageTheme"].ToString()+"/css/RoleInfo.css") %>"
        rel="stylesheet" />
    <link rel="stylesheet" href="<%: Url.Content("~/Content/themes/ui/css/smoothness/jquery-ui-1.8.22.custom.css") %>"
        type="text/css" media="all" />
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="TabTitleContent" runat="server">
    权限预览
</asp:Content>
