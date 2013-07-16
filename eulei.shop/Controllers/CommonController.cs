using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Xml;
using System.Text;
using System.Xml.Linq;
using System.Web.Mvc;
using eulei.shop.Models;
using eulei.shop.Code;
using Eword.ValidateCode;
namespace eulei.shop.Controllers
{
    public class CommonController : Controller, IAuthorityOperate
    {
        public bool GetArticltTypeAuthority(int ArticleTypeID)
        {
            return SystemMemberShip.GetArticleTypeAuthority(Session["ArticleType"].ToString(), ArticleTypeID);
        }

        public bool GetSystemAuthority(int AuthorityID)
        {
            return SystemMemberShip.GetSystemAuthority(Session["SystemAuthority"].ToString(), AuthorityID);
        }
        public ContentResult GetFlashADXML(int id, string XMLPath)
        {

            string _Date = System.IO.File.ReadAllText(Server.MapPath("~/Content/XML/" + XMLPath + ".xml"));


            try
            {
                MemoryStream _stream = new MemoryStream(Encoding.UTF8.GetBytes(_Date));
                XElement _xe = XElement.Load(XmlReader.Create(_stream));
                var _xmle = _xe.Element("channel");
                Linq_DefaultDataContext _dcx = new Linq_DefaultDataContext();
                var _lists = _dcx.TB_FlashADXML.Where(m => m.FlashADXMLType.Equals(id)
                    && m.FlashADXMLState.Equals((int)AdvertisementState.Auditing));
                foreach (var _row in _lists)
                {
                    XElement _item = new XElement("item",
                                                new XElement("link",
                               Url.Content(_row.FlashADXMLRedirectUrl)
                              ),
                        new XElement("image", Url.Content(_row.FlashADXMLPicUrl)),
                        new XElement("title", _row.FlashADXMLTitle));
                    _xmle.Add(_item);
                }
                return Content(_xe.ToString(), "text/xml");
            }
            catch (Exception ex)
            {
                MemoryStream _stream = new MemoryStream(Encoding.UTF8.GetBytes(_Date));
                XElement _xe = XElement.Load(XmlReader.Create(_stream));
                var _xmle = _xe.Element("channel");
                XElement _item = new XElement("item",
                                            new XElement("link", Url.Action("Error")),
                    new XElement("image", Url.Content("~/Content/images/error.png")),
                    new XElement("title", "图片链接异常"));
                _xmle.Add(_item);
                LogHelper.WriteErrorLog(ex.Message + "@" + "“Common/GetFlashADXML”");
                return Content(_xe.ToString(), "text/xml");
            }


        }

        /// <summary>
        /// 文章菜单
        /// </summary>
        /// <returns></returns>
        public JsonResult GetJsonForArticleClassID()
        {
            var _res = new JsonResult();
            try
            {
                Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
                var _return = _dct.TB_Menu.OrderBy(m => m.MenuParameterID).OrderBy(m => m.MenuOrder);
                var _list = from _r in _return
                            select new
                            {
                                id = _r.MenuID.ToString(),
                                fid = _r.MenuParentID.ToString(),
                                textcontent = _r.MenuName,
                                isexpand = true
                            };
                _res.Data = _list;
                _res.JsonRequestBehavior = JsonRequestBehavior.AllowGet;//允许使用GET方式获取，否则用GET获取是会报错。      

            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“Common/GetJsonForArticleClassID”");
            }
            return _res;
        }

