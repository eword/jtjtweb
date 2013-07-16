using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eulei.shop.Code
{
    public interface IAuthorityOperate
    {
        bool GetArticltTypeAuthority(int ArticleTypeID);
        bool GetSystemAuthority(int AuthorityID);
    }
}