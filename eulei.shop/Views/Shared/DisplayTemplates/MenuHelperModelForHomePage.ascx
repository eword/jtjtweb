<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl`1[[MvcSiteMapProvider.Web.Html.Models.MenuHelperModel,MvcSiteMapProvider]]" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="MvcSiteMapProvider.Web.Html.Models" %>
<%@ Import Namespace="eulei.shop.Code" %>
<script type="text/javascript">
<!--
    $(document).ready(function () {
        $('#menu li.node').hover(
			function () { $('div.nodeBox', this).css('display', 'block'); },
			function () { $('div.nodeBox', this).css('display', 'none'); });
    });
// -->
</script>
<ul id="menu">
    <% 
        int _i = 0;
        foreach (var node in Model.Nodes)
       {
          
            %>
       <% if (node.Title.Equals("人才招聘")) { _i++; continue; }%>
    <li class="node">
        <% if (node.IsInCurrentPath)
           {%>
        <a class="node click" href="<%=node.Url %>" >
            <%=node.Title%></a>
        <%}
           else
           { %>
        <a class="node" href="<%=node.Url %>">
            <%=node.Title%></a>
        <%} %>
        <% if (node.Children.Any())
           { %>
        <div class="nodeBox" style="  left:<%=node.Children.Count>(Model.Nodes.Count-_i-1-1)?((Model.Nodes.Count-_i-2)-node.Children.Count)*110:0%>px; top: 25px; width: <%=node.Children.Count*110 %>px;">
            <div class="nodeBox-title"  style=" background-position: <%=node.Children.Count>(Model.Nodes.Count-_i-2)?-((Model.Nodes.Count-_i-2)-node.Children.Count)*110+40:40%>px bottom;">
            </div>
            <ul class="nodeBox-box"  style="width: <%=node.Children.Count*110 %>px;">
            <%int _j=0;
              foreach (var nodeNum in node.Children)
              {
                  _j = _j > nodeNum.Children.Count ? _j : nodeNum.Children.Count;
              }
                 %>
                <% foreach (var node1 in node.Children)
                   { %>
                <li class="node1" style=" height:<%=_j*30+40%>px; "><a class="node1" href="<%=node1.Url %>">
                    <%=node1.Title %>
                </a>
                    <% if (node1.Children.Any())
                       { %>
                    <ul>
                        <%foreach (var node2 in node1.Children)
                          { %>
                        <li class="node2"><a class="node2" href="<%=node2.Url %>">
                            <%=node2.Title %>
                        </a></li>
                        <%} %>
                    </ul>
                    <%} %>
                </li>
                <%} %>
            </ul>
        </div>
        <% } %>
    </li>
    <%
           _i++;
        } %>
</ul>