using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using TestCreatorWebSite.Models;

namespace TestCreatorWebSite.Helpers
{
    public class UserSecurity
    {

        public static bool Login(Uzytkownik uzytkownik)
        {
            HttpResponseMessage httpResponseMessage2 = GlobalVariables.WebApiClient.PostAsJsonAsync("UserLogin", uzytkownik).Result;

            var user = httpResponseMessage2.Content.ReadAsAsync<Uzytkownik>().Result;

            if (user.IdUser == 0 || user.Rola != 1)
            {
                return false;
            }
            else
            {

                return true;
            }
        }


    }
}