<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<eulei.shop.Models.TB_ProductType>" %>
<script src="<%: Url.Content("~/Scripts/jquery-1.4.4.min.js") %>" type="text/javascript"></script>
<script src="<%: Url.Content("~/Scripts/jquery.validate.min.js") %>" type="text/javascript"></script>
<script src="<%: Url.Content("~/Scripts/jquery.validate.unobtrusive.min.js") %>"
    type="text/javascript"></script>
<% using (Html.BeginForm("ProductTypeCreateChild", "Class", new { @area = "manage" }, FormMethod.Post, null))
   { %>
<fieldset>
    <legend>添加
        <%=ViewBag.CurrentNodeTitle%></legend>
    <table width="100%" bgcolor="#b5d6e6" border="0" cellpadding="0" cellspacing="0">
        <tr class="tbg">
            <td class="tc w180">
                类目
            </td>
            <td class="tc ">
                信息
            </td>
        </tr>
        <tr class="lbg">
            <td>
                ID号：
            </td>
            <td>
                <%:Model.ProductTypeID %>
            </td>
        </tr>
        <tr class="lbgalt">
            <td>
                类型名称
            </td>
            <td>
                <%: Html.EditorFor(model => model.ProductTypeName) %>
            </td>
        </tr>
        <tr class="lbg">
            <td>
                菜单等级（整站中的等级）：
            </td>
            <td>
                <%:Model.ProductTypeLevel %>
            </td>
        </tr>
        <tr class="lbgalt">
            <td>
                菜单顺序：
            </td>
            <td>
                <%: Html.EditorFor(model => model.ProductTypeOrder) %>
            </td>
        </tr>
        <tr class="lbg">
            <td colspan="2">
                <%:Html.CheckBoxFor(m=>m.ProductTypeIsLock) %>锁定选项
            </td>
        </tr>
    </table>
    <%: Html.HiddenFor(model => model.ProductTypeID) %>
    <%: Html.HiddenFor(model => model.ProductTypeParentID) %>
    <%: Html.HiddenFor(model => model.ProductTypeLevel) %>
    <p>
        <input type="submit" value="保存" />
    </p>
</fieldset>
<% } %>
