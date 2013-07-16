<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/manage/Views/Shared/Manage_Areas.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    角色信息
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" src="<%: Url.Content("~/Libs/ligerUI/js/core/base.js") %>"></script>
    <script type="text/javascript" src="<%: Url.Content("~/Libs/ligerUI/js/ligerui.min.js") %>"></script>
    <script type="text/javascript" src="<%: Url.Content("~/Libs/ligerUI/js/plugins/ligerTree.js") %>"></script>
    <script type="text/javascript" src="<%: Url.Content("~/Content/js/AjaxLoad.js") %>"></script>
    <script type="text/javascript" src="<%: Url.Content("~/Content/themes/ui/js/jquery-ui-1.8.22.custom.min.js") %>"></script>
    <script type="text/javascript" src="<%: Url.Content("~/Scripts/jquery.validate.min.js") %>"></script>
    <script type="text/javascript" src="<%: Url.Content("~/Scripts/jquery.unobtrusive-ajax.min.js") %>"></script>
    <script type="text/javascript" src="<%: Url.Content("~/Scripts/jquery.form.js") %>"></script>  
    <%
        var _roleList = ViewBag.RoleList as List<string>;
    %>
    <table class="main-table">
        <tr>
             <td class="td-left"></td>
            <td class="td-middle">功能权限</td>
            <td class="td-right">文章分类权限</td>
        </tr>
        <tr>
            <td class="td-left">
                <form id="postForm" method="post" action="manage/account/roleinfo">
                    <%foreach (var item in _roleList)
                      {%>
                    <div>
                        <%:Html.CheckBox("Roles",new{ @value=item}) %><%=item %>
                    </div>
                    <%} %>
                </form>
                <div>
                </div>
            </td>
            <td class="td-middle">
                <div>
                    <ul id="authorityList" class="fl"></ul>
                </div>

            </td>
            <td class="td-right">

                    <ul id="ArticleTypeList" class="fl"></ul>
            </td>
        </tr>
        <tr>
            <td>
                <div class="error">
                </div>
                <div>
                    <script type="text/javascript">
                        $(function () {
                            
                            $("#tb_editAuthority").bind("click", function (event) {
                               
                                if ($("input[name='Roles']:checked").length == 1) {
                                    var roleName = '';
                                    roleName = $("input[name='Roles']:checked").attr("value");
                                    roleName = encodeURI(roleName);
                                    roleName = encodeURI(roleName);
                                    $("#authorityList").ligerTree({
                                        url: '<%: Url.Content("~/manage/account/GetJsonForAuthorityList?id=") %>' + roleName + '&time=' + Math.random(),
                                        checkbox: true,
                                        ischecked: true,
                                        single: false,
                                        textFieldName: "textcontent",
                                        idFieldName: "id",
                                        parentIDFieldName: "fid"
                                    });
                                    $("#ArticleTypeList").ligerTree({
                                        url: '<%: Url.Content("~/manage/account/GetJsonForArticleTypeList?id=") %>' + roleName + '&time=' + Math.random(),
                                        checkbox: true,
                                        ischecked: true,
                                        single: false,
                                        textFieldName: "textcontent",
                                        idFieldName: "id",
                                        parentIDFieldName: "fid"
                                    });

                                    $("#authorityList").show();
                                    $("#ArticleTypeList").show();
                                    $("#tb_save").show();
                                    $("#tb_cancel").show();
                                    $("#tb_editAuthority").hide();
                                    $("#tb_add").hide();
                                    $("#tb_del").hide();
                                    $("input[name='Roles']").attr("disabled", true);

                                } else {
                                    alert("编辑权限时必须且只能选中1个角色！");
                                }
                            });
                            $("#tb_add").bind("click", function (event) {
                                $("#dialog-form-addRole").dialog('open');
                            });
                            $("#tb_del").bind("click", function (event) {
                                $("#postForm").first().attr("action", "DeleteRole").submit();
                            });
                        });
                    </script>
                    <input type="submit" id="tb_editAuthority" class="btnsubmit fl rmargin15" value="编辑权限" />
                    <input type="submit" id="tb_add" class="btnsubmit fl rmargin15" value="添加" />
                    <input type="submit" id="tb_del" class="btnsubmit fl rmargin15" value="删除" />
                </div>
            </td>
            <td colspan="2">
                <script type="text/javascript">
                    $(function () {
                        $("#authorityList").hide();
                        $("#ArticleTypeList").hide();
                        $("#tb_save").hide();
                        $("#tb_cancel").hide();
                        $("#tb_save").bind("click", function (event) {
                            var notes = $("#authorityList").ligerGetTreeManager().getChecked();
                            var authorityList = "";
                            for (var i = 0; i < notes.length; i++) {
                                if (i == 0) {
                                    authorityList += notes[i].data.queueOrder;
                                }
                                else {
                                    authorityList += "," + notes[i].data.queueOrder;
                                }
                            }
                            var notes = $("#ArticleTypeList").ligerGetTreeManager().getChecked();
                            var ArticleTypeList = "";
                            for (var i = 0; i < notes.length; i++) {
                                if (i == 0) {
                                    ArticleTypeList += notes[i].data.id;
                                }
                                else {
                                    ArticleTypeList += "," + notes[i].data.id;
                                }
                            }

                            var roleName = '';
                            roleName = $("input[name='Roles']:checked").attr("value");
                            roleName = encodeURI(roleName);
                            roleName = encodeURI(roleName);
                            $.ajax({
                                type: "get",
                                url: "UpdateRoleAuthority" ,
                                contentType: "application/x-www-form-urlencoded; charset=utf-8",
                                data: "id=" + roleName + "&authorityList=" + authorityList + "&" + "articleTypeList=" + ArticleTypeList,//提交表单，相当于CheckCorpID.ashx?ID=XXX
                                success: function (msg) {alert(msg); },   //操作成功后的操作！msg是后台传过来的值
                                error: function (msg) {alert(msg); }   //操作成功后的操作！msg是后台传过来的值
                            });


                            $("#tb_save").hide();
                            $("#tb_cancel").hide();
                            $("#tb_editAuthority").show();
                            $("#tb_add").show();
                            $("#tb_del").show();
                            $("#authorityList").ligerGetTreeManager().clear();
                            $("#authorityList").hide();
                            $("#ArticleTypeList").ligerGetTreeManager().clear();
                            $("#ArticleTypeList").hide();
                            $("input[name='Roles']").attr("disabled", false);
                            $("input[name='Roles']").attr("checked",false);



                            //alert('选择的节点数：' + authorityList + "@@@" + ArticleTypeList);

                        });
                        $("#tb_cancel").bind("click", function (event) {
                            
                            $("#tb_save").hide();
                            $("#tb_cancel").hide();
                            $("#tb_editAuthority").show();
                            $("#tb_add").show();
                            $("#tb_del").show();
                            $("#authorityList").ligerGetTreeManager().clear();
                            $("#authorityList").hide();
                            $("#ArticleTypeList").ligerGetTreeManager().clear();
                            $("#ArticleTypeList").hide();
                            $("input[name='Roles']").attr("disabled", false);
                        });
                    });
                </script>
                <input type="submit" id="tb_save" class="btnsubmit fl rmargin15 lmargin200" value="保存" />
                <input type="submit" id="tb_cancel" class="btnsubmit fl rmargin15" value="取消" />
            </td>
        </tr>
    </table>
    <input type="hidden" id="Tip" />
    <script type="text/javascript">
        $(function () {
            $("#dialog-form-addRole").dialog({
                autoOpen: false,
                height: 150,
                width: 350,
                modal: true,
                buttons: {
                    "提交": function () {
                        var _return = "";

                        if ($("#roleName").attr("value") == '') {
                            _return += "<div>角色名不能为空！</div>";
                        }
                        if (_return == "") {
                            $("#postRole").submit();
                            $("#postRole").each(function () {
                                this.reset();
                            });
                            $(".role_error").html("");
                            $(this).dialog({ height: 150 });
                            $(this).dialog("close");
                        }
                        else {
                            $(".role_error").html("");
                            $(this).dialog({ height: 150 });
                            $(".role_error").append(_return);
                        }
                    },
                    "取消": function () {
                        $("#postRole").each(function () {
                            this.reset();
                        });
                        $(this).dialog({ height: 150 });
                        $(this).dialog("close");
                    }
                }
            });
        });
    </script>
    <div id="dialog-form-addRole" title="添加">
        <% using (Html.BeginForm("CreateRole", "Account", new { @area = "manage" }, FormMethod.Post, new { @id = "postRole" }))
           { %>
        <div>角色名：<input type="text" name="roleName" id="roleName" /></div>
        <div class="role_error">
        </div>
        <% } %>
    </div>
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
    角色信息
</asp:Content>
