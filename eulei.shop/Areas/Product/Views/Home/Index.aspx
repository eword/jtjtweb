<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Product/Views/Shared/Product_Areas.Master"
    Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
 商城首页
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="headOtherInfo" runat="server">
    <link type="text/css" href="<%:Url.Content("~/App_Themes/"+ConfigurationManager.AppSettings["ProductTheme"].ToString()+"/css/general.css") %>"
        rel="stylesheet" />
    <link type="text/css" href="<%:Url.Content("~/App_Themes/"+ConfigurationManager.AppSettings["ProductTheme"].ToString()+"/css/master.css") %>"
        rel="stylesheet" />
    <link type="text/css" href="<%:Url.Content("~/App_Themes/"+ConfigurationManager.AppSettings["ProductTheme"].ToString()+"/css/product.css") %>"
        rel="stylesheet" />
                    <link type="text/css" href="<%:Url.Content("~/Libs/ligerUI/skins/Aqua/css/ligerui-all.css") %>"
        rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="LeftContent" runat="server">
      <%Html.RenderAction("Index", "ContentNav", new { @id =ViewBag.NavID,@area="Product" }); %>  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        $.ajax({
            type: "get",
            url: '<%: Url.Content("~/Product/Home/ProductList/1") %>',
            cache: false,
            async: true,
            dataType: "html",
            contentType: 'text/html;charset=utf-8', //编码格式 
            beforeSend: function (data) {
                $("#Hot-productList").html('');
                $("#Hot-productList").append('<img alt=\"列表加载中……\" width=\"100\" height=\"100\"  src=\"<%: Url.Content("~/Content/images/Loding.gif") %>\" ');
            }, //发送请求前
            success: function (Htmlobj) {
                $("#Hot-productList").html(Htmlobj);
            },
            error: function (data) {
                $('#Hot-productList').html('failed to load data.')
            } //请求错误 
        });

        $.ajax({
            type: "get",
            url: '<%: Url.Content("~/Product/Home/ProductList/2") %>',
            cache: false,
            async: true,
            dataType: "html",
            contentType: 'text/html;charset=utf-8', //编码格式 
            beforeSend: function (data) {
                $("#New-productList").html('');
                $("#New-productList").append('<img alt=\"列表加载中……\" width=\"100\" height=\"100\"  src=\"<%: Url.Content("~/Content/images/Loding.gif") %>\" ');
            }, //发送请求前
            success: function (Htmlobj) {
                $("#New-productList").html(Htmlobj);
            },
            error: function (data) {
                $('#New-productList').html('failed to load data.')
            } //请求错误 
        });
    </script>
    <div class="ui-box">
        <div class="ui-box-title">
            <h3>
                热销上市</h3>
        </div>
        <div class="ui-box-content" id="Hot-productList">
        </div>
    </div>
    <div class="ui-box tmargin">
        <div class="ui-box-title">
            <h3>
                新品商品</h3>
        </div>
        <div class="ui-box-content" id="New-productList">
        </div>
    </div>
</asp:Content>
