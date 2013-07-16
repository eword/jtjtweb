<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Article/Views/Shared/Article_Areas.Master"
    Inherits="System.Web.Mvc.ViewPage<IEnumerable<eulei.shop.Models.TV_Article>>" %>

<%@ Import Namespace="Q42.Wheels.Mvc.Paging" %>
<%@ Import Namespace="eulei.shop.Models" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    内容搜索-<%:ConfigurationManager.AppSettings["webTitle"].ToString()%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
 <div class="SearchBox">
        <% using (Html.BeginForm("Index", "Search", new { @area = "Article" }, FormMethod.Post, null))
           { %>
        <%:Html.TextBox("SearchText", "", new { @class = "SearchText", @id = "SearchText" })%>
        <input type="submit" id="btnSubmit" class="searchSubmit" value="   " />
        <% }%>
    </div>

    <div>
    </div>
    <div class="article-list-box">
        <%   foreach (var item in this.GetPage(Model))
             { %>
        <div class="article-box">
            <div class="article-box-date">
                <div class="article-box-date-yearandmonth">
                    <%:string.Format("{0}-{1}", item.ArticleSendDate.Year, item.ArticleSendDate.Month)%></div>
                <div class="article-box-date-day">
                    <%:item.ArticleSendDate.Day%>
                </div>
            </div>
            <div class="article-box-title">
                <h1>
                    <a href="/Article/Home/Index/<%:item.ArticleTypeID %>?aid=<%:item.ArticleID %>"
                        target="_blank">
                        <%=item.ArticleTitle %></a>
                </h1>
                <span class="categories">分类：
                <%: Html.ActionLink(item.ArticleTypeName, "Index", "Home", new { @id = item.ArticleTypeID, @Area = "Article" }, null)%>
                </span>
            </div>
            <div class="article-details-box-content">
                <%=item.ArticleContent%>
            </div>
            <div class="article-box-footer">
                <p class="article-box-footer-tags">
                    标签:
                    <%Html.RenderPartial("Label", item); %>
                </p>
                <p class="article-box-footer-docomment">
                    浏览量:<%=item.ArticleClickCount %></p>
            </div>
        </div>
        <%} %>
        <br />
        <div class="pager">
            <%= Html.RenderPagination() %>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="headOtherInfo" runat="server">
    <link type="text/css" href="<%:Url.Content("~/App_Themes/"+ConfigurationManager.AppSettings["ArticleTheme"].ToString()+"/css/general.css") %>"
        rel="stylesheet" />
    <link type="text/css" href="<%:Url.Content("~/App_Themes/"+ConfigurationManager.AppSettings["ArticleTheme"].ToString()+"/css/master.css") %>"
        rel="stylesheet" />
    <link type="text/css" href="<%:Url.Content("~/App_Themes/"+ConfigurationManager.AppSettings["ArticleTheme"].ToString()+"/css/article.css") %>"
        rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="RightContent" runat="server">
    <%Html.RenderAction("Index", "ContentNav", new { @id = 2, @area = "Article" }); %>
</asp:Content>
