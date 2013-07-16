<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<eulei.shop.Models.TV_Article>>" %>
<table class="tablelist" border="0" cellpadding="0" cellspacing="0" width="100%">
    <% foreach (var item in Model)
       { %>
    <tr class="trlist line-hight16 ">
        <%if (item.ArticleIsemp)
          { %>
        <td class="overflowHidden list-dot tleft tdtitle border-bottom">
            <%:Html.ActionLink(item.ArticleTitle,"Index", "Home",new{@id=item.ArticleTypeID,@aid=item.ArticleID,@Area="Article"},new{ target="_blank",@title=item.ArticleTitle}) %>
        </td>
        <td class="tdisemp  border-bottom" ></td>
        <%}
          else
          { %>
        <td class="overflowHidden list-dot tleft tdtitle border-bottom" colspan="2">
            <%:Html.ActionLink(item.ArticleTitle,"Index", "Home",new{@id=item.ArticleTypeID,@aid=item.ArticleID,@Area="Article"},new{ target="_blank",@title=item.ArticleTitle}) %>
        </td>
        <%} %>
         <td class="tright tdtime border-bottom">
            <%:String.Format("({0:yyyy/MM/dd})", item.ArticleSendDate)%>
        </td>
    </tr>
    <% } %>
</table>
