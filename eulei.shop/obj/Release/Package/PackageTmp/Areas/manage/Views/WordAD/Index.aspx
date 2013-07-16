<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/manage/Views/Shared/Manage_Areas.Master"
    Inherits="System.Web.Mvc.ViewPage<IEnumerable<eulei.shop.Models.TB_Advertisement>>" %>

<%@ Import Namespace="Q42.Wheels.Mvc.Paging" %>
<%@ Import Namespace="eulei.shop.Code" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    文字广告
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" src="<%: Url.Content("~/Libs/ligerUI/js/core/base.js") %>"></script>
    <script type="text/javascript" src="<%: Url.Content("~/Libs/ligerUI/js/ligerui.min.js") %>"></script>
    <script type="text/javascript" src="<%: Url.Content("~/Libs/ligerUI/js/plugins/ligerComboBox.js") %>"></script>
    <script type="text/javascript" src="<%: Url.Content("~/Libs/ligerUI/js/plugins/ligerTree.js") %>"></script>
    <script type="text/javascript" src="<%: Url.Content("~/Content/js/confirm.js") %>"></script>
    <script type="text/javascript">
        $(function () {
            $("#AdvertisementTypeIDCombobox").ligerComboBox({
                width: 150,
                height: 20,
                selectBoxWidth: 200,
                selectBoxHeight: 200, lable: '广告类别',
                textField: 'textcontent', valueField: 'id', valueFieldID: 'AdvertisementType', treeLeafOnly: false,
                tree: {
                    url: '<%: Url.Content("~/Common/GetJsonForAdvertisementTypeID") %>',
                    checkbox: false,
                    textFieldName: "textcontent",
                    idFieldName: "id",
                    parentIDFieldName: "fid"
                }
            });
            $('.deletelink').confirm({
                msg: '删除？',
                ok: '是',
                cancel: '否',
                timeout: 10000
            }); $('.Auditinglink').confirm({
                msg: '审核？',
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
            <%using (Html.BeginForm("Search", "WordAD", new { @area = "manage" }, FormMethod.Post, null))
              { %>
            <div>
                标题：<%:Html.TextBox("AdvertisementTitle", "", new { @class = "SearchTextAdvertisementTitle" })%></div>
            <div>
                <input id="AdvertisementTypeIDCombobox" type="text" />
            </div>
            <div>
                <input type="submit" id="searchSubmit" value="  " />
            </div>
            <%} %>
            <%:Html.ActionLink("添加", "Create", "WordAD", new { @_returnUrl = Request.Url.PathAndQuery, @area = "manage"  }, new { @class = "create"})%>
        </div>
        <table width="100%" bgcolor="#b5d6e6" border="0" cellpadding="0" cellspacing="0">
            <tr class="tbg">
                <td class="tc w40">
                    <%= Html.RenderSortLink("ID", "AdvertisementID")%>
                </td>
                <td class="tc">
                    <%= Html.RenderSortLink("标题", "AdvertisementTitle")%>
                </td>
                <td class="tc  datecss">
                    <%= Html.RenderSortLink("提醒日期", "AdvertisementRemindDate")%>
                </td>
                <td class="tc  w80">
                    <%= Html.RenderSortLink("广告分类", "AdvertisementType")%>
                </td>
                <td class="tc  w40">
                    <%= Html.RenderSortLink("状态", "AdvertisementState")%>
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
                        <%: item.AdvertisementID %>
                    </td>
                    <td class="tl">
                        <%:item.AdvertisementTitle %>
                    </td>
                    <td class="tc">
                        <%: String.Format("{0:D}", item.AdvertisementRemindDate) %>
                    </td>
                    <td class="tc">
                        <% switch (item.AdvertisementType)
                           { %>
                        <%case (int)AdvertisementType.HomePageMaster:%>
                        首页模板
                        <%break; %>
                        <%case (int)AdvertisementType.ArticlePageMaster:%>
                        文章页模板
                        <%break; %>
                        <%case (int)AdvertisementType.ProductPageMaster:%>
                        商品页模板
                        <%break; %>
                        <%case (int)AdvertisementType.HomePageMasterTitle:%>
                        首页模板标头
                        <%break; %>
                        <%default:%>
                        状态未设置
                        <%break; %>
                        <% }%>
                    </td>
                    <td class="tc">
                        <% switch (item.AdvertisementState)
                           { %>
                        <%case (int)AdvertisementState.Editing:%>
                        待审核
                        <%break; %>
                        <%case (int)AdvertisementState.Auditing:%>
                        通过
                        <%break; %>
                        <%default:%>
                        状态未设置
                        <%break; %>
                        <% }%>
                    </td>
                    <td class="tc">
                        <div>
                            <% switch (item.AdvertisementState)
                               { %>
                            <%case (int)AdvertisementState.Editing:%>
                            <%: Html.ActionLink("编辑", "Edit", new { @id = item.AdvertisementID, @_returnUrl = Request.Url.PathAndQuery }, new { @class = "editlink" })%>
                            <%: Html.ActionLink("审核", "Auditing", new { @id = item.AdvertisementID, @_returnUrl = Request.Url.PathAndQuery }, new { @class = "Auditinglink" })%>
                            <%: Html.ActionLink("删除", "Delete", new { @id = item.AdvertisementID, @_returnUrl = Request.Url.PathAndQuery }, new { @class = "deletelink" })%>
                            <%break; %>
                            <%case (int)AdvertisementState.Auditing:%>
                            <%: Html.ActionLink("反审核", "ReAuditing", new { @id = item.AdvertisementID, @_returnUrl = Request.Url.PathAndQuery }, new { @class = "ReAuditinglink" })%>
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
    <link type="text/css" href="<%:Url.Content("~/App_Themes/"+ConfigurationManager.AppSettings["ManageTheme"].ToString()+"/css/WordAD.css") %>"
        rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="TabTitleContent" runat="server">
    文章广告
</asp:Content>
