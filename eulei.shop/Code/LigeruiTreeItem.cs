using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eulei.shop.Code
{
    public class LigeruiTreeItem
    {
        public string id { set; get; }
        public string fid { set; get; }
        public int queueOrder { set; get; }
        public string textcontent { set; get; }
        public bool ischecked { set; get; }
        public bool isexpand { set; get; }
    }
}