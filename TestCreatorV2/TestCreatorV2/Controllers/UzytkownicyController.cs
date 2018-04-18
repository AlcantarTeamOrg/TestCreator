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
    public class UzytkownicyController : ApiController
    {

        private List<Uzytkownik> users = new List<Uzytkownik>();



        public List<Uzytkownik> GetAllUsers()
        {
            using (var dbContext = new TestCreatorEntities())
            {
                var us = dbContext.GetAllUsers().ToList();

                foreach (var item in us)
                {
                    users.Add(new Uzytkownik()
                    {
                        IdUser = item.id_uzytkownik,
                        Login = item.login,

                        Role = item.nazwa,
                        DataDodania = item.data_dodania.ToString(),
                        Name = item.name
                    });
                }

                return users;
            }
        }




        public HttpResponseMessage GetUserById(long id)
        {
            using (var dbContext = new TestCreatorEntities())
            {
                var us = dbContext.Uzytkownicy.FirstOrDefault(e => e.id_uzytkownik == id);

                Uzytkownik user = new Uzytkownik()
                {
                    IdUser = us.id_uzytkownik,
                    Login = us.login,
                    Haslo = us.haslo,
                    Rola = (int)us.id_rola,
                    DataDodania = us.data_dodania.ToString(),
                    Name = us.name
                };

                if (user != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, user);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Uzytkownik o id " + id + " nie odnaleziony");
                }
            }
        }

        [HttpPost]
        [RegistryAutenticationAttribute]
        public HttpResponseMessage NewUser(Uzytkownik uzytkownik)
        {

            string userData = Thread.CurrentPrincipal.Identity.Name;
            string[] data = userData.Split(':');



            try
            {
                int roleId = 0;
                Int32.TryParse(data[3], out roleId);


                using (var dbContext = new TestCreatorEntities())
                {
                    int res = dbContext.DodawanieUzytkownika(data[0], data[1], data[2], roleId);

                    if (res == 0)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Błąd zapisu nowego użytkownika");
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }


                }
            }
            catch (Exception ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);

            }



        }

        public HttpResponseMessage DeleteUser(long id)
        {
            try
            {
                using (var dbContext = new TestCreatorEntities())
                {
                    int res = dbContext.DeleteUser(id);

                    if (res != 0)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Nie skasowano");
                    }

                }
            }
            catch (Exception ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPut]
        public HttpResponseMessage UserReoranization(long id, Uzytkownik uzytkownik)
        {
            try
            {
                using (var dbContext = new TestCreatorEntities())
                {

                    long roleId;
                    bool res = Int64.TryParse(uzytkownik.Role, out roleId);

                    if (roleId == 0)
                    {
                        int result = dbContext.AktualizacjaUzytkownika(id, uzytkownik.Login, uzytkownik.Name, uzytkownik.Haslo, null);
                        if (result != 0)
                        {
                            return Request.CreateResponse(HttpStatusCode.OK);
                        }
                        else
                        {
                            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Nie zaktualizowano");
                        }
                    }
                    else
                    {
                        int result = dbContext.AktualizacjaUzytkownika(id, uzytkownik.Login, uzytkownik.Name, uzytkownik.Haslo, roleId);
                        if (result != 0)
                        {
                            return Request.CreateResponse(HttpStatusCode.OK);
                        }
                        else
                        {
                            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Nie zaktualizowano");
                        }
                    }




                }
            }
            catch (Exception ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }


    }
}
