<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/manage/Views/Shared/Manage_Areas.Master"
    Inherits="System.Web.Mvc.ViewPage<IEnumerable<eulei.shop.Models.TV_Article>>" %>
<%@ Import Namespace="Q42.Wheels.Mvc.Paging" %>
<%@ Import Namespace="eulei.shop.Code" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    文章列表-文章管理
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
            $("#ArticleTypeIDCombobox").ligerComboBox({
                width: 150,
                height: 20,
                selectBoxWidth: 250,
                selectBoxHeight: 300, lable: '特性分类',
                textField: 'textcontent', valueField: 'id', valueFieldID: 'ArticleTypeID', treeLeafOnly: false,
                tree: {
                    url: '<%: Url.Content("~/Common/GetJsonForArticleTypeID") %>',
                    checkbox: false,
                    textFieldName: "textcontent",
                    idFieldName: "id",
                    parentIDFieldName: "fid"
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
            <%using (Html.BeginForm("Search", "Article", new { @area = "manage" }, FormMethod.Post, new { @id = "postForm" }))
              { %>
            <div>
                标题：<%:Html.TextBox("ArticleTitle", "", new { @class = "SearchTextArticleTitle" })%>
            </div>
            <div>
                <input id="ArticleTypeIDCombobox" type="text" />
            </div>
            <div>
                <input type="submit" id="tb_needhandle" class="btnsubmit  rmargin" value="待办文章" />
                <input type="submit" id="tb_handled" class="btnsubmit  rmargin" value="已办文章" />
                <input type="submit" id="tb_mysend" class="btnsubmit  rmargin" value="我的发文" />
                <%if (SystemMemberShip.GetSystemAuthority(Session["SystemAuthority"].ToString(), SystemMemberShip.ArticleBrowseAll))
                  { %>
                <input type="submit" id="tb_all" class="btnsubmit  rmargin" value="全部文章" />
                <%} %>
            </div>
            <%} %>
            <%if (SystemMemberShip.GetSystemAuthority(Session["SystemAuthority"].ToString(), SystemMemberShip.ArticleCreate))
              { %>
            <%:Html.ActionLink("添加", "Create", "Article", new { @_returnUrl = Request.Url.PathAndQuery, @area = "manage" }, new { @class = "create" })%>
            <%} %>
        </div>
        <table width="100%" bgcolor="#b5d6e6" border="0" cellpadding="0" cellspacing="0">
            <tr class="tbg">
                <td class="tc w40">
                    <%= Html.RenderSortLink("ID", "ArticleID")%>
                </td>
                <td class="tc ">
                    <%= Html.RenderSortLink("标题", "ArticleTitle")%>
                </td>
                <td class="tc datecss">
                    <%= Html.RenderSortLink("发布日期", "ArticleSendDate")%>
                </td>
                <td class="tc datecss">
                    <%= Html.RenderSortLink("作者", "ArticleAuthor")%>
                </td>
                <td class="tc  w80">
                    <%= Html.RenderSortLink("文章分类", "ClassName")%>
                </td>
                <td class="tc  w80">
                    <%= Html.RenderSortLink("状态", "ArticleState")%>
                </td>
                <td class="tc  w40">
                    <%= Html.RenderSortLink("访问量", "ArticleCount")%>
                </td>
                <td class="tc  w240">基本操作
                </td>
            </tr>
            <% 
                int _i = 1;
                foreach (var item in this.GetPage(Model))
                {
                   
            %>
         <tr class="<%=_i%2==0?"lbgalt":"lbg" %>  <%=item.ArticleIsemp?"isemp":"" %>">
                <td class="tc">
                    <%: item.ArticleID %>
                </td>
                <td class=" <%=item.ArticleIsemp?"tred":"" %>">
                     <%:Html.ActionLink("【操作记录】", "GetArticleOperationgRecord", "Article", new { @id = item.ArticleID,@area="manage", @_returnUrl = Request.Url.PathAndQuery }, new {@target="_blank " ,@class="tlink" })%>
                    <%:Html.ActionLink("【预览】", "Preview", "Home", new { @id = item.ArticleID,@area="Article" }, new {@target="_blank ",@class="tlink" })%>
                    <%:item.ArticleTitle %>
                </td>
                            <td class="tc">
                                <%: String.Format("{0:D}", item.ArticleSendDate) %>
                            </td>
                            <td class="tc">
                                <%:item.UserInfoFriendName %>
                                <br />
                                (<%: item.ArticleAuthor %>)
                            </td>
                            <td class="tc">
                                <%: item.ArticleTypeName %>
                            </td>
                            <td class="tc">
                                <%if (item.ArticleState.Equals((int)ArticleState.Auditing)) %>
                                <%{ %>
                                 已发布
                                <%}
                                  else
                                  { %>
                                <% if (!string.IsNullOrEmpty(item.ArticleCurrentFlowName))
                                   { %>

                                <%:item.ArticleCurrentFlowName%>

                                <% }
                                   else
                                   {%>
                                文章流程
                                <br />
                                未定义
                                     <% }%>
                                <%} %>
                                <%if (item.ArticleIsApplyReturn)
                                  { %>
                                <br />
                                <strong class="tred">申请退回中</strong>
                                <%} %>
                            </td>
                            <td class="tr">
                                <%: item.ArticleClickCount %>
                            </td>
                            <td class="tl">
                                <%switch (item.ArticleState)
                                  { %>
                                <%case (int)ArticleState.Auditing: %>
                                <%if (item.ArticleIsemp)
                                  { %>
                                <%: Html.ActionLink("熄灯", "SetIsemp", new { @id = item.ArticleID, @_returnUrl = Request.Url.PathAndQuery }, new { @class = "detailslink line-height24" })%>
                                <%}
                                  else
                                  { %>
                                <%: Html.ActionLink("点灯", "SetIsemp", new { @id = item.ArticleID, @_returnUrl = Request.Url.PathAndQuery }, new { @class = "detailslink line-height24" })%>
                                <%} %>

                                <%if (item.ArticleIsNew)
                                  { %>
                                <%: Html.ActionLink("旧闻", "SetIsNew", new { @id = item.ArticleID, @_returnUrl = Request.Url.PathAndQuery }, new { @class = "detailslink line-height24" })%>
                                <%}
                                  else
                                  {%>
                                <%: Html.ActionLink("新闻", "SetIsNew", new { @id = item.ArticleID, @_returnUrl = Request.Url.PathAndQuery }, new { @class = "detailslink line-height24" })%>
                                <%} %>
                                <%: Html.ActionLink("反审核", "ReAuditing", new { @id = item.ArticleID, @_returnUrl = Request.Url.PathAndQuery }, new { @class = "ReAuditinglink line-height24" })%>
                                <%if (item.ArticleIsShare)
                                  { %>
                                <%: Html.ActionLink("推送撤回", "Repush", new { @id = item.ArticleID, @_returnUrl = Request.Url.PathAndQuery }, new { @class = "detailslink line-height24" })%>
                                <%}
                                  else
                                  { %>
                                <%: Html.ActionLink("推送", "Push", new { @id = item.ArticleID, @_returnUrl = Request.Url.PathAndQuery }, new { @class = "detailslink line-height24" })%>
                                <%} %>
                                <%break; %>
                                <%case (int)ArticleState.Editing: %>
                                <%if (item.ArticleStatusID.Equals(1))
                                  { %>
                                <%: Html.ActionLink("编辑", "Edit", new { @id = item.ArticleID, @_returnUrl = Request.Url.PathAndQuery }, new { @class = "editlink line-height24" })%>
                                <%: Html.ActionLink("删除", "Delete", new { @id = item.ArticleID, @_returnUrl = Request.Url.PathAndQuery }, new { @class = "deletelink line-height24" })%>

                                <%} %>
                                <%else %>
                                <%{ %>
                                <%: Html.ActionLink("快速退回", "ReturnToAuthor", new { @id = item.ArticleID, @_returnUrl = Request.Url.PathAndQuery }, new { @class = "Withdraw detailslink line-height24" })%>
                                <%} %>
                                <%: Html.ActionLink("审核/发布", "Auditing", new { @id = item.ArticleID, @_returnUrl = Request.Url.PathAndQuery }, new { @class = "detailslink line-height24" })%>
                                <%break; %>
                                <%case (int)ArticleState.IsShare: %>
                                <%: Html.ActionLink("删除", "Delete", new { @id = item.ArticleID, @_returnUrl = Request.Url.PathAndQuery }, new { @class = "deletelink line-height24" })%>
                                <%break; %>
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
    <link type="text/css" href="<%:Url.Content("~/App_Themes/"+ConfigurationManager.AppSettings["ManageTheme"].ToString()+"/css/Article.css") %>"
        rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="TabTitleContent" runat="server">
    文章管理
</asp:Content>
