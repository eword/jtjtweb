using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eulei.shop.Models;
using Q42.Wheels.Mvc.Paging;
using eulei.shop.Code;

namespace eulei.shop.Areas.manage.Controllers
{
    [Authorize(Roles = "Logins")]
    [HandleError]
    public class ProductController : Controller
    {
        //
        // GET: /manage/Article/
         [Paging(PageSize = 15, VaryByParams = new string[] { "ProductTitle", "MerchandiseTypeID" })]
        public ActionResult Index(string ProductTitle, int? MerchandiseTypeID)
        {
            Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
            string _title = string.IsNullOrEmpty(ProductTitle) ? "" : ProductTitle;
            int _typeID = MerchandiseTypeID.HasValue ? MerchandiseTypeID.Value : 0;
            if (_typeID == 0)
            {
                ViewData.Model = _dct.TV_Merchandise
                    .Where(m => !m.MerchandiseState.Equals((int)ProductState.Delete)
                    &&m.MerchandiseTitle.Contains(_title))
                    .OrderBy(m => m.MerchandiseIsemp).OrderByDescending(m => m.MerchandiseID);
            }
            else
            {
                ViewData.Model = _dct.TV_Merchandise
        .Where(m => !m.MerchandiseState.Equals((int)ProductState.Delete)
        && m.MerchandiseTitle.Contains(_title)
        &&m.MerchandiseType.Equals(_typeID))
        .OrderBy(m => m.MerchandiseIsemp).OrderByDescending(m => m.MerchandiseID);
            }        
           
            return View();
        }

        [Paging(PageSize = 15, VaryByParams = new string[] { "ProductTitle", "MerchandiseTypeID" })]
        //[HttpPost]
        public ActionResult Search(string ProductTitle, int? MerchandiseTypeID)
        {
            System.Web.Routing.RouteValueDictionary _returns = new System.Web.Routing.RouteValueDictionary();
            // _returns.Add("area","manage");
            try
            {
                string _title = string.IsNullOrEmpty(ProductTitle) ? "" : ProductTitle;
                int _typeID = MerchandiseTypeID.HasValue ? MerchandiseTypeID.Value : 0;
                if (_typeID == 0)
                {
                    _returns.Add("ProductTitle", _title);
                }
                else
                {
                    _returns.Add("ProductTitle", _title);
                    _returns.Add("MerchandiseTypeID", _typeID);
                }

            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
            return RedirectToAction("Index", "Product", _returns);
        }

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /manage/Article/Create

        public ActionResult Create()
        {
            string url = Request.QueryString["_returnUrl"].ToString();
            ViewData["_returnUrl"] = url;
            return View();
        }

        //
        // POST: /manage/Article/Create

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                string url = collection["_returnUrl"].ToString();
                Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
                TB_Merchandise _result = new TB_Merchandise();
                _result.MerchandiseType = int.Parse(collection["MerchandiseType"].ToString());
                _result.MerchandiseTitle = collection["MerchandiseTitle"].ToString();
                _result.MerchandiseName = collection["MerchandiseName"].ToString();
                _result.MerchandiseTitlePicUrl = collection["MerchandiseTitlePicUrl"].ToString();
                _result.MerchandiseState = (int)ProductState.Editing;
                _result.MerchandiseTitleDescription = collection["MerchandiseTitleDescription"].ToString();
                _result.MerchandiseDescription = collection["MerchandiseDescription"].ToString();
                _result.MerchandiseSynopsis = collection["MerchandiseSynopsis"].ToString();
                _result.MerchandiseAfterSaleService = collection["MerchandiseAfterSaleService"].ToString();
                _result.MerchandiseLabel = collection["MerchandiseLabel"].ToString();
                _result.MerchandiseClickCount = int.Parse(collection["MerchandiseClickCount"].ToString());
                //////////////////   
                _result.MerchandiseDate = DateTime.Parse(collection["MerchandiseDate"].ToString());
                _result.MerchandiseBeginDate = DateTime.Parse(collection["MerchandiseBeginDate"].ToString());
                 _result.MerchandiseLastOperatDate = DateTime.Now;
                _result.MerchandiseLastOperator = User.Identity.Name;
                _result.MerchandiseAuthor = User.Identity.Name;
                _result.MerchandiseOperatingRecord += "\r\n" + DateTime.Now.ToString() + ":" + User.Identity.Name + " 操作类型：添加";
               
                _dct.TB_Merchandise.InsertOnSubmit(_result);
                _dct.SubmitChanges();
                return Redirect(url);
            }
            catch
            {
                return RedirectToAction("Index", "Error", new { @area = "" });
            }
        }

        //
        // GET: /manage/Article/Edit/5

        public ActionResult Edit(int id)
        {
            string url = Request.QueryString["_returnUrl"].ToString();
            ViewData["_returnUrl"] = url;
            Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
            ViewData.Model = _dct.TB_Merchandise.Single(m => m.MerchandiseID.Equals(id));
            return View();
        }

