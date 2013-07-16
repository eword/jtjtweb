<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<IEnumerable<eulei.shop.Models.SA_ArticleOperationLog>>" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>GetArticleOperationgRecord</title>
    <style>
        table {
            width: 100%;
        }

        td, th {
            border: 1px solid #CCCCCC;
        }
    </style>
</head>
<body>
    <table>
        <tr>
            <th>记录ID
            </th>
            <th>文章ID
            </th>
            <th>操作时间
            </th>
            <th>操作描述
            </th>
            <th>账号
            </th>
            <th>姓名
            </th>
            <th>步骤名
            </th>
        </tr>

        <% foreach (var item in Model)
           { %>
        <tr>
            <td>
                <%: Html.DisplayFor(modelItem => item.OperationLogID) %>
            </td>
            <td>
                <%: Html.DisplayFor(modelItem => item.OperationLogArticleID) %>
            </td>
            <td>
                <%: Html.DisplayFor(modelItem => item.OperationLogOperateTime) %>
            </td>
            <td>
                <%: Html.DisplayFor(modelItem => item.OperationLogContent) %>
            </td>
            <td>
                <%: Html.DisplayFor(modelItem => item.OperationLogOperaterUserName) %>
            </td>
            <td>
                <%: Html.DisplayFor(modelItem => item.OperationLogOperaterFriendlyName) %>
            </td>
            <td>
                <%: Html.DisplayFor(modelItem => item.OperationLogOperateDesp) %>
            </td>
        </tr>
        <% } %>
    </table>
</body>
</html>