        /// <summary>
        /// 文章分类
        /// </summary>
        /// <returns></returns>
        public JsonResult GetJsonForArticleTypeID()
        {
            var _res = new JsonResult();
            try
            {
                Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();                
                var _return = _dct.TB_ArticleType.Where(m => m.ArticleTypeIsUp.Equals(true)).OrderBy(m => m.ArticleTypeParentID).OrderBy(m => m.ArticleTypeOrder);
                List<LigeruiTreeItem> _lists = new List<LigeruiTreeItem>();
                foreach (var item in _return)
                {
                    if (GetArticltTypeAuthority(item.ArticleTypeID))
                    {
                        LigeruiTreeItem _list = new LigeruiTreeItem();
                        _list.id = item.ArticleTypeID.ToString();
                        _list.fid = item.ArticleTypeParentID.ToString();
                        _list.textcontent = item.ArticleTypeName;
                        _list.isexpand = true;
                        _lists.Add(_list);
                    }
                }
                _res.Data = _lists;
                _res.JsonRequestBehavior = JsonRequestBehavior.AllowGet;//允许使用GET方式获取，否则用GET获取是会报错。      
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“Common/GetJsonForArticleTypeID”");
            }
            return _res;
        }

        public JsonResult GetJsonForMerchandiseTypeID()
        {
            var _res = new JsonResult();
            try
            {
                Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
                var _return = _dct.TB_ProductType.OrderBy(m => m.ProductTypeParentID).OrderBy(m => m.ProductTypeOrder);

                var _list = from _r in _return
                            select new
                            {
                                id = _r.ProductTypeID.ToString(),
                                fid = _r.ProductTypeParentID.ToString(),
                                textcontent = _r.ProductTypeName,
                                isexpand = true
                            };
                _res.Data = _list;
                _res.JsonRequestBehavior = JsonRequestBehavior.AllowGet;//允许使用GET方式获取，否则用GET获取是会报错。      

            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“Common/GetJsonForMerchandiseTypeID”");
            }
            return _res;
        }

        private class ListType
        {
            public int id { set; get; }
            public int fid { set; get; }
            public string textcontent { set; get; }
            public bool isexpand { set; get; }
        }

        public JsonResult GetJsonForAdvertisementTypeID()
        {
            var _res = new JsonResult();
            try
            {
                var _list = new ListType[5]{
            new ListType{id=(int)AdvertisementType.HomePageMaster, fid=-1, isexpand=true, textcontent="首页模板小幅"},
            new ListType{id=(int)AdvertisementType.ArticlePageMaster, fid=-1, isexpand=true, textcontent="文章页模板"},
            new ListType{id=(int)AdvertisementType.ProductPageMaster, fid=-1, isexpand=true, textcontent="商品页模板"},
            new ListType{id=(int)AdvertisementType.HomePageMasterTitle, fid=-1, isexpand=true, textcontent="首页模板大幅"}, 
            new ListType{id=(int)AdvertisementType.HomePageMasterBanner, fid=-1, isexpand=true, textcontent="首页模板横幅"}  
            };
                _res.Data = _list;
                _res.JsonRequestBehavior = JsonRequestBehavior.AllowGet;//允许使用GET方式获取，否则用GET获取是会报错。      
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“Common/GetJsonForAdvertisementTypeID”");
            }
            return _res;
        }

        public ActionResult WordADList(int id)
        {
            try
            {
                Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
                switch (id)
                {
                    case (int)AdvertisementType.ArticlePageMaster:
                        ViewData.Model = _dct.TB_Advertisement
                            .Where(m => m.AdvertisementState.Equals((int)AdvertisementState.Auditing) && m.AdvertisementType.Equals((int)AdvertisementType.ArticlePageMaster))
                            .OrderByDescending(m => m.AdvertisementID).Take(5);
                        break;
                    case (int)AdvertisementType.HomePageMaster:
                        ViewData.Model = _dct.TB_Advertisement
                            .Where(m => m.AdvertisementState.Equals((int)AdvertisementState.Auditing) && m.AdvertisementType.Equals((int)AdvertisementType.HomePageMaster))
                            .OrderByDescending(m => m.AdvertisementID).Take(5);
                        break;
                    case (int)AdvertisementType.ProductPageMaster:
                        ViewData.Model = _dct.TB_Advertisement
                            .Where(m => m.AdvertisementState.Equals((int)AdvertisementState.Auditing) && m.AdvertisementType.Equals((int)AdvertisementType.ProductPageMaster))
                            .OrderByDescending(m => m.AdvertisementID).Take(5);
                        break;
                    case (int)AdvertisementType.HomePageMasterTitle:
                        ViewData.Model = _dct.TB_Advertisement
                            .Where(m => m.AdvertisementState.Equals((int)AdvertisementState.Auditing) && m.AdvertisementType.Equals((int)AdvertisementType.HomePageMasterTitle))
                            .OrderByDescending(m => m.AdvertisementID).Take(5);
                        break;
                    default:
                        ViewData.Model = _dct.TB_Advertisement
                            .Where(m => m.AdvertisementState.Equals((int)AdvertisementState.Auditing))
                            .OrderByDescending(m => m.AdvertisementID).Take(5);
                        break;
                }
                if (Request.IsAjaxRequest())
                    return PartialView();
                else
                    return View();
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“Common/WordADList”");
                return RedirectToAction("Index", "Error", new { @area = "" });
            }

        }

