using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eulei.shop.Code
{
    public enum MenuModel
    {
        /// <summary>
        /// 列表模式=1
        /// </summary>
        Lists = 1,
        /// <summary>
        /// 单页模式=2
        /// </summary>
        OnlyPage = 2,
        /// <summary>
        /// 自定义控制器模式=3
        /// </summary>
        CustomerControllers = 3,
        /// <summary>
        /// 根级模式=99
        /// </summary>
        Root = 99

    }
    /// <summary>
    /// 菜单模式
    /// </summary>
    public enum MenuType
    {
        /// <summary>
        /// 路由连接=1
        /// </summary>
        RoutePage = 1,
        /// <summary>
        /// 文字Url连接=2
        /// </summary>
        UrlString = 2,
        /// <summary>
        /// 无连接=3
        /// </summary>
        NoRoute = 3,
        /// <summary>
        /// 导航链接根级模式=99
        /// </summary>
        ModelRoot = 99
    }
    /// <summary>
    /// 文章分类模式
    /// </summary>
    public enum ArticleTypeModel
    {
        /// <summary>
        /// 列表模式=1
        /// </summary>
        Lists = 1,
        /// <summary>
        /// 单页模式=2
        /// </summary>
        OnlyPage = 2,
        /// <summary>
        /// 根模式=99
        /// </summary>
        Root = 99
    }
    /// <summary>
    /// 文章状态
    /// </summary>
    public enum ArticleState
    {
        /// <summary>
        /// 编辑中=1
        /// </summary>
        Editing = 1,
        /// <summary>
        /// 删除=88
        /// </summary>
        Delete = 88,
        /// <summary>
        /// 已审核=99
        /// </summary>
        Auditing = 99,
        /// <summary>
        /// 来源为共享文章=999
        /// </summary>
        IsShare=999
    }
    /// <summary>
    /// 共享文章状态
    /// </summary>
    public enum ShareArticleState
    {
        /// <summary>
        /// 待处理=1001
        /// </summary>
        Waiting = 1001,
        /// <summary>
        /// 删除=1002
        /// </summary>
        Delete = 1002,
        /// <summary>
        /// 引用=1003
        /// </summary>
        Cited = 1003,
        /// <summary>
        /// 转载=1004
        /// </summary>
        Reprint =1004
    }
    /// <summary>
    /// 文章搜索模式
    /// </summary>
    public enum ArticleSearchType
    { 
        /// <summary>
        /// 我的发文=1
        /// </summary>
        MySend=1,  
        /// <summary>
        /// 代办文章=2
        /// </summary>
        NeedHandle=2,
        /// <summary>
        /// 已办文章=3
        /// </summary>
        Handled = 3, 
        /// <summary>
        /// 全部文章=4
        /// </summary>
        All=4      
    }

    public enum ProductState
    {
        /// <summary>
        /// 编辑中=1
        /// </summary>
        Editing = 1,
        /// <summary>
        /// 删除=88
        /// </summary>
        Delete = 88,
        /// <summary>
        /// 已审核=99
        /// </summary>
        Auditing = 99
    }
    public enum ProductFilter
    {
        /// <summary>
        /// 热卖商品=1
        /// </summary>
        Hot = 1,
        /// <summary>
        /// 新品上市=2
        /// </summary>
        New = 2,
        /// <summary>
        /// 掌柜推荐=3
        /// </summary>
        commendatory = 3,
        /// <summary>
        /// 未设置=99
        /// </summary>
        NotSet = 99
    }

    public enum AdvertisementType
    {
        /// <summary>
        /// 首页=1
        /// </summary>
        HomePageMaster = 1,
        /// <summary>
        /// 文章页=2
        /// </summary>
        ArticlePageMaster = 2,
        /// <summary>
        /// 商品页=3（即小幅）
        /// </summary>
        ProductPageMaster = 3,
        /// <summary>
        /// 首页标头=4（即大幅）
        /// </summary>
        HomePageMasterTitle = 4,
        /// <summary>
        /// 横幅广告=5
        /// </summary>
        HomePageMasterBanner = 5
    }
    public enum AdvertisementState
    {
        /// <summary>
        /// 编辑中=1
        /// </summary>
        Editing = 1,
        /// <summary>
        /// 删除=88
        /// </summary>
        Delete = 88,
        /// <summary>
        /// 已审核=99
        /// </summary>
        Auditing = 99
    }
    public enum LinkState
    {
        /// <summary>
        /// 编辑中=1
        /// </summary>
        Editing = 1,
        /// <summary>
        /// 删除=88
        /// </summary>
        Delete = 88,
        /// <summary>
        /// 已审核=99
        /// </summary>
        Auditing = 99
    }

}