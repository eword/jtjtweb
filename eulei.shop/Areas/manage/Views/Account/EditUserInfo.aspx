<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/manage/Views/Shared/Manage_Areas.Master" Inherits="System.Web.Mvc.ViewPage<eulei.shop.Models.VW_SA_UserInfo>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    完善用户信息
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" src="<%: Url.Content("~/Scripts/jquery.validate.min.js") %>"></script>
    <script type="text/javascript" src="<%: Url.Content("~/Content/JqueryUI/js/jquery-ui-1.9.1.custom.min.js") %>"></script>
    <script type="text/javascript" src="<%: Url.Content("~/Libs/ligerUI/js/core/base.js") %>"></script>
    <script type="text/javascript" src="<%: Url.Content("~/Libs/ligerUI/js/ligerui.min.js") %>"></script>
    <script type="text/javascript" src="<%: Url.Content("~/Libs/ligerUI/js/plugins/ligerComboBox.js") %>"></script>
    <script type="text/javascript" src="<%: Url.Content("~/Libs/ligerUI/js/plugins/ligerTree.js") %>"></script>
           
                    <% using (Html.BeginForm("EditUserInfo", "Account",new{@area="manage"},FormMethod.Post, new { @id = "postForm" }))
                       { %>
                       <%:Html.Hidden("_returnUrl",ViewData["_returnUrl"]) %>
                    <table>
                        <tr class="lbg">
                            <td class="editor-label">
                                账号：
                            </td>
                            <td class="editor-field" colspan="3">
                                     <%:Html.HiddenFor(m=>m.UserName) %>
                               <%:Model.UserName %>
                            </td>
                        </tr>
                                                <tr class="lbgalt">
                            <td class="editor-label">
                                姓名：
                            </td>
                            <td class="editor-field">
                                <%:Html.TextBoxFor(m => m.UserInfoFriendName)%>
                            </td>
                                                                                <td class="editor-label">
                               
                            </td>
                            <td class="editor-field">

                            </td>
                        </tr>   
                        <tr class="lbg">
                            <td class="editor-label">
                                单位/部门：
                            </td>
                            <td class="editor-field">
                                <input id="FrameworkIDCombobox" type="text" />
                            </td>
                            <td class="editor-label">
                               
                            </td>
                            <td class="editor-field">

                            </td>
                        </tr>         
                                                                                             <tr class="lbgalt">
                            <td class="editor-label">
                                电话：
                            </td>
                            <td class="editor-field">
                                <%:Html.TextBoxFor(m => m.UserInfoTEL)%>
                            </td>
                            <td class="editor-label">
                               
                            </td>
                            <td class="editor-field">

                            </td>
                        </tr>               
                        <tr class="lbg">
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
        $(function () {
            $("#FrameworkIDCombobox").ligerComboBox({
                width: 200,
                height: 20,
                selectBoxWidth: 200,
                selectBoxHeight: 200,
                initValue:<%=Model.UserInfoFrameworkID %>,
                textField: 'textcontent', valueField: 'id', valueFieldID: 'UserInfoFrameworkID', treeLeafOnly: true, hideOnLoseFocus: true,
                tree: {
                    url: '<%: Url.Content("~/Common/GetJsonForFrameworkID") %>',
                    checkbox: false,
                    textFieldName: "textcontent",
                    idFieldName: "id",
                    btnClickToToggleOnly: false,
                    single: true,
                    needCancel: false,
                    parentIDFieldName: "fid"
                }
            });
            $(".btnsubmit").bind("click", function (event) {

                var _return = "";
                if ($("#FrameworkIDCombobox").val() == "") {
                    _return += '<div>请选择单位/部门！</div>';
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
                    UserInfoFriendName: { required: true },
                    UserInfoTEL: { required: true }
                },
                messages: {
                    UserInfoFriendName: '请填写姓名！',
                    UserInfoTEL: '请填写电话号码！'
                },
                //                              success: function (label) {
                //                               label.addClass("valid").text("√")
                //                              },
                errorContainer: "div.error",
                errorLabelContainer: $("div.error"),
                wrapper: "div"
                //submitHandler: function () { alert("操作已完成!") }
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
        <link type="text/css" href="<%:Url.Content("~/App_Themes/"+ConfigurationManager.AppSettings["ManageTheme"].ToString()+"/css/UserInfo.css") %>"
        rel="stylesheet" />    <link rel="stylesheet" href="<%: Url.Content("~/Content/JqueryUI/css/smoothness/jquery-ui-1.9.1.custom.min.css") %>"
        type="text/css" media="all" />
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="TabTitleContent" runat="server">
    信息完善
</asp:Content>
