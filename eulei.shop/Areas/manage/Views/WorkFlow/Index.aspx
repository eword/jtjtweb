<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/manage/Views/Shared/Manage_Areas.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<eulei.shop.Models.VW_SA_WorkFlow>>" %>

<%@ Import Namespace="Q42.Wheels.Mvc.Paging" %>
<%@ Import Namespace="eulei.shop.Code" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    流程设置
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" src="<%: Url.Content("~/Libs/ligerUI/js/core/base.js") %>"></script>
    <script type="text/javascript" src="<%: Url.Content("~/Libs/ligerUI/js/ligerui.min.js") %>"></script>
    <script type="text/javascript" src="<%: Url.Content("~/Libs/ligerUI/js/plugins/ligerComboBox.js") %>"></script>
    <script type="text/javascript" src="<%: Url.Content("~/Libs/ligerUI/js/plugins/ligerTree.js") %>"></script>
    <script type="text/javascript" src="<%: Url.Content("~/Content/js/confirm.js") %>"></script>
    <script type="text/javascript" src="<%: Url.Content("~/Content/themes/ui/js/jquery-ui-1.8.22.custom.min.js") %>"></script>
    <script type="text/javascript" src="<%: Url.Content("~/Scripts/jquery.form.js") %>"></script>
    <script type="text/javascript" src="<%: Url.Content("~/Scripts/jquery.unobtrusive-ajax.min.js") %>"></script>
    <script type="text/javascript">
        $(function () {
            $("#ArticleTypeIDCombobox").ligerComboBox({
                width: 150,
                height: 20,
                selectBoxWidth: 250,
                selectBoxHeight: 300, lable: '文章分类',
                textField: 'textcontent', valueField: 'id', valueFieldID: 'ArticleTypeID', treeLeafOnly: false, hideOnLoseFocus: true,
                tree: {
                    url: '<%: Url.Content("~/Common/GetJsonForArticleTypeID") %>',
                    checkbox: false,
                    textFieldName: "textcontent",
                    idFieldName: "id",
                    parentIDFieldName: "fid"
                }
            });




            $("#link_add").bind("click", function (event) {
                $("#ArticleTypeIDCombobox1").ligerComboBox({
                    width: 150,
                    height: 20,
                    selectBoxWidth: 250,
                    selectBoxHeight: 300,
                    textField: 'textcontent', valueField: 'id', valueFieldID: 'FlowTemplateArticleTypeID', treeLeafOnly: false, hideOnLoseFocus: true,
                    tree: {
                        url: '<%: Url.Content("~/WorkFlow/GetJsonForArticleTypeIDWithNoCreate") %>',
                        checkbox: false,
                        textFieldName: "textcontent",
                        idFieldName: "id",
                        parentIDFieldName: "fid"
                    }
                });
                $("#dialog-form-Create").dialog("open");
            });



            $('.deletelink').confirm({
                wrapper: '<div style="float:right;"><br/><div>',
                msg: '删除？',
                ok: '是',
                cancel: '否',
                timeout: 10000
            });




            $("#dialog-form-Create").dialog({
                autoOpen: false,
                height: 200,
                width: 350,
                modal: true,
                buttons: {
                    "提交": function () {
                        var _return = "";


                        if ($("#ArticleTypeIDCombobox1").val() == "") {
                            _return += '<div>请选择文章分类</div>';
                        }

                        if (_return == "") {
                            $("#CreatepostForm").submit();
                            $(this).dialog({ height: 200 });
                            $(this).dialog("close");
                        }
                        else {
                            $(".error").html("");
                            $(this).dialog({ height: 250 });
                            $(".error").append(_return);
                        }
                    },
                    "取消": function () {
                        $(this).dialog({ height: 200 });
                        $(this).dialog("close");
                    }
                }
            });

        });
    </script>
    <div>
        <div class="searchBox">
            <%using (Html.BeginForm("Search", "WorkFlow", new { @area = "manage" }, FormMethod.Post, new { @id = "postForm" }))
              { %>
            <div>
                <input id="ArticleTypeIDCombobox" type="text" />
            </div>
            <div>
                <input type="submit" id="searchSubmit" value="  " />
            </div>
            <%} %>
            <a href="javascript:void();" class="create" id="link_add">添加</a>
        </div>
        <table width="100%" bgcolor="#b5d6e6" border="0" cellpadding="0" cellspacing="0">
            <tr class="tbg">
                <td class="tc w40">
                    <%= Html.RenderSortLink("ID", "ArticleTypeID")%>
                </td>
                <td class="tc ">
                    <%= Html.RenderSortLink("文章类别", "ArticleTypeName")%>
                </td>
                <td class="tc ">
                    <%= Html.RenderSortLink("上级类别", "ParentArticleTypeName")%>
                </td>

                <td class="tc  w200">基本操作
                </td>
            </tr>
            <% 
                int _i = 1;
                foreach (var item in this.GetPage(Model))
                {                  
            %>
            <tr class="<%=(_i % 2 == 0?"lbgalt":"lbg") %>">
                <td class="tc">
                    <%: item.ArticleTypeID %>
                </td>
                <td class="tc">
                    <%: String.Format("{0:D}", item.ArticleTypeName) %>
                </td>
                <td class="tc">
                    <%= string.IsNullOrEmpty(item.ParentArticleTypeName)?"站点根目录":item.ParentArticleTypeName %>
                </td>


                <td class="tl">
                    <%: Html.ActionLink("步骤设置", "SetStep", new { @id = item.ArticleTypeID, @_returnUrl = Request.Url.PathAndQuery }, new { @class = "editlink line-height24" })%>
                    <%: Html.ActionLink("删除", "Delete", new { @id = item.ArticleTypeID, @_returnUrl = Request.Url.PathAndQuery }, new { @class = "deletelink line-height24" })%>
                </td>
            </tr>
            <%_i++;
                } %>
        </table>
        <%= Html.RenderPagination() %>
    </div>
    <div id="dialog-form-Create" title="创建流程">
        <div id="CreateAjaxBox">
            <%using (Html.BeginForm("Create", "WorkFlow", new { @area = "manage", @_returnUrl = Request.Url.PathAndQuery }, FormMethod.Post, new { @id = "CreatepostForm" }))
              { %>
            <div>
                文章分类：<input id="ArticleTypeIDCombobox1" type="text" />
            </div>
            <br />
            <div>
                <%:Html.CheckBox("FlowTemplateSendMoveMsg") %>是否默认发送短信提醒？
            </div>
            <div class="error">
            </div>
            <%} %>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="headOtherInfo" runat="server">
    <link type="text/css" href="<%:Url.Content("~/Libs/ligerUI/skins/Aqua/css/ligerui-all.css") %>" rel="stylesheet" />
    <link type="text/css" href="<%:Url.Content("~/App_Themes/"+ConfigurationManager.AppSettings["ManageTheme"].ToString()+"/css/general.css") %>"
        rel="stylesheet" />
    <link type="text/css" href="<%:Url.Content("~/App_Themes/"+ConfigurationManager.AppSettings["ManageTheme"].ToString()+"/css/master.css") %>"
        rel="stylesheet" />
    <link type="text/css" href="<%:Url.Content("~/App_Themes/"+ConfigurationManager.AppSettings["ManageTheme"].ToString()+"/css/WorkFlow.css") %>"
        rel="stylesheet" />
    <link rel="stylesheet" href="<%: Url.Content("~/Content/themes/ui/css/smoothness/jquery-ui-1.8.22.custom.css") %>"
        type="text/css" media="all" />
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="TabTitleContent" runat="server">
    流程设置
</asp:Content>
