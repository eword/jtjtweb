using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcSiteMapProvider.Extensibility;
using eulei.shop.Models;
using eulei.shop.Code;
namespace eulei.shop.Code
{
    public class MyDynamicNodeProvider : DynamicNodeProviderBase
    {
        Linq_DefaultDataContext _dct;
        List<TB_Menu> m_class;
        public MyDynamicNodeProvider()
        {
            try
            {
                _dct = new Linq_DefaultDataContext();
                m_class = _dct.TB_Menu
                    //.OrderBy(m => m.MenuLevel)
                    //.OrderBy(m => m.MenuParentID)
                    //.OrderBy(m => m.MenuOrder)
                    .ToList<TB_Menu>();

            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message + "@" + "“code=>MyDynamicNodeProvider”");
            }


        }

        public override IEnumerable<DynamicNode> GetDynamicNodeCollection()
        {


            // Create a node for each item
            foreach (var item in m_class)
            {
                DynamicNode node = new DynamicNode();
                try
                {
                    node.Key = "key_" + item.MenuID;
                    node.ParentKey = item.MenuParentID.Equals("-1") ? "root" : "key_" + item.MenuParentID;
                    node.Title = item.MenuName;
                    node.Description = item.MenuName;
                    if (!string.IsNullOrWhiteSpace(item.MenuNameSpace))
                    {
                        node.Area = item.MenuNameSpace;
                    }
                    switch (item.MenuType)
                    {
                        case (int)MenuType.RoutePage:
                            node.Action = item.MenuActionName;
                            node.Controller = item.MenuControllerName;
                            node.RouteValues.Add("id", item.MenuParameterID.ToString());
                            break;
                        case (int)MenuType.UrlString:
                            node.Attributes.Add("Url", item.MenuUrlString);
                            break;
                        case (int)MenuType.ModelRoot:
                            node.Controller = item.MenuControllerName;
                            node.Action = item.MenuActionName;
                            break;
                    }

                }
                catch (Exception ex)
                {
                    LogHelper.WriteErrorLog(ex.Message + "@" + "“code=>MyDynamicNodeProvider”");
                }
                yield return node;
            }


        }

    }
}
