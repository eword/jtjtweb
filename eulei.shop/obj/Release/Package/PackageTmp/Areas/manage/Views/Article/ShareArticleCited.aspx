<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/manage/Views/Shared/Manage_Areas.Master" Inherits="System.Web.Mvc.ViewPage<eulei.shop.Models.TB_Article>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    共享文章引用/添加
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
           
                    <% using (Html.BeginForm("ShareArticleCited", "Article", new { @area = "manage" }, FormMethod.Post, new { @id = "postForm" }))
                       { %>

<%--                      <%:Html.Hidden("id",(Guid)ViewBag.Guid) %>--%>
                       <%:Html.Hidden("_returnUrl",ViewData["_returnUrl"]) %>
                    <table>
                        <tr class="lbg">
                            <td class="editor-label">
                                标题：
                            </td>
                            <td class="editor-field" colspan="3">
                                <%:Html.TextBoxFor(m => m.ArticleTitle)%>
                            </td>
                        </tr>
                        <tr class="lbgalt">
                            <td class="editor-label">
                                文章分类：
                            </td>
                            <td class="editor-field">
                                <input id="ArticleTypeIDCombobox" type="text" />
                            </td>
                            <td class="editor-label">
                               
                            </td>
                            <td class="editor-field">

                            </td>
                        </tr> 
                        <tr class="lbg">
                            <td class="editor-label">
                                内容：
                            </td>
                            <td class="editor-field" colspan="3">
                                <%:Html.TextAreaFor(m => m.ArticleContent, new { @class = "minheight300" })%>
                            </td>
                        </tr>
                        <tr class="lbgalt">
                            <td class="editor-label">
                                描述
                            </td>
                            <td class="editor-field" colspan="3">
                                <%:Html.TextAreaFor(m => m.ArticleDescription, new { @class = "minheight100" })%>
                            </td>
                        </tr>
                        <tr class="lbg">
                            <td class="editor-label">
                                标签
                            </td>
                            <td class="editor-field" colspan="3">
                                <%:Html.TextBoxFor(m => m.ArticleLabel)%>
                                <br />
                                <strong class="red">多个标签用“,”分隔开</strong>
                            </td>
                        </tr>
                        <tr class="lbgalt">
                            <td class="editor-label">
                                日期：
                            </td>
                            <td class="editor-field ">
                                <%:Html.TextBox("ArticleSendDate", DateTime.Now.ToShortDateString(), new { @class = "datecss" })%>
                            </td>
                            <td class="editor-label">
                                浏览数：
                            </td>
                            <td class="editor-field">
                                <%:Html.TextBox("ArticleClickCount", 0, new { @class = "count" })%>
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
        CKEDITOR.replace('ArticleContent');
        CKEDITOR.replace('ArticleDescription');
    </script>
    <script type="text/javascript">
        $(function () {
            $("#ArticleSendDate").attr("readonly", "true").datepicker({ dateFormat: 'yy/mm/dd' });

            $("#ArticleTypeIDCombobox").ligerComboBox({
                width: 200,
                height: 20,
                selectBoxWidth: 200,
                selectBoxHeight: 200,
                textField: 'textcontent', valueField: 'id', valueFieldID: 'ArticleTypeID', treeLeafOnly: false,
                tree: { url: '<%: Url.Content("~/Common/GetJsonForArticleTypeID") %>',
                            checkbox: false,
                    textFieldName: "textcontent",
                    idFieldName: "id",
                    parentIDFieldName: "fid"
                }
            });
            $(".btnsubmit").bind("click", function (event) {

                var _return = "";

                if (CKEDITOR.instances.ArticleContent.getData() == "") {
                    _return += '<div>请添加内容</div>';
                }

                if (CKEDITOR.instances.ArticleDescription.getData() == "") {
                    _return += '<div>请添加描述</div>';
                }

                if ($("#ArticleTypeIDCombobox").val() == "") {
                    _return += '<div>请选择文章分类</div>';
                }

                if ($("#ArticleIsPicTheme").attr("checked")) {
                    if ($("#ArticlePictureUrl").attr("value") == "") {
                        _return += '<div>请添加图片地址</div>';
                    }
                    if ($("#PictureTitle").attr("value") == "") {
                        _return += '<div>请添加图片标题</div>';
                    }
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
                    ArticleTitle: { required: true },
                    //                    ArticleContent: { required: true },
                    //                    ArticleDescription: { required: true },
                    ArticleSendDate: { required: true, dateISO: true },
                    ArticleCount: { required: true, digits: true }
                },
                messages: {
                    ArticleTitle: '请填写标题！',
                    //                    ArticleContent: '请填写内容！',
                    //                    ArticleDescription: '请填写描述！', 
                    ArticleSendDate: { required: "请填写日期！", dateISO: "日期格式错误！" },
                    ArticleClickCount: { required: "访问量不许为空！", digits: "访问量必须输入整数！" }
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
        <link type="text/css" href="<%:Url.Content("~/App_Themes/"+ConfigurationManager.AppSettings["ManageTheme"].ToString()+"/css/Article.css") %>"
        rel="stylesheet" />    <link rel="stylesheet" href="<%: Url.Content("~/Content/JqueryUI/css/smoothness/jquery-ui-1.9.1.custom.min.css") %>"
        type="text/css" media="all" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="TabTitleContent" runat="server">
文章引用
</asp:Content>

