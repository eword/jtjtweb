using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Q42.Wheels.Mvc.Paging;
using eulei.shop.Models;
using eulei.shop.Code;
using Eword.Linq;
namespace eulei.shop.Areas.Article.Controllers
{
    public class SearchController : Controller
    {

        [Paging(PageSize = 5, VaryByParams = new string[] { "SearchText" })]
        public ActionResult Index(string SearchText)
        {
            string KeyWords = SearchText;
            try
            {
                string m_KeyWords = KeyWords.Trim();
                string[] _KeyWords = m_KeyWords.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                string _sql = "";
                foreach (string _queryStr in _KeyWords)
                {
                    string _temp = _queryStr;
                    if (_sql.Equals(""))
                    {
                        _sql = _sql +
                        " (" +
                        "ArticleTitle.Contains(\"" + _temp + "\")"
                        + "  or " +
                        " ArticleContent.Contains(\"" + _temp + "\") "
                        + " or " +
                        " ArticleDescription.Contains(\"" + _temp + "\")"
                        + " or " +
                        " ArticleLabel.Contains(\"" + _temp + "\")"
                        + ")";
                    }
                    else
                    {
                        _sql = _sql +
                        "  and  (" +
                        "ArticleTitle.Contains(\"" + _temp + "\")"
                        + "  or " +
                        " ArticleContent.Contains(\"" + _temp + "\") "
                        + " or " +
                        " ArticleDescription.Contains(\"" + _temp + "\")"
                        + " or " +
                        " ArticleLabel.Contains(\"" + _temp + "\")"
                        + ")";
                    }
                }
                Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
                var _return = _dct.TV_Article.Where(_sql, "")
                                             .Where(m => m.ArticleState.Equals((int)ArticleState.Auditing) || m.ArticleState.Equals((int)ArticleState.IsShare));
                List<TV_Article> _result = new List<TV_Article>();
                foreach (var item in _return)
                {
                    item.ArticleTitle = ClearHtmlLabel.NoHTML(item.ArticleTitle);
                    item.ArticleContent = ClearHtmlLabel.NoHTML(item.ArticleContent);
                    if (string.IsNullOrEmpty(item.ArticleTitle))
                    {
                        item.ArticleTitle = "搜索内容被过滤清空了，请联系管理员！";
                    }
                    if (string.IsNullOrEmpty(item.ArticleContent))
                    {
                        item.ArticleContent = "搜索内容被过滤清空了，请联系管理员！";
                    }
                    string _returnStr = "";
                    foreach (string _queryStr in _KeyWords)
                    {
                        int bInt = item.ArticleContent.IndexOf(_queryStr);
                        bInt = bInt - 20 > 0 ? bInt - 20 : 0;
                        int eInt = _queryStr.Length;
                        eInt = eInt + 40 < (item.ArticleContent.Length - bInt) ? eInt + 40 : item.ArticleContent.Length - bInt - 1;
                        //_returnStr += item.ArticleContent.Substring(bInt, eInt).Replace(_queryStr, "<span class=\"mark\" >" + _queryStr + "</span>") + "…………";
                        _returnStr += item.ArticleContent.Replace(_queryStr, "<span class=\"mark\" >" + _queryStr + "</span>") + "…………";
                        item.ArticleTitle = item.ArticleTitle.Replace(_queryStr, "<span class=\"mark\" >" + _queryStr + "</span>");
                    }
                    item.ArticleContent = _returnStr;
                    _result.Add(item);
                }
                ViewData.Model = _result;
                return View();
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“Article/Search/Index”");
                return RedirectToAction("Index", "Error", new { @area = "" });
            }
        }

    }
}
