<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/manage/Views/Shared/Manage_Areas.Master"
    Inherits="System.Web.Mvc.ViewPage<eulei.shop.Models.TV_Article>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    退回申请
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" src="<%: Url.Content("~/Scripts/jquery.form.js") %>"></script>
        <script type="text/javascript" src="<%: Url.Content("~/Content/js/confirm.js") %>"></script>
    <script type="text/javascript">
        $(function () {        
            $("#tb_past").bind("click", function (event) {
               if ($("#ReturnInfo").val() == "")
                {
                    alert("请填写退回原因信息！");
                    return false;
                }
        
            });
        });
    </script>
    <br />
    <h3>您真的确定要“申请退回”该文章吗？如果该文章还未被其他人办理将直接退回！</h3>
       <% using (Html.BeginForm())
       { %>
        <table>
            <tr>
                <td>
                    <%:Html.Hidden("_returnUrl", ViewData["_returnUrl"].ToString())%>                            
                    <input type="submit" id="tb_past" class="btnsubmit  rmargin15" value="申请退回" />
                    | <a href="<%= ViewData["_returnUrl"].ToString() %>">返回</a>
                </td>
            </tr>
            <tr>
                <td class="tl">退回原因：</td>     
            </tr>
            <tr>
                <td>
                    <textarea id="ReturnInfo" name="ReturnInfo" rows="3" cols="200"></textarea>
                </td>
            </tr>
        </table>
     <% } %>
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
 退回申请
</asp:Content>
