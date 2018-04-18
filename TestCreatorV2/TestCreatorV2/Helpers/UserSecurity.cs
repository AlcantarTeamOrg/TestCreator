using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestCreatorV2.Data;
using TestCreatorV2.Models;

namespace TestCreatorV2.Helpers
{
    public class UserSecurity
    {
        public static bool Login(Uzytkownik uzytkownik)
        {

            using (var dbContext = new TestCreatorEntities())
            {
                //if (!ModelState.IsValid)
                //{
                //    return null;
                //}

                var us = dbContext.Uzytkownicy.Where(x => x.login == uzytkownik.Login).Where(x => x.haslo == uzytkownik.Haslo).FirstOrDefault();

                Uzytkownik user = new Uzytkownik()
                {
                    IdUser = us.id_uzytkownik,
                    Login = us.login,
                    Haslo = us.haslo,
                    Name = us.name,
                    IsVisible = us.is_visible,
                    Rola = (int)us.id_rola,
                    DataDodania = us.data_dodania.ToString()
                };

                if (user.IdUser == 0 )
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
}