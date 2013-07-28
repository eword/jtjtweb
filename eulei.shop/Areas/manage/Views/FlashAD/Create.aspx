<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/manage/Views/Shared/Manage_Areas.Master"
    Inherits="System.Web.Mvc.ViewPage<eulei.shop.Models.TB_FlashADXML>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    添加广告
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" src="<%: Url.Content("~/Scripts/jquery.validate.min.js") %>"></script>
    <%--<script type="text/javascript" src="<%: Url.Content("~/ckeditor/ckeditor.js") %>"></script>--%>
    <script type="text/javascript" src="<%: Url.Content("~/ckfinder/ckfinder.js") %>"></script>
    <script type="text/javascript" src="<%: Url.Content("~/Content/JqueryUI/js/jquery-ui-1.9.1.custom.min.js") %>"></script>
    <script type="text/javascript" src="<%: Url.Content("~/Libs/ligerUI/js/core/base.js") %>"></script>
    <script type="text/javascript" src="<%: Url.Content("~/Libs/ligerUI/js/ligerui.min.js") %>"></script>
    <script type="text/javascript" src="<%: Url.Content("~/Libs/ligerUI/js/plugins/ligerComboBox.js") %>"></script>
    <script type="text/javascript" src="<%: Url.Content("~/Libs/ligerUI/js/plugins/ligerTree.js") %>"></script>
    <% using (Html.BeginForm("Create", "FlashAD", new { @area = "manage" }, FormMethod.Post, new { @id = "postForm" }))
       { %>
    <%:Html.Hidden("_returnUrl",ViewData["_returnUrl"]) %>
    <table>
        <tr class="lbg">
            <td class="editor-label">
                标题：
            </td>
            <td class="editor-field" colspan="3">
                <%:Html.TextBoxFor(m => m.FlashADXMLTitle)%>
            </td>
        </tr>
        <tr class="lbgalt">
            <td class="editor-label">
                广告分类：
            </td>
            <td class="editor-field">
                <input id="FlashADXMLTypeIDCombobox" type="text" />
            </td>
            <td class="editor-label">
                提醒时间：
            </td>
            <td class="editor-field">
                <%:Html.TextBox("FlashADXMLRemindDate", DateTime.Now.ToShortDateString(), new { @class = "datecss" })%>
            </td>
        </tr>
        <tr class="lbg">
            <td class="editor-label">
                图片地址：
            </td>
            <td class="editor-field" colspan="3">
                        <table>
                <tr>
                    <td>
                        <%:Html.TextBoxFor(m=>m.FlashADXMLPicUrl) %>
                    </td>
                    <td class="w80">
                        <input id="btnUpload" value="上传" type="button" class="btnsubmit fr" />
                    </td>
                </tr>
            </table>
               
            </td>
        </tr>
                <tr class="lbgalt">
            <td class="editor-label">
                链接地址：
            </td>
            <td class="editor-field" colspan="3">
                                    <table>
                <tr>
                    <td>
                         <%:Html.TextBoxFor(m=>m.FlashADXMLRedirectUrl) %>
                    </td>
                    <td class="w80">
                        <input id="btnUrlSelect" value="选择" type="button" class="btnsubmit fr" />
                    </td>
                </tr>
            </table>               
            </td>
        </tr>
        <tr class="lbg">
            <td colspan="4">
                <div class="error">
                </div>
                <input type="submit" id="btnsubmit" class="btnsubmit fr" value="提交" />
            </td>
        </tr>
    </table>
    <% } %>
    <div>
        <a href="<%:ViewData["_returnUrl"]%>">返回列表</a>
    </div>
    <script type="text/javascript">
        $(function () {
            $("#FlashADXMLRemindDate").attr("readonly", "true").datepicker({ dateFormat: 'yy/mm/dd' });

            $("#FlashADXMLTypeIDCombobox").ligerComboBox({
                width: 150,
                height: 20,
                selectBoxWidth: 200,
                selectBoxHeight: 200,
                textField: 'textcontent', valueField: 'id', valueFieldID: 'FlashADXMLType', treeLeafOnly: false, hideOnLoseFocus: true,
                tree: {
                    url: '<%: Url.Content("~/Common/GetJsonForAdvertisementTypeID") %>',
                    checkbox: false,
                    textFieldName: "textcontent",
                    idFieldName: "id",
                    parentIDFieldName: "fid"
                }
            });

            $("#btnsubmit").bind("click", function (event) {

                var _return = "";



                if ($("#FlashADXMLTypeIDCombobox").val() == "") {
                    _return += '<div>请选择广告分类</div>';
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
                    FlashADXMLTitle: { required: true },
                    FlashADXMLPicUrl: { required: true },
                    FlashADXMLRedirectUrl: { required: true },
                    FlashADXMLRemindDate: { required: true, dateISO: true }
                },
                messages: {
                    FlashADXMLTitle: '请填写标题！',
                    FlashADXMLPicUrl: '请填图片链接地址！',
                    FlashADXMLRedirectUrl: '请填跳转链接地址！',
                    FlashADXMLRemindDate: { required: "请填写提醒日期！", dateISO: "日期格式错误！" }

                },
                errorContainer: "div.error",
                errorLabelContainer: $("div.error"),
                wrapper: "div"
            });
            $("#btnUpload").bind("click", function (event) {
                BrowseServer("FlashADXMLPicUrl");
            });
            function BrowseServer(inputId) {
                var finder = new CKFinder();
                finder.basePath = '/ckfinder';  //导入CKFinder的路径                
                finder.selectActionFunction = SetFileField; //设置文件被选中时的函数               
                finder.selectActionData = inputId;  //接收地址的input ID               
                finder.popup();
            }
            //文件选中时执行               
            function SetFileField(fileUrl, data) {
                document.getElementById(data["selectActionData"]).value = ("~" + fileUrl);
            }
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
    <link type="text/css" href="<%:Url.Content("~/App_Themes/"+ConfigurationManager.AppSettings["ManageTheme"].ToString()+"/css/FlashAD.css") %>"
        rel="stylesheet" />
    <link rel="stylesheet" href="<%: Url.Content("~/Content/JqueryUI/css/smoothness/jquery-ui-1.9.1.custom.min.css") %>"
        type="text/css" media="all" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="TabTitleContent" runat="server">
    添加广告
</asp:Content>
