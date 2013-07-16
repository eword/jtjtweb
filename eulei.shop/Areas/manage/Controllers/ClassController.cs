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
    public class ClassController : Controller
    {
        public ActionResult ProductType()
        {
            return View();
        }

        public ActionResult ProductTypeDetails(int id)
        {
            Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
            var _return = _dct.TB_ProductType.Single(m => m.ProductTypeID.Equals(id));
            ViewData.Model = _return;
            if (Request.IsAjaxRequest())
                return PartialView();
            else
                return View();
        }

        public ActionResult ProductTypeEdit(int id)
        {
            Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
            var _return = _dct.TB_ProductType.Single(m => m.ProductTypeID.Equals(id));
            ViewData.Model = _return;
            if (Request.IsAjaxRequest())
                return PartialView();
            else
                return View();
        }

        [HttpPost]
        public ActionResult ProductTypeEdit(int id, FormCollection collection)
        {
            Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
            var _result = _dct.TB_ProductType.Single(m => m.ProductTypeID.Equals(id));
            _result.ProductTypeLevel = int.Parse(collection["ProductTypeLevel"].ToString());
            _result.ProductTypeName = collection["ProductTypeName"].ToString();
            _result.ProductTypeOrder = int.Parse(collection["ProductTypeOrder"].ToString());
            _result.ProductTypeParentID = int.Parse(collection["ProductTypeParentID"].ToString());
            _result.ProductTypeIsLock = collection["ProductTypeIsLock"].ToString().Equals("false") ? false : true;
            _dct.SubmitChanges();
            return RedirectToAction("ProductType", "Class", new { @area = "manage" });

        }

        public ActionResult ProductTypeCreateChild(int id)
        {
            Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
            var _currentNode = _dct.TB_ProductType.Single(m => m.ProductTypeID.Equals(id));
            TB_ProductType _result = new TB_ProductType();
            ViewBag.CurrentNodeTitle = _currentNode.ProductTypeName + "子项目";
            int _order = _dct.TB_ProductType.Where(m => m.ProductTypeParentID.Equals(id)).Count() + 1;  
            _result.ProductTypeParentID = id;
            _result.ProductTypeLevel = _currentNode.ProductTypeLevel + 1;
            _result.ProductTypeOrder = _order;
            ViewData.Model = _result;
            if (Request.IsAjaxRequest())
                return PartialView();
            else
                return View();
        }

        [HttpPost]
        public ActionResult ProductTypeCreateChild(FormCollection collection)
        {
            try
            {

                Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
                TB_ProductType _result = new TB_ProductType();
                _result.ProductTypeLevel = int.Parse(collection["ProductTypeLevel"].ToString());
                _result.ProductTypeName = collection["ProductTypeName"].ToString();
                _result.ProductTypeOrder = int.Parse(collection["ProductTypeOrder"].ToString());
                _result.ProductTypeParentID = int.Parse(collection["ProductTypeParentID"].ToString());
                _result.ProductTypeIsLock = collection["ProductTypeIsLock"].ToString().Equals("false") ? false : true;
                _dct.TB_ProductType.InsertOnSubmit(_result);
                _dct.SubmitChanges();
                return RedirectToAction("ProductType", "Class", new { @area = "manage" });
            }
            catch
            {
                return View();
            }
        }
        public ActionResult ProductTypeCreateBrother(int id)
        {
            Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
            var _currentNode = _dct.TB_ProductType.Single(m => m.ProductTypeID.Equals(id));
            TB_ProductType _result = new TB_ProductType();
            ViewBag.CurrentNodeTitle = _currentNode.ProductTypeName + "同级项目";
            int _order = _dct.TB_ProductType.Where(m => m.ProductTypeParentID.Equals(_currentNode.ProductTypeParentID)).Count() + 1;
                             _result.ProductTypeParentID = _currentNode.ProductTypeParentID;
                             _result.ProductTypeLevel = _currentNode.ProductTypeLevel;
            _result.ProductTypeOrder = _order;
            ViewData.Model = _result;
            if (Request.IsAjaxRequest())
                return PartialView();
            else
                return View();
        }

        [HttpPost]
        public ActionResult ProductTypeCreateBrother(FormCollection collection)
        {
            try
            {

                Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
                TB_ProductType _result = new TB_ProductType();
                _result.ProductTypeLevel = int.Parse(collection["ProductTypeLevel"].ToString());
                _result.ProductTypeName = collection["ProductTypeName"].ToString();
                _result.ProductTypeOrder = int.Parse(collection["ProductTypeOrder"].ToString());
                _result.ProductTypeParentID = int.Parse(collection["ProductTypeParentID"].ToString());
                _result.ProductTypeIsLock = collection["ProductTypeIsLock"].ToString().Equals("false") ? false : true;
                _dct.TB_ProductType.InsertOnSubmit(_result);
                _dct.SubmitChanges();
                return RedirectToAction("ProductType", "Class", new { @area = "manage" });
            }
            catch
            {
                return View();
            }
        }
        public ActionResult ProductTypeDelete(int id)
        {
            Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
            var _return = _dct.TB_ProductType.Single(m => m.ProductTypeID.Equals(id));
            if (!_return.ProductTypeIsLock)
            {
                _dct.TB_ProductType.DeleteOnSubmit(_return);
                _dct.SubmitChanges(); return RedirectToAction("ProductType", "Class", new { @area = "manage" });
            }
            else
            {
                return Content("该项被锁定！无法删除");
            }
           

        }
    }
}