        public ActionResult CopyRight()
        {
            if (Request.IsAjaxRequest())
                return PartialView();
            else
                return View();
        }

        public ActionResult Bulletin(int id)
        {
            try
            {
                Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
                var _return = _dct.TV_Article.Where(m => (m.ArticleState.Equals((int)ArticleState.Auditing) || m.ArticleState.Equals((int)ArticleState.IsShare)) && m.ArticleTypeID.Equals(id))
                    .OrderByDescending(m => m.ArticleSendDate);
                ViewData.Model = _return;
                if (Request.IsAjaxRequest())
                    return PartialView();
                else
                    return View();
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“Common/Bulletin”");
                return RedirectToAction("Index", "Error", new { @area = "" });
            }
        }

        /// <summary>
        /// 自定义文章分页
        /// </summary>
        /// <param name="CustomerUrl"></param>
        /// <param name="FirstPage"></param>
        /// <param name="CurrentPage"></param>
        /// <param name="LastPage"></param>
        /// <returns></returns>
        public ActionResult CustomerPagination(string CustomerUrl, int FirstPage, int CurrentPage, int LastPage)
        {
            try
            {
                ViewBag.Url = CustomerUrl;
                ViewBag.CurrentPage = CurrentPage;
                ViewBag.FirstPage = FirstPage;
                ViewBag.LastPage = LastPage;
                if (Request.IsAjaxRequest())
                    return PartialView();
                else
                    return View();
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“Common/CustomerPagination”");
                return RedirectToAction("Index", "Error", new { @area = "" });
            }
        }

        /// <summary>
        /// 获取组织结构列表
        /// </summary>
        /// <returns></returns>
        public JsonResult GetJsonForFrameworkID()
        {
            var _res = new JsonResult();
            try
            {
                Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
                var _return = _dct.SA_Framework.OrderBy(m => m.FrameworkLevel).OrderBy(m => m.FrameworkOrder);

                var _list = from _r in _return
                            select new
                            {
                                id = _r.FrameworkID.ToString(),
                                fid = _r.FrameworkParentID.ToString(),
                                textcontent = _r.FrameworkName,
                                isexpand = true
                            };
                _res.Data = _list;
                _res.JsonRequestBehavior = JsonRequestBehavior.AllowGet;//允许使用GET方式获取，否则用GET获取是会报错。      
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“Common/GetJsonForAdvertisementTypeID”");
            }
            return _res;
        }

        public ActionResult GetValidateCodeImage(string code)
        {
            ValidateCode vCode = new ValidateCode();
            Session["ValidateCode"] = code;
            byte[] bytes = vCode.CreateValidateGraphic(code);
            return File(bytes, @"image/jpeg");
        }

        public ActionResult GetValidateCodeString()
        {
            ValidateCode vCode = new ValidateCode();
            string code = vCode.CreateValidateCode(5);
            return Content(code);

        }
        public ActionResult GetValidateCodeForSession()
        {
            if (Session["ValidateCode"] != null)
                return Content(Session["ValidateCode"].ToString());
            return Content("55555");
        }


    }
}
