<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%@ Import Namespace="eulei.shop.Code" %>
<%--版权信息--%>
<div class="copyright ">
    <div class="copyright-first">
        <ul class=" border-none">
            <li>
                <%:Html.ActionLink("关于交通IC卡", "Index", "OnlyPage", new { @id = 8, @area = "Article" }, new { @title = "关于交通IC卡" })%></li>
            <li class="divide"><a href="http://www.qzjtjt.com" title="xxxx">官方网站</a></li>
            <li class="divide">
                <%:Html.ActionLink("免责声明", "Index", "Home", new { @id = 121, @area = "Article" }, new { @title = "免责声明" })%>
            </li>
            <li class="divide">
                <%:Html.ActionLink("联系我们", "Index", "OnlyPage", new { @id = 7, @area = "Article" }, new { @title = "联系我们" })%>
            </li>
            <li class="divide"><a href="#" title="About qzjtjt.com">About qzjtjt.com</a></li>
            <li class="divide "><%:ConfigurationManager.AppSettings["webTitle"].ToString()%>版权所有 CopyRight©2012</li>
            <li class="divide"><a href="http://www.miibeian.gov.cn/" title="闽ICP备11005906号">闽ICP备11005906号</a></li>
        </ul>
    </div>
    <div>
        地址：福建省泉州市丰泽区丰泽街公交大厦10楼 邮编：362000 <strong>访问量：<%=Application["CustomerCount"].ToString() %></strong>
    </div>
    <div style="display: none;">
<script type="text/javascript">
    var _bdhmProtocol = (("https:" == document.location.protocol) ? " https://" : " http://");
    document.write(unescape("%3Cscript src='" + _bdhmProtocol + "hm.baidu.com/h.js%3Fddd0b7673b12883210403c1f4d6dd2b0' type='text/javascript'%3E%3C/script%3E"));
</script>
    </div>
</div>


