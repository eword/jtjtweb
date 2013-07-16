<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/manage/Views/Shared/Manage_Areas.Master"
    Inherits="System.Web.Mvc.ViewPage<IEnumerable<eulei.shop.Models.TB_OnlyPage>>" %>

<%@ Import Namespace="Q42.Wheels.Mvc.Paging" %>
<%@ Import Namespace="eulei.shop.Code" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    文章列表-单页文章管理
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" src="<%: Url.Content("~/Libs/ligerUI/js/core/base.js") %>"></script>
    <script type="text/javascript" src="<%: Url.Content("~/Libs/ligerUI/js/ligerui.min.js") %>"></script>
    <script type="text/javascript" src="<%: Url.Content("~/Libs/ligerUI/js/plugins/ligerComboBox.js") %>"></script>
    <script type="text/javascript" src="<%: Url.Content("~/Libs/ligerUI/js/plugins/ligerTree.js") %>"></script>
    <script type="text/javascript" src="<%: Url.Content("~/Content/js/confirm.js") %>"></script>
    <script type="text/javascript">
        $(function () {

            $('.deletelink').confirm({
                msg: '删除？',
                ok: '是',
                cancel: '否',
                timeout: 10000
            });
            $('.ReAuditinglink').confirm({
                msg: '反审核？',
                ok: '是',
                cancel: '否',
                timeout: 10000
            });
        });
    </script>
    <div>
        <div class="searchBox">
            <%using (Html.BeginForm("Search", "OnlyPage", new { @area = "manage" }, FormMethod.Post, null))
              { %>
            <div>
                标题：<%:Html.TextBox("OnlyPageTitle")%>
            </div>
            <div>
                <input type="submit" id="searchSubmit" value="  " />
            </div>
            <%} %>
            <%if (ConfigurationManager.AppSettings["SetModel"].ToString().Equals("true") ? true : false)
              { %>
            <%:Html.ActionLink("添加", "Create", "OnlyPage", new { @_returnUrl = Request.Url.PathAndQuery, @area = "manage" }, new { @class = "create" })%>
            <%} %>
        </div>
        <table width="100%" bgcolor="#b5d6e6" border="0" cellpadding="0" cellspacing="0">
            <tr class="tbg">
                <td class="tc w40">
                    <%= Html.RenderSortLink("ID", "OnlyPageID")%>
                </td>
                <td class="tc ">
                    <%= Html.RenderSortLink("标题", "OnlyPageTitle")%>
                </td>
                <td class="tc  w40">
                    <%= Html.RenderSortLink("状态", "OnlyPageState")%>
                </td>
                <td class="tc  w40">
                    <%= Html.RenderSortLink("访问量", "OnlyPageClickCount")%>
                </td>
                <td class="tc  w180">基本操作
                </td>
            </tr>
            <% 
                int _i = 1;
                foreach (var item in this.GetPage(Model))
                {
                    if (_i % 2 == 0)
                    {
            %>
            <tr class="lbgalt">
                <% }
                    else
                    { %>
                <tr class="lbg">
                    <%} %>
                    <td class="tc">
                        <%:item.OnlyPageID %>
                    </td>
                    <td>
                        <%:Html.ActionLink(item.OnlyPageTitle, "OnlyPagePreview", "Home", new { @id = item.OnlyPageID,@area = "Article" }, new { @target = "_blank " })%>
                    </td>
                    <td class="tc">
                        <% switch (item.OnlyPageState)
                           { %>
                        <%case (int)ArticleState.Editing:%>
                        待审核
                        <%break; %>
                        <%case (int)ArticleState.Auditing:%>
                        通过
                        <%break; %>
                        <%default:%>
                        状态未设置
                        <%break; %>
                        <% }%>
                    </td>
                    <td class="tr">
                        <%: item.OnlyPageClickCount %>
                    </td>
                    <td class="tc">
                        <div>
                            <% switch (item.OnlyPageState)
                               { %>
                            <%case (int)ArticleState.Editing:%>
                            <%: Html.ActionLink("编辑", "Edit", new { @id = item.OnlyPageID, @_returnUrl = Request.Url.PathAndQuery }, new { @class = "editlink" })%>
                            <%: Html.ActionLink("审核", "Auditing", new { @id = item.OnlyPageID, @_returnUrl = Request.Url.PathAndQuery }, new { @class = "detailslink" })%>
                            <%if (ConfigurationManager.AppSettings["SetModel"].ToString().Equals("true") ? true : false)
                              { %>
                            <%: Html.ActionLink("删除", "Delete", new { @id = item.OnlyPageID, @_returnUrl = Request.Url.PathAndQuery }, new { @class = "deletelink" })%>
                            <%} %>
                            <%break; %>
                            <%case (int)ArticleState.Auditing:%>
                            <%: Html.ActionLink("反审核", "ReAuditing", new { @id = item.OnlyPageID, @_returnUrl = Request.Url.PathAndQuery }, new { @class = "ReAuditinglink" })%>
                            <%break; %>
                            <%default:%>
                            状态未设置
                            <%break; %>
                            <% }%>
                        </div>
                    </td>
                </tr>
                <%_i++;
                } %>
        </table>
        <%= Html.RenderPagination() %>
    </div>
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
    文章列表
</asp:Content>
