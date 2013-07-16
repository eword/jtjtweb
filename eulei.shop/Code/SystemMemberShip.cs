using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using eulei.shop.Models;
namespace eulei.shop.Code
{
    public static class SystemMemberShip
    {
        public static bool GetArticleTypeAuthority(string AuthorityString,int id)
        {
        
            return AuthorityString.Substring(id, 1).Equals("1") ?true:false;     
        }

        public static bool GetSystemAuthority(string SystemAuthorityString, int id)
        {

            return SystemAuthorityString.Substring(id, 1).Equals("1") ? true : false;     
        }
        #region 文章管理
        /// <summary>
        /// 文章浏览
        /// </summary>
        public static int ArticleBrowse = 2;
        /// <summary>
        /// 添加文章
        /// </summary>
        public static int ArticleCreate = 3;
        /// <summary>
        /// 编辑全部
        /// </summary>
        public static int ArticleEditAll = 5;
        /// <summary>
        /// 删除文章
        /// </summary>
        public static int ArticleDelete = 4;
        /// <summary>
        /// 浏览全部
        /// </summary>
        public static int ArticleBrowseAll = 6;
        ///// <summary>
        ///// 发布
        ///// </summary>
        //public static int ArticleIssue = 7;
        /// <summary>
        /// 共享文章
        /// </summary>
        public static int ArticleShare = 8;
        /// <summary>
        /// 管理共享
        /// </summary>
        public static int ArticleShareManage = 9;
        #endregion
        #region 站点单页
        /// <summary>
        /// 站点单页浏览
        /// </summary>
        public static int SiteOnlyPageBrowse = 11;
        /// <summary>
        /// 站点单页编辑
        /// </summary>
        public static int SiteOnlyPageEdit = 12;
        #endregion
        #region 广告管理
        /// <summary>
        /// Flash广告管理
        /// </summary>
        public static int FlashADManage = 21;
        /// <summary>
        /// 文字广告管理
        /// </summary>
        public static int WordADManage = 22;
        #endregion
        #region 友情链接管理
        /// <summary>
        /// 友情链接管理
        /// </summary>
        public static int FriendLinkManage = 31;
        #endregion
        #region 上传管理
        /// <summary>
        /// 上传管理
        /// </summary>
        public static int UploadFileManage = 41;
        #endregion
        #region 权限管理
        /// <summary>
        /// 账号管理
        /// </summary>
        public static int SystemUserManage = 51;
        /// <summary>
        /// 角色管理
        /// </summary>
        public static int SystemRoleManage = 52;
        #endregion
        #region 流程管理
        /// <summary>
        /// 流程管理
        /// </summary>
        public static int WorkFlowManage = 61;      
        #endregion
        #region 留言管理
        /// <summary>
        /// 流程管理
        /// </summary>
        public static int LetterManage = 71;
        #endregion
       
        
    }
}