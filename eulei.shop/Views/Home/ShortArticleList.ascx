<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<eulei.shop.Models.TV_Article>>" %>
<table  class="tablelist"  border="0" cellpadding="0" cellspacing="0"  width="100%"><% foreach (var item in Model)
   { %>
<tr class="trlist line-height16 ">
<td class="overflowHidden list-dot tleft tdtitle border-bottom">
 <%:Html.ActionLink(item.ArticleTitle,"Index", "Home",new{@id=item.ArticleTypeID,@aid=item.ArticleID,@Area="Article"},new{ target="_blank",@title=item.ArticleTitle,@class=item.ArticleIsemp?"tred":""}) %>
</td>
</tr><% } %>
</table>
