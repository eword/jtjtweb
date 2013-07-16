<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/manage/Views/Shared/Manage_Areas.Master"
    Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    商品类型设置
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" src="<%: Url.Content("~/Libs/ligerUI/js/core/base.js") %>"></script>
    <script type="text/javascript" src="<%: Url.Content("~/Libs/ligerUI/js/ligerui.min.js") %>"></script>
    <script type="text/javascript" src="<%: Url.Content("~/Libs/ligerUI/js/plugins/ligerTree.js") %>"></script>
     <script type="text/javascript" src="<%: Url.Content("~/Content/js/AjaxLoad.js") %>"></script>
    <script type="text/javascript">
        $(function () {
            var treemanager = null;
            $("#tree").ligerTree({
                url: '<%: Url.Content("~/Common/GetJsonForMerchandiseTypeID") %>?time=' + Math.random(),
                checkbox: false,
                single: true,
                textFieldName: "textcontent",
                idFieldName: "id",
                parentIDFieldName: "fid"
            });
            treemanager = $("#tree").ligerGetTreeManager();
            $("#edit").hide();
            $("#delete").hide();
            $("#addChild").hide();
            $("#addBrother").hide();
            $("#select").bind("click", function (event) {
                var node = treemanager.getSelected();
                if (node == null) {
                    alert("请选择一个项目");
                    return false;
                }
                 ajaxLoad('#ajaxBox', '<%:Url.Content("~/manage/class/ProductTypeDetails/") %>' + node.data.id, '<%:Url.Content("~/Content/images/Loding.gif") %>');
                 $("#edit").show();
                 $("#delete").show();
                 $("#addChild").show();
                 $("#addBrother").show();

            });
            $("#edit").bind("click", function (event) {
                var node = treemanager.getSelected();
                ajaxLoad('#ajaxBox', '<%:Url.Content("~/manage/class/ProductTypeEdit/") %>' + node.data.id, '<%:Url.Content("~/Content/images/Loding.gif") %>');
                $("#edit").hide();
                $("#delete").hide();
                $("#addChild").hide();
                $("#addBrother").hide();
            });
            $("#addChild").bind("click", function (event) {
                var node = treemanager.getSelected();
                ajaxLoad('#ajaxBox', '<%:Url.Content("~/manage/class/ProductTypeCreateChild/") %>' + node.data.id, '<%:Url.Content("~/Content/images/Loding.gif") %>');
                $("#edit").hide();
                $("#delete").hide();
                $("#addChild").hide();
                $("#addBrother").hide();
            });
            $("#addBrother").bind("click", function (event) {
                var node = treemanager.getSelected();
                ajaxLoad('#ajaxBox', '<%:Url.Content("~/manage/class/ProductTypeCreateBrother/") %>' + node.data.id, '<%:Url.Content("~/Content/images/Loding.gif") %>');
                $("#edit").hide();
                $("#delete").hide();
                $("#addChild").hide();
                $("#addBrother").hide();
            });
            $("#delete").bind("click", function (event) {
                var node = treemanager.getSelected();
                location.href ='<%:Url.Content("~/manage/class/ProductTypeDelete/") %>' + node.data.id

            });
        });      
    </script>
    <br />

    <ul id="tree" class="fl"></ul>
    <div>
    <div class="fl buttonBox">
    <input class="fl lmargin" type="button" id="select" value="获取选择" />
    <input class="fl lmargin" type="button" id="edit" value="编辑选择" />
    <input class="fl lmargin" type="button" id="delete" value="删除选择" />
    <input class="fl lmargin" type="button" id="addChild" value="添加子项" />
    <input  class="fl lmargin"type="button" id="addBrother" value="添加同级项" />
    </div> 
    </div>
    <div id="ajaxBox"></div>
    
    
   
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="headOtherInfo" runat="server">
    <link type="text/css" href="<%:Url.Content("~/Libs/ligerUI/skins/Aqua/css/ligerui-all.css") %>"
        rel="stylesheet" />
    <link type="text/css" href="<%:Url.Content("~/App_Themes/"+ConfigurationManager.AppSettings["ManageTheme"].ToString()+"/css/general.css") %>"
        rel="stylesheet" />
    <link type="text/css" href="<%:Url.Content("~/App_Themes/"+ConfigurationManager.AppSettings["ManageTheme"].ToString()+"/css/master.css") %>"
        rel="stylesheet" />
            <link type="text/css" href="<%:Url.Content("~/App_Themes/"+ConfigurationManager.AppSettings["ManageTheme"].ToString()+"/css/ProductType.css") %>"
        rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="TabTitleContent" runat="server">
    商品类型
</asp:Content>
