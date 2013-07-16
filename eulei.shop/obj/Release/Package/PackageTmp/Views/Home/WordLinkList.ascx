<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<eulei.shop.Models.TB_Link>>" %>
<ul style=" padding-top:15px;">
<%
    int _i = 0;
    foreach (var item in Model)
   { %><li style=" width:120px; float:left; list-style-type:none; margin-bottom:5px;">
    <a href="<%:item.LinkUrl %>" title="<%:item.LinkTitle %>" target="_blank" class="tblack"><h6 class="fl">
        
            
            <%:item.LinkTitle %>
       
    </h6> </a>
</li>
<% 
       _i++;
    } %>
</ul>