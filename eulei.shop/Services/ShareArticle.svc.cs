using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using eulei.shop.Models;
using eulei.shop.Code;
using System.ServiceModel.Activation;
namespace eulei.shop.Services
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“ShareArticle”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 ShareArticle.svc 或 ShareArticle.svc.cs，然后开始调试。
    [AspNetCompatibilityRequirements(RequirementsMode=AspNetCompatibilityRequirementsMode.Allowed)]
    public class ShareArticle : IShareArticle
    {
        public bool Push(ShareArticleEntity shareArticleEntity)
        {
            try
            {
                Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
                var _article = _dct.VW_SA_ShareArticle.Where(m => m.ShareArticleArticleID.Equals(shareArticleEntity.ShareArticleArticleID)
                    &&(!m.ShareArticleState.Equals((int)ShareArticleState.Delete)));
                if (_article.Count() > 0)
                {
                    return false;
                }
                SA_ShareArticle _result = new SA_ShareArticle();
                _result.ShareArticleArticleID = shareArticleEntity.ShareArticleArticleID;
                _result.ShareArticleAuthor = shareArticleEntity.ShareArticleAuthor;
                _result.ShareArticleCitedTypeID = shareArticleEntity.ShareArticleCitedTypeID;
                _result.ShareArticleContent = shareArticleEntity.ShareArticleContent;
                _result.ShareArticleDescription = shareArticleEntity.ShareArticleDescription;
                _result.ShareArticleFrameworkName = shareArticleEntity.ShareArticleFrameworkName;
                _result.ShareArticleID = shareArticleEntity.ShareArticleID;
                _result.ShareArticleIsApplyReturn = shareArticleEntity.ShareArticleIsApplyReturn;
                _result.ShareArticleIsemp = shareArticleEntity.ShareArticleIsemp;
                _result.ShareArticleIsNew = shareArticleEntity.ShareArticleIsNew;
                _result.ShareArticleIsPicTheme = shareArticleEntity.ShareArticleIsPicTheme;
                _result.ShareArticleIsShare = shareArticleEntity.ShareArticleIsShare; ;
                _result.ShareArticleLabel = shareArticleEntity.ShareArticleLabel;
                _result.ShareArticleLastOperatDate = shareArticleEntity.ShareArticleLastOperatDate;
                _result.ShareArticleLastOperstor = shareArticleEntity.ShareArticleLastOperstor;
                _result.ShareArticleOperationgRecord = shareArticleEntity.ShareArticleOperationgRecord;
                _result.ShareArticlePictureTitle = shareArticleEntity.ShareArticlePictureTitle;
                _result.ShareArticleSendDate = shareArticleEntity.ShareArticleSendDate;
                _result.ShareArticleShareDate = shareArticleEntity.ShareArticleShareDate;
                _result.ShareArticleState = shareArticleEntity.ShareArticleState;
                _result.ShareArticleStatusID = shareArticleEntity.ShareArticleStatusID;
                _result.ShareArticleTitle = shareArticleEntity.ShareArticleTitle;
                _result.ShareArticleType = shareArticleEntity.ShareArticleType;
                _result.ShareArticleUrl = shareArticleEntity.ShareArticleUrl;
                _result.ShareArticlePusher = shareArticleEntity.ShareArticlePusher;
                _result.ShareArticlePusherFriendName = shareArticleEntity.ShareArticlePusherFriendName;
                _result.ShareArticlePusherID = shareArticleEntity.ShareArticlePusherID;
                _dct.SA_ShareArticle.InsertOnSubmit(_result);
                _dct.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@ShareArticle/Push");
                throw ex;
            }
        }

        public bool RePush(ShareArticleEntity shareArticleEntity)
        {
            try
            {
                Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
                var _article = _dct.VW_SA_ShareArticle.Where(m => m.ShareArticleArticleID.Equals(shareArticleEntity.ShareArticleArticleID)
    && (!m.ShareArticleState.Equals((int)ShareArticleState.Delete)));
                if (_article.Count() !=1)
                {
                    return false;
                }
                var _return = _dct.SA_ShareArticle.Single(m => m.ShareArticleArticleID.Equals(shareArticleEntity.ShareArticleArticleID) && (!m.ShareArticleState.Equals((int)ShareArticleState.Delete)));
                _return.ShareArticleIsApplyReturn = true;
                _dct.SubmitChanges();
                return true;
            }
            catch (ArgumentNullException ex)
            {
                LogHelper.WriteErrorLog("远程目标文章不存在！@" + ex.Message + "@ShareArticle/RePush");
                throw ex;
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@ShareArticle/RePush");
                throw ex;
            }


        }

    }
}
