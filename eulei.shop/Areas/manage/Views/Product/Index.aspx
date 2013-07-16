<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/manage/Views/Shared/Manage_Areas.Master"
    Inherits="System.Web.Mvc.ViewPage<IEnumerable<eulei.shop.Models.TV_Merchandise>>" %>

<%@ Import Namespace="Q42.Wheels.Mvc.Paging" %>
<%@ Import Namespace="eulei.shop.Code" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    商品列表-商品管理
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" src="<%: Url.Content("~/Libs/ligerUI/js/core/base.js") %>"></script>
    <script type="text/javascript" src="<%: Url.Content("~/Libs/ligerUI/js/ligerui.min.js") %>"></script>
    <script type="text/javascript" src="<%: Url.Content("~/Libs/ligerUI/js/plugins/ligerComboBox.js") %>"></script>
    <script type="text/javascript" src="<%: Url.Content("~/Libs/ligerUI/js/plugins/ligerTree.js") %>"></script>
    <script type="text/javascript" src="<%: Url.Content("~/Content/js/confirm.js") %>"></script>
    <script type="text/javascript">
        $(function () {
            $("#MerchandiseTypeIDCombobox").ligerComboBox({
                width: 150,
                height: 20,
                selectBoxWidth: 200,
                selectBoxHeight: 200, lable: '商品分类',
                textField: 'textcontent', valueField: 'id', valueFieldID: 'MerchandiseTypeID', treeLeafOnly: false,
                tree: { url: '<%: Url.Content("~/Common/GetJsonForMerchandiseTypeID") %>',
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
            <%using (Html.BeginForm("Search", "Product", new { @area = "manage" }, FormMethod.Post, null))
              { %>
            <div>
                标题/商品名称：<%:Html.TextBox("ProductTitle", "", new { @class = "SearchTextProductTitle" })%></div>
            <div>
                <input id="MerchandiseTypeIDCombobox" type="text" />
            </div>
            <div>
                <input type="submit" id="searchSubmit" value="  " />
            </div>
            <%} %>
            <%:Html.ActionLink("添加", "Create", "Product", new { @_returnUrl = Request.Url.PathAndQuery, @area = "manage" }, new { @class = "create" })%>
        </div>
        <table width="100%" bgcolor="#b5d6e6" border="0" cellpadding="0" cellspacing="0">
            <tr class="tbg">
                <td class="tc w40">
                    <%= Html.RenderSortLink("ID", "ArticleID")%>
                </td>
                <td class="tc ">
                    <%= Html.RenderSortLink("标题/商品名称", "ArticleTitle")%>
                </td>
                <td class="tc  w80">
                    <%= Html.RenderSortLink("商品分类", "ClassName")%>
                </td>
                <td class="tc datecss">
                    <%= Html.RenderSortLink("上架日期", "ArticleDate")%>
                </td>
                <td class="tc datecss">
                    <%= Html.RenderSortLink("最后编辑", "ArticleDate")%>
                </td>
                <td class="tc  w40">
                    <%= Html.RenderSortLink("状态", "ArticleState")%>
                </td>
                <td class="tc  w40">
                    <%= Html.RenderSortLink("访问量", "ArticleCount")%>
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
            <%if (item.MerchandiseIsemp)
              { %>
            <tr class="lbgalt isemp">
                <%}
              else
              { %>
                <tr class="lbgalt">
                    <%} %>
                    <%}
                    else
                    { %>
                    <%if (item.MerchandiseIsemp)
                      { %>
                    <tr class="lbg isemp">
                        <%}
                      else
                      { %>
                        <tr class="lbg">
                            <%} %>
                            <%} %>
                            <td class="tc">
                                <%: item.MerchandiseID %>
                            </td>
                            <td class="tl">
                                <label>
                                    标题：</label>
                                <%:Html.ActionLink(item.MerchandiseTitle, "Details", "Home", new { @id = item.MerchandiseID, @area = "Product" }, new { @target = "_blank " })%>
                                <br />
                                <label>
                                    名称：</label>
                                <%:Html.ActionLink(item.MerchandiseName, "Details", "Home", new { @id = item.MerchandiseID, @area = "Product" }, new { @target = "_blank " })%>
                            </td>
                            <td class="tc">
                                <%: item.ProductTypeName %>
                            </td>
                            <td class="tc">
                                <%: String.Format("{0:D}", item.MerchandiseBeginDate) %>
                            </td>
                            <td class="tl">
                                <label>
                                    日 期：</label><%= item.MerchandiseLastOperatDate.ToShortDateString() %>
                                <br />
                                <label>
                                    时 间：</label><%= item.MerchandiseLastOperatDate.ToShortTimeString() %>
                                <br />
                                <label>
                                    操作人：</label>
                                <%= item.MerchandiseLastOperator %>
                            </td>
                            <td class="tc">
                                <% switch (item.MerchandiseState)
                                   { %>
                                <%case (int)ProductState.Editing:%>
                                待审核
                                <%break; %>
                                <%case (int)ProductState.Auditing:%>
                                通过
                                <%break; %>
                                <%default:%>
                                状态未设置
                                <%break; %>
                                <% }%>
                            </td>
                            <td class="tr">
                                <%: item.MerchandiseClickCount %>
                            </td>
                            <td class="tc">
                                <div>
                                    <% switch (item.MerchandiseState)
                                       { %>
                                    <%case (int)ArticleState.Editing:%>
                                    <%: Html.ActionLink("编辑", "Edit", new { @id = item.MerchandiseID, @_returnUrl = Request.Url.PathAndQuery }, new { @class = "editlink" })%>
                                    <%: Html.ActionLink("审核", "Auditing", new { @id = item.MerchandiseID, @_returnUrl = Request.Url.PathAndQuery }, new { @class = "detailslink" })%>
                                    <%: Html.ActionLink("删除", "Delete", new { @id = item.MerchandiseID, @_returnUrl = Request.Url.PathAndQuery }, new { @class = "deletelink" })%>
                                    <%break; %>
                                    <%case (int)ArticleState.Auditing:%>
                                    <%: Html.ActionLink("反审核", "ReAuditing", new { @id = item.MerchandiseID, @_returnUrl = Request.Url.PathAndQuery }, new { @class = "ReAuditinglink" })%>
                                    <%if (item.MerchandiseIsemp)
                                      { %>
                                    <%: Html.ActionLink("熄灯", "SetIsemp", new { @id = item.MerchandiseID, @_returnUrl = Request.Url.PathAndQuery }, new { @class = "detailslink" })%>
                                    <%}
                                      else
                                      { %>
                                    <%: Html.ActionLink("点灯", "SetIsemp", new { @id = item.MerchandiseID, @_returnUrl = Request.Url.PathAndQuery }, new { @class = "detailslink" })%>
                                    <%} %>
                                    <%if (item.MerchandiseIsNew)
                                      { %>
                                    <%: Html.ActionLink("旧款", "SetIsNew", new { @id = item.MerchandiseID, @_returnUrl = Request.Url.PathAndQuery }, new { @class = "detailslink" })%>
                                    <%}
                                      else
                                      { %>
                                    <%: Html.ActionLink("新款", "SetIsNew", new { @id = item.MerchandiseID, @_returnUrl = Request.Url.PathAndQuery }, new { @class = "detailslink" })%>
                                    <%} %>
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
    <link type="text/css" href="<%:Url.Content("~/App_Themes/"+ConfigurationManager.AppSettings["ManageTheme"].ToString()+"/css/Product.css") %>"
        rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="TabTitleContent" runat="server">
    商品管理
</asp:Content>
