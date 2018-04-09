using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using TestCreator.Helpers;
using TestCreator.Models;
using static TestCreator.Enums.Enums;

namespace TestCreator.Controllers
{
    public class LoginController
    {
        private void CheckIsCookie(HttpRequest request)
        {
            HttpCookie cookie = request.Cookies[ParamHelper.CookieName];
            if (cookie != null)
            {
                TestUser ur = new TestUser();
              //  ur= ur.GetUserID(cookie.Value);
                if (true)//cookie.Value == sprawdzamy czy jest taki user)
                {
                    
                }
                else
                {//usunięcie cookies
                    HttpCookie rememberMeCookie = request.Cookies[ParamHelper.CookieName];
                    rememberMeCookie.Expires = DateTime.Now.AddDays(-1);
                    request.Cookies.Add(rememberMeCookie);
                }
            }
        }

        private void LoginUser(TestUser user, HttpSessionState Session)
        {
            Session[ParamHelper.SessionName] = user;
            if (true)//user.Type == UserType.Admin)
            {
               //Przejście do admin
            }
            else
            {
                //przejscie do user
            }
        }
    }
}