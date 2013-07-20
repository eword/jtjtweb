<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/manage/Views/Shared/Manage_Areas.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<eulei.shop.Models.VW_SA_FlowStepInfo>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    步骤设置
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" src="<%: Url.Content("~/Scripts/jquery.validate.min.js") %>"></script>
    <script type="text/javascript" src="<%: Url.Content("~/Libs/ligerUI/js/core/base.js") %>"></script>
    <script type="text/javascript" src="<%: Url.Content("~/Libs/ligerUI/js/ligerui.min.js") %>"></script>
    <script type="text/javascript" src="<%: Url.Content("~/Libs/ligerUI/js/plugins/ligerComboBox.js") %>"></script>
    <script type="text/javascript" src="<%: Url.Content("~/Libs/ligerUI/js/plugins/ligerTree.js") %>"></script>
    <script type="text/javascript" src="<%: Url.Content("~/Content/js/confirm.js") %>"></script>
    <script type="text/javascript" src="<%: Url.Content("~/Content/themes/ui/js/jquery-ui-1.8.22.custom.min.js") %>"></script>
    <script type="text/javascript" src="<%: Url.Content("~/Scripts/jquery.form.js") %>"></script>
    <script type="text/javascript" src="<%: Url.Content("~/Scripts/jquery.unobtrusive-ajax.min.js") %>"></script>
    <h2><%:ViewBag.Title %></h2>
    <%: Ajax.ActionLink("添加", "CreateStep", new { @id = ViewBag.id, @_returnUrl = Request.Url.PathAndQuery },new AjaxOptions{ UpdateTargetId="CreateAjaxBox" }, new { @class = "create" })%>
     <table width="100%" bgcolor="#b5d6e6" border="0" cellpadding="0" cellspacing="0">
        <tr class="tbg">
            <td class="tc w40">ID
            </td>
            <td class="tc w80 ">步骤序号
            </td>
            <td class="tc ">步骤名
            </td>
              <td class="tc ">上一步
            </td>
              <td class="tc ">下一步
            </td>
            <td class="tc  w80">参与人数
            </td>
            <td class="tc  w80">默认短信
            </td>
            <td class="tc  w80">启用汇审
            </td>
            <td class="tc  w80">允许编辑
            </td>
            <td class="tc w80">状态
            </td>
            <td class="tc  w240">基本操作
            </td>
        </tr>
        <% 
            int _i = 1;
            foreach (var item in Model)
            {                  
        %>
        <tr class="<%=(_i % 2 == 0?"lbgalt":"lbg") %>">
            <td class="tc">
                <%: item.FlowTemplateID %>
            </td>
            <td class="tc">
                <%: item.FlowTemplateStatusID %>
            </td>
            <td class="tc">
                <%: item.FlowTemplateStatusDesp %>
            </td>
                       <td class="tc">
                <%: item.FlowTemplateNextStatusID%>
            </td>
            <td class="tc">
                <%: item.FlowTemplateNextStatusDesp %>
            </td>
            <td class="tc">
                <%if (item.FlowTemplateStatusID.Equals(1))
                  { %>
                ——
                <%}
                  else
                  { %>
                <%: item.FlowTemplateUserCount.HasValue?item.FlowTemplateUserCount.Value:0 %>
                <%} %>
            </td>
            <td class="tc">
                <%: item.FlowTemplateSendMoveMsg?"是":"否" %>
            </td>
            <td class="tc">
                <%: item.FlowTemplateIsSynergy?"是":"否" %>
            </td>
                                    <td class="tc">
                <%: item.FlowTemplateAlowEdit?"是":"否" %>
            </td>
            <td class="tc">
                <%= item.FlowTemplateState?"启用":"失效" %>
            </td>

            <td class="tl">
                <%if (!item.FlowTemplateStatusID.Equals(1))
                  { %>
                <a href="javascript:getUserList(<%:item.FlowTemplateID %>)" class="editlink line-height24">操作员设置</a>
                <%} %>
                <%if ((!item.FlowTemplateStatusID.Equals(1)) && (!item.FlowTemplateStatusID.Equals(99)))
                  { %>
                <%: Ajax.ActionLink("编辑", "EditStep", new { @id = item.FlowTemplateID, @_returnUrl = Request.Url.PathAndQuery },new AjaxOptions{ UpdateTargetId="EditAjaxBox" }, new { @class = "editlink line-height24" })%>
                <%: Html.ActionLink("删除", "DeleteStep", new { @id = item.FlowTemplateID, @_returnUrl = Request.Url.PathAndQuery }, new { @class = "deletelink line-height24" })%>
                <%} %>
            </td>
        </tr>
        <%_i++;
            } %>
    </table>
    <div>
        <a href="<%:ViewData["_returnUrl"]%>">返回列表</a>
    </div>
    <script type="text/javascript">

        $(function () {
            $('.deletelink').confirm({
                wrapper: '<div style="float:right;"><br/><div>',
                msg: '删除？',
                ok: '是',
                cancel: '否',
                timeout: 10000
            });
            $("#dialog-form-SetUsers").dialog({
                autoOpen: false,
                height: 600,
                width: 500,
                modal: true,
                close: function () {
                    $("#userList").ligerTree().clear();
                },
                buttons: {
                    "提交": function () {

                        var notes = $("#userList").ligerGetTreeManager().getChecked();
                        var userList = "";
                        for (var i = 0; i < notes.length; i++) {
                            if (i == 0) {
                                userList += notes[i].data.id;
                            }
                            else {
                                userList += "," + notes[i].data.id;
                            }
                        }

                        userList = encodeURI(userList);
                        userList = encodeURI(userList);
                        $.ajax({
                            type: "get",
                            url: "/manage/WorkFlow/SetUsers",
                            contentType: "application/x-www-form-urlencoded; charset=utf-8",
                            data: "id=" + $("#FlowTemplateID").val() + "&userList=" + userList,//提交表单，相当于CheckCorpID.ashx?ID=XXX
                            success: function (msg) {
                                $("#dialog-form-SetUsers").dialog("close");
                                alert("OK:" + msg);
                                window.location.reload();
                            },   //操作成功后的操作！msg是后台传过来的值
                            error: function (msg) { alert("Error:" + msg); }   //操作成功后的操作！msg是后台传过来的值
                        });

                    },
                    "取消": function () {
                        $(this).dialog("close");
                    }
                }
            });
        });

        function getUserList(id) {
            $("#FlowTemplateID").val(id);
            $("#userList").ligerTree({
                nodeWidth: 180,
                url: '<%: Url.Content("~/manage/WorkFlow/GetJsonForUserList?id=") %>' + id + '&time=' + Math.random(),
                checkbox: true,
                ischecked: true,
                single: false,
                textFieldName: "textcontent",
                idFieldName: "id",
                parentIDFieldName: "fid"
            });
            $("#dialog-form-SetUsers").dialog("open");
        };
    </script>
    <div id="dialog-form-SetUsers" title="设置人员">
        <div id="SetUsersAjaxBox" class="widtPercent100">
            <%:Html.Hidden("FlowTemplateID") %>
            <ul id="userList" class="fl"></ul>
        </div>
    </div>
    <script type="text/javascript">

        $(function () {
            $("#dialog-form-Create").dialog({
                autoOpen: false,
                height: 300,
                width: 450,
                modal: true,
                close: function () {
                    $("#CreateAjaxBox").html("");
                },
                buttons: {
                    "提交": function () {
                
                     $("#CreateStepPost").submit();                  
                       
                    },
                    "取消": function () {
                        $(this).dialog("close");
                    }
                }
            });
        });
    </script>
    <div id="dialog-form-Create" title="添加步骤">
        <div id="CreateAjaxBox" class="widtPercent100">
        </div>
    </div>
    <script type="text/javascript">

        $(function () {
            $("#dialog-form-Edit").dialog({
                autoOpen: false,
                height: 300,
                width: 450,
                modal: true,
                close: function () {
                    $("#EditAjaxBox").html("");
                },
                buttons: {
                    "提交": function () {
               
                   $("#EditStepPost").submit();
                     
                    },
                    "取消": function () {
                        $(this).dialog("close");
                    }
                }
            });
        });
    </script>
    <div id="dialog-form-Edit" title="编辑步骤">
        <div id="EditAjaxBox" class="widtPercent100">
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
    步骤设置
</asp:Content>
