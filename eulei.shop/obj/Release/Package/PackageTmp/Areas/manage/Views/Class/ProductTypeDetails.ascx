<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<eulei.shop.Models.TB_ProductType>" %>
<fieldset>
    <legend>选项信息</legend>
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
                <%:Model.ProductTypeName %>
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
                <%:Model.ProductTypeOrder %>
            </td>
        </tr><tr class="lbg">
            <td>
               锁定选项：
            </td>
            <td>
               <%=Model.ProductTypeIsLock?"是":"否" %>
            </td>
        </tr>
    </table>
</fieldset>
