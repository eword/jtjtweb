using eulei.shop.Code;
using eulei.shop.Models;
using Q42.Wheels.Mvc.Paging;
using System.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
namespace eulei.shop.Areas.CustomerService.Controllers
{
   
    public class LetterController : MyController
    {
        //
        // GET: /CustomerService/Letter/
        [Paging(PageSize = 10, VaryByParams = new string[] { "SearchTextSender", "SearchTextTEL" })]
        public ActionResult Index(string SearchTextSender, string SearchTextTEL)
        {
            try
            {
                Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
                if (!(string.IsNullOrEmpty(SearchTextSender) && string.IsNullOrEmpty(SearchTextTEL)))
                {
                    ViewData.Model = _dct.VW_Letter
                       .Where(m => m.LetterSender.Equals(SearchTextSender) && m.LetterTEL.Equals(SearchTextTEL))
                        .OrderByDescending(m => m.LetterTimeSend);
                }
                else
                {
                    ViewData.Model = _dct.VW_Letter
                        .Where(m => m.LetterIsPublic.Equals(true) && m.LetterIsWriteBack.Equals(true))
                        .OrderByDescending(m => m.LetterTimeSend);
                }
                ViewBag.LetterTypeID = new SelectList((from _r in _dct.TB_Dictionary where _r.DictionaryParentID.Equals(3) orderby _r.DictionaryOrder select _r), "DictionaryID", "DictionaryDescription");
                ViewBag.LetterAddressee = new SelectList((from _r in _dct.SA_Framework orderby _r.FrameworkLevel, _r.FrameworkOrder select _r), "FrameworkName", "FrameworkName");

                return View();
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“CustomerService/Letter/Create”");
                return RedirectToAction("Index", "Error", new { @area = "", @MyContent = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                string url = Request.QueryString["_returnUrl"].ToString();
                ViewData["_returnUrl"] = url;
                Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
                TB_Letter _rusult = new TB_Letter();
                _rusult.LetterID = Guid.NewGuid();
                _rusult.LetterTypeID = int.Parse(collection["LetterTypeID"]);
                _rusult.LetterSender = collection["LetterSender"];
                _rusult.LetterEmail = collection["LetterEmail"];
                _rusult.LetterTEL = collection["LetterTEL"];
                _rusult.LetterPostalCode = collection["LetterPostalCode"];
                _rusult.LetterAddress = collection["LetterAddress"];
                _rusult.LetterAddressee = collection["LetterAddressee"];
                _rusult.LetterTitle = collection["LetterTitle"];
                _rusult.LetterContent = collection["LetterContent"];
                _rusult.LetterTimeSend = DateTime.Now;
                _rusult.LetterIsPublic = ConfigurationManager.AppSettings["LetterAutoShow"].ToString().Equals("true") ? true : false;
                _dct.TB_Letter.InsertOnSubmit(_rusult);
                _dct.SubmitChanges();
                return Redirect(url);
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“CustomerService/Letter/Create【post】”");
                return RedirectToAction("Index", "Error", new { @area = "", @MyContent = ex.Message });
            }


        }


    }
}
