using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using eulei.shop.Models;
namespace eulei.shop.Code
{
    public static class ArticleOperationLogHelper
    {
        public static void WriteSendLog(int ArticleID,string OperateDesp,string content,string OperaterUserName,string OperaterFriendlyName)
        {
            try
            {
                Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
                SA_ArticleOperationLog _return = new SA_ArticleOperationLog();
                _return.OperationLogArticleID = ArticleID;
                _return.OperationLogOperateDesp = OperateDesp;
                _return.OperationLogContent = content;
                _return.OperationLogOperaterUserName = OperaterUserName;
                _return.OperationLogOperaterFriendlyName = OperaterFriendlyName;
                _return.OperationLogOperateTime = DateTime.Now;
                _dct.SA_ArticleOperationLog.InsertOnSubmit(_return);
                _dct.SubmitChanges();
            }
            catch(Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@“ArticleOperationLogHelper=>WriteSendLog”");
            }
        }
    }
}