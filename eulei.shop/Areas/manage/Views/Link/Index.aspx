<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/manage/Views/Shared/Manage_Areas.Master"
    Inherits="System.Web.Mvc.ViewPage<IEnumerable<eulei.shop.Models.TB_Link>>" %>

<%@ Import Namespace="Q42.Wheels.Mvc.Paging" %>
<%@ Import Namespace="eulei.shop.Code" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    友情链接管理
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
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
            <%using (Html.BeginForm("Search", "Link", new { @area = "manage" }, FormMethod.Post, null))
              { %>
            <div>
                标题：<%:Html.TextBox("LinkTitle", "", new { @class = "SearchTextLinkTitle" })%></div>
            <div>
                <input type="submit" id="searchSubmit" value="  " />
            </div>
            <%} %>
            <%:Html.ActionLink("添加", "Create", "Link", new { @_returnUrl = Request.Url.PathAndQuery, @area = "manage"  }, new { @class = "create"})%>
        </div>
        <table width="100%" bgcolor="#b5d6e6" border="0" cellpadding="0" cellspacing="0">
            <tr class="tbg">
                <td class="tc w40">
                    <%= Html.RenderSortLink("顺序", "LinkOrder")%>
                </td>
                <td class="tc ">
                    图片
                </td>
                <td class="tc ">
                    <%= Html.RenderSortLink("名称", "LinkName")%>
                </td>
                <td class="tc ">
                    <%= Html.RenderSortLink("显示标题", "LinkTitle")%>
                </td>
                <td class="tc  w40">
                    <%= Html.RenderSortLink("状态", "FlashADXMLState")%>
                </td>
                <td class="tc  w180">
                    基本操作
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
                <%}
                    else
                    { %>
                <tr class="lbg">
                    <%} %>
                    <td class="tc">
                        <%: item.LinkOrder %>
                    </td>
                    <td class="tc">
                        <%if (item.LinkIsPicLink)
                          { %>
                        <img src="<%: Url.Content(item.LinkPicUrl) %>" alt="<%: item.LinkTitle %>" width="130"
                            height="45" />
                        <%}
                          else
                          { %>
                        文本链接
                        <%} %>
                    </td>
                    <td class="tl">
                        <%:item.LinkName %>
                    </td>
                    <td class="tl">
                        <%:item.LinkTitle %>
                    </td>
                    <td class="tc">
                        <% switch (item.LinkState)
                           { %>
                        <%case (int)LinkState.Editing:%>
                        待审核
                        <%break; %>
                        <%case (int)LinkState.Auditing:%>
                        通过
                        <%break; %>
                        <%default:%>
                        状态未设置
                        <%break; %>
                        <% }%>
                    </td>
                    <td class="tc">
                        <div>
                            <% switch (item.LinkState)
                               { %>
                            <%case (int)LinkState.Editing:%>
                            <%: Html.ActionLink("编辑", "Edit", new { @id = item.LinkID, @_returnUrl = Request.Url.PathAndQuery }, new { @class = "editlink" })%>
                            <%: Html.ActionLink("审核", "Auditing", new { @id = item.LinkID, @_returnUrl = Request.Url.PathAndQuery }, new { @class = "detailslink" })%>
                            <%: Html.ActionLink("删除", "Delete", new { @id = item.LinkID, @_returnUrl = Request.Url.PathAndQuery }, new { @class = "deletelink" })%>
                            <%break; %>
                            <%case (int)LinkState.Auditing:%>
                            <%: Html.ActionLink("反审核", "ReAuditing", new { @id = item.LinkID, @_returnUrl = Request.Url.PathAndQuery }, new { @class = "ReAuditinglink" })%>
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
        <%= Html.RenderPagination() %></div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="headOtherInfo" runat="server">
    <link type="text/css" href="<%:Url.Content("~/Libs/ligerUI/skins/Aqua/css/ligerui-all.css") %>"
        rel="stylesheet" />
    <link type="text/css" href="<%:Url.Content("~/App_Themes/"+ConfigurationManager.AppSettings["ManageTheme"].ToString()+"/css/general.css") %>"
        rel="stylesheet" />
    <link type="text/css" href="<%:Url.Content("~/App_Themes/"+ConfigurationManager.AppSettings["ManageTheme"].ToString()+"/css/master.css") %>"
        rel="stylesheet" />
    <link type="text/css" href="<%:Url.Content("~/App_Themes/"+ConfigurationManager.AppSettings["ManageTheme"].ToString()+"/css/Link.css") %>"
        rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="TabTitleContent" runat="server">
    友情链接
</asp:Content>
