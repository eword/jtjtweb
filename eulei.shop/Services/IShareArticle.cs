using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using eulei.shop.Models;
namespace eulei.shop.Services
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IShareArticle”。
    [ServiceContract]
    public interface IShareArticle
    {
        [OperationContract]
        bool Push(ShareArticleEntity shareArticleEntity);
        [OperationContract]
        bool RePush(ShareArticleEntity shareArticleEntity);
    }

    [DataContract]
    public class ShareArticleEntity
    {
        [DataMember]
        public System.Guid ShareArticleID
        {
            get;
            set;
        }

        [DataMember]
        public string ShareArticleType
        {
            get;
            set;
        }

        [DataMember]
        public string ShareArticleTitle
        {
            get;
            set;
        }

        [DataMember]
        public string ShareArticleContent
        {
            get;
            set;
        }

        [DataMember]
        public string ShareArticleDescription
        {
            get;
            set;
        }

        [DataMember]
        public bool ShareArticleIsPicTheme
        {
            get;
            set;
        }

        [DataMember]
        public string ShareArticlePictureTitle
        {
            get;
            set;
        }

        [DataMember]
        public string ShareArticleUrl
        {
            get;
            set;
        }

        [DataMember]
        public System.DateTime ShareArticleSendDate
        {
            get;
            set;
        }

        [DataMember]
        public string ShareArticleLabel
        {
            get;
            set;
        }

        [DataMember]
        public System.DateTime ShareArticleShareDate
        {
            get;
            set;
        }

        [DataMember]
        public bool ShareArticleIsNew
        {
            get;
            set;
        }

        [DataMember]
        public bool ShareArticleIsemp
        {
            get;
            set;
        }

        [DataMember]
        public int ShareArticleState
        {
            get;
            set;
        }

        [DataMember]
        public string ShareArticleOperationgRecord
        {
            get;
            set;
        }

        [DataMember]
        public string ShareArticleAuthor
        {
            get;
            set;
        }

        [DataMember]
        public System.DateTime ShareArticleLastOperatDate
        {
            get;
            set;
        }

        [DataMember]
        public string ShareArticleLastOperstor
        {
            get;
            set;
        }

        [DataMember]
        public int ShareArticleStatusID
        {
            get;
            set;
        }

        [DataMember]
        public bool ShareArticleIsShare
        {
            get;
            set;
        }

        [DataMember]
        public bool ShareArticleIsApplyReturn
        {
            get;
            set;
        }
        [DataMember]
        public string ShareArticleFrameworkName
        {
            get;
            set;
        }

        [DataMember]
        public int ShareArticleArticleID
        {
            get;
            set;
        }

        [DataMember]
        public int ShareArticleCitedTypeID
        {
            get;
            set;
        }
        [DataMember]
        public string ShareArticlePusher
        {
            get;
            set;
        }
        [DataMember]
        public string ShareArticlePusherFriendName
        {
            get;
            set;
        }
        [DataMember]
        public System.Guid ShareArticlePusherID
        {
            get;
            set;
        }
        [DataMember]
        public int ShareArticleCitedArticleID
        {
            get;
            set;
        }

    }
}
