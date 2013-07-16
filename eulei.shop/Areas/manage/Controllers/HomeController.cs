using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eulei.shop.Models;
using eulei.shop.Code;
namespace eulei.shop.Areas.manage.Controllers
{
    [Authorize(Roles = "Logins")]
    [HandleError]
    public class HomeController : Controller
    {
        //
        // GET: /manage/Home/

        public ActionResult Index()
        {
            Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();

            ViewBag.NeedHandled = _dct.VW_SA_ArticleNeedHandle
                                .Where(m => m.ArticleState.Equals((int)ArticleState.Editing)
                                    &&
                                    (
                                    m.ArticleStatusID.Equals(m.FlowStatusID) && m.FlowUserOperaterName.Equals(User.Identity.Name))
                                    ||
                                    (m.ArticleStatusID.Equals(1) && m.ArticleAuthor.Equals(User.Identity.Name))
                                    )
                                .Count();

            ViewBag.MySend = _dct.VW_SA_ArticleMySend
                                .Where(m => !m.ArticleState.Equals((int)ArticleState.Delete)
                                  && m.ArticleAuthor.Equals(User.Identity.Name))
                                 .Count();

            ViewBag.Handled = _dct.VW_SA_ArticleHandled
                                 .Where(m => !m.ArticleState.Equals((int)ArticleState.Delete)
                                      &&
                                    (!m.ArticleStatusID.Equals(1) && m.OperationLogOperaterUserName.Equals(User.Identity.Name))
                                   )
                                 .Count();

            ViewBag.ShardArticle = _dct.VW_SA_ShareArticle
                                 .Where(m =>
                                     (
                                     m.ShareArticleState.Equals((int)ShareArticleState.Waiting) ||
                                     (m.ShareArticleIsApplyReturn.Equals(true) && !m.ShareArticleState.Equals((int)ShareArticleState.Delete)))
                                     )
                                 .Count();

            ViewBag.WordAD = _dct.TB_Advertisement
                                  .Where(m => m.AdvertisementRemindDate.Date < DateTime.Now.Date && m.AdvertisementState.Equals((int)AdvertisementState.Auditing))
                                  .Count();

            ViewBag.FlashAD = _dct.TB_FlashADXML
                     .Where(m => m.FlashADXMLRemindDate.Date < DateTime.Now.Date && m.FlashADXMLState.Equals((int)AdvertisementState.Auditing))
                     .Count();
            return View();
        }

    }
}
