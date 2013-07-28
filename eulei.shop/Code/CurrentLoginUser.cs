using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Profile;
using eulei.shop.Models;
namespace eulei.shop.Code
{
    public  class CurrentLoginUser
    {
        public  UserInfo GetUserInfo(string userName)
        {
            UserInfo _user = new UserInfo();
            ProfileBase _profile = ProfileBase.Create(userName,true);
            _user.UserName = userName;
            _user.FriendlyName =_profile.GetPropertyValue("FriendlyName").ToString();
            _user.TEL = _profile.GetPropertyValue("TEL").ToString();
            Linq_DefaultDataContext _dct = new Linq_DefaultDataContext();
            _user.UserID = _dct.aspnet_Users.Single(m => m.UserName.Equals(userName)).UserId;
            return _user;
        }
    }
}