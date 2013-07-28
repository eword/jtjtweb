<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/manage/Views/Shared/Manage_Areas.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<eulei.shop.Models.VW_SA_ShareArticle>>" %>

<%@ Import Namespace="Q42.Wheels.Mvc.Paging" %>
<%@ Import Namespace="eulei.shop.Code" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    文章共享列表-下级推送
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" src="<%: Url.Content("~/Libs/ligerUI/js/core/base.js") %>"></script>
    <script type="text/javascript" src="<%: Url.Content("~/Libs/ligerUI/js/ligerui.min.js") %>"></script>
    <script type="text/javascript" src="<%: Url.Content("~/Libs/ligerUI/js/plugins/ligerComboBox.js") %>"></script>
    <script type="text/javascript" src="<%: Url.Content("~/Libs/ligerUI/js/plugins/ligerTree.js") %>"></script>
    <script type="text/javascript" src="<%: Url.Content("~/Content/js/confirm.js") %>"></script>
    <script type="text/javascript" src="<%: Url.Content("~/Scripts/jquery.form.js") %>"></script>
    <script type="text/javascript">
        $(function () {
            $("#ShareArticleCitedTypeIDCombobox").ligerComboBox({
                width: 150,
                height: 20,
                selectBoxWidth: 250,
                selectBoxHeight: 300, lable: '文章转载分类：',
                textField: 'textcontent', valueField: 'id', valueFieldID: 'ShareArticleCitedTypeID', treeLeafOnly: false,hideOnLoseFocus: true,
                tree: {
                    url: '<%: Url.Content("~/Common/GetJsonForArticleTypeID") %>',
                    checkbox: false,
                    textFieldName: "textcontent",
                    idFieldName: "id",
                    parentIDFieldName: "fid"
                }
            });
            var proData =
                [
                    { id: <%=(int)ShareArticleState.Waiting %>, textcontent: '待处理' },
                       { id: <%=(int)ShareArticleState.Cited %>, textcontent: '已引用' },
                                { id: <%=(int)ShareArticleState.Reprint %>, textcontent: '已转载' },
                                         { id: <%=(int)ShareArticleState.Delete %>, textcontent: '已删除' }
                ];
            $("#ShareArticleStateCombobox").ligerComboBox({
                width: 150,
                height: 20,
                selectBoxWidth: 250,
                selectBoxHeight: 300, lable: '文章状态：',
                textField: 'textcontent', valueField: 'id', valueFieldID: 'id', treeLeafOnly: false,hideOnLoseFocus: true,
                tree: {
                    data: proData,
                    checkbox: false,
                    textFieldName: "textcontent",
                    idFieldName: "id"
                }
            });


            $('.deletelink').confirm({
                wrapper: '<div style="float:right;"><br/><div>',
                msg: '删除？',
                ok: '是',
                cancel: '否',
                timeout: 10000
            });
            $('.ReAuditinglink').confirm({
                wrapper: '<div style="float:right;"><br/><div>',
                msg: '反审核？',
                ok: '是',
                cancel: '否',
                timeout: 10000
            });
            $('.SendToAuditing').confirm({
                wrapper: '<div style="float:right;"><br/><div>',
                msg: '确定送审？',
                ok: '是',
                cancel: '否',
                timeout: 10000
            });
            $('.Withdraw').confirm({
                wrapper: '<div style="float:right;"><br/><div>',
                msg: '确定撤回？',
                ok: '是',
                cancel: '否',
                timeout: 10000
            });
        });
    </script>
    <div>
        <div class="searchBox">
            <script type="text/javascript">
                $(function () {
                    $("#tb_mysend").bind("click", function (event) {
                        $("#postForm").first().attr("action", "/manage/article/Search/<%=(int)ArticleSearchType.MySend%>").submit();
                    });
                    $("#tb_needhandle").bind("click", function (event) {
                        $("#postForm").first().attr("action", "/manage/article/Search/<%=(int)ArticleSearchType.NeedHandle%>").submit();
                    });
                    $("#tb_handled").bind("click", function (event) {
                        $("#postForm").first().attr("action", "/manage/article/Search/<%=(int)ArticleSearchType.Handled%>").submit();
                    });
                    $("#tb_all").bind("click", function (event) {
                        $("#postForm").first().attr("action", "/manage/article/Search/<%=(int)ArticleSearchType.All%>").submit();
                    });
                });
            </script>
            <%using (Html.BeginForm("ShareArticleSearch", "Article", new { @area = "manage" }, FormMethod.Post, new { @id = "postForm" }))
              { %>
            <div>
                标题：<%:Html.TextBox("ShareArticleTitle", "", new { @class = "SearchTextArticleTitle" })%>
            </div>
            <div>
                <input id="ShareArticleCitedTypeIDCombobox" type="text" />
            </div>
            <div>
                <input id="ShareArticleStateCombobox" type="text" />
                </div><div class="tred">默认为：待处理状态</div>
            </div>
            <input type="submit" id="searchSubmit" value="  " />            
            <%} %>            
            
            <div>

                <table width="100%" bgcolor="#b5d6e6" border="0" cellpadding="0" cellspacing="0">
                    <tr class="tbg">
                        <td class="tc w40">
                            <%= Html.RenderSortLink("目标ID", "ShareArticleID")%>
                        </td>
                        <td class="tc ">
                            <%= Html.RenderSortLink("标题", "ShareArticleTitle")%>
                        </td>
                        <td class="tc datecss">
                            <%= Html.RenderSortLink("发布日期", "ShareArticleSendDate")%>
                        </td>
                        <td class="tc datecss">
                            <%= Html.RenderSortLink("推送日期", "ShareArticleShareDate")%>
                        </td>
                        <td class="tc datecss">
                            <%= Html.RenderSortLink("引用日期", "ShareArticleCitedDate")%>
                        </td>
                        <td class="tc datecss">
                            <%= Html.RenderSortLink("作者", "ShareArticleAuthor")%>
                        </td>
                        <td class="tc datecss">
                            <%= Html.RenderSortLink("推送者", "ShareArticlePusher")%>
                        </td>
                        <td class="tc datecss">
                            <%= Html.RenderSortLink("引用者", "UserName")%>
                        </td>
                        <td class="tc  w80">
                            <%= Html.RenderSortLink("文章分类", "ShareArticleType")%>
                        </td>
                        <td class="tc  w80">
                            <%= Html.RenderSortLink("文章目标类别", "ShareArticleCitedTypeID")%>
                        </td>

                        <td class="tc  w80">
                            <%= Html.RenderSortLink("状态", "ShareArticleState")%>
                        </td>

                        <td class="tc  w200">基本操作
                        </td>
                    </tr>
                    <% 
                        int _i = 1;
                        foreach (var item in this.GetPage(Model))
                        {                           
                    %>
                    <tr class="<%=_i%2==0?"lbgalt":"lbg" %>  <%=item.ShareArticleIsemp?"isemp":"" %>">                    
                        <td class="tc">
                            <%: item.ShareArticleCitedArticleID %>
                        </td>
                        <td <%=item.ShareArticleIsemp?"class=\"tred\"":"" %>>
                            <%:item.ShareArticleTitle %>
                        </td>
                        <td class="tc">
                            <%: String.Format("{0:D}", item.ShareArticleSendDate) %>
                        </td>
                        <td class="tc">
                            <%: String.Format("{0:D}", item.ShareArticleShareDate) %>
                        </td>
                        <td class="tc">
                            <%: String.Format("{0:D}", item.ShareArticleCitedDate) %>
                        </td>
                        <td class="tc">
                            <%:item.ShareArticleAuthor %>
                        </td>
                        <td class="tc">
                            <%:item.ShareArticlePusherFriendName %>
                    
                            (<%: item.ShareArticlePusher %>)
                        </td>
                        <td class="tc">
                            <%if(!string.IsNullOrEmpty(item.UserName)) {%>
                            <%:item.UserInfoFriendName %>                    
                            (<%: item.UserName %>)
                            <%} %>
                        </td>
                        <td class="tc">
                            <%: item.ShareArticleType %>
                        </td>
                        <td class="tc">
                            <%: item.ArticleTypeName %>
                        </td>
                        <td class="tc">
                            <%switch (item.ShareArticleState)
                              { %>
                            <%case (int)ShareArticleState.Waiting: %>
                                待处理
                                <%break;%>
                            <%case (int)ShareArticleState.Delete: %>
                                已删除
                                <%break;%>
                            <%case (int)ShareArticleState.Reprint: %>
                                已转载
                                <%break;%>
                            <%case (int)ShareArticleState.Cited: %>
                                已引用
                                <%break;%>
                            <%} %>
                            <%if (item.ShareArticleIsApplyReturn)
                              { %>
                            <br />
                            <strong class="tred">申请退回中</strong>
                            <%} %>
                        </td>
                        <td class="tl">
                            <%switch (item.ShareArticleState)
                              { %>
                            <%case (int)ShareArticleState.Waiting: %>
                            <%: Html.ActionLink("引用", "ShareArticleCited", new { @id = item.ShareArticleID, @_returnUrl = Request.Url.PathAndQuery }, new { @class = "detailslink line-height24" })%>
                            <%: Html.ActionLink("转载", "ShareArticleReprint", new { @id = item.ShareArticleID, @_returnUrl = Request.Url.PathAndQuery }, new { @class = "detailslink line-height24" })%>
                            <%: Html.ActionLink("删除", "ShareArticleDelete", new { @id = item.ShareArticleID, @_returnUrl = Request.Url.PathAndQuery }, new { @class = "deletelink line-height24" })%>
                            <%break;%>
                            <%case (int)ShareArticleState.Delete: %>
                                已删除
                                <%break;%>
                            <%case (int)ShareArticleState.Reprint: %>
                            <%: Html.ActionLink("引用", "ShareArticleCited", new { @id = item.ShareArticleID, @_returnUrl = Request.Url.PathAndQuery }, new { @class = "detailslink line-height24" })%>
                            <%: Html.ActionLink("再次转载", "ShareArticleReprint", new { @id = item.ShareArticleID, @_returnUrl = Request.Url.PathAndQuery }, new { @class = "detailslink line-height24" })%>
                            <%: Html.ActionLink("删除", "ShareArticleDelete", new { @id = item.ShareArticleID, @_returnUrl = Request.Url.PathAndQuery }, new { @class = "deletelink line-height24" })%>
                            <%break;%>
                            <%case (int)ShareArticleState.Cited: %>
                            <%: Html.ActionLink("再次引用", "ShareArticleCited", new { @id = item.ShareArticleID, @_returnUrl = Request.Url.PathAndQuery }, new { @class = "detailslink line-height24" })%>
                            <%: Html.ActionLink("删除", "ShareArticleDelete", new { @id = item.ShareArticleID, @_returnUrl = Request.Url.PathAndQuery }, new { @class = "deletelink line-height24" })%>
                            <%break;%>
                            <%} %>      
                        </td>
                    </tr>
                    <%_i++;
                        } %>
                </table>
                <%= Html.RenderPagination() %>
            </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="headOtherInfo" runat="server">
    <link type="text/css" href="<%:Url.Content("~/Libs/ligerUI/skins/Aqua/css/ligerui-all.css") %>" rel="stylesheet" />
    <link type="text/css" href="<%:Url.Content("~/App_Themes/"+ConfigurationManager.AppSettings["ManageTheme"].ToString()+"/css/general.css") %>"
        rel="stylesheet" />
    <link type="text/css" href="<%:Url.Content("~/App_Themes/"+ConfigurationManager.AppSettings["ManageTheme"].ToString()+"/css/master.css") %>"
        rel="stylesheet" />
    <link type="text/css" href="<%:Url.Content("~/App_Themes/"+ConfigurationManager.AppSettings["ManageTheme"].ToString()+"/css/ShareArticle.css") %>"
        rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="TabTitleContent" runat="server">
    文章共享
</asp:Content>
