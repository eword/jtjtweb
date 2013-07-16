using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eulei.shop.Areas.Product.Controllers
{
    public class JsController : Controller
    {
        //
        // GET: /Product/Js/

        public JavaScriptResult AjaxLoadProductList()
        {
            string _JsStr=@"$.ajax({
    type: 'get',
    url: '"+Url.Content("~/Product/Home/ProductList/2")+@"',
    cache: false,
    async: true,
    dataType: 'html',
    contentType: 'text/html;charset=utf-8', 
    beforeSend: function (data) {
        $('#Hot-productList').html('');
        $('#Hot-productList').append('<img alt=\\""列表加载中……\\"" width=\\""100\\"" height=\\""100\\""  src=\\"""+Url.Content("~/Content/images/Loding.gif")+@"\\"" ');
    }, 
    success: function (Htmlobj) {
        $('#Hot-productList').html(Htmlobj);
    },
    error: function (data) {
        $('#Hot-productList').html('failed to load data.')
    } 
});";
            return  JavaScript(_JsStr);
        }

    }
}
