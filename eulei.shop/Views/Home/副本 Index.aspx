<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<%@ Import Namespace="eulei.shop.Code" %>
<!DOCTYPE html
PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" 
"http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="icon" href="~/Content/images/favicon.ico" type="image/x-icon" />
    <link rel="shortcut icon" href="~/Content/images/favicon.ico" type="image/x-icon" />
    <title>
        <%:ConfigurationManager.AppSettings["webTitle"].ToString()%></title>
    <link type="text/css" href="<%:Url.Content("~/App_Themes/"+ConfigurationManager.AppSettings["IndexTheme"].ToString()+"/css/default.css") %>"
        rel="stylesheet" />
    <link type="text/css" href="<%:Url.Content("~/App_Themes/"+ConfigurationManager.AppSettings["IndexTheme"].ToString()+"/css/Site.css") %>"
        rel="stylesheet" />
</head>
<body>
    <script type="text/javascript" src="<%: Url.Content("~/Scripts/jquery-1.7.2.min.js") %>"></script>
    <script type="text/javascript" src="<%: Url.Content("~/Content/js/AjaxLoad.js") %>"></script>
    <script type="text/javascript">
        $(function () {
            ajaxLoad('#News', '<%:Url.Content("~/Home/ArticleList/") %><%=(int)ArticleType.News %>', '<%:Url.Content("~/Content/images/Loding.gif") %>');
            ajaxLoad('#promotion', '<%:Url.Content("~/Home/ArticleList/") %><%=(int)ArticleType.promotion %>', '<%:Url.Content("~/Content/images/Loding.gif") %>');
            ajaxLoad('#Hot', '<%:Url.Content("~/Home/ArticleList/") %><%=(int)ArticleType.Hot %>', '<%:Url.Content("~/Content/images/Loding.gif") %>');
            ajaxLoad('#Help', '<%:Url.Content("~/Home/ArticleList/") %><%=(int)ArticleType.Help %>', '<%:Url.Content("~/Content/images/Loding.gif") %>');
            ajaxLoad('#merchandiseBox', '<%:Url.Content("~/Home/ProductList/") %><%=(int)ProductFilter.NotSet %>?_count=6', '<%:Url.Content("~/Content/images/Loding.gif") %>');
            ajaxLoad('#linkList', '<%:Url.Content("~/Home/LinkList") %>', '<%:Url.Content("~/Content/images/Loding.gif") %>');

            $("#btnSubmit").bind("click", function (event) {
                var _return = "";
                if ($("#SearchText").val() == "") {
                    _return += '请输入查询内容！';
                }
                if (_return != "") {
                    alert(_return);
                    _return = "";
                    return false;
                }
            });
        });
    </script>
    <center>
        <%--page--%>
        <div class="page">
            <%--头部--%>
            <div class="page-head">
                <%--  导航--%>
                <div class="page-head-top">
                    <img class="page-head-top-logo" src="../../Content/images/logo.gif" alt="<%:ConfigurationManager.AppSettings["webTitle"].ToString()%>"
                        width="280" height="90" />
                    <%:Html.MvcSiteMap().Menu("MenuHelperModelForHomePage")%>
                </div>
                <%--大图--%>
                <div>
                    <object id="page-head-top-flash" data="<%: Url.Content("~/Content/flash/title.swf") %>"
                        type="application/x-shockwave-flash" width="1000" height="350">
                        <param name="menu" value="false">
                        <param name="movie" value="<%: Url.Content("~/Content/flash/title.swf") %>" />
                        <embed src="<%: Url.Content("~/Content/flash/title.swf") %>" type="application/x-shockwave-flash"></embed>
                    </object>
                    <%--                    <object id="page-head-top-flash" data="<%: Url.Content("~/Content/flash/flashAD.swf") %>?xml=<%: Url.Content("~/Common/GetFlashADXML/"+((int)AdvertisementType.HomePageMasterTitle).ToString()) %>"
                        type="application/x-shockwave-flash" width="1000" height="350">
                        <param name="menu" value="false">
                        <param name="movie" value="<%: Url.Content("~/Content/flash/flashAD.swf") %>?xml=<%: Url.Content("~/Common/GetFlashADXML/"+((int)AdvertisementType.HomePageMasterTitle).ToString()) %>" />
                        <embed src="<%: Url.Content("~/Content/flash/flashAD.swf") %>?xml=<%: Url.Content("~/Common/GetFlashADXML/"+((int)AdvertisementType.HomePageMasterTitle).ToString()) %>"
                            type="application/x-shockwave-flash"></embed>
                    </object>--%>
                </div>
            </div>
            <%--新闻与查询--%>
            <div class="page-newandsearch">
                <div class="page-newandsearch-bulletin">
                    <h3>
                        <%=Html.ActionLink("最新公告：", "Index", "Home", new { @id = (int)ArticleType.Bulletin, @area = "Article" }, new { @class = "tblack", @target = "_blank" })%>
                    </h3>
                    <%Html.RenderAction("Bulletin",18); %>
                </div>
                <div class="page-newandsearch-new">
                    <div class="page-newandsearch-new-new">
                        <div>
                            <h4 class="w120 fl">
                                新闻播报</h4>
                            <%=Html.ActionLink("更多>>>", "Index", "Home", new { @id = (int)ArticleType.News,@area="Article" }, new { @class = "more fr",@target="_blank" })%>
                        </div>
                        <div id="News">
                        </div>
                    </div>
                    <div class="page-newandsearch-new-guide">
                        <div>
                            <h4 class="w120 fl">
                                优惠指南</h4>
                            <%=Html.ActionLink("更多>>>", "Index", "Home", new { @id = (int)ArticleType.promotion,@area="Article"  }, new { @class = "more fr",@target="_blank" })%>
                        </div>
                        <div id="promotion">
                        </div>
                    </div>
                </div>
                <div class="page-newandsearch-search">
                    <h4>
                        站内信息查询</h4>
                    <ul>
                        <li class="list-dot">请输入您想查询的关键字</li>
                        <li class="list-dot">支持多关键字模糊查询</li>
                        <li class="list-dot">多个关键字以空格分开</li>
                    </ul>
                    <div>
                        <% using (Html.BeginForm("Index", "Search", new { @area = "Article" }, FormMethod.Post, null))
                           { %>
                        <%:Html.TextBox("SearchText", "", new { @class = "SearchText", @id = "SearchText" })%>
                        <input type="submit" id="btnSubmit" class="searchSubmit" value="   " />
                        <% }%>
                    </div>
                </div>
            </div>
            <%--中部--%>
            <div class="middle">
                <%--产品介绍与网上服务--%>
                <div class="middle-left">
                    <%--产品介绍--%>
                    <div class="middle-left-merchandise">
                        <div class="middle-left-merchandise-title">
                            <h4 class=" fl">
                                产品介绍</h4>
                            <%=Html.ActionLink("更多>>>", "Index", "Home", new { @area="Product"}, new { @class = "more fr",@target="_blank" })%>
                        </div>
                        <div id="merchandiseBox">
                        </div>
                    </div>
                    <hr />
                    <%--网上服务--%>
                    <div class="middle-left-service">
                        <div class="middle-left-service-title">
                            <h4 class=" fl">
                                网上服务</h4>
                            <a class="more fr" href="#" title="更多">更多>>></a>
                        </div>
                        <div class="service">
                            <div>
                                <img src="../../Content/images/zxcz.png" alt="IC卡在线充值服务" />
                                <h6>
                                    <%:Html.ActionLink("IC卡在线充值服务", "OnlyPage", "Home", new { @id = 9, @area = "Article" }, new { @title = "IC卡在线充值服务", @class = "tblack" })%>
                                </h6>
                            </div>
                            <div class="service-info">
                                本站预计在不久的将来提供交通IC卡在线充值功能。届时您将可以通过本站进行IC卡在线……</div>
                        </div>
                        <div class="service">
                            <div>
                                <img src="../../Content/images/yecx.png" alt="余额查询服务" />
                                <h6>
                                    <%:Html.ActionLink("余额查询服务", "OnlyPage", "Home", new { @id = 10, @area = "Article" }, new { @title = "余额查询服务", @class = "tblack" })%>
                                </h6>
                            </div>
                            <div class="service-info">
                                本站预计在不久的将来提供交通IC卡在线余额查询功能。届时您将可以通过本站进行在线余额……
                            </div>
                        </div>
                        <div class="service">
                            <div>
                                <img src="../../Content/images/tsdz.png" alt="特色定制服务" />
                                <h6>
                                    <%:Html.ActionLink("特色定制服务", "OnlyPage", "Home", new { @id = 11, @area = "Article" }, new { @title = "特色定制服务", @class = "tblack" })%></h6>
                            </div>
                            <div class="service-info">
                                还在为看遍了千篇一律的交通卡而觉的视觉疲劳吗？现在您终于可以摆脱这一困扰……
                            </div>
                        </div>
                    </div>
                </div>
                <%--中部右侧--%>
                <div class="middle-right">
                    <%--     热门资讯--%>
                    <div>
                        <div class="line-height32">
                            <h4 class=" fl">
                                热点资讯</h4>
                        </div>
                        <object id="flashAD" data="<%: Url.Content("~/Content/flash/flashAD.swf") %>?xml=<%: Url.Content("~/Common/GetFlashADXML/"+((int)AdvertisementType.HomePageMaster).ToString()) %>"
                            type="application/x-shockwave-flash" width="245" height="150">
                            <param name="menu" value="false">
                            <param name="movie" value="<%: Url.Content("~/Content/flash/flashAD.swf") %>?xml=<%: Url.Content("~/Common/GetFlashADXML/"+((int)AdvertisementType.HomePageMaster).ToString()) %>" />
                            <embed src="<%: Url.Content("~/Content/flash/flashAD.swf") %>?xml=<%: Url.Content("~/Common/GetFlashADXML/"+((int)AdvertisementType.HomePageMaster).ToString()) %>"
                                type="application/x-shockwave-flash"></embed>
                        </object>
                    </div>
                    <%--  热点排行--%>
                    <div class="middle-right-hot">
                        <div class="line-height32">
                            <h4 class=" fl">
                                热点排行</h4>
                            <%=Html.ActionLink("更多>>>", "Index", "Home", new { @id = (int)ArticleType.Hot,@area="Article"  }, new { @class = "more fr",@target="_blank" })%>
                        </div>
                        <div id="Hot">
                        </div>
                    </div>
                    <%--968856--%>
                    <div>
                        <a href="#" title="968856客服热线">
                            <img src="../../Content/images/968856.png" alt="968856" />
                        </a>
                    </div>
                    <div>
                        <img class="fl" width="60" height="60" src="../../Content/images/text.gif" alt="在线留言" />
                        <h6 class="fl line-height32">
                            在线留言</h6>
                        <br />
                        <br />
                        <br />
                        <a href="http://weibo.com/fjqzgj" title="点击留言" class="fr tblue" target="_blank">我有话要说>>></a>
                    </div>
                    <div class="tright">
                        <img class="fl" width="60" height="60" src="../../Content/images/znz.gif" alt="在线留言" />
                        <h6 class="fl line-height32">
                            乘车指南</h6>
                        <br />
                        <br />
                        <a href="http://wap.bus.qzjtjt.com" title="公交线路查询" class="tblue" target="_blank">公交线路查询>>></a><br />
                        <a href="http://www.mqqy.com" title="客运线路班次查询" class="tblue" target="_blank">客运线路班次查询>>></a>
                    </div>
                </div>
            </div>
            <%--底部--%>
            <div class="bottom">
                <div>
                    <%--友情链接--%>
                    <div class="bottom-left">
                        <div id="linkList">
                        </div>
                    </div>
                    <%--站点帮助--%>
                    <div class="bottom-right">
                        <div class="line-height32">
                            <h4 class=" fl">
                                站点帮助</h4>
                            <%=Html.ActionLink("更多>>>", "Index", "Home", new { @id = (int)ArticleType.Help,@area="Article"  }, new { @class = "more fr",@target="_blank" })%>
                        </div>
                        <div id="Help">
                        </div>
                    </div>
                </div>
            </div>
            <%--版权信息--%>
            <% Html.RenderAction("CopyRight", "Common", new { @area = "" }); %>
        </div>
    </center>
</body>
</html>
