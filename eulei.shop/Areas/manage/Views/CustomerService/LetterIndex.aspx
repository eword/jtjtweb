<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/manage/Views/Shared/Manage_Areas.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<eulei.shop.Models.VW_Letter>>" %>

<%@ Import Namespace="Q42.Wheels.Mvc.Paging" %>
<%@ Import Namespace="eulei.shop.Code" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    留言管理
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" src="<%: Url.Content("~/Content/js/confirm.js") %>"></script>
    <script type="text/javascript" src="<%: Url.Content("~/Scripts/jquery.form.js") %>"></script>
    <script type="text/javascript" src="<%: Url.Content("~/Scripts/jquery.unobtrusive-ajax.min.js") %>"></script>
    <script type="text/javascript" src="<%: Url.Content("~/Scripts/jquery.validate.min.js") %>"></script>
    <script type="text/javascript" src="<%: Url.Content("~/Content/themes/ui/js/jquery-ui-1.8.22.custom.min.js") %>"></script>
    <script type="text/javascript">
        $(function () {
            $('.DeleteLetter').confirm({
                wrapper: '<div style="float:right;"><div>',
                msg: '确定删除？',
                ok: '是',
                cancel: '否',
                timeout: 10000
            });
            $('.PublicLetter').confirm({
                wrapper: '<div style="float:right;"><div>',
                msg: '改变现实状态？',
                ok: '是',
                cancel: '否',
                timeout: 10000
            });
        });
    </script>
    <div class="searchBox">
        <%using (Html.BeginForm("SearchLetterIndex", "CustomerService", new { @area = "manage" }, FormMethod.Post, null))
          { %>
        <div>
            标题：<%:Html.TextBox("LetterTitle", "", new { @class = "SearchText" })%>
        </div>

        <div>
            留言人：<%:Html.TextBox("LetterSender", "", new { @class = "SearchText" })%>
        </div>

        <div>
            联系电话：<%:Html.TextBox("LetterTEL", "", new { @class = "SearchText" })%>
        </div>

        <div>
            类型：<%: Html.DropDownList("LetterTypeID",ViewBag.LetterTypeID as SelectList,"请选择")%>
        </div>

        <div>
            收信人：<%: Html.DropDownList("LetterAddressee", ViewBag.LetterAddressee as SelectList,"请选择")%>
        </div>
        <div>
            状态：<select id="LetterIsWriteBack" name="LetterIsWriteBack"><option value="">请选择</option>
                <option value="0">未回复</option>
                <option value="1">已回复</option>
            </select>
        </div>
        <div>
            <input type="submit" id="searchSubmit" value="  " />
        </div>
        <div class="tred">默认为：未回复状态</div>
        <%} %>
    </div>
    <div>
        <table class="leaveWordTable" width="100%" bgcolor="#b5d6e6" border="0" cellpadding="0" cellspacing="0">
            <%
                int _i = 1;
                foreach (var item in this.GetPage(Model))
                { %>
            <tr class="<%=_i%2==0?"lbgalt":"lbg" %> ">

                <td class="tl w200 tb">
                    <div>
                        <strong>类&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;型： </strong><%: Html.DisplayFor(modelItem => item.DictionaryDescription) %>
                    </div>
                    <div>
                        <strong>留言时间： </strong><%: Html.DisplayFor(modelItem => item.LetterTimeSend) %>
                    </div>
                    <div>
                        <strong>留&nbsp;&nbsp;言&nbsp;人： </strong><%: Html.DisplayFor(modelItem => item.LetterSender) %>
                    </div>
                    <div>
                        <strong>联系电话： </strong><%: Html.DisplayFor(modelItem => item.LetterTEL) %>
                    </div>
                    <div>
                        <strong>Email： </strong>
                        <br />
                        <%: Html.DisplayFor(modelItem => item.LetterEmail) %>
                    </div>
                    <div>
                        <strong>联系地址： </strong>
                        <br />
                        <%: Html.DisplayFor(modelItem => item.LetterAddress) %>
                    </div>
                    <div>
                        <strong>邮政编码： </strong><%: Html.DisplayFor(modelItem => item.LetterPostalCode) %>
                    </div>

                    <div>
                        <strong>收件单位： </strong>
                        <br />
                        <%: Html.DisplayFor(modelItem => item.LetterAddressee) %>
                    </div>
                    <div>
                        <strong class="tred">状态：<%=item.LetterIsWriteBack?
                                             (item.LetterIsPublic?"回复并已公示":"回复但隐藏"
                                             ):"未回复……" %> </strong>
                    </div>
                </td>
                <td class="tl">
                    <div class="title">
                        <strong>标题：<%: Html.DisplayFor(modelItem => item.LetterTitle) %> </strong>
                        <div class="fr">
                            <%if (!(item.LetterIsWriteBack && (!string.IsNullOrEmpty(item.LetterWriteBackContent))))
                              { %>
                            <%: Ajax.ActionLink("回复", "EditLetter","CustomerService", new {@aera="manage", @id = item.LetterID, @_returnUrl = Request.Url.PathAndQuery },new AjaxOptions{@UpdateTargetId="EditAjaxBox"}, new { @class = "editlink" })%>
                            <%}
                              else
                              {
                            %>
                            <%: Html.ActionLink(item.LetterIsPublic?"隐藏":"公示", "PublicLetter","CustomerService", new {@aera="manage", @id = item.LetterID, @_returnUrl = Request.Url.PathAndQuery }, new { @class = "editlink PublicLetter"})%>
                            <%: Ajax.ActionLink("编辑", "EditLetter","CustomerService", new {@aera="manage", @id = item.LetterID, @_returnUrl = Request.Url.PathAndQuery },new AjaxOptions{@UpdateTargetId="EditAjaxBox"}, new { @class = "editlink" })%>
                            <%} %>
                            <%: Html.ActionLink("删除", "DeleteLetter","CustomerService", new {@aera="manage", @id = item.LetterID, @_returnUrl = Request.Url.PathAndQuery }, new { @class = "deletelink DeleteLetter" })%>
                        </div>
                    </div>
                    <div><strong>内容： </strong></div>
                    <div><%: Html.DisplayFor(modelItem => item.LetterContent) %></div>

                </td>
            </tr>
            <%if (item.LetterIsWriteBack && (!string.IsNullOrEmpty(item.LetterWriteBackContent)))
              { %>
            <tr class="<%=_i%2==0?"lbgalt":"lbg" %> ">
                <td class="tl">
                    <div>
                        <strong>回&nbsp;&nbsp;复&nbsp;人：</strong><%= item.UserInfoFriendName+"("+item.UserName+")" %>
                    </div>
                    <div>
                        <strong>回复时间：</strong><%: Html.DisplayFor(modelItem => item.LetterTimeWriteBack) %>
                    </div>
                </td>
                <td class="tl">
                    <div class="title"><strong>回复内容：</strong></div>
                    <div><%: Html.DisplayFor(modelItem => item.LetterWriteBackContent) %></div>
                </td>
            </tr>
            <%} %>
            <%_i++;
                } %>
        </table>
    </div>
    <div class="page">
        <%= Html.RenderPagination() %>
    </div>
    <script type="text/javascript">
        $(function () {
            $("#dialog-form-Edit").dialog({
                autoOpen: false,
                height: 400,
                width: 550,
                modal: true,
                buttons: {
                    "提交": function () {
                        $("#EditpostForm").submit();
                    },
                    "取消": function () {
                        $(this).dialog("close");
                        $("#EditpostForm").resetForm();
                    }
                },
                close: function () {
                    $(".editError").html("");
                    $(this).dialog({ height: 400 });
                }
            });
        });
    </script>
    <div id="dialog-form-Edit" title="回复" style="display: none;">
        <div id="EditAjaxBox">
        </div>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="headOtherInfo" runat="server">
    <link type="text/css" href="<%:Url.Content("~/App_Themes/"+ConfigurationManager.AppSettings["ManageTheme"].ToString()+"/css/general.css") %>"
        rel="stylesheet" />
    <link type="text/css" href="<%:Url.Content("~/App_Themes/"+ConfigurationManager.AppSettings["ManageTheme"].ToString()+"/css/master.css") %>"
        rel="stylesheet" />
    <link type="text/css" href="<%:Url.Content("~/App_Themes/"+ConfigurationManager.AppSettings["ManageTheme"].ToString()+"/css/letter.css") %>"
        rel="stylesheet" />
    <link rel="stylesheet" href="<%: Url.Content("~/Content/themes/ui/css/smoothness/jquery-ui-1.8.22.custom.css") %>"
        type="text/css" media="all" />
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="TabTitleContent" runat="server">
    留言管理
</asp:Content>
