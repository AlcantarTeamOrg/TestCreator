using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;
using TestCreatorWebSite.Data;
using TestCreatorWebSite.Helpers;
using TestCreatorWebSite.Models;

namespace TestCreatorWebSite.Controllers
{
    public class MainController : Controller
    {
        List<Rola> rolaList;
        List<TestUser> uzytkownikList = new List<TestUser>();
        // GET: Main
        public ActionResult Index()
        {
            GetUsersRoles();


            return View();
        }

        [HttpPost]
        public ActionResult DeleteUser(string id)
        {

            Int64 userID = Convert.ToInt64(id);

            HttpResponseMessage httpResponseMessage2 = GlobalVariables.WebApiClient.DeleteAsync("Uzytkownicy/" + id).Result;

            var res = httpResponseMessage2.StatusCode;

            if (res == System.Net.HttpStatusCode.OK)
            {
                GetAllUserData();

                return PartialView("_UserTable");
            }
            else
            {
                return null;
            }


        }


        [HttpPost]
        public ActionResult AddUser(Uzytkownik tu)
        {


            if (UserValidation(tu))
            {
                string faze1Token = tu.Login + ":" + tu.Name + ":" + tu.Haslo + ":" + tu.Role;

                byte[] bytes = Encoding.ASCII.GetBytes(faze1Token);

                string data = Convert.ToBase64String(bytes);

                GlobalVariables.WebApiClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", data);

                Uzytkownik u2 = new Uzytkownik();
                HttpResponseMessage httpResponseMessage2 = GlobalVariables.WebApiClient.PostAsJsonAsync("Uzytkownicy", u2).Result;
                if (httpResponseMessage2.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    GetAllUserData();

                    return PartialView("_UserTable");
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }






        }

        private void GetAllUserData()
        {



            HttpResponseMessage httpResponseMessage = GlobalVariables.WebApiClient.GetAsync("Uzytkownicy").Result;
            var userList = httpResponseMessage.Content.ReadAsAsync<List<Uzytkownik>>().Result;

            try
            {
                foreach (var item in userList)
                {
                    string dt = item.DataDodania;

                    TestUser tu = new TestUser()
                    {
                        UserId = (long)item.IdUser,
                        UserName = item.Login,
                        FirstName = item.Name,
                        AddedTimeString = dt,
                        Role = item.Role



                    };
                    uzytkownikList.Add(tu);
                }
            }
            catch (Exception ex)
            {


            }




            ViewBag.UsersList = uzytkownikList;


        }

        [HttpPost]
        public ActionResult UpdateUser(Uzytkownik model)
        {



            try
            {
                HttpResponseMessage httpResponseMessage = GlobalVariables.WebApiClient.PutAsJsonAsync("Uzytkownicy/" + model.IdUser, model).Result;

                if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    GetAllUserData();

                    return PartialView("_UserTable");
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                return null;

            }








        }


        private void GetUsersRoles()
        {
            HttpResponseMessage httpResponseMessage = GlobalVariables.WebApiClient.GetAsync("Role").Result;

            rolaList = httpResponseMessage.Content.ReadAsAsync<List<Rola>>().Result;

            List<SelectListItem> dditems1 = new List<SelectListItem>();

            if (rolaList != null)
            {
                foreach (var item in rolaList)
                {
                    SelectListItem selectItem = new SelectListItem() { Text = item.NazwaRoli, Value = item.IDRola.ToString() };
                    dditems1.Add(selectItem);
                }
            }

            ViewBag.Role = dditems1;

        }

        [HttpPost]
        public ActionResult LoginOperation(Uzytkownik model)
        {
            string faze1Token = model.Login + ":" + model.Haslo;

            byte[] bytes = Encoding.ASCII.GetBytes(faze1Token);

            string data = Convert.ToBase64String(bytes);

            GlobalVariables.WebApiClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", data);

            HttpResponseMessage httpResponseMessage2 = GlobalVariables.WebApiClient.GetAsync("UserLogin").Result;





            var user = httpResponseMessage2.Content.ReadAsAsync<Uzytkownik>().Result;

            ViewBag.UserName = user.Name;

            GetAllUserData();

            if (user.IdUser == 0 )
            {
                return null;
            }
            else
            {

                if (user.Rola == 1)
                {
                    GetUsersRoles();

                    using(var dbContext = new TestCreatorEntities())
                    {
                        var stanowiska = dbContext.Stanowiska.ToList();

                        return PartialView("_AdminPanel", stanowiska);
                    }

                    
                }else if(user.Rola == 2)
                {
                    return RedirectToAction("Index", "Testies");
                }
                else
                {
                    return null;
                }
                
            }



        }





        private bool UserValidation(Uzytkownik nu)
        {
            if (nu.Login != "" && nu.Name != "" && nu.Haslo != "" && nu.ReHaslo != "" && nu.Role != "")
            {
                if (nu.Haslo.Length > 6 && nu.Haslo == nu.ReHaslo)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}