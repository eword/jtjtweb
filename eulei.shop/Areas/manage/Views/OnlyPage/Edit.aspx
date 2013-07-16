<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/manage/Views/Shared/Manage_Areas.Master" Inherits="System.Web.Mvc.ViewPage<eulei.shop.Models.TB_OnlyPage>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    编辑单页文章
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" src="<%: Url.Content("~/Scripts/jquery.validate.min.js") %>"></script>
    <script type="text/javascript" src="<%: Url.Content("~/ckeditor/ckeditor.js") %>"></script>
    <script type="text/javascript" src="<%: Url.Content("~/ckfinder/ckfinder.js") %>"></script>
    <% using (Html.BeginForm("Edit", "OnlyPage",new{@area="manage"},FormMethod.Post, new { @id = "postForm" }))
                       { %>
    <%:Html.Hidden("_returnUrl",ViewData["_returnUrl"]) %>
    <table>
        <tr class="lbg">
            <td class="editor-label">
                标题：
            </td>
            <td class="editor-field" colspan="3">
                <%:Html.TextBoxFor(m => m.OnlyPageTitle)%>
            </td>
        </tr>
        <tr class="lbgalt">
            <td class="editor-label">
                内容：
            </td>
            <td class="editor-field" colspan="3">
                <%:Html.TextAreaFor(m => m.OnlyPageContent, new { @class = "minheight300" })%>
            </td>
        </tr>
        <tr class="lbg">
            <td class="editor-label">
                标签
            </td>
            <td class="editor-field" colspan="3">
                <%:Html.TextBoxFor(m => m.OnlyPageLabel)%>
                <br />
                <strong class="red">多个标签用“,”分隔开</strong>
            </td>
        </tr>
        <tr class="lbgalt">
            <td colspan="4">
                <div class="error">
                </div>
                <input type="submit" class="btnsubmit fr" value="提交" />
            </td>
        </tr>
    </table>
    <% } %>
    <div>
        <a href="<%:ViewData["_returnUrl"]%>">返回列表</a>
    </div>
    <script type="text/javascript">
        CKEDITOR.replace('OnlyPageContent');
    </script>
    <script type="text/javascript">
        $(function () {
            $(".btnsubmit").bind("click", function (event) {

                var _return = "";

                if (CKEDITOR.instances.OnlyPageContent.getData() == "") {
                    _return += '<div>请添加内容</div>';
                }


                $("div.error").html("");

                if (_return != "") {
                    $("div.error").append(_return);
                    _return = "";
                    return false;
                }

            });

            $("#postForm").validate({
                onfocusout: false,
                onkeyup: false,
                onclick: false,
                rules: {
                    OnlyPageTitle: { required: true }
                },
                messages: {
                    OnlyPageTitle: '请填写标题！'
                },
                errorContainer: "div.error",
                errorLabelContainer: $("div.error"),
                wrapper: "div"
            });
        });      
    </script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="headOtherInfo" runat="server">
    <link type="text/css" href="<%:Url.Content("~/Libs/ligerUI/skins/Aqua/css/ligerui-all.css") %>"
        rel="stylesheet" />
    <link type="text/css" href="<%:Url.Content("~/App_Themes/"+ConfigurationManager.AppSettings["ManageTheme"].ToString()+"/css/general.css") %>"
        rel="stylesheet" />
    <link type="text/css" href="<%:Url.Content("~/App_Themes/"+ConfigurationManager.AppSettings["ManageTheme"].ToString()+"/css/master.css") %>"
        rel="stylesheet" />
    <link type="text/css" href="<%:Url.Content("~/App_Themes/"+ConfigurationManager.AppSettings["ManageTheme"].ToString()+"/css/OnlyPage.css") %>"
        rel="stylesheet" />
    <link rel="stylesheet" href="<%: Url.Content("~/Content/JqueryUI/css/smoothness/jquery-ui-1.9.1.custom.min.css") %>"
        type="text/css" media="all" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="TabTitleContent" runat="server">
    添加单页
</asp:Content>

