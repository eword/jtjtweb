<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Article/Views/Shared/Article_Areas.Master"
    Inherits="System.Web.Mvc.ViewPage<eulei.shop.Models.TV_Article>" %>
    <%@ Import Namespace="Q42.Wheels.Mvc.Paging" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%:ConfigurationManager.AppSettings["webTitle"].ToString()%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="headOtherInfo" runat="server">
    <link type="text/css" href="<%:Url.Content("~/App_Themes/"+ConfigurationManager.AppSettings["ArticleTheme"].ToString()+"/css/general.css") %>"
        rel="stylesheet" />
    <link type="text/css" href="<%:Url.Content("~/App_Themes/"+ConfigurationManager.AppSettings["ArticleTheme"].ToString()+"/css/master.css") %>"
        rel="stylesheet" />
    <link type="text/css" href="<%:Url.Content("~/App_Themes/"+ConfigurationManager.AppSettings["ArticleTheme"].ToString()+"/css/article.css") %>"
        rel="stylesheet" />
    <link type="text/css" href="<%:Url.Content("~/App_Themes/"+ConfigurationManager.AppSettings["GeneralTheme"].ToString()+"/css/official.css") %>"
        rel="stylesheet" />
        <meta name="keywords" content="<%:Model.ArticleLabel %>" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="listpager_arrow_box">
        <div class="listpager_arrow">
            当前位置：<%:Html.MvcSiteMap().SiteMapPath() %>&gt;&gt;内容
        </div>
    </div>
    <div class="article-details">
        <div class="article-details-box">
            <div>
                <div class="article-details-box-date">
                    <div class="article-details-box-date-yearandmonth">
                        <%:string.Format("{0}-{1}",this.Model.ArticleSendDate.Year, Model.ArticleSendDate.Month)%></div>
                    <div class="article-details-box-date-day">
                        <%:Model.ArticleSendDate.Day%>
                    </div>
                </div>
                <div class="article-details-box-title">
                    <h1>
                        <%: Html.ActionLink(Model.ArticleTitle, "Index", "Home", new { @id = Model.ArticleTypeID, @aid = Model.ArticleID, @Area = "Article" }, null)%>
                    </h1>
                    <span class="categories">分类：
                    <%: Html.ActionLink(Model.ArticleTypeName, "Index", "Home", new { @id = Model.ArticleTypeID, @Area = "Article" }, null)%>
                    </span>
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
                    浏览量:<%=Model.ArticleClickCount%></p>
            </div>
        </div>
    </div>
            <div class="pager">
            <% Html.RenderAction("CustomerPagination", "Common", new {@Area="",@CustomerUrl=ViewBag.Url, @FirstPage = ViewBag.FirstPage, @CurrentPage = ViewBag.CurrentPage, @LastPage = ViewBag.LastPage });%>
        </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="RightContent" runat="server">
<%Html.RenderAction("Details", "ContentNav", new { @id =ViewBag.NavID,@area="Article" }); %>  
</asp:Content>
