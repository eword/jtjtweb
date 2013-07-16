<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<eulei.shop.Models.TV_Article>" %>
<%if (!string.IsNullOrEmpty(Model.ArticleLabel))
{
    string[] sArray = Model.ArticleLabel.Split(new char[] { ',' });
    for (int _i = 0; _i < sArray.Length; _i++)
    {
        if (sArray[_i] != "")
        {%>
            <%:Html.ActionLink(sArray[_i].ToString(), "Index", "Search", new { @area = "Article", @SearchText= sArray[_i].ToString() }, new { @traget = "_blank" })%>
            <%if (_i < sArray.Length - 1)
            { 
                Response.Write("、");
            }
        }
    }
}%>