using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.IO;
using System.Xml;
using System.Text;
using System.Web.Mvc;
using System.Xml.Linq;
using eulei.shop.Models;
using eulei.shop.Code;
namespace eulei.shop.Controllers
{
    public class RSSController : Controller
    {
        //
        // GET: /RSS/

        string _rootPath = ConfigurationManager.AppSettings["RSSDomain"].ToString();
        public ActionResult Index()
        {
            try
            {
                var _data = System.IO.File.ReadAllText(Server.MapPath("~/Content/XML/rss.xml"));
                Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
                var _table = _dct.TV_Article
                    .Where(m=>m.ArticleState.Equals((int)ArticleState.Auditing)||m.ArticleState.Equals((int)ArticleState.IsShare))
                    .OrderByDescending(m => m.ArticleSendDate).Take(50);
                MemoryStream _stream = new MemoryStream(Encoding.UTF8.GetBytes(_data));
                XElement _xe = XElement.Load(XmlReader.Create(_stream));
                var _xmle = _xe.Element("channel");
                foreach (var _row in _table)
                {
                    XElement _item = new XElement("item",
                        new XElement("title", _row.ArticleTitle),
                        new XElement("link",
                            string.Format("http://{0}{1}",
                             _rootPath,
                             Url.Action("Index", "Home", new { @area = "Article", @id = _row.ArticleTypeID.ToString(), @aid = _row.ArticleID }))
                            ),
                        new XElement("pubDate", _row.ArticleSendDate),
                        new XElement("description", _row.ArticleDescription),
                        new XElement("category", _row.ArticleTypeName));
                    _xmle.Add(_item);
                }
                return Content(_xe.ToString(), "text/xml");
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“Rss/Index”");
                return RedirectToAction("Index", "Error", new { @area = "" });
            }
        }

    }

}
