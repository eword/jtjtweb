<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%@ Import Namespace="eulei.shop.Models" %>
<%@ Import Namespace="eulei.shop.Code" %>
<script type="text/javascript" src="<%: Url.Content("~/Libs/ligerUI/js/core/base.js") %>"></script>
<script type="text/javascript" src="<%: Url.Content("~/Libs/ligerUI/js/ligerui.min.js") %>"></script>
<script type="text/javascript" src="<%: Url.Content("~/Libs/ligerUI/js/plugins/ligerTree.js") %>"></script>

<%--<script type="text/javascript">
    $(function () {
        $("#tree1").ligerTree({
            url: '<%: Url.Content("~/Common/GetJsonForArticleTypeID") %>?time=' + Math.random(),
            checkbox: false,
            textFieldName: "textcontent",
            idFieldName: "id",
            parentIDFieldName: "fid",
            onClick: onClicktree1
        });
        $("#tree2").ligerTree({
            data: [
{ text: 'IC卡在线充值服务', url: '<%:Url.Content("~/Article/Home/OnlyPage/9") %>' },
{ text: '余额查询服务', url: '<%:Url.Content("~/Article/Home/OnlyPage/10") %>' },
{ text: '特色定制服务', url: '<%:Url.Content("~/Article/Home/OnlyPage/11") %>' }
            ],
            checkbox: false,
            onClick: onClicktree2
        });
        function onClicktree2(note) {
            var sFeatures = "";
            window.open(note.data.url, '3km', sFeatures);

        }

        function onClicktree(note) {
            var sFeatures = "";
            window.open('<%:Url.Content("~/Product/Class/Index/") %>' + note.data.id, '3km', sFeatures);

        }
        function onClicktree1(note) {
            var sFeatures = "";
            window.open('<%:Url.Content("~/Article/Class/Index/") %>' + note.data.id, '3km', sFeatures);

        }

    });
</script>
--%>
<div id="accordion">
    <h3>线上服务</h3>
    <div>
        <ul>
            <li class="servers">

                <img src="/Content/images/phonesearch.png" alt="掌上公交" />
                <h6>
                    <a href="http://wap.bus.qzjtjt.com" title="掌上公交" class="tblack" target="_blank">掌上公交</a>
                </h6>
            </li>
            <li class="servers">
                <img src="/Content/images/zxcz.png" alt="银联网上购票" />
                <h6>
                    <a href="http://www.mqqy.com" title="银联网上购票" class="tblack"  target="_blank">银联网上购票</a>
                </h6>

            </li>

   



            <li class="servers">
                <img src="/Content/images/taxiserver.png" alt="出租车电招" />
                <h6>
                    <%:Html.ActionLink("出租车电招", "OnlyPage", "Home", new { @id = 6, @area = "Article" }, new { @title = "出租车电招", @class = "tblack" ,@target="_blank"})%></h6>

            </li>

            <li class="servers">
                <img src="/Content/images/phone.png" alt="12580电话购票" />
                <h6>
                    <%:Html.ActionLink("12580电话购票", "OnlyPage", "Home", new { @id = 17, @area = "Article" }, new { @title = "12580电话购票", @class = "tblack",@target="_blank" })%>
                </h6>
            </li>


        </ul>
    </div>
        <h3>线下服务</h3>
    <div class="treebox">
        <ul>
           

            <li class="servers">
                <img src="/Content/images/linkticket.png" alt="异地联网购票" />
                <h6>
                    <%:Html.ActionLink("异地联网购票", "OnlyPage", "Home", new { @id = 14, @area = "Article" }, new { @title = "异地联网购票", @class = "tblack",@target="_blank" })%></h6>

            </li>

            <li class="servers">
                <img src="/Content/images/card.png" alt="IC卡购票" />
                <h6>
                    <%:Html.ActionLink("IC卡购票", "OnlyPage", "Home", new { @id = 19, @area = "Article" }, new { @title = "IC卡购票", @class = "tblack",@target="_blank" })%></h6>

            </li>
            

            <li class="servers">

                <img src="/Content/images/selfhelp.png" alt="自助购票" />
                <h6>
                    <%:Html.ActionLink("自助购票", "OnlyPage", "Home", new { @id = 18, @area = "Article" }, new { @title = "自助购票", @class = "tblack",@target="_blank" })%></h6>
            </li>
        </ul>
    </div>
</div>

