using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using TestCreatorV2.Data;
using TestCreatorV2.Helpers;
using TestCreatorV2.Models;

namespace TestCreatorV2.Controllers
{
    public class UserLoginController : ApiController
    {

        [BasicAutenticationAttribute]
        public Uzytkownik GetUzytkownik()
        {

            string username = Thread.CurrentPrincipal.Identity.Name;



            using (var dbContext = new TestCreatorEntities())
            {
                if (!ModelState.IsValid)
                {
                    return null;
                }

                var us = dbContext.Uzytkownicy.Where(x => x.login == username).FirstOrDefault();

                if (us != null)
                {
                    Uzytkownik user = new Uzytkownik()
                    {
                        IdUser = us.id_uzytkownik,
                      
                        Name = us.name,
                        IsVisible = us.is_visible,
                        Rola = (int)us.id_rola,
                        DataDodania = us.data_dodania.ToString()
                    };

                    return user;
                }
                else
                {
                    return null;
                }

            }







        }

    }
}
