<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/manage/Views/Shared/Manage_Areas.Master"
    Inherits="System.Web.Mvc.ViewPage<eulei.shop.Models.TV_Article>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    文章流程
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" src="<%: Url.Content("~/Scripts/jquery.form.js") %>"></script>
    <script type="text/javascript" src="<%: Url.Content("~/Content/js/confirm.js") %>"></script>
    <script type="text/javascript">
        $(function () {
                        <%if (Model.ArticleIsApplyReturn)
                          {%>
            $('#tb_past').confirm({
                msg: '作者“<%:Model.ArticleAuthor%>”申请了退回操作，请问是否继续发布？',
                ok: '是',
                cancel: '否',
                timeout: 10000,
                buttons: {
                wrapper:'<button></button>',
                separator:'  '
                }
            });
            <%}%>
            $("#tb_past").bind("click", function (event) {
                $("#postForm").first().attr("action", "/manage/article/SendToNextAuditing/<%=Model.ArticleID%>").submit();
            });
            $("#tb_return").bind("click", function (event) {
                if ($("#ReturnInfo").val() == "") {
                    alert("请填写操作信息！");
                    return false;
                }
                $("#postForm").first().attr("action", "/manage/article/ReturnToAuthor/<%=Model.ArticleID%>").submit();
            });



        });
    </script>
    <br />
    <h3>您真的确定要“<%:ViewBag.Title %>”该文章吗？</h3>
    <%if (Model.ArticleIsApplyReturn)
      {%>
    <strong class="tred">作者“<%:Model.ArticleAuthor%>”已经申请了退回！</strong>
    <%}%>
    <form id="postForm" method="post" action="manage/account/roleinfo">
        <table>
            <tr>
                <td>
                    <%:Html.Hidden("_returnUrl", ViewData["_returnUrl"].ToString())%>
                      <%:Html.Hidden("_returnUrl", ViewData["_returnUrl"].ToString())%>
                      <%:Html.Hidden("NextUserIDList",(string)ViewBag.NextUserIDList)%>
                    下一步办理人：    <%:Html.Hidden("NextUserList",(string)ViewBag.NextUserList)%><input type="submit" id="changeUser" class="btnsubmit  rmargin15" value="修改" />
                    <br />
                    <input type="submit" id="tb_past" class="btnsubmit  rmargin15" value="<%:ViewBag.Title %>" />
                    =><%:Html.ActionLink("操作记录","GetArticleOperationgRecord","Article",new{@id=Model.ArticleID,@area="manage", @_returnUrl = Request.Url.PathAndQuery },new{@target="_blank",@class="tred",@title="点击查看操作记录"}) %><=
                    <%if (!Model.ArticleStatusID.Equals(1))
                      { %>
                    <input type="submit" id="tb_return" class="btnsubmit  rmargin15" value="退回" />
                    <%} %>
                    | <a href="<%= ViewData["_returnUrl"].ToString() %>">返回</a>
                </td>
            </tr>
            <tr>
                <td class="tl">操作信息</td>
            </tr>
            <tr>
                <td>
                    <textarea id="ReturnInfo" name="ReturnInfo" rows="3" cols="200"></textarea>
                </td>
            </tr>
        </table>
    </form>
    <hr />
    <br />
    <div class="article-details">
        <div class="article-details-box">
            <div>
                <div class="article-details-box-date">
                    <div class="article-details-box-date-yearandmonth">
                        <%:string.Format("{0}-{1}", Model.ArticleSendDate.Year, Model.ArticleSendDate.Month)%>
                    </div>
                    <div class="article-details-box-date-day">
                        <%:Model.ArticleSendDate.Day%>
                    </div>
                </div>
                <div class="article-details-box-title">
                    <h1>
                        <%: Html.ActionLink(Model.ArticleTitle, "Index", "Home", new { @id = Model.ArticleTypeID, @aid = Model.ArticleID, @Area = "Article" }, null)%>
                    </h1>
                    <span class="categories">分类：<a href="#" title="分类内容"><%:Model.ArticleTypeName %></a></span>
                </div>
            </div>
            <div class="fixck article-details-box-content">
                <%=Model.ArticleContent %>
            </div>
            <div class="article-details-box-footer">
                <p class="article-details-box-footer-tags">
                    标签:
                    <%Html.RenderPartial("Label", Model); %>
                </p>
                <p class="article-details-box-footer-docomment">
                    浏览量:<%=Model.ArticleClickCount%>
                </p>
            </div>
        </div>
    </div>
    <div class="pager">
        <% Html.RenderAction("CustomerPagination", "Common", new { @Area = "", @CustomerUrl = ViewBag.Url, @FirstPage = ViewBag.FirstPage, @CurrentPage = ViewBag.CurrentPage, @LastPage = ViewBag.LastPage });%>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="headOtherInfo" runat="server">
    <link type="text/css" href="<%:Url.Content("~/Libs/ligerUI/skins/Aqua/css/ligerui-all.css") %>"
        rel="stylesheet" />
    <link type="text/css" href="<%:Url.Content("~/App_Themes/"+ConfigurationManager.AppSettings["ManageTheme"].ToString()+"/css/general.css") %>"
        rel="stylesheet" />
    <link type="text/css" href="<%:Url.Content("~/App_Themes/"+ConfigurationManager.AppSettings["ManageTheme"].ToString()+"/css/master.css") %>"
        rel="stylesheet" />
    <link type="text/css" href="<%:Url.Content("~/App_Themes/"+ConfigurationManager.AppSettings["ManageTheme"].ToString()+"/css/ArticleAuditing.css") %>"
        rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="TabTitleContent" runat="server">
    文章流程
</asp:Content>
