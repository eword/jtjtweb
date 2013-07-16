<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/manage/Views/Shared/Manage_Areas.Master"
    Inherits="System.Web.Mvc.ViewPage<eulei.shop.Models.TB_Merchandise>" %>
        <%@ Import Namespace="eulei.shop.Content" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    添加商品
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" src="<%: Url.Content("~/Scripts/jquery.validate.min.js") %>"></script>
    <script type="text/javascript" src="<%: Url.Content("~/ckeditor/ckeditor.js") %>"></script>
    <script type="text/javascript" src="<%: Url.Content("~/ckfinder/ckfinder.js") %>"></script>
    <script type="text/javascript" src="<%: Url.Content("~/Content/JqueryUI/js/jquery-ui-1.9.1.custom.min.js") %>"></script>
    <script type="text/javascript" src="<%: Url.Content("~/Libs/ligerUI/js/core/base.js") %>"></script>
    <script type="text/javascript" src="<%: Url.Content("~/Libs/ligerUI/js/ligerui.min.js") %>"></script>
    <script type="text/javascript" src="<%: Url.Content("~/Libs/ligerUI/js/plugins/ligerComboBox.js") %>"></script>
    <script type="text/javascript" src="<%: Url.Content("~/Libs/ligerUI/js/plugins/ligerTree.js") %>"></script>
    <% using (Html.BeginForm("Create", "Product", new { @area = "manage" }, FormMethod.Post, new { @id = "postForm" }))
       { %>
    <%:Html.Hidden("_returnUrl",ViewData["_returnUrl"]) %>
    <table>
        <tr class="lbg">
            <td class="editor-label">
                标题：
            </td>
            <td class="editor-field" colspan="3">
                <%:Html.TextBoxFor(m => m.MerchandiseTitle)%>
            </td>
        </tr>
        <tr class="lbgalt">
            <td class="editor-label">
                标题描述：
            </td>
            <td class="editor-field" colspan="3">
                <%:Html.TextBoxFor(m => m.MerchandiseTitleDescription)%>
            </td>
        </tr>
        <tr class="lbg">
            <td class="editor-label">
                商品名称：
            </td>
            <td class="editor-field" colspan="3">
                <%:Html.TextBoxFor(m => m.MerchandiseName)%>
            </td>
        </tr>
        <tr class="lbgalt">
            <td class="editor-label">
                商品分类：
            </td>
            <td class="editor-field">
                <input id="MerchandiseTypeIDCombobox" type="text" />
            </td>
            <td class="editor-label">
                上架时间：
            </td>
            <td class="editor-field">
                <%:Html.TextBox("MerchandiseBeginDate", DateTime.Now.ToShortDateString(), new { @class = "datecss" })%>
            </td>
        </tr>
        <tr class="lbg">
            <td class="editor-label">
                主图地址：
            </td>
            <td class="editor-field" colspan="3">
                <table>
                    <tr>
                        <td>
                            <%:Html.TextBoxFor(m => m.MerchandiseTitlePicUrl)%>
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
                商品概述：
            </td>
            <td class="editor-field" colspan="3">
                <%:Html.TextAreaFor(m => m.MerchandiseSynopsis, new { @class = "minheight300" })%>
            </td>
        </tr>
        <tr class="lbg">
            <td class="editor-label">
                商品描述：
            </td>
            <td class="editor-field" colspan="3">
                <%:Html.TextAreaFor(m => m.MerchandiseDescription, new { @class = "minheight100" })%>
            </td>
        </tr>
        <tr class="lbgalt">
            <td class="editor-label">
               <%=Resource.Article_AfterSaveService%>:
            </td>
            <td class="editor-field" colspan="3">
                <%:Html.TextAreaFor(m => m.MerchandiseAfterSaleService, new { @class = "minheight100" })%>
            </td>
        </tr>
        <tr class="lbg">
            <td class="editor-label">
                标签：
            </td>
            <td class="editor-field" colspan="3">
                <%:Html.TextBoxFor(m => m.MerchandiseLabel)%>
                <br />
                <strong class="red">多个标签用“,”分隔开</strong>
            </td>
        </tr>
        <tr class="lbgalt">
            <td class="editor-label">
                日期：
            </td>
            <td class="editor-field ">
                <%:Html.TextBox("MerchandiseDate", DateTime.Now.ToShortDateString(), new { @class = "datecss" })%>
            </td>
            <td class="editor-label">
                浏览数：
            </td>
            <td class="editor-field">
                <%:Html.TextBox("MerchandiseClickCount", 0, new { @class = "count" })%>
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
        CKEDITOR.replace('MerchandiseSynopsis');
        CKEDITOR.replace('MerchandiseDescription');
        CKEDITOR.replace('MerchandiseAfterSaleService');
    </script>
    <script type="text/javascript">
        $(function () {
            $("#MerchandiseDate").attr("readonly", "true").datepicker({ dateFormat: 'yy/mm/dd' });
            $("#MerchandiseBeginDate").attr("readonly", "true").datepicker({ dateFormat: 'yy/mm/dd' });
            $("#MerchandiseTypeIDCombobox").ligerComboBox({
                width: 200,
                height: 20,
                selectBoxWidth: 200,
                selectBoxHeight: 200,
                textField: 'textcontent', valueField: 'id', valueFieldID: 'MerchandiseType', treeLeafOnly: false,
                tree: { url: '<%: Url.Content("~/Common/GetJsonForMerchandiseTypeID") %>?time=' + Math.random(),
                    checkbox: false,
                    textFieldName: "textcontent",
                    idFieldName: "id",
                    parentIDFieldName: "fid"
                }
            });

            $(".btnsubmit").bind("click", function (event) {

                var _return = "";

                if (CKEDITOR.instances.MerchandiseSynopsis.getData() == "") {
                    _return += '<div>请添加商品概况内容</div>';
                }

                if (CKEDITOR.instances.MerchandiseDescription.getData() == "") {
                    _return += '<div>请添加商品描述内容</div>';
                }
//                if (CKEDITOR.instances.MerchandiseAfterSaleService.getData() == "") {
//                    _return += '<div>请添加售后信息内容</div>';
//                }
                if ($("#MerchandiseTypeIDCombobox").val() == "") {
                    _return += '<div>请选择商品分类</div>';
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
                    MerchandiseTitle: { required: true },
                    MerchandiseTitleDescription: { required: true },
                    MerchandiseName: { required: true },
                    MerchandiseTitlePicUrl: { required: true },
                    MerchandiseBeginDate: { required: true, dateISO: true },
                    MerchandiseDate: { required: true, dateISO: true },
                    MerchandiseClickCount: { required: true, digits: true }
                },
                messages: {
                    MerchandiseTitle: '请填写标题！',
                    MerchandiseTitleDescription: '请填写标题描述！',
                    MerchandiseName: '请填写商品名称！',
                    MerchandiseTitlePicUrl: '请填写主图地址！',
                    MerchandiseBeginDate: { required: "请填写上架日期！", dateISO: "日期格式错误！" },
                    MerchandiseDate: { required: "请填写日期！", dateISO: "日期格式错误！" },
                    MerchandiseClickCount: { required: "访问量不许为空！", digits: "访问量必须输入整数！" }
                },                        
                errorContainer: "div.error",
                errorLabelContainer: $("div.error"),
                wrapper: "div"
            });
            $("#btnUpload").bind("click", function (event) {
                BrowseServer("MerchandiseTitlePicUrl");
            });
            function BrowseServer(inputId) {
                var finder = new CKFinder();
                finder.basePath = '/ckfinder';  //导入CKFinder的路径                
                finder.selectActionFunction = SetFileField; //设置文件被选中时的函数               
                finder.selectActionData = inputId;  //接收地址的input ID               
                finder.popup();
            };
            //文件选中时执行               
            function SetFileField(fileUrl, data) {
                document.getElementById(data["selectActionData"]).value = ("~" + fileUrl);
            };

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
    <link type="text/css" href="<%:Url.Content("~/App_Themes/"+ConfigurationManager.AppSettings["ManageTheme"].ToString()+"/css/Product.css") %>"
        rel="stylesheet" />
    <link rel="stylesheet" href="<%: Url.Content("~/Content/JqueryUI/css/smoothness/jquery-ui-1.9.1.custom.min.css") %>"
        type="text/css" media="all" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="TabTitleContent" runat="server">
添加商品
</asp:Content>
