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
        <link type="text/css" href="<%:Url.Content("~/App_Themes/"+ConfigurationManager.AppSettings["HolidayTheme"].ToString()+"/css/general.css") %>"
        rel="stylesheet" />
</head>
<body>
    <script type="text/javascript" src="<%: Url.Content("~/Scripts/jquery-1.7.2.min.js") %>"></script>
    <script type="text/javascript" src="<%: Url.Content("~/Content/js/AjaxLoad.js") %>"></script>
    <script type="text/javascript">
        $(function () {

            //<!--
            tmpDate = new Date();
            date = tmpDate.getDate();
            month = tmpDate.getMonth() + 1;
            year = tmpDate.getYear();
            var dateTime = "<strong>[" + year + "年" + month + "月" + date + "日";
            myArray = new Array(6);
            myArray[0] = "星期日"
            myArray[1] = "星期一"
            myArray[2] = "星期二"
            myArray[3] = "星期三"
            myArray[4] = "星期四"
            myArray[5] = "星期五"
            myArray[6] = "星期六"
            weekday = tmpDate.getDay();
            if (weekday == 0 | weekday == 6) {
                dateTime += "" + myArray[weekday] + "]</strong>";
            }
            else {
                dateTime += "" + myArray[weekday] + "]</strong>";
            };
            // -->
            $("#dateTime").html(dateTime);
            //动态加载新闻
            ajaxLoad('#siteNew', '<%:Url.Content("~/Home/ArticleList/15?count=7") %>&type=New', '<%:Url.Content("~/Content/images/Loding.gif") %>');
            //动态加载公告通告
            //ajaxLoad('#siteBulletin', '<%:Url.Content("~/Home/ArticleList/18?count=5") %>', '<%:Url.Content("~/Content/images/Loding.gif") %>');
            //动态加载政策法规
            ajaxLoad('#statuteInfo', '<%:Url.Content("~/Home/ShortArticleList/13,72,77,82,87,96,101,106,111?count=8") %>', '<%:Url.Content("~/Content/images/Loding.gif") %>');
            //动态加载党务工作
            ajaxLoad('#sitePartyInfo', '<%:Url.Content("~/Home/ArticleList/49?count=5") %>', '<%:Url.Content("~/Content/images/Loding.gif") %>');
            //动态加载厂务工作
            ajaxLoad('#siteFactoryInfo', '<%:Url.Content("~/Home/ArticleList/70?count=5") %>', '<%:Url.Content("~/Content/images/Loding.gif") %>');
            //动态加载友情链接
            ajaxLoad('#linkList', '<%:Url.Content("~/Home/LinkList") %>', '<%:Url.Content("~/Content/images/Loding.gif") %>');
            //动态加载友情链接
            ajaxLoad('#wordLinkList', '<%:Url.Content("~/Home/WordLinkList") %>', '<%:Url.Content("~/Content/images/Loding.gif") %>');

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
        <%--  导航--%>
        <div>
        <div class="page-head-topbg">
            <div class="page-head-top">
                <object id="logo" class="page-head-top-logo" data="<%: Url.Content("~/Content/flash/logo.swf") %>"
                    type="application/x-shockwave-flash" width="190" height="78">
                    <param name="menu" value="false" />
                    <param name="wmode" value="transparent" />
                    <param name="movie" value="<%: Url.Content("~/Content/flash/logo.swf") %>" />
                    <embed src="<%: Url.Content("~/Content/flash/logo.swf") %>" type="application/x-shockwave-flash"
                        wmode="transparent"></embed>
                </object>                
            </div>
        </div>
        <div class="festivalbg">
            <div class="festivalbg-top">
                <script type="text/javascript">
<!--
                    $(document).ready(function () {
                        $('.mbt_close').bind("click", function (event) {
                            $('.festivalbg').css('background-image', 'none');
                            $('body').css('background-color', '#CCFFFF');
                            $('.festivalbg-top').css('display', 'none');
                            $('.festivalbg-left').css('display', 'none');
                            $('.festivalbg-right').css('display', 'none');
                        });
                    });
            // -->
                </script>
                <a class="mbt_close" href="javascript:void()" title="点击关闭节日背景">Ⅹ</a>
            </div>
            <div class="page">
                <%--头部--%>
                <div class="page-head">
                    <div class="festivalbg-left">
                    </div>
                    <div class="festivalbg-right">
                    </div>
                    <%:Html.MvcSiteMap().Menu("MenuHelperModelForHomePage")%>
                    <%--大图--%>
                    <div>
                        <object id="page-head-top-flash" data="<%: Url.Content("~/Content/flash/title.swf") %>"
                            type="application/x-shockwave-flash" width="1000" height="250">
                            <param name="menu" value="false" />
                            <param name="wmode" value="Opaque" />
                            <param name="movie" value="<%: Url.Content("~/Content/flash/title.swf") %>" />
                            <embed src="<%: Url.Content("~/Content/flash/title.swf") %>" type="application/x-shockwave-flash"
                                wmode="Opaque"></embed>
                        </object>
                    </div>
                </div>
                <%--日期与查询--%>
                <div class="page-search">
                    <div class="page-search-date " id="dateTime">
                    </div>
                    <%:Html.ActionLink("订阅RSS","Index","RSS",null,new{ @area="",@class="rss"})%>
                </div>
                <%--中部--%>
                <div class="middle">
                    <%--产品介绍与网上服务--%>
                    <div class="middle-left">
                        <%--图文资讯与新闻--%>
                        <div class="middle-left-top">
                            <%--     图文资讯--%>
                            <div class="middle-left-picNew fl">
                                <object id="picNew" data="<%: Url.Content("~/Content/flash/flashAD.swf") %>?xml=<%: Url.Content("~/Common/GetFlashADXML/"+((int)AdvertisementType.HomePageMasterTitle).ToString()) %>?XMLPath=PicPlayerConfig"
                                    type="application/x-shockwave-flash" width="300" height="225">
                                    <param name="menu" value="false" />
                                    <param name="movie" value="<%: Url.Content("~/Content/flash/flashAD.swf") %>?xml=<%: Url.Content("~/Common/GetFlashADXML/"+((int)AdvertisementType.HomePageMasterTitle).ToString()) %>?XMLPath=PicPlayerConfig" />
                                    <embed src="<%: Url.Content("~/Content/flash/flashAD.swf") %>?xml=<%: Url.Content("~/Common/GetFlashADXML/"+((int)AdvertisementType.HomePageMasterTitle).ToString()) %>?XMLPath=PicPlayerConfig"
                                        type="application/x-shockwave-flash"></embed>
                                </object>
                            </div>
                            <%--新闻--%>
                            <div class="middle-left-new">
                                <div class="line-height32 title3">
                                    <h4 class="fl lmargin">
                                        最新资讯</h4>
                                    <%=Html.ActionLink("更多>>>", "Index", "Home", new { @id = 15,@area="Article"  }, new { @class = "more fr",@target="_blank" })%>
                                </div>
                                <div id="siteNew">
                                </div>
                            </div>
                        </div>
                        <div class="middle-left-imgAD">
                            <object id="imgAD" data="<%: Url.Content("~/Content/flash/flashAD.swf") %>?xml=<%: Url.Content("~/Common/GetFlashADXML/"+((int)AdvertisementType.HomePageMasterBanner).ToString()) %>?XMLPath=flashADXMLForIndexBanner"
                                type="application/x-shockwave-flash" width="738" height="65">
                                <param name="menu" value="false" />
                                <param name="movie" value="<%: Url.Content("~/Content/flash/flashAD.swf") %>?xml=<%: Url.Content("~/Common/GetFlashADXML/"+((int)AdvertisementType.HomePageMasterBanner).ToString()) %>?XMLPath=flashADXMLForIndexBanner" />
                                <embed src="<%: Url.Content("~/Content/flash/flashAD.swf") %>?xml=<%: Url.Content("~/Common/GetFlashADXML/"+((int)AdvertisementType.HomePageMasterBanner).ToString()) %>?XMLPath=flashADXMLForIndexBanner"
                                    type="application/x-shockwave-flash"></embed>
                            </object>
                        </div>
                        <%--网上服务--%>
                        <div class="middle-left-service">
                            <div class="middle-left-service-title">
                                <h4 class=" fl">
                                    电商服务</h4>
                                <a class="more fr" href="#" title="更多">更多>>></a>
                            </div>
                            <div class="service">
                                <div>
                                    <img src="../../Content/images/linkticket.png" alt="异地联网购票" />
                                    <h6>
                                        <%:Html.ActionLink("异地联网购票", "OnlyPage", "Home", new { @id = 14, @area = "Article" }, new { @title = "异地联网购票", @class = "tblack",@target="_blank" })%></h6>
                                </div>
                                <div class="service-info ">
                                    长久以来，我们总是为了方便您出行而努力，异地联网购票可方便您在我集团所属车站进行异地购票……
                                </div>
                            </div>
                            <div class="service">
                                <div>
                                    <img src="../../Content/images/card.png" alt="IC卡购票" />
                                    <h6>
                                        <%:Html.ActionLink("IC卡购票", "OnlyPage", "Home", new { @id = 19, @area = "Article" }, new { @title = "IC卡购票", @class = "tblack",@target="_blank" })%></h6>
                                </div>
                                <div class="service-info ">
                                    还在为携带现金和找零感到头痛吗？那您就OUT了，我集团所属车站各大车站均配备有交通那个IC卡购票系统……
                                </div>
                            </div>
                            <div class="service">
                                <div>
                                    <img src="../../Content/images/phonesearch.png" alt="掌上公交" />
                                    <h6>
                                        <a href="http://wap.bus.qzjtjt.com" title="掌上公交" class="tblack" target="_blank">掌上公交</a>
                                       <%-- <%:Html.ActionLink("掌上公交", "Index", "Home", new { @id = 11, @area = "Article" }, new { @title = "掌上公交", @class = "tblack" })%>--%>
                                        </h6>
                                </div>
                                <div class="service-info ">
                                    现在您只要用各类上网终端，比如手机登入"http://wap.bus.968856.cn"就可实时掌握公交车运行动态，方便您候车……
                                </div>
                            </div>
                            <div class="service">
                                <div>
                                    <img src="../../Content/images/taxiserver.png" alt="出租车电招" />
                                    <h6>
                                        <%:Html.ActionLink("出租车电招", "OnlyPage", "Home", new { @id = 6, @area = "Article" }, new { @title = "出租车电招", @class = "tblack",@target="_blank" })%></h6>
                                </div>
                                <div class="service-info ">
                                    还在为打不到的士而苦恼吗？现在您只要拿起手机拨打968856选择出租车电招服务，问题便可引刃而解……
                                </div>
                            </div>
                            <div class="service">
                                <div>
                                    <img src="../../Content/images/zxcz.png" alt="银联网上购票" />
                                    <h6>
                                        <a href="http://www.mqqy.com" title="银联网上购票" class="tblack" target="_blank">银联网上购票</a>
                                        <%--<%:Html.ActionLink("银联网上购票", "Index", "Home", new { @id = 9, @area = "Article" }, new { @title = "银联网上购票", @class = "tblack" })%>--%>
                                    </h6>
                                </div>
                                <div class="service-info ">
                                    您足不出户，就可以轻松掌握福建省泉州市汽车运输总公司下属各联网车站的最新的班次动态信息，并在网上及时购买到所需的汽车票……
                                </div>
                            </div>
                            <div class="service">
                                <div>
                                    <img src="../../Content/images/phone.png" alt="12580电话购票" />
                                    <h6>
                                        <%:Html.ActionLink("12580电话购票", "OnlyPage", "Home", new { @id = 17, @area = "Article" }, new { @title = "12580电话购票", @class = "tblack",@target="_blank" })%>
                                    </h6>
                                </div>
                                <div class="service-info ">
                                    通过拨打12580进入网上购票服务，您足不出户，就可以轻松、及时购买到所需的汽车票……
                                </div>
                            </div>
                            <div class="service">
                                <div>
                                    <img src="../../Content/images/selfhelp.png" alt="自助购票" />
                                    <h6>
                                        <%:Html.ActionLink("自助购票", "OnlyPage", "Home", new { @id = 18, @area = "Article" }, new { @title = "自助购票", @class = "tblack",@target="_blank" })%></h6>
                                </div>
                                <div class="service-info ">
                                    还在为冗长购票队伍而怯步吗？现在您可以通过自助购票终端摆脱这一烦恼……
                                </div>
                            </div>
                        </div>
                        <%--组织机构与内容--%>
                        <script type="text/javascript">
<!--
                            $(document).ready(function () {
                                $('.mqqyimg').hover(
			function () { $('.framewordTree').attr('src', '../../Content/images/mqqyimg.png'); },
			function () { $('.framewordTree').attr('src', '../../Content/images/zzjg.png'); });
                                $('.taxiimg').hover(
			function () { $('.framewordTree').attr('src', '../../Content/images/taxiimg.png'); },
			function () { $('.framewordTree').attr('src', '../../Content/images/zzjg.png'); });
                                $('.busimg').hover(
			function () { $('.framewordTree').attr('src', '../../Content/images/busimg.png'); },
			function () { $('.framewordTree').attr('src', '../../Content/images/zzjg.png'); });
                            });
// -->
                        </script>
                        <div class="middle-left-frameword">
                            <div class="frameword-left">
                                <img class="framewordTree" src="../../Content/images/zzjg.png" ismap="ismap" usemap="#zzjg"
                                    alt="组织机构" />
                                <map name="zzjg" id="zzjg">
                                    <area class="mqqyimg" shape="rect" coords="35,100,60,310" href="http://www.mqqy.com"
                                        alt="点击打开：泉州市汽车运输总公司" />
                                    <area class="busimg" shape="rect" coords="100,100,125,330" href="http://www.qzgj.net"
                                        alt="点击打开：泉州市公交发展有限公司" />
                                    <area class="taxiimg" shape="rect" coords="165,100,190,310" href="http://taxi.qzjtjt.com"
                                        alt="点击打开：泉州市出租车有限公司" />
                                </map>
                            </div>
                            <div class="frameword-right">
                                <div class="frameword-right-content">
                                    <div class="line-height32 title6">
                                        <h4 class=" fl lmargin20 ">
                                            党务工作</h4>
                                        <%=Html.ActionLink("更多>>>", "Index", "Home", new { @id = 49,@area="Article"  }, new { @class = "more fr",@target="_blank" })%>
                                    </div>
                                    <div class="box6">
                                        <div id="sitePartyInfo">
                                        </div>
                                    </div>
                                </div>
                                <div class="frameword-right-content">
                                    <div class="line-height32 title6">
                                        <h4 class=" fl lmargin20">
                                            厂务工作</h4>
                                        <%=Html.ActionLink("更多>>>", "Index", "Home", new { @id = 70,@area="Article"  }, new { @class = "more fr",@target="_blank" })%>
                                    </div>
                                    <div class="box6">
                                        <div id="siteFactoryInfo">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <%--中部右侧--%>
                    <div class="middle-right">
                        <%--通告公告--%>
                        <div class="middle-right-bulletin">
                            <div class="line-height40 title1">
                                <h4 class=" fl lmargin40 twhite">
                                    通告公告</h4>
                                <%=Html.ActionLink("更多>>>", "Index", "Home", new { @id = 18, @area = "Article" }, new { @class = "more1 fr title1-right rpadding5", @target = "_blank" })%>
                            </div>
                            <div class="box1">
                                <%Html.RenderAction("Bulletin", new { @id = 18 }); %>
                            </div>
                        </div>
                        <%--     热门资讯--%>
                        <div>
                            <object id="flashAD" data="<%: Url.Content("~/Content/flash/flashAD.swf") %>?xml=<%: Url.Content("~/Common/GetFlashADXML/"+((int)AdvertisementType.HomePageMaster).ToString()) %>?XMLPath=PicPlayerConfig"
                                type="application/x-shockwave-flash" width="245" height="150">
                                <param name="menu" value="false" />
                                <param name="movie" value="<%: Url.Content("~/Content/flash/flashAD.swf") %>?xml=<%: Url.Content("~/Common/GetFlashADXML/"+((int)AdvertisementType.HomePageMaster).ToString()) %>?XMLPath=PicPlayerConfig" />
                                <embed src="<%: Url.Content("~/Content/flash/flashAD.swf") %>?xml=<%: Url.Content("~/Common/GetFlashADXML/"+((int)AdvertisementType.HomePageMaster).ToString()) %>?XMLPath=PicPlayerConfig"
                                    type="application/x-shockwave-flash"></embed>
                            </object>
                            <%--968856--%>
                            <div class="bmargin5">
                                <a href="#" title="968856客服热线">
                                    <img src="../../Content/images/968856.png" alt="968856" />
                                </a>
                            </div>
                            <%--航空机票、旅游组团--%>
                            <script type="text/javascript">
<!--
                                $(document).ready(function () {
                                    $('.hkjp').hover(
			function () { $('#mxlADimg').attr('src', '../../Content/images/mxlADPressfj.png'); },
			function () { $('#mxlADimg').attr('src', '../../Content/images/mxlAD.png'); });
                                    $('.lyzt').hover(
			function () { $('#mxlADimg').attr('src', '../../Content/images/mxlADPressgp.png'); },
			function () { $('#mxlADimg').attr('src', '../../Content/images/mxlAD.png'); });
                                });

// -->
                            </script>
                            <div>
                                <img id="mxlADimg" src="../../Content/images/mxlAD.png" ismap="ismap" usemap="#mxlAD"
                                    alt="闽兴旅行社" />
                                <map name="mxlAD" id="mxlAD">
                                    <area class="hkjp" shape="rect" coords="40,10,160,30" href="http://mxl.qzjtjt.com/b2b/login.jsp"
                                        alt="点击打开：航空机票订购" />
                                    <area class="lyzt" shape="rect" coords="115,50,230,70" href="/Article/Home/Index/36"
                                        alt="点击打开：旅游线路组团" />
                                </map>
                            </div>
                            <%--  行业资讯--%>
                            <div class="middle-right-industryInfo">
                                <div class="line-height32 title2">
                                    <h4 class=" fl lmargin5 ">
                                        政策法规</h4>
                                    <%=Html.ActionLink("更多>>>", "Index", "Home", new { @id = "13,72,77,82,87,96,101,106,111", @area = "Article" }, new { @class = "more fr", @target = "_blank" })%>
                                </div>
                                <div class="box2">
                                    <div id="statuteInfo">
                                    </div>
                                </div>
                            </div>
                            <%-- 服务指南--%>
                            <div class="middle-right-serviceGuide">
                                <div class="line-height32 title5">
                                    <h4 class=" fl lmargin">
                                        服务指南</h4>
                                </div>
                                <div class="box3">
                                    <p>
                                        <a href="http://www.mqqy.com" target="_blank">客运班线查询</a> <a href="http://wap.bus.qzjtjt.com"
                                            target="_blank">公交线路查询</a>
                                    </p>
                                    <p>
                                        <a href="#" target="_blank">电招业务指南</a> <a href="#" target="_blank">包车业务指南</a>
                                    </p>
                                    <p>
                                        <a href="http://card.qzjtjt.com" target="_blank">IC卡业务指南</a> <a href="http://card.qzjtjt.com/Article/Home/OnlyPage/5"
                                            target="_blank">IC卡业务网点</a>
                                    </p>
                                    <p>
                                        <a href="#" target="_blank">维修业务指南</a> <a href="#" target="_blank">物流业务指南</a>
                                    </p>
                                    <p>
                                        <a href="#" target="_blank">售票网点查询</a> <a href="#" target="_blank">旅游业务指南</a>
                                    </p>
                                </div>
                            </div>
                            <%--搜索留言--%>
                            <div class="middle-right-search">
                                <div class="page-search-box">
                                    <% using (Html.BeginForm("Index", "Search", new { @area = "Article" }, FormMethod.Post, null))
                                       { %>
                                    <%:Html.TextBox("SearchText", "", new { @class = "SearchText", @id = "SearchText" })%>
                                    <input type="submit" id="btnSubmit" class="searchSubmit" value="   " />
                                    <% }%>
                                </div>
                                <div>
                                    <img class="fl" width="60" height="60" src="../../Content/images/text.gif" alt="在线留言" />
                                    <h6 class="fl line-height32">
                                        在线留言</h6>
                                    <br />
                                    <br />
                                    <br />
                                    <a href="http://www.mqqy.com/message.aspx" title="点击留言" class="fr tblue" target="_blank">
                                        我有话要说>>></a>
                                </div>
                            </div>
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
                        <%--底部右侧--%>
                        <div class="bottom-right">
                            <div id="wordLinkList">
                            </div>
                        </div>
                    </div>
                </div>
                <%--版权信息--%>
                <% Html.RenderAction("CopyRight", "Common", new
                               {
                                   @area = ""
                               }); %>
            </div>
        </div>
        </div>
    </center>
</body>
</html>
