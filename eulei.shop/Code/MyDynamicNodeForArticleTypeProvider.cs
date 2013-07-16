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
    public class MyDynamicNodeForArticleTypeProvider : DynamicNodeProviderBase
    {
        Linq_DefaultDataContext _dct;
        List<TB_Class> m_class;
        public MyDynamicNodeForArticleTypeProvider()
        {
            try
            {
                _dct = new Linq_DefaultDataContext();
                m_class = _dct.TB_Class.Where(m => m.ClassModel.Equals((int)ClassModel.ArticleType))
                    .ToList<TB_Class>();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "One");
            }
            finally
            {
                _dct.Dispose();
            }

        }

        public override IEnumerable<DynamicNode> GetDynamicNodeCollection()
        {

            // Create a node for each item
            foreach (var item in m_class)
            {

                DynamicNode node = new DynamicNode();

                node.Key = item.ClassID;

                node.ParentKey = item.ClassParentID.Equals("-1") ? "root" : item.ClassParentID;

                node.Title = item.ClassName;

                node.Description = item.ClassName;


                switch (item.ClassType)
                {
                    case (int)ClassType.ListPage:
                        node.Action = item.ClassActionName;
                        node.Controller = item.ClassControllerName;
                        if (!string.IsNullOrWhiteSpace(item.ClassNameSpace))
                        {
                            node.Area = item.ClassNameSpace;
                        }
                        node.RouteValues.Add("id", item.ClassID.ToString());
                        yield return node;
                        break;
                    case (int)ClassType.OnlyPage:
                        node.Action = item.ClassActionName;
                        node.Controller = item.ClassControllerName;
                        if (!string.IsNullOrWhiteSpace(item.ClassNameSpace))
                        {
                            node.Area = item.ClassNameSpace;
                        }
                        node.RouteValues.Add("id", item.ClassParameterID.ToString());
                        yield return node;
                        break;
                    case (int)ClassType.UrlString:
                        node.Attributes.Add("Url", item.ClassUrlString);
                        yield return node;
                        break;
                    case (int)ClassType.ModelRoot:
                        node.Controller = item.ClassControllerName;
                        node.Action = item.ClassActionName;
                        //node.RouteValues.Add("id", item.ClassID.ToString());
                        if (!string.IsNullOrWhiteSpace(item.ClassNameSpace))
                        {
                            node.Area = item.ClassNameSpace;
                        }

                        yield return node;
                        break;
                   
                }
            }


        }

    }
}
