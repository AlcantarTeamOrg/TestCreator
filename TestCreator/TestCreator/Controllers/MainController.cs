using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestCreator.Data;
using TestCreator.Models;

namespace TestCreator.Controllers
{
    public class MainController : Controller
    {

        private static long userId = 0;
        private static Int64 roleId = 0;
        private List<TestUser> userList = new List<TestUser>();

        // GET: Main
        public ActionResult Index()
        {
            GetUsersRoles();
            return View();
        }

        private void GetUsersRoles()
        {
            using(var dbContext = new TestCreatorEntities())
            {
                var rolesList = dbContext.Role.ToList();

                List<SelectListItem> dditems1 = new List<SelectListItem>();

                if (rolesList != null)
                {
                    foreach (var item in rolesList)
                    {
                        SelectListItem selectItem = new SelectListItem() { Text = item.nazwa, Value = item.id_rola.ToString() };
                        dditems1.Add(selectItem);
                    }
                }

                ViewBag.Role = dditems1;
            }
        }

        private void GetAllUserData()
        {
            using (var dbContext = new TestCreatorEntities())
            {
                var tempList = dbContext.GetAllUsers().ToList();



                foreach (var item in tempList)
                {
                    DateTime dt = (DateTime)item.data_dodania;
                    string date = dt.ToShortDateString();
                    TestUser tu = new TestUser()
                    {
                        UserId = (long)item.id_uzytkownik,
                        UserName = item.login,
                        FirstName = item.name,
                        AddedTimeString = date,
                        Role = item.nazwa



                    };
                    userList.Add(tu);
                }

                ViewBag.UsersList = userList;
            }
            
        }

        [HttpPost]
        public ActionResult LoginOperation(TestUser model)
        {
            using(var dbContext = new TestCreatorEntities())
            {
                userId = dbContext.Uzytkownicy.Where(x => x.login == model.UserName).Where(x => x.haslo == model.Password).Select(x => x.id_uzytkownik).FirstOrDefault();
                var roleId = dbContext.Uzytkownicy.Where(x => x.login == model.UserName).Where(x => x.haslo == model.Password).Select(x => x.id_rola).FirstOrDefault();

                string userName = dbContext.Uzytkownicy.Where(x => x.login == model.UserName).Where(x => x.haslo == model.Password).Select(x => x.name).FirstOrDefault();

                ViewBag.UserName = userName;

                GetAllUserData();

                if (userId == 0 || roleId != 1)
                {
                    return null;
                }
                else
                {
                    return PartialView("_AdminPanel");
                }
            }

            
        }

        [HttpPost]
        public ActionResult UpdateUser(TestUser model)
        {
            using(var dbContext = new TestCreatorEntities())
            {
                long roleId;
                 bool res= Int64.TryParse(model.Role,out roleId);

                try
                {
                    if(roleId == 0)
                    {
                        int result = dbContext.UserActualization((long)model.UserId, model.UserName, model.FirstName, model.Password, null);
                        if (result != 0)
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
                        int result = dbContext.UserActualization((long)model.UserId, model.UserName, model.FirstName, model.Password, roleId);
                        if (result != 0)
                        {
                            GetAllUserData();

                            return PartialView("_UserTable");
                        }
                        else
                        {
                            return null;
                        }
                    }
                    
                    
                }
                catch (Exception ex)
                {
                    return null;

                }
                   

                    
                
               

              
            }
        }

        [HttpPost]
        public ActionResult DeleteUser(string id)
        {
            using(var dbContext = new TestCreatorEntities())
            {
                Int64 userID = Convert.ToInt64(id);

               int result = dbContext.DeleteUser(userID);

                if(result != 0)
                {
                    GetAllUserData();

                    return PartialView("_UserTable");
                }
                else
                {
                    return null;
                }
            }
                
        }
    }
}