        //
        // POST: /manage/Article/Edit/5

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                string url = collection["_returnUrl"].ToString();
                Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
                var _result = _dct.TB_Merchandise.Single(m => m.MerchandiseID.Equals(id));

                _result.MerchandiseType = int.Parse(collection["MerchandiseType"].ToString());
                _result.MerchandiseTitle = collection["MerchandiseTitle"].ToString();
                _result.MerchandiseName = collection["MerchandiseName"].ToString();
                _result.MerchandiseTitlePicUrl = collection["MerchandiseTitlePicUrl"].ToString();    
                _result.MerchandiseTitleDescription = collection["MerchandiseTitleDescription"].ToString();
                _result.MerchandiseDescription = collection["MerchandiseDescription"].ToString();
                _result.MerchandiseSynopsis = collection["MerchandiseSynopsis"].ToString();
                _result.MerchandiseAfterSaleService = collection["MerchandiseAfterSaleService"].ToString();
                _result.MerchandiseLabel = collection["MerchandiseLabel"].ToString();
                _result.MerchandiseClickCount = int.Parse(collection["MerchandiseClickCount"].ToString());
                //////////////////   
                _result.MerchandiseDate = DateTime.Parse(collection["MerchandiseDate"].ToString());
                _result.MerchandiseBeginDate = DateTime.Parse(collection["MerchandiseBeginDate"].ToString());
                _result.MerchandiseLastOperatDate = DateTime.Now;
                _result.MerchandiseLastOperator = User.Identity.Name;
                _result.MerchandiseOperatingRecord += "\r\n" + DateTime.Now.ToString() + ":" + User.Identity.Name + " 操作类型：编辑";
                               _dct.SubmitChanges();
                return Redirect(url);
            }
            catch
            {
                return RedirectToAction("Index", "Error", new { @area = "" });
            }
        }

        //
        // GET: /manage/Article/Delete/5

        //
        // POST: /manage/Article/Delete/5

        public ActionResult Delete(int id)
        {
            try
            {
                string url = Request.QueryString["_returnUrl"].ToString();
                ViewData["_returnUrl"] = url;
                Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
                var _result = _dct.TB_Merchandise.Single(m => m.MerchandiseID.Equals(id));
                _result.MerchandiseState = (int)ProductState.Delete;
                _dct.SubmitChanges();
                return Redirect(url);
            }
            catch
            {
                return RedirectToAction("Index", "Error", new { @area = "" });
            }
        }

        public ActionResult Auditing(int id)
        {
            try
            {
                string url = Request.QueryString["_returnUrl"].ToString();
                ViewData["_returnUrl"] = url;
                Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
                var _result = _dct.TV_Merchandise.Single(m => m.MerchandiseID.Equals(id));
                ViewData.Model = _result;
                return View();
            }
            catch
            {
                return RedirectToAction("Index", "Error", new { @area = "" });
            }
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Auditing(int id, FormCollection collection)
        {
            try
            {
                string url = Request.QueryString["_returnUrl"].ToString();
                ViewData["_returnUrl"] = url;
                Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
                var _result = _dct.TB_Merchandise.Single(m => m.MerchandiseID.Equals(id));
                _result.MerchandiseState = (int)ProductState.Auditing;
                _dct.SubmitChanges();
                return Redirect(url);
            }
            catch
            {
                return RedirectToAction("Index", "Error", new { @area = "" });
            }
        }
        public ActionResult ReAuditing(int id)
        {
            try
            {
                string url = Request.QueryString["_returnUrl"].ToString();
                ViewData["_returnUrl"] = url;
                Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
                var _result = _dct.TB_Merchandise.Single(m => m.MerchandiseID.Equals(id));
                _result.MerchandiseState = (int)ProductState.Editing;
                _dct.SubmitChanges();
                return Redirect(url);
            }
            catch
            {
                return RedirectToAction("Index", "Error", new { @area = "" });
            }
        }
        public ActionResult SetIsemp(int id)
        {
            try
            {
                string url = Request.QueryString["_returnUrl"].ToString();
                ViewData["_returnUrl"] = url;
                Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
                var _result = _dct.TB_Merchandise.Single(m => m.MerchandiseID.Equals(id));
                _result.MerchandiseIsemp = !_result.MerchandiseIsemp;
                _dct.SubmitChanges();
                return Redirect(url);
            }
            catch
            {
                return RedirectToAction("Index", "Error", new { @area = "" });
            }
        }
        public ActionResult SetIsNew(int id)
        {
            try
            {
                string url = Request.QueryString["_returnUrl"].ToString();
                ViewData["_returnUrl"] = url;
                Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
                var _result = _dct.TB_Merchandise.Single(m => m.MerchandiseID.Equals(id));
                _result.MerchandiseIsNew = !_result.MerchandiseIsNew;
                _dct.SubmitChanges();
                return Redirect(url);
            }
            catch
            {
                return RedirectToAction("Index", "Error", new { @area = "" });
            }
        }
    }
}
