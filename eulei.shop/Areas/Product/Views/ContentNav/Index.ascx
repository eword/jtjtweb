<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%@ Import Namespace="eulei.shop.Models" %>
<%@ Import Namespace="eulei.shop.Code" %>
 <script type="text/javascript" src="<%: Url.Content("~/Libs/ligerUI/js/core/base.js") %>"></script>
<script type="text/javascript" src="<%: Url.Content("~/Libs/ligerUI/js/ligerui.min.js") %>"></script>
<script type="text/javascript" src="<%: Url.Content("~/Libs/ligerUI/js/plugins/ligerTree.js") %>"></script>
<script type="text/javascript">
    $(function () {
        $("#tree").ligerTree({
            url: '<%: Url.Content("~/Common/GetJsonForMerchandiseTypeID") %>?time=' + Math.random(),
            checkbox: false,
            textFieldName: "textcontent",
            idFieldName: "id",
            parentIDFieldName: "fid",
            onClick: onClicktree
        });
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
<div id="accordion">
    <h3>
        网上服务</h3>
    <div class="treebox">
        <ul id="tree2">
        </ul>
    </div>
    <h3>
        商品类型</h3>
    <div class="treebox">
        <div>
            <ul id="tree">
            </ul>
        </div>
    </div>
    <h3>
        站点咨询</h3>
    <div class="treebox">
        <ul id="tree1">
        </ul>
    </div>
</div>